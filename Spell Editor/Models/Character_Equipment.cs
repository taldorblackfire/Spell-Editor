//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Spell_Editor.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Character_Equipment
    {
        public int Id { get; set; }
        public int characterId { get; set; }
        public int equipmentId { get; set; }
        public Nullable<int> current_ammo { get; set; }
        public Nullable<int> ammoId { get; set; }
        public bool equipped { get; set; }
        public short quantity { get; set; }
    
        public virtual Ammo Ammo { get; set; }
        public virtual Character_List Character_List { get; set; }
    }
}
