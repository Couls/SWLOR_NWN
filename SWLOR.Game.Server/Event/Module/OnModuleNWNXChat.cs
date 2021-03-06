﻿using SWLOR.Game.Server.GameObject;
using NWN;
using SWLOR.Game.Server.Service.Contracts;

namespace SWLOR.Game.Server.Event.Module
{
    public class OnModuleNWNXChat: IRegisteredEvent
    {
        private readonly IActivityLoggingService _activityLogging;
        private readonly IChatCommandService _chatCommand;
        private readonly IBaseService _base;
        private readonly IChatTextService _chatText;
        private readonly ICraftService _craft;

        public OnModuleNWNXChat(
            IActivityLoggingService activityLogging,
            IChatCommandService chatCommand,
            IBaseService @base,
            IChatTextService chatText,
            ICraftService craft)
        {
            _activityLogging = activityLogging;
            _chatCommand = chatCommand;
            _base = @base;
            _chatText = chatText;
            _craft = craft;
        }

        public bool Run(params object[] args)
        {
            NWPlayer player = (Object.OBJECT_SELF);
            _activityLogging.OnModuleNWNXChat(player);
            _chatCommand.OnModuleNWNXChat(player);
            _base.OnModuleNWNXChat(player);
            _chatText.OnNWNXChat();
            _craft.OnNWNXChat();
            return true;
        }
    }
}
