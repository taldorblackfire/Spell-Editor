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
    
    public partial class Merit
    {
        public int MeritId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int MaxValue { get; set; }
        public Nullable<bool> AdditionalInfo { get; set; }
        public bool Leveled { get; set; }
        public Nullable<int> LevelStart { get; set; }
        public Nullable<bool> Multiple { get; set; }
        public string Description { get; set; }
    }
}