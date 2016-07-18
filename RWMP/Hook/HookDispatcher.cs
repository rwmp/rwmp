using System.Collections.Generic;
using System.Runtime.InteropServices;
using RWMP.Gui;
using RWMP.Util;
using UnityEngine;
using Verse;

namespace RWMP.Hook
{
    public class HookDispatcher
    {
        public static float Verse_OptionListingUtility_DrawOptionListing(Rect rect, List<ListableOption> optList)
        {
            HookSystem.IsInstalled = true;

            if (ModUtil.Active && optList[0].label == "NewColony".Translate())
            {
                optList.Insert(1,
                    new ListableOption("Multiplayer", () => { Find.WindowStack.Add(new MultiplayerDialog()); }));
                optList.Insert(1,
                    new ListableOption("Repatch", () => { Find.WindowStack.Add(new HookPatcherDialog()); }));
            }

            return OptionListingUtility.DrawOptionListing(rect, optList);
        }

        public static bool Verse_Scribe_EnterNode(string elementName)
        {
            return Scribe.EnterNode(elementName);
        }

        public static void Verse_Scribe_ExitNode()
        {
            Scribe.ExitNode();
        }

        public static void Verse_Scribe_WriteElement(string elementName, string value)
        {
            Scribe.WriteElement(elementName, value);
        }

        public static void Verse_Scribe_WriteAttribute(string attributeName, string value)
        {
            Scribe.WriteAttribute(attributeName, value);
        }

        public static void Verse_Scribe_Deep_LookDeep<T>(ref T target, bool saveDestroyedThings, string label,
            params object[] ctorArgs)
        {
            Scribe_Deep.LookDeep(ref target, saveDestroyedThings, label, ctorArgs);
        }

        public static void Verse_Scribe_Values_LookValue<T>(ref T value, string label, [Optional] T defaultValue,
            bool forceSave = false)
        {
            Scribe_Values.LookValue(ref value, label, defaultValue, forceSave);
        }

        public static void Verse_Scribe_TargetInfo_LookTargetInfo(ref TargetInfo value, bool saveDestroyedThings,
            string label,
            TargetInfo defaultValue)
        {
            Scribe_TargetInfo.LookTargetInfo(ref value, saveDestroyedThings, label, defaultValue);
        }

        public static void Verse_Scribe_References_LookReference<T>(ref T refee, string label,
            bool saveDestroyedThings = false)
            where T : ILoadReferenceable
        {
            Scribe_References.LookReference(ref refee, label, saveDestroyedThings);
        }

        public static void Verse_Scribe_Collections_LookList<T>(ref List<T> list, bool saveDestroyedThings, string label,
            LookMode lookMode = LookMode.Undefined, params object[] ctorArgs)
        {
            Scribe_Collections.LookList(ref list, saveDestroyedThings, label, lookMode, ctorArgs);
        }

        public static void Verse_Scribe_Collections_LookDictionary<K, V>(ref Dictionary<K, V> dict, string label,
            LookMode keyLookMode = LookMode.Undefined, LookMode valueLookMode = LookMode.Undefined) where K : new()
        {
            Scribe_Collections.LookDictionary(ref dict, label, keyLookMode, valueLookMode);
        }

        public static void Verse_Scribe_Collections_LookHashSet<T>(ref HashSet<T> valueHashSet, bool saveDestroyedThings,
            string label,
            LookMode lookMode = LookMode.Undefined) where T : new()
        {
            Scribe_Collections.LookHashSet(ref valueHashSet, saveDestroyedThings, label, lookMode);
        }

        public static void Verse_Scribe_Collections_LookStack<T>(ref Stack<T> valueStack, string label,
            LookMode lookMode = LookMode.Undefined)
            where T : new()
        {
            Scribe_Collections.LookStack(ref valueStack, label, lookMode);
        }

        public static void Verse_Scribe_Defs_LookDef<T>(ref T value, string label) where T : Def, new()
        {
            Scribe_Defs.LookDef(ref value, label);
        }
    }
}