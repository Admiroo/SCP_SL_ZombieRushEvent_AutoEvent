using Exiled.API.Enums;
using Exiled.API.Features;
using System;
using Player = Exiled.Events.Handlers.Player;
using Server = Exiled.Events.Handlers.Server;
using ZombieRushEvent_AutoEvent.Handlers;
using Org.BouncyCastle.Asn1.Cms;

namespace ZombieRushEvent_AutoEvent
{
    public class ZombieRushEvent : Plugin<Config>
    {
        private static readonly Lazy<ZombieRushEvent> Newinstance = new Lazy<ZombieRushEvent>(valueFactory: () => new ZombieRushEvent());
        public static ZombieRushEvent Instance => Newinstance.Value;

        public override PluginPriority Priority { get; } = PluginPriority.Medium;

        private EventHandlers player;
        private EventHandlers server;

       
        private ZombieRushEvent()
        {
        }
        public override void OnEnabled()
        {
            RegisterEvents();
        }
        public override void OnDisabled()
        {
            UnRegisterEvents();
        }
        public void RegisterEvents()
        {
            server = new EventHandlers();
            player = new EventHandlers();

            Server.RoundStarted += server.OnEventStart;
            Server.RespawningTeam += server.MtfAndChaosWaveNotAllowed;
            Server.RoundEnded += server.EndEvent;
            Server.RestartingRound += server.EventEndOnRoundRestarted;


        }
        public void UnRegisterEvents() 
        {

            Server.RoundStarted -= server.OnEventStart;
            Server.RespawningTeam -= server.MtfAndChaosWaveNotAllowed;
            Server.RoundEnded -= server.EndEvent;
            Server.RestartingRound -= server.EventEndOnRoundRestarted;


            server = null;
            player = null;
        }



    }
}
