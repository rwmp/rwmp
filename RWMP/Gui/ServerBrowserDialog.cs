using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace RWMP.Gui
{
    public class ServerBrowserDialog : Window
    {
        public ServerBrowserDialog()
        {
            doCloseX = true;
        }

        public override Vector2 InitialSize => new Vector2(1020f, 764f);

        private readonly List<HostData> _servers = new List<HostData>();

        private Vector2 _scrollPosition;

        public override void WindowUpdate()
        {
            base.WindowUpdate();

            _servers.AddRange(MasterServer.PollHostList());
            MasterServer.ClearHostList();
        }

        public override void DoWindowContents(Rect bounds)
        {
            Text.Font = GameFont.Medium;
            Widgets.Label(new Rect(0f, 0f, bounds.width, 80), "Server Browser");
            Text.Font = GameFont.Small;


            var listViewWidth = bounds.width - 8 * 2;
            var listViewHeight = bounds.height - 80 - 8 - 32 - 8;

            var itemHeight = 26.0f;

            var listWidth = listViewWidth - 16;
            var listHeight = _servers.Count * itemHeight;

            if (listHeight <= listViewHeight)
            {
                listHeight = listViewHeight + 1;
            }


            Widgets.BeginScrollView(new Rect(8, 80, listViewWidth, listViewHeight), ref _scrollPosition,
                new Rect(0, 0, listWidth, listHeight));

            for (var i = 0; i < _servers.Count; i++)
            {
                var server = _servers[i];

                Widgets.Label(new Rect(0, i * itemHeight, listWidth, itemHeight), server.gameName);
                Widgets.DrawLine(new Vector2(0, i * itemHeight + itemHeight),
                    new Vector2(listWidth, i * itemHeight + itemHeight), Color.grey, 1);

                if (Widgets.ButtonText(new Rect(listWidth - 100 + 1, i * itemHeight, 100, itemHeight - 2), "Join"))
                {
                    Network.Connect(server);
                }
            }

            Widgets.EndScrollView();

            if (Widgets.ButtonText(new Rect(8, bounds.height - 32 - 8, 200, 32), "Refresh"))
            {
                _servers.Clear();
                MasterServer.ClearHostList();
                MasterServer.RequestHostList("RWMP");
            }

            if (Widgets.ButtonText(new Rect(8 + 200 + 8, bounds.height - 32 - 8, 200, 32), "Host"))
            {
                Network.InitializeServer(32, 25002, true);
                MasterServer.RegisterHost("RWMP", "Test", "testing!");
            }
        }
    }
}