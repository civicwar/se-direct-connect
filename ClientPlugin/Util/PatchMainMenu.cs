using HarmonyLib;
using Sandbox;
using Sandbox.Game.Screens;
using Sandbox.Graphics.GUI;
using SeDirectConnect.Settings;
using SpaceEngineers.Game.GUI;
using System;
using VRage.Game;
using VRage.Utils;
using VRageMath;

namespace SeDirectConnect.Util
{
    [HarmonyPatch(typeof(MyGuiScreenMainMenu), "RecreateControls")]
    public static class PatchMainMenu
    {
        private static readonly Type DirectConnectType = typeof(MyGuiScreenServerConnect);
        private static readonly Config Config = Plugin.Instance.Config;

        public static void Postfix(MyGuiScreenMainMenu __instance)
        {
            if (DirectConnectType == null) return;
            if (!Config.ShowConnectButton) return;
            //if (Config.EnableHotReload) Plugin.Instance.ReloadConfig(); TODO: Enable hot reload for the plugin.


            if (Config.EnableDebugLogging)
                foreach (var c in __instance.Controls)
                    MyLog.Default.WriteLineAndConsole(
                        $"[SEDC‑GUI] {c.GetType().Name,-25}  name=\"{c.Name}\""
                        + $"  pos={c.Position}  size={c.Size}");

            MyGuiControlButton refButton =
                __instance.Controls.GetControlByName("JoinWorld") as MyGuiControlButton ??
                __instance.Controls.GetControlByName("SaveWorld") as MyGuiControlButton;

            if (refButton == null)
            {
                MyLog.Default.WriteLineAndConsole($"[SEDC-Plugin] Could not find the reference button.");
                return;
            }

            // show at the right of the join button
            var quickJoinButton = new MyGuiControlButton()
            {
                Position = refButton.Position + new Vector2(refButton.Size.X, -refButton.Size.Y / 2),
                Size = refButton.Size,
                Name = "Direct Connect",
                Text = "Direct Connect",
                VisualStyle = MyGuiControlButtonStyleEnum.Default,
            };

            quickJoinButton.SetToolTip("Direct Connect to a server");
            quickJoinButton.ButtonClicked += (_) =>
            {
                MySandboxGame.Static.Invoke(() =>
                {
                    MyGuiScreenBase screen = MyGuiSandbox.CreateScreen(DirectConnectType);
                    MyGuiSandbox.AddScreen(screen);
                }, "Direct Connect");
            };

            __instance.Controls.Add(quickJoinButton);
        }


    }
}
