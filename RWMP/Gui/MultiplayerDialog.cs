using System;
using System.Collections.Generic;
using RWMP.Util;
using UnityEngine;
using Verse;

namespace RWMP.Gui
{
    public class MultiplayerDialog : Window
    {
        private readonly List<Tuple<string, Func<Window>>> _items = new List<Tuple<string, Func<Window>>>();

        public MultiplayerDialog()
        {
            doCloseButton = true;
            absorbInputAroundWindow = true;

            AddItem("Server Browser", () => new ServerBrowserDialog());
            AddItem("Connect", () => new ConnectDialog());
            AddItem("Host", () => new HostServerDialog());
        }

        private void AddItem(string name, Func<Window> action)
        {
            _items.Add(Tuple.Create(name, action));
        }

        public override void DoWindowContents(Rect bounds)
        {
            const int itemHeight = 40;

            for (var i = 0; i < _items.Count; i++)
            {
                var item = _items[i];

                if (Widgets.ButtonText(new Rect(8, 8 + i * itemHeight, bounds.width - 8 * 2, itemHeight - 8), item.Item1))
                {
                    Close();
                    Find.WindowStack.Add(item.Item2());
                }
            }
        }
    }
}