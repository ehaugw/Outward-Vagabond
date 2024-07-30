using InstanceIDs;
using SideLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyHelper;
using UnityEngine;
using HarmonyLib;


namespace Vagabond
{
    public class PrecisionStrikeSkill
    {
        public const string NAME = "Precision Strike";
        public static Skill Init()
        {
            var myitem = new SL_Skill()
            {
                Name = NAME,
                EffectBehaviour = EditBehaviours.Destroy,
                Target_ItemID = IDs.arbitraryPassiveSkillID,
                New_ItemID = IDs.precisionStrikeSkillID,
                SLPackName = Vagabond.ModFolderName,
                SubfolderName = "PrecisionStrike",
                Description = "Weapon attacks against prone targets or from behind deal some bonus damage, ignore half of the targets' resistances and cause extreme bleeding for a brief moment.",
                IsUsable = false,
                CastType = Character.SpellCastType.NONE,
                CastModifier = Character.SpellCastModifier.Immobilized,
                CastLocomotionEnabled = false,
                MobileCastMovementMult = -1f,
            };
            myitem.ApplyTemplate();
            Skill skill = ResourcesPrefabManager.Instance.GetItemPrefab(myitem.New_ItemID) as Skill;
            return skill;
        }
    }
    [HarmonyPatch(typeof(Character), nameof(Character.ReceiveHit), new Type[] { typeof(UnityEngine.Object), typeof(DamageList), typeof(Vector3), typeof(Vector3), typeof(float), typeof(float), typeof(Character), typeof(float), typeof(bool) })]
    public class Character_ReceiveHit
    {
        [HarmonyPrefix]
        public static void HarmonyPrefix(Character __instance, UnityEngine.Object _damageSource, DamageList _damage, Vector3 _hitDir, float _angle)
        {
            //var eligibleTypes = new Weapon.WeaponType[] { Weapon.WeaponType.Axe_1H, Weapon.WeaponType.Axe_2H, Weapon.WeaponType.Spear_2H, Weapon.WeaponType.Sword_1H, Weapon.WeaponType.Sword_2H };

            //taken from official code, but missbehaves
            //var hitFromBack = Vector3.Dot(__instance.transform.forward, -_hitDir) < 0f && _angle >= 120f;
            
            if (_damageSource is Weapon _weapon)
            {
                var hitFromBack = Vector3.Angle(__instance.transform.forward, _weapon.OwnerCharacter.transform.forward) < 60f;

                if (/*eligibleTypes.Contains(_weapon.Type) &&*/ SkillRequirements.SafeHasSkillKnowledge(_weapon?.OwnerCharacter, IDs.precisionStrikeSkillID) && (hitFromBack || __instance.CharHurtType == Character.HurtType.Knockdown))
                {
                    var attackType = _weapon.LastAttackID;
                    if (attackType == 0 || attackType == 1)
                    {
                        Debug.Log("from behind");
                        _damage.IgnoreHalfResistances = true;
                        _damage.Add(_damage * 0.2f);
                        TinyEffectManager.AddStatusEffectForDuration(__instance, IDs.extremeBleedNameID, 10, _weapon.OwnerCharacter);
                    }
                }
            }
        }
    }
}
