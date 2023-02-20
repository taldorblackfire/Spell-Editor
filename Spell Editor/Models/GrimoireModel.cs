using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Spell_Editor.Models
{
    public class GrimoireModel
    {
        public GrimoireModel()
        {
            Spells = new List<Rote>();
        }
        public int? Id { get; set; }

        public string Name { get; set; }
        
        public short Cost { get; set; }

        public string Description { get; set; }

        public Nullable<short> CharacterID { get; set; }

        public List<Rote> Spells { get; set; }
    }

    public class Rote
    {
        public int Id { get; set; }
        public int RoteId { get; set;}
        public string Spell_Name { get; set; }
        public int SkillID { get; set; }
    }
}