//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SWLOR.Game.Server.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class SpawnObject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SpawnObject()
        {
            this.SpawnRule = "";
            this.BehaviourScript = "";
        }
    
        public int SpawnObjectID { get; set; }
        public int SpawnID { get; set; }
        public string Resref { get; set; }
        public int Weight { get; set; }
        public string SpawnRule { get; set; }
        public Nullable<int> NPCGroupID { get; set; }
        public string BehaviourScript { get; set; }
        public int DeathVFXID { get; set; }
    
        public virtual NPCGroup NPCGroup { get; set; }
        public virtual Spawn Spawn { get; set; }
    }
}