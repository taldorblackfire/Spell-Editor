using System.Collections.Generic;
namespace Spell_Editor.Models
{
    public class ArtifactViewModel
    {
        public Artifact_Table ArtifactInfo { get; set; } = new Artifact_Table();

        public List<ArtifactAttainment> Attainments { get; set; } = new List<ArtifactAttainment>();
    }
}