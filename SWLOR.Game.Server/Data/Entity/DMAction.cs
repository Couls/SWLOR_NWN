﻿using System;
using SWLOR.Game.Server.Data.Contracts;

namespace SWLOR.Game.Server.Data.Entity
{
    [Table("[DMAction]")]
    public class DMAction: IEntity
    {
        public DMAction()
        {
            ID = Guid.NewGuid();
        }

        [ExplicitKey]
        public Guid ID { get; set; }
        public int DMActionTypeID { get; set; }
        public string Name { get; set; }
        public string CDKey { get; set; }
        public DateTime DateOfAction { get; set; }
        public string Details { get; set; }
    }
}
