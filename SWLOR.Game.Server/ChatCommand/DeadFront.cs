﻿using NWN;
using SWLOR.Game.Server.ChatCommand.Contracts;
using SWLOR.Game.Server.Enumeration;
using SWLOR.Game.Server.GameObject;
using static NWN.NWScript;

namespace SWLOR.Game.Server.ChatCommand
{
    [CommandDetails("Plays a dead front animation.", CommandPermissionType.Player | CommandPermissionType.DM)]
    public class DeadFront : LoopingAnimationCommand
    {
        private readonly INWScript _;

        public DeadFront(INWScript script)
        {
            _ = script;
        }
        
        protected override void DoAction(NWPlayer user, float duration)
        {
            user.AssignCommand(() =>
            {
                _.ActionPlayAnimation(ANIMATION_LOOPING_DEAD_FRONT, 1.0f, duration);
            });
        }
    }
}