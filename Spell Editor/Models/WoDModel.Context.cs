﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class WoDEntities : DbContext
    {
        public WoDEntities()
            : base("name=WoDEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Ammo> Ammoes { get; set; }
        public virtual DbSet<Arcana_Table> Arcana_Table { get; set; }
        public virtual DbSet<Artifact_Table> Artifact_Table { get; set; }
        public virtual DbSet<Attainment_Table> Attainment_Table { get; set; }
        public virtual DbSet<Ceremony> Ceremonies { get; set; }
        public virtual DbSet<Character_Equipment> Character_Equipment { get; set; }
        public virtual DbSet<Character_List> Character_List { get; set; }
        public virtual DbSet<Condition> Conditions { get; set; }
        public virtual DbSet<Grimoire_Table> Grimoire_Table { get; set; }
        public virtual DbSet<Haunt> Haunts { get; set; }
        public virtual DbSet<Imbued_Table> Imbued_Table { get; set; }
        public virtual DbSet<ImbuedItemSpell> ImbuedItemSpells { get; set; }
        public virtual DbSet<MageCaucusInfo> MageCaucusInfoes { get; set; }
        public virtual DbSet<MageConsiliumTitle> MageConsiliumTitles { get; set; }
        public virtual DbSet<Memento> Mementoes { get; set; }
        public virtual DbSet<Merit> Merits { get; set; }
        public virtual DbSet<RefGeistKey> RefGeistKeys { get; set; }
        public virtual DbSet<Sight_Table> Sight_Table { get; set; }
        public virtual DbSet<Skill_Table> Skill_Table { get; set; }
        public virtual DbSet<Spell_Arcana_Table> Spell_Arcana_Table { get; set; }
        public virtual DbSet<Spell_Table> Spell_Table { get; set; }
        public virtual DbSet<Tilt> Tilts { get; set; }
        public virtual DbSet<Vampire_Coils> Vampire_Coils { get; set; }
        public virtual DbSet<Vampire_Cruac> Vampire_Cruac { get; set; }
        public virtual DbSet<Vampire_Devotion> Vampire_Devotion { get; set; }
        public virtual DbSet<Vampire_Disciplines> Vampire_Disciplines { get; set; }
        public virtual DbSet<Vampire_Sorcery> Vampire_Sorcery { get; set; }
        public virtual DbSet<Rote_Table> Rote_Table { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<MageOrder> MageOrders { get; set; }
        public virtual DbSet<MagePath> MagePaths { get; set; }
        public virtual DbSet<RefLegacy> RefLegacies { get; set; }
        public virtual DbSet<ArtifactAttainment> ArtifactAttainments { get; set; }
        public virtual DbSet<ItemEnhancement> ItemEnhancements { get; set; }
        public virtual DbSet<RefEnhancement> RefEnhancements { get; set; }
        public virtual DbSet<Attribute_Table> Attribute_Table { get; set; }
        public virtual DbSet<FetishTable> FetishTable { get; set; }
        public virtual DbSet<SpellView> SpellView { get; set; }
        public virtual DbSet<Fate_Hex_Boons> Fate_Hex_Boons { get; set; }
    }
}
