using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using VRage.FileSystem;
using VRage.Utils;

namespace SeDirectConnect.Settings
{
    public static class ConfigPersistence
    {
        private static readonly string ConfigFilePath = "SEDirectConnect.cfg";
        private static readonly string defaultConfigFilePath = "SEDirectConnect_Default.cfg";
        private static string ConfigFileFullPath => Path.Combine(MyFileSystem.UserDataPath, ConfigFilePath);
        private static string DefaultConfigFileFullPath => Path.Combine(MyFileSystem.UserDataPath, defaultConfigFilePath);

        public static void SaveConfig(Config config, string path)
        {
            try
            {
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    var serializer = new XmlSerializer(typeof(Config));
                    serializer.Serialize(stream, config);
                }
                MyLog.Default.WriteLineAndConsole($"[SEDC-Config] Saved Config file at {path}");
            }
            catch (Exception ex)
            {
                MyLog.Default.WriteLineAndConsole($"[SEDC-Config] Error saving config: {ex}");
            }
        }


        public static Config LoadConfig()
        {
            try
            {
                MyLog.Default.WriteLineAndConsole($"[SEDC-Config] Loading file at {ConfigFileFullPath}.");
                if (!File.Exists(ConfigFileFullPath))
                {
                    MyLog.Default.WriteLineAndConsole($"[SEDC-Config] Config file not found, creating a new one.");
                    SaveConfig(new Config(), ConfigFileFullPath);

                    return new Config();
                }
                using (var stream = new FileStream(ConfigFileFullPath, FileMode.Open))
                {
                    var serializer = new XmlSerializer(typeof(Config));
                    MyLog.Default.WriteLineAndConsole($"[SEDC-Config] Config file Loaded");
                    return (Config)serializer.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                MyLog.Default.WriteLineAndConsole($"[SEDC-Config] Error loading config: {ex}");
                return new Config();
            }
        }


        public static void GenerateDefaultConfig()
        {
            try
            {
                var defaultConfig = new Config
                {
                    Servers = new List<ServerInfo>() { new ServerInfo() { Name = "Demo Server", IP = "ip:port" } }
                };

                SaveConfig(defaultConfig, DefaultConfigFileFullPath);
                MyLog.Default.WriteLineAndConsole($"[SEDC-Config] Default config generated at {DefaultConfigFileFullPath}");
            }
            catch (Exception ex)
            {
                MyLog.Default.WriteLineAndConsole($"[SEDC-Config] Error generating default config: {ex}");
            }
        }
    }
}
