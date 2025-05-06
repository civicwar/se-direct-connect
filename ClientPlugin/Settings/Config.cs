using ProtoBuf;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SeDirectConnect.Settings
{
    [DataContract]
    public class Config
    {
        [DataMember]
        [DisplayName("Direct Connect Servers")]
        [Description("List of Direct Connect servers. Format: Name,IP")]
        public List<ServerInfo> Servers { get; set; }

        [DataMember]
        [DisplayName("Show Connect Button")]
        [Description("Show the connect button in the Main Menu UI.")]
        public bool ShowConnectButton { get; set; } = true;


        [DataMember]
        [DisplayName("Show Server List")]
        [Description("Show the server list in the Direct Connect UI.")]
        public bool ShowServerList { get; set; } = true;

        [DataMember]
        [DisplayName("Enable Hot Reload")]
        [Description("Enable hot reload for the plugin. This will reload the plugin conigs when changes are made.")]
        public bool EnableHotReload { get; set; } = true;

        [DataMember]
        [DisplayName("Enable Debug Logging")]
        [Description("Enable debug logging for the plugin. This will log all messages to the console.")]
        public bool EnableDebugLogging { get; set; } = false;
    }

    [DataContract]
    public class ServerInfo
    {
        [DataMember]
        [DisplayName("Server Name")]
        [Description("Name of the server.")]
        public string Name { get; set; } = string.Empty;

        [DataMember]
        [DisplayName("Server IP")]
        [Description("IP address of the server.")]
        public string IP { get; set; } = string.Empty;
    }

}