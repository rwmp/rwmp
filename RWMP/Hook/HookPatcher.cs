using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Mono.Cecil;
using Mono.Cecil.Cil;
using UnityEngine;
using Verse;
using Debug = System.Diagnostics.Debug;

namespace RWMP.Hook
{
    public class HookPatcher
    {
        private readonly Thread _patchingThread;

        private int _progress;
        private int _progressTotal = 100;

        public HookPatcher()
        {
            _patchingThread = new Thread(DoPatch);
        }

        public void Patch()
        {
            IsPatching = true;
            _patchingThread.Start();
        }

        private void DoPatch()
        {
            ProgressText = "Opening rwmp assembly...";
            var rwmpAssembly = AssemblyDefinition.ReadAssembly("Mods/RWMP/Assemblies/RWMP.dll");

            var rimWorldDll = Application.dataPath + "/Managed/Assembly-CSharp.dll";

            var useBak = File.Exists(rimWorldDll + ".bak");

            ProgressText = "Opening game assembly...";
            var assembly = AssemblyDefinition.ReadAssembly(rimWorldDll + (useBak ? ".bak" : ""));

            if (!useBak)
                File.Copy(rimWorldDll, rimWorldDll + ".bak");

            ProgressText = "Collecting hook dispatcher methods...";
            var hookDispatcherMethods = new Dictionary<string, MethodDefinition>();
            var hookDispatcher = rwmpAssembly.MainModule.GetType("RWMP.Hook.HookDispatcher");
            foreach (var method in hookDispatcher.Methods)
            {
                hookDispatcherMethods[method.Name] = method;
            }

            ProgressText = "Scanning methods...";
            var methods =
                (from module in assembly.Modules from type in module.Types from method in type.Methods select method)
                    .ToList();

            _progressTotal = methods.Count;
            foreach (var method in methods)
            {
                _progress++;
                ProgressText = "Patching " + method.DeclaringType.Name + "." + method.Name + "...";

                if (method.Body == null)
                    continue;

                foreach (Instruction inst in method.Body.Instructions)
                {
                    if (inst.OpCode.Code != Code.Callvirt && inst.OpCode.Code != Code.Call) continue;

                    var target = inst.Operand as MethodReference;
                    Debug.Assert(target != null, "target != null");

                    var targetName = target.DeclaringType.FullName.Replace('.', '_') + '_' + target.Name;

                    if (!hookDispatcherMethods.ContainsKey(targetName)) continue;


                    var hookMethod = hookDispatcherMethods[targetName];

                    if (hookMethod.Parameters.Count != target.Parameters.Count) continue;

                    var newTarget = assembly.MainModule.Import(hookMethod);

                    if (target is GenericInstanceMethod)
                    {
                        newTarget = new GenericInstanceMethod(newTarget);
                        var newGenericTarget = (GenericInstanceMethod) newTarget;
                        var genericTarget = target as GenericInstanceMethod;
                        foreach (var genericArgument in genericTarget.GenericArguments)
                        {
                            newGenericTarget.GenericArguments.Add(genericArgument);
                        }
                    }

                    Log.Message("Hooking call to " + targetName + " in " + method.FullName);
                    inst.Operand = newTarget;
                }
            }
            _progress = _progressTotal;

            assembly.Write(rimWorldDll);

            Done = true;
            ProgressText = "Done, please restart RimWorld!";
        }

        public bool IsPatching { get; private set; }

        public bool Done { get; private set; }

        public float Progress => (float) _progress / _progressTotal;

        public string ProgressText { get; private set; } = "Loading...";
    }
}