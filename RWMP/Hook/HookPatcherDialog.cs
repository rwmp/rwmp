using RWMP.Util;
using UnityEngine;
using Verse;

namespace RWMP.Hook
{
    public class HookPatcherDialog : Window
    {
        public override void DoWindowContents(Rect bounds)
        {
            Text.Font = GameFont.Medium;
            Widgets.Label(new Rect(0f, 0f, bounds.width, 80), "RWMP Patcher");

            Text.Font = GameFont.Small;
            Widgets.Label(new Rect(0f, 36f, bounds.width, 200),
                "Because RWMP needs to access certain functionally that's generally not available trough modding it needs to patch the RimWorld game files.");

            if (!HookPatcher.IsPatching)
            {
                if (Widgets.ButtonText(new Rect(0, bounds.height - 32, (bounds.width - 8) / 2, 32), "Cancel"))
                {
                    ModUtil.Disable();
                    Close();
                }

                if (Widgets.ButtonText(new Rect((bounds.width + 8) / 2, bounds.height - 32, (bounds.width - 8) / 2, 32),
                    "Let's do it!"))
                {
                    HookPatcher.Patch();
                }
            }
            else
            {
                Text.Font = GameFont.Small;
                Widgets.Label(new Rect(2, bounds.height - 24 - 24, bounds.width - 2, 24), HookPatcher.ProgressText);
                Widgets.FillableBar(new Rect(0, bounds.height - 24, bounds.width, 24), HookPatcher.Progress);
            }
        }
    }
}