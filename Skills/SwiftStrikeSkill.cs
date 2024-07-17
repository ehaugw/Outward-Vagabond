using InstanceIDs;
using SideLoader;
using System.Collections.Generic;
using UnityEngine;
using TinyHelper;
using System;

namespace Vagabond
{
    public class SwiftStrikeSkill
    {
        public static Skill Init()
        {
            var myitem = new SL_AttackSkill()
            {
                Name = "Swift Strike",
                EffectBehaviour = EditBehaviours.Destroy,
                Target_ItemID = IDs.perfectStrikeID,
                New_ItemID = IDs.swiftStrikeID,
                SLPackName = Vagabond.ModFolderName,
                SubfolderName = "SwiftStrike",
                Description = "Dash forward and strike. Applies Bleeding and causes Drawback.",
                CastType = Character.SpellCastType.PreciseStrike,
                CastModifier = Character.SpellCastModifier.Attack,
                CastLocomotionEnabled = false,
                MobileCastMovementMult = -1,
                CastSheatheRequired = 2,

                RequiredWeaponTypes = new Weapon.WeaponType[] {
                    Weapon.WeaponType.FistW_2H,
                    Weapon.WeaponType.Axe_1H,
                    Weapon.WeaponType.Axe_2H,
                    Weapon.WeaponType.Sword_1H,
                    Weapon.WeaponType.Sword_2H,
                    Weapon.WeaponType.Mace_1H,
                    Weapon.WeaponType.Mace_2H,
                    Weapon.WeaponType.Halberd_2H,
                    Weapon.WeaponType.Spear_2H,
                    Weapon.WeaponType.FistW_2H,
                    Weapon.WeaponType.FistW_2H
                },
                

                Cooldown = 15,
                StaminaCost = 7,
                ManaCost = 0,
                HealthCost = 0,
                DurabilityCost = 3,
                
                EffectTransforms = new SL_EffectTransform[] {
                    new SL_EffectTransform() {
                        TransformName = "Effects",
                        Effects = new SL_Effect[] {
                        }
                    },
                    new SL_EffectTransform() {
                        TransformName = "ActivationEffects",
                        Effects = new SL_Effect[] {
                        }
                    },
                    new SL_EffectTransform() {
                        TransformName = "HitEffects",
                        Effects = new SL_Effect[] {
                        }
                    },
                },
            };

            myitem.ApplyTemplate();
            Skill skill = ResourcesPrefabManager.Instance.GetItemPrefab(myitem.New_ItemID) as Skill;

            var activationEffects = TinyGameObjectManager.GetOrMake(skill.transform, "ActivationEffects", true, true);
            var drawback = activationEffects.gameObject.AddComponent<AddStatusEffect>();
            drawback.SetChanceToContract(100);
            drawback.Status = ResourcesPrefabManager.Instance.GetStatusEffectPrefab(IDs.drawbackNameID);

            new SL_PlaySoundEffect()
            {
                Follow = true,
                OverrideCategory = EffectSynchronizer.EffectCategories.None,
                Delay = 0,
                MinPitch = 1,
                MaxPitch = 1,
                SyncType = Effect.SyncTypes.OwnerSync,
                Sounds = new List<GlobalAudioManager.Sounds>() { GlobalAudioManager.Sounds.SFX_SKILL_PreciseStrike_PhysicalCharge }
            }.ApplyToTransform(activationEffects);

            Transform hitEffects = TinyGameObjectManager.GetOrMake(skill.transform, "HitEffects", true, true).transform;
            
            var damage = hitEffects.gameObject.AddComponent<WeaponDamage>();
            setDamage(damage);

            var bleed = hitEffects.gameObject.AddComponent<AddStatusEffect>();
            bleed.SetChanceToContract(100);
            bleed.Status = ResourcesPrefabManager.Instance.GetStatusEffectPrefab(IDs.bleedNameID);

            //if (skill.transform.FindContains("VFXPreciseStrike").transform is Transform vfx)
            //{
            //    if (vfx.FindContains("Body").transform is Transform body)
            //    {
            //        body.parent= null;
            //        GameObject.Destroy(body.gameObject);
            //    }
            //}
            //GameObject.Destroy(skill.gameObject.GetComponentInChildren<VFXParticlesOnChar>().transform);
            return skill;
        }

        private static void setDamage(WeaponDamage damage)
        {
            float minDamage = 3f;

            damage.WeaponDamageMult = minDamage;
            damage.WeaponDamageMultKDown = -1.0f;
            damage.WeaponKnockbackMult = 1.5f;

            damage.WeaponDurabilityLossPercent = 0;
            damage.WeaponDurabilityLoss = 5;
            damage.OverrideDType = DamageType.Types.Count;
            damage.Damages = new DamageType[] { };

        }
    }
}
