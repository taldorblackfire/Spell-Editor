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
    
    public partial class MageOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MageOrder()
        {
            this.MageCaucusInfoes = new HashSet<MageCaucusInfo>();
            this.RefLegacies = new HashSet<RefLegacy>();
            this.RefLegacies1 = new HashSet<RefLegacy>();
        }
    
        public int ID { get; set; }
        public string OrderName { get; set; }
        public Nullable<int> RoteSpecialty1 { get; set; }
        public Nullable<int> RoteSpecialty2 { get; set; }
        public Nullable<int> RoteSpecialty3 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MageCaucusInfo> MageCaucusInfoes { get; set; }
        public virtual Skill Skill { get; set; }
        public virtual Skill Skill1 { get; set; }
        public virtual Skill Skill2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RefLegacy> RefLegacies { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RefLegacy> RefLegacies1 { get; set; }
    }
}