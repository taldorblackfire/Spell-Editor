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
    
    public partial class MagePath
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MagePath()
        {
            this.RefLegacies = new HashSet<RefLegacy>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public string FirstRulingArcana { get; set; }
        public string SecondRulingArcana { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RefLegacy> RefLegacies { get; set; }
    }
}
