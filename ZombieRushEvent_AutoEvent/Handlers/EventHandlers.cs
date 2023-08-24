using CustomPlayerEffects;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Server;
using MEC;
using Mono.Math.Prime;
using PlayerRoles;
using System;
using System.Collections.Generic;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace ZombieRushEvent_AutoEvent.Handlers
{
    public class EventHandlers
    {
        System.Random rand = new System.Random();
        static bool IsZombieRushEventGoingOn = false;

        public static void StartEvent()
        {

            IsZombieRushEventGoingOn = true;
        
        }
        public void EndEvent(RoundEndedEventArgs ev)
        {


            IsZombieRushEventGoingOn = false;
        
        
        }
        public void EventEndOnRoundRestarted()
        {


            IsZombieRushEventGoingOn = false;


        }
        public void OnEventStart()
        {
            if (IsZombieRushEventGoingOn == true)
            {
                List<Player> PlyList = new List<Player>();


                Door.Get(DoorType.EscapePrimary).Lock(5000, DoorLockType.NoPower);
                Door.Get(DoorType.EscapeSecondary).Lock(5000, DoorLockType.NoPower);
                Door.Get(DoorType.ElevatorGateA).Lock(5000, DoorLockType.NoPower);
                Door.Get(DoorType.ElevatorGateB).Lock(5000, DoorLockType.NoPower);
                Door.Get(DoorType.SurfaceGate).Lock(Config.TimeBeforeRound, DoorLockType.NoPower);


                foreach (Player ply in Player.List)
                {
                    PlyList.Add(ply);
                }
                float Z = PlyList.Count / 3;
                Math.Floor(Z);

                for (int i = 0; i < Z; i++)
                {
                    int RandPly = rand.Next(PlyList.Count);
                    Player Selected0492 = PlyList[RandPly];
                    Selected0492.Role.Set(RoleTypeId.Scp0492);
                    Selected0492.Position = RoleExtensions.GetRandomSpawnLocation(RoleTypeId.ChaosRepressor).Position;

                    int RandomBuffs = rand.Next(1,4);
                    if (RandomBuffs == 1)
                    {
                        Selected0492.Health = 800;
                    }
                    else if (RandomBuffs == 2)
                    {
                        Selected0492.EnableEffect(EffectType.MovementBoost, 1200);
                        Selected0492.ChangeEffectIntensity(EffectType.MovementBoost, 50, 1200);
                        Selected0492.Health = 400;
                    }
                    else
                    {
                        Selected0492.Health = 400;
                    }

                    int CoinCounter = rand.Next(1, 21);
                    if (CoinCounter > 0 && CoinCounter < 14)
                    {
                        Selected0492.AddItem(ItemType.Coin);
                    }
                    else if (CoinCounter > 13 && CoinCounter < 18)
                    {
                        Selected0492.AddItem(ItemType.Coin);
                        Selected0492.AddItem(ItemType.Coin);
                    }
                    else
                    {
                        for (int a = 0; a < 3; a++)
                        {
                            Selected0492.AddItem(ItemType.Coin);
                        }
                    }

                    PlyList.RemoveAt(RandPly);
                }

                foreach (Player ply in PlyList)
                {
                    ply.Role.Set(RoleTypeId.NtfSergeant);
                    ply.Position = RoleExtensions.GetRandomSpawnLocation(RoleTypeId.NtfCaptain).Position;
                }


                for (int i = Config.TimeBeforeRound; i > 0; i--)
                {
                    Map.Broadcast(1, "O evento Zombie Rush vai começar em " + i + " segundos...");
                }
                Map.Broadcast(5, "O evento Zombie Rush começou...");

                Timing.CallDelayed(Config.TimeBeforeRound, OpenSurfaceGate);
                Timing.CallDelayed(120, RespawnZombies);
                Timing.CallDelayed(240, RespawnZombies);
                Timing.CallDelayed(240, TimerToRespawnZombies);

            }
        }
        public void RespawnZombies()
        {
            if (IsZombieRushEventGoingOn == true)
            {
                foreach (Player player in Player.List)
                {
                    if (player.Role == RoleTypeId.Spectator)
                    {
                        player.Role.Set(RoleTypeId.Scp0492);
                        player.Position = RoleExtensions.GetRandomSpawnLocation(RoleTypeId.ChaosRepressor).Position;
                        int RandomBuffs = rand.Next(1, 4);
                        if (RandomBuffs == 1)
                        {
                            player.Health = 800;
                        }
                        else if (RandomBuffs == 2)
                        {
                            player.EnableEffect(EffectType.MovementBoost, 1200);
                            player.ChangeEffectIntensity(EffectType.MovementBoost, 50, 1200);
                            player.Health = 400;
                        }
                        else
                        {
                            player.Health = 400;
                        }

                        int CoinCounter = rand.Next(1, 21);
                        if (CoinCounter > 0 && CoinCounter < 14)
                        {
                            player.AddItem(ItemType.Coin);
                        }
                        else if (CoinCounter > 13 && CoinCounter < 18)
                        {
                            player.AddItem(ItemType.Coin);
                            player.AddItem(ItemType.Coin);
                        }
                        else
                        {
                            for (int a = 0; a < 3; a++)
                            {
                                player.AddItem(ItemType.Coin);
                            }
                        }
                    }
                }
            }
        }
        public void OpenSurfaceGate()
        {
            Door.Get(DoorType.SurfaceGate).IsOpen = true;
        }
        public void MtfAndChaosWaveNotAllowed(RespawningTeamEventArgs ev)
        {
            if (IsZombieRushEventGoingOn == true)
            {
                    ev.IsAllowed = false;
            }
        }
        public void TimerToRespawnZombies()
        {
            Timing.CallPeriodically(1200, 60, RespawnZombies);
        }
    }
}
        
