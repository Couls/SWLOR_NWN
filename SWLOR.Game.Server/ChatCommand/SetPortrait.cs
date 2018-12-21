﻿using System;
using System.Linq;
using System.Reflection;
using NWN;
using SWLOR.Game.Server.ChatCommand.Contracts;
using SWLOR.Game.Server.Data.Entity;
using SWLOR.Game.Server.Enumeration;
using SWLOR.Game.Server.GameObject;
using SWLOR.Game.Server.Service.Contracts;
using SWLOR.Game.Server.NWNX.Contracts;

namespace SWLOR.Game.Server.ChatCommand
{
    [CommandDetails("Sets portrait of the target player using the string specified. (Remember to add po_ to the portrait)xxx", CommandPermissionType.DM)]
    public class SetPortrait : IChatCommand
    {
        private readonly INWScript _;
        private readonly IDataService _data;
        private readonly INWNXObject _object;

        public SetPortrait(
            INWScript script,
            IDataService data,
            INWNXObject @object)
        {
            _ = script;
            _data = data;
            _object = @object;
        }

        public void DoAction(NWPlayer user, NWObject target, NWLocation targetLocation, params string[] args)
        {
            if (!target.IsPlayer)
            {
                user.SendMessage("Only players may be targeted with this command.");
                return;
            }

            NWPlayer player = target.Object;
            _object.SetPortrait(player, args[0]);
            player.FloatingText("Your portrait has been changed");
        }

        public string ValidateArguments(NWPlayer user, params string[] args)
        {
            if (args.Length > 15)
            {
                return "The string is too long.";
            }


            return string.Empty;
        }

        public bool RequiresTarget => true;
    }
}
