using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Spell_Editor.Models
{
    public class SpellModel
    {
        public SpellModel()
        {
            spell_arcana = new List<Spell_Arcana_Table>();
            spell_arcana.Add(new Spell_Arcana_Table() { Arcana = "", Level = 1 });
        }
        public Spell_Table spell_table { get; set; }
        [EnsureOneElement(ErrorMessage = "You must have at least one Arcana Specified.")]
        public List<Spell_Arcana_Table> spell_arcana { get; set; }
    }

    public class EnsureOneElementAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var list = value as IList;
            if (list != null)
            {
                return list.Count > 0;
            }
            return false;
        }
    }
}