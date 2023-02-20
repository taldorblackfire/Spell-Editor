using System.Collections.Generic;
namespace Spell_Editor.Models
{
    public class SpellListModel
    {
        public List<SpellInfo> Spells { get; set; } = new List<SpellInfo>();
    }

    public class SpellInfo
    {
        public int Id { get; set; }

        public string SpellName { get; set; }

        public string Arcana { get; set; }

        public int Level { get; set; }
    }
}