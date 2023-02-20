using System.Collections.Generic;
namespace Spell_Editor.Models
{
    public class TitleViewModel
    {
        public List<MageCaucusInfo> MageCaucus { get; set; } = new List<MageCaucusInfo>();

        public List<MageConsiliumTitle> Consilium { get; set; } = new List<MageConsiliumTitle>();
    }
}