namespace Vagabond
{
    using System.Collections.Generic;
    using UnityEngine;
    using SideLoader;
    using BepInEx;
    using InstanceIDs;
    using TinyHelper;
    using System.IO;
    using HarmonyLib;
    using System;

    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInDependency(SL.GUID, BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency(SynchronizedWorldObjects.SynchronizedWorldObjects.GUID, SynchronizedWorldObjects.SynchronizedWorldObjects.VERSION)]

    public class Vagabond : BaseUnityPlugin
    {
        public const string GUID = "com.ehaugw.vagabond";
        public const string VERSION = "0.0.1";
        public const string NAME = "Vagabond";
        public static string ModFolderName = Directory.GetParent(typeof(Vagabond).Assembly.Location).Name.ToString();

        public SkillSchool martialArtistTreeInstance;
        public Tag ForagerTag;

        public static Vagabond Instance;
        internal void Awake()
        {
            Instance = this;

            WhiteFangNPC.Init();
            WhiteFangOutsideTracker.Init();

            SL.OnPacksLoaded += OnPackLoaded;
            SL.OnSceneLoaded += OnSceneLoaded;
            SL.BeforePacksLoaded += BeforePacksLoaded;

            var harmony = new Harmony(GUID);
            harmony.PatchAll();
        }
        private void BeforePacksLoaded()
        {
            ForagerTag = TinyTagManager.GetOrMakeTag(IDs.ForagerTag);

        }
        private void OnSceneLoaded()
        {
        }

        private void OnPackLoaded()
        {
            EffectInitializer.MakeHonedBladeInfusion();
            EffectInitializer.MakeSittingPrefab();

            PrecisionStrikeSkill.Init();
            ApplyHonedBlade.Init();
            CarefulMaintenanceSkill.Init();
            ThrowSandSKill.Init();
            ForagerSkill.Init();
            TameBeastSkill.Init();

            VagabondSkillTree.SetupSkillTree(ref martialArtistTreeInstance);
            
            foreach (var tup in new Tuple<int, string[]>[] {
                new Tuple<int, string[]>(IDs.gaberryID, new string[]{IDs.ForagerTag}),
                new Tuple<int, string[]>(IDs.krimpNutID, new string[]{IDs.ForagerTag}),
                new Tuple<int, string[]>(IDs.cactusFruidID, new string[]{IDs.ForagerTag}),
                new Tuple<int, string[]>(IDs.marshmelonID, new string[]{IDs.ForagerTag}),
                new Tuple<int, string[]>(IDs.turmmipID, new string[]{IDs.ForagerTag}),
                new Tuple<int, string[]>(IDs.dreamersRootID, new string[]{IDs.ForagerTag}),
                new Tuple<int, string[]>(IDs.marshmelonID, new string[]{IDs.ForagerTag}),
                new Tuple<int, string[]>(IDs.crawlberryID, new string[]{IDs.ForagerTag}),
                new Tuple<int, string[]>(IDs.purpkinID, new string[]{IDs.ForagerTag}),
                new Tuple<int, string[]>(IDs.maizeID, new string[]{IDs.ForagerTag}),
                new Tuple<int, string[]>(IDs.rainbowPeachID, new string[]{IDs.ForagerTag}),
                new Tuple<int, string[]>(IDs.ablerootID, new string[]{IDs.ForagerTag}),
                new Tuple<int, string[]>(IDs.goldenCrescentID, new string[]{IDs.ForagerTag}),
            }) if (ResourcesPrefabManager.Instance.GetItemPrefab(tup.Item1) is Item item) CustomItems.SetItemTags(item, TinyTagManager.GetSafeTags(tup.Item2), false);
        }
    }
}
