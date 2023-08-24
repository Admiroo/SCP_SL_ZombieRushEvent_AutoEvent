using Exiled.API.Interfaces;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace ZombieRushEvent_AutoEvent
{
    public sealed class Config : IConfig
    {
        [Description("Wheter the plugin is Enabled or not.")]
        public bool IsEnabled { get; set; } = true;

        [Description("Whether or not debug messages should be shown in the console.")]
        public bool Debug { get; set; } = false;

        [Description("Amount of time, in seconds, that the surface gate will be locked before round start")]
        public static int TimeBeforeRound = 20;

        
        

    }
}
