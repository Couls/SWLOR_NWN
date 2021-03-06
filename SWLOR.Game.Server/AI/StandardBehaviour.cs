﻿using FluentBehaviourTree;
using SWLOR.Game.Server.AI.AIComponent;
using SWLOR.Game.Server.Extension;
using SWLOR.Game.Server.GameObject;

using NWN;
using SWLOR.Game.Server.NWNX.Contracts;
using SWLOR.Game.Server.Service.Contracts;
using static NWN.NWScript;

namespace SWLOR.Game.Server.AI
{
    /// <summary>
    /// Standard behaviour which executes for all derived behaviours
    /// </summary>
    public class StandardBehaviour : BehaviourBase
    {
        private readonly INWScript _;
        protected readonly BehaviourTreeBuilder _builder;
        private readonly IEnmityService _enmity;
        private readonly IDialogService _dialog;
        private readonly INWNXObject _nwnxObject;

        public StandardBehaviour(BehaviourTreeBuilder builder,
            INWScript script,
            IEnmityService enmity,
            IDialogService dialog,
            INWNXObject nwnxObject)
        {
            _ = script;
            _builder = builder;
            _enmity = enmity;
            _dialog = dialog;
            _nwnxObject = nwnxObject;
        }

        public override bool IgnoreNWNEvents => true;

        public override BehaviourTreeBuilder Behaviour
        {
            get
            {
                if (!Self.IsValid) return null;

                return _builder
                    .Parallel("StandardBehaviour", 5, 1)
                    .Do<CleanUpEnmity>(Self)
                    .Do<AttackHighestEnmity>(Self)
                    .Do<WarpToTargetIfStuck>(Self);
            }
        } 

        public override void OnPhysicalAttacked()
        {
            base.OnPhysicalAttacked();
            _enmity.OnNPCPhysicallyAttacked();
        }

        public override void OnDeath()
        {
            base.OnDeath();

            int vfx = Self.GetLocalInt("DEATH_VFX");
            if (vfx > 0)
            {
                _.ApplyEffectToObject(DURATION_TYPE_INSTANT, _.EffectVisualEffect(vfx), Self);
            }
        }

        public override void OnDamaged()
        {
            base.OnDamaged();
            _enmity.OnNPCDamaged();
        }

        public override void OnConversation()
        {
            base.OnConversation();
            string convo = Self.GetLocalString("CONVERSATION");
            
            if (!string.IsNullOrWhiteSpace(convo))
            {
                NWPlayer player = (_.GetLastSpeaker());
                _dialog.StartConversation(player, Self, convo);
            }
            else if (!string.IsNullOrWhiteSpace(_nwnxObject.GetDialogResref(Self)))
            {
                _.BeginConversation(_nwnxObject.GetDialogResref(Self));
            }
        }

        public override void OnBlocked()
        {
            base.OnBlocked();

            NWObject door = (_.GetBlockingDoor());
            if (!door.IsValid) return;

            if (_.GetIsDoorActionPossible(door.Object, DOOR_ACTION_OPEN) == TRUE)
            {
                _.DoDoorAction(door.Object, DOOR_ACTION_OPEN);
            }
        }

    }
}
