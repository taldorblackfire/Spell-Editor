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
    
    public partial class Grimoire_Table
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Grimoire_Table()
        {
            this.Rote_Table = new HashSet<Rote_Table>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public short Cost { get; set; }
        public Nullable<int> CharacterID { get; set; }
        public string Description { get; set; }
    
        public virtual Character_List Character_List { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rote_Table> Rote_Table { get; set; }
    }
}
