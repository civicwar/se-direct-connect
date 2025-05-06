using HarmonyLib;
using Sandbox.Game.Screens;
using Sandbox.Graphics.GUI;
using SeDirectConnect.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using VRage.Utils;
using VRageMath;

namespace SeDirectConnect.Util
{
    [HarmonyPatch(typeof(MyGuiSandbox), nameof(MyGuiSandbox.AddScreen))]
    public static class MainMenuPatching
    {
        private static readonly Config Config = Plugin.Instance.Config;
        private static readonly List<MyGuiControlCombobox> myGuiControlComboboxes = new List<MyGuiControlCombobox>();
        private static readonly Type DirectConnectType = typeof(MyGuiScreenServerConnect);

        private static List<ServerInfo> DirectConnectServers => Config.Servers;

        public static void Postfix(MyGuiScreenBase screen)
        {
            if (screen.GetType() != DirectConnectType) return;
            if (!Config.ShowServerList) return;
            //if (Config.EnableHotReload) Plugin.Instance.ReloadConfig(); TODO: Enable hot reload for the plugin.


            var connectButton = screen.Controls.GetControlByName("Button") as MyGuiControlButton;
            var ipBox = screen.Controls.GetControlByName("Textbox") as MyGuiControlTextbox;

            if (Config.EnableDebugLogging)
                foreach (var c in screen.Controls)
                    MyLog.Default.WriteLineAndConsole(
                        $"[SEDC‑GUI] {c.GetType().Name,-25}  name=\"{c.Name}\""
                        + $"  pos={c.Position}  size={c.Size}");


            var combobox = new MyGuiControlCombobox()
            {
                Position = connectButton.Position + new Vector2(0, -0.05f),
                Size = connectButton.Size,
                Name = "Direct Connect",
            };

            combobox.AddItem(-1, new StringBuilder("Server List"));
            combobox.SelectItemByKey(-1);

            for (int i = 0; i < DirectConnectServers.Count; i++)
            {
                combobox.AddItem(i, new StringBuilder(DirectConnectServers[i].Name));
            }

            combobox.ItemSelected += () =>
            {
                if (combobox.GetSelectedKey() == -1) return;

                var selected = combobox.GetSelectedKey();
                var server = DirectConnectServers[(int)selected];

                ipBox.SetText(new StringBuilder(server.IP));
                connectButton.RaiseButtonClicked();
            };

            screen.Controls.Add(combobox);
            myGuiControlComboboxes.Add(combobox);
        }


    }
}
