using HarmonyLib;
using SeDirectConnect.Settings;
using System;
using System.Reflection;
using VRage.Plugins;
using VRage.Utils;

namespace SeDirectConnect
{
    // ReSharper disable once UnusedType.Global
    public class Plugin : IPlugin, IDisposable
    {
        public const string Name = "SEDirectConnect";
        public static Plugin Instance { get; private set; }
        public Config Config;

        private Harmony _harmony;
        public static string PluginNamespace = "SeDirectConnect";

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public void Init(object gameInstance)
        {
            Instance = this;
            Instance.Config = ConfigPersistence.LoadConfig();
            ConfigPersistence.GenerateDefaultConfig();


            _harmony = new Harmony(PluginNamespace);
            _harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        public void Dispose()
        {
            Instance = null;
        }

        public void Update()
        { }

        public void ReloadConfig()
        {
            Instance.Config = ConfigPersistence.LoadConfig();
            MyLog.Default.WriteLineAndConsole($"[SEDC-Plugin] Reloaded config");
        }
    }
}