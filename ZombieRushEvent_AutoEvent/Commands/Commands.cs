using CommandSystem;
using Org.BouncyCastle.Asn1.Ocsp;
using System;

namespace ZombieRushEvent_AutoEvent.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    internal class Commands : ICommand
    {
        public string Command { get; } = "ZombieRushEvent Start";

        public string[] Aliases { get; } = { "zombierushevent" };

        public string Description { get; } = "Starts the Zombie Rush event...";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
        if (sender is CommandSender)
            {
                if (RoundSummary.RoundInProgress() == false)
                {
                    response = "Starting Zombie Rush Event Next Round...";
                    Handlers.EventHandlers.StartEvent();
                    return true;
                }
                else
                {
                    response = "Can't iniciate Zombie Rush event while round is in progress.";
                    return false;
                }
            }
            else
            {
                response = "You do not have permission to use this command";
                return false;
            }
        }
    }
}
