using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vagabond
{
    using InstanceIDs;
    using SideLoader;
    using TinyHelper;
    using UnityEngine;

    class TamingEffect : Effect
    {
        public const string EFFECT_NAME = "TamingEffect";
        public const string DISPLAY_NAME = "Taming Beasts";
        public const string DESCRIPTION = "Nearby beasts may eat the food you left behind and become friendly towards you.";
        public const string IMAGE_PATH = @"\SideLoader\Texture2D\animalCompanionStatusEffect.png";
        public const float TICK_RATE = 0.2f;
        protected override void ActivateLocally(Character _affectedCharacter, object[] _infos)
        {
            // standing up removes the taming status effect
            if (_affectedCharacter.CurrentSpellCast != Character.SpellCastType.Sit && this.m_parentStatusEffect.Age > 1)
            {
                _affectedCharacter.StatusEffectMngr.CleanseStatusEffect(TamingEffect.EFFECT_NAME);
                return;
            }

            // get all the raw meats on the ground, as game objects 
            var foodItems = GameObject.FindObjectsOfType<Item>().Where(x => x.ItemID == IDs.rawMeatID || x.ItemID == IDs.rawAlphaMeatID).ToList();

            // iterate every food item close enough to the player to be used to taming beasts
            foreach (var food in foodItems.Where(x => x.transform.parent == null && Vector3.Distance(_affectedCharacter.transform.position, x.transform.position) <= TameBeastSkill.PLAYER_RANGE))
            {
                //tame beasts
                List<Character> beastsInRange = new List<Character>();
                CharacterManager.Instance.FindCharactersInRange(food.transform.position, TameBeastSkill.FOOD_RANGE, ref beastsInRange);

                beastsInRange = beastsInRange.Where(x => x.Faction == Character.Factions.Hounds).ToList();
                
                if (beastsInRange.Count > 0)
                {
                    var closestBeast = beastsInRange[0];
                    closestBeast.StatusEffectMngr.AddStatusEffect(Vagabond.Instance.TamedStatusEffectPrefab, _affectedCharacter);
                    EatFood(closestBeast, _affectedCharacter, food);
                    continue;
                }

                if (_affectedCharacter.Inventory.SkillKnowledge.IsItemLearned(IDs.animalCompanionSkillID))
                {
                    //feed companions
                    List<Character> tamedInRange = new List<Character>();
                    CharacterManager.Instance.FindCharactersInRange(food.transform.position, TameBeastSkill.FOOD_RANGE, ref tamedInRange);

                    tamedInRange = tamedInRange.Where(
                        x =>
                        x.StatusEffectMngr is StatusEffectManager manager && manager.HasStatusEffect(TamedEffect.EFFECT_NAME) && !manager.HasStatusEffect(AnimalCompanionEffect.EFFECT_NAME)
                    ).ToList();

                    if (tamedInRange.Count > 0)
                    {
                        var closestBeast = tamedInRange[0];
                        EatFood(closestBeast, _affectedCharacter, food);
                        continue;
                    }

                }

                //attract potential companions
                List<Character> companionsInRange = new List<Character>();
                CharacterManager.Instance.FindCharactersInRange(food.transform.position, AnimalCompanionSkill.ATTRACTION_RANGE, ref companionsInRange);
                companionsInRange = companionsInRange.Where(
                    x =>
                    x.StatusEffectMngr is StatusEffectManager manager
                    //&& manager.HasStatusEffect(TamedEffect.EFFECT_NAME)
                    && !manager.HasStatusEffect(AnimalCompanionEffect.EFFECT_NAME)
                    && !x.InCombat
                ).ToList();
                foreach (var c in companionsInRange)
                {
                    if (
                        _affectedCharacter.Inventory.SkillKnowledge.IsItemLearned(IDs.animalCompanionSkillID)
                        || Random.Range(0f, 1f) < TICK_RATE / 10
                    )
                    {
                        var characterAI = c.gameObject.GetComponentInChildren<CharacterAI>();
                        characterAI.SetDestination(food.transform.position, false);
                    }
                }
            }
        }

        private static void EatFood(Character character, Character sourceCharacter, Item food)
        {
            character.StatusEffectMngr?.AddStatusEffect(Vagabond.Instance.AnimalCompanionStatusEffectPrefab, sourceCharacter);
            switch (food.ItemID)
            {
                case IDs.rawMeatID:
                    character.StatusEffectMngr?.AddStatusEffect(IDs.healthRecovery2NameID);
                    break;
                case IDs.rawAlphaMeatID:
                    character.StatusEffectMngr?.AddStatusEffect(IDs.healthRecovery3NameID);
                    character.StatusEffectMngr?.AddStatusEffect(IDs.rageNameID);
                    break;
                default:
                    break;
            }
            Destroy(food.gameObject);
            CasualStagger.Stagger(character);
        }
    }
}
