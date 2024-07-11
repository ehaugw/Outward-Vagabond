using InstanceIDs;
using SideLoader;
using UnityEngine;

namespace Vagabond
{
    public class TameBeastSkill
    {
        public const float PLAYER_RANGE = 50;
        public const float FOOD_RANGE = 1;
        public static Skill Init()
        {
            var myitem = new SL_Skill()
            {
                Name = "Tame Beast",
                EffectBehaviour = EditBehaviours.Destroy,
                Target_ItemID = IDs.pushKickID,
                New_ItemID = IDs.tameBeastID,
                SLPackName = Vagabond.ModFolderName,
                SubfolderName = "TameBeast",
                Description = "Sit down and wait for nearby predator to eat fresh meat that you left behind, and hope that they become friendly towards you.",
                CastType = Character.SpellCastType.Sit,
                CastModifier = Character.SpellCastModifier.NONE,
                CastLocomotionEnabled = false,
                MobileCastMovementMult = 1,
                CastSheatheRequired = 2,

                EffectTransforms = new SL_EffectTransform[] {
                    new SL_EffectTransform() {
                        TransformName = "Effects",
                        Effects = new SL_Effect[] {
                            new SL_AddStatusEffect() { StatusEffect = TameBeastStatusEffect.TAMING_EFFECT_NAME, ChanceToContract = 100, Delay = 2 },
                        }
                    }
                },

                Cooldown = 2,
                StaminaCost = 5,
                ManaCost= 0,
            };

            myitem.ApplyTemplate();
            Skill skill = ResourcesPrefabManager.Instance.GetItemPrefab(myitem.New_ItemID) as Skill;

            //var myEffects = skill.transform.Find("Effects");
            //myEffects.gameObject.AddComponent<TameBeastEffect>();

            return skill;
        } //PART OF CHANNEL DIVINITY
    }
}
