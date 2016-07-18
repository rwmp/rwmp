using UnityEngine;
using Verse;

namespace RWMP.Gui
{
    public class ConnectDialog : Window
    {
        public override Vector2 InitialSize => new Vector2(480f, 300f);

        public ConnectDialog()
        {
            doCloseX = true;
        }

        private string _host = "";
        private string _portString = "";
        private int _port = 25000;

        public override void DoWindowContents(Rect bounds)
        {
            Text.Font = GameFont.Medium;
            Widgets.Label(new Rect(0f, 0f, bounds.width, 80), "Connect");
            Text.Font = GameFont.Small;

            const int yOffset = 40;
            const int portWidth = 60;

            _host = Widgets.TextField(new Rect(0, yOffset, bounds.width - portWidth - 8, 26), _host);
            Widgets.TextFieldNumeric(new Rect(bounds.width - portWidth, yOffset, portWidth, 26), ref _port,
                ref _portString, 0, ushort.MaxValue);
        }
    }
}