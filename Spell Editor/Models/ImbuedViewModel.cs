using System.Collections.Generic;
namespace Spell_Editor.Models
{
    public class ImbuedViewModel
    {
        public Imbued_Table ImbuedInfo { get; set; } = new Imbued_Table();

        public List<ItemEnhancement> ItemEnhancement { get; set; } = new List<ItemEnhancement>();

        public List<ImbuedItemSpell> Spells { get; set; } = new List<ImbuedItemSpell>();
    }
}