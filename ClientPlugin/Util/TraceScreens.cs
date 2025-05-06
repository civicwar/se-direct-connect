using HarmonyLib;
using Sandbox.Graphics.GUI;
using SeDirectConnect.Settings;
using System;
using System.Linq;
using VRage.GameServices;
using VRage.Utils;

namespace SeDirectConnect.Util
{
    [HarmonyPatch(typeof(MyGuiSandbox), nameof(MyGuiSandbox.AddScreen))]
    static class TraceScreens
    {

        private static readonly Config Config = Plugin.Instance.Config;

        static void Prefix(MyGuiScreenBase screen)
        {

            if (!Config.EnableDebugLogging) return;

            // 1.  print every screen as it is pushed on‑stack
            MyLog.Default.WriteLineAndConsole(
                $"[TRACE] {screen.GetType().FullName}");

            // 2.  when it's the Direct‑Connect dialog dump its controls
            if (screen.GetType().Name.Contains("ServerConnect",
                                                  StringComparison.Ordinal))
            {
                foreach (var c in screen.Controls)
                    MyLog.Default.WriteLineAndConsole(
                        $"        {c.GetType().Name,-25} name=\"{c.Name}\"");
            }
        }
    }
}
