using Verse;

namespace RWMP.Hook
{
    public static class HookSystem
    {
        public static bool IsInstalled { get; set; }

        public static void Install()
        {
            Find.WindowStack.Add(new HookPatcherDialog());
        }
    }
}