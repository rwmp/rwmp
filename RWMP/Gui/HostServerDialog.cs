using System;
using RimWorld;
using Verse;

namespace RWMP.Gui
{
    public class HostServerDialog : Dialog_MapList
    {
        public HostServerDialog()
        {
            interactButLabel = "Host";
        }

        protected override void DoFileInteraction(string mapName)
        {
            PreLoadUtility.CheckVersionAndLoad(GenFilePaths.FilePathForSavedGame(mapName),
                ScribeMetaHeaderUtility.ScribeHeaderMode.Map, () =>
                {
                    Action preLoadLevelAction = () =>
                    {
                        Current.Game = new Game { InitData = new GameInitData { mapToLoad = mapName } };
                    };
                    LongEventHandler.QueueLongEvent(preLoadLevelAction, "Map", "LoadingLongEvent", true, null);
                });
        }
    }
}