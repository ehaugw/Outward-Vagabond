using InstanceIDs;
using SideLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using TinyHelper;

namespace Vagabond
{
    public class CommandBeastSkill
    {
        public static Skill Init()
        {
            var myitem = new SL_AttackSkill()
            {
                Name = "Command Beast",
                EffectBehaviour = EditBehaviours.Destroy,
                Target_ItemID = IDs.sparkID,
                New_ItemID = IDs.commandBeastSkillID,
                SLPackName = Vagabond.ModFolderName,
                SubfolderName = "CommandBeast",
                Description = "Commands animal companions to attack the targeted creature.",
                CastType = Character.SpellCastType.Fast,
                CastModifier = Character.SpellCastModifier.Mobile,
                CastLocomotionEnabled = true,
                MobileCastMovementMult = 0.7f,
                CastSheatheRequired = 0,
                
                Cooldown = 20,
                StaminaCost = 2,
                HealthCost = 0,
                ManaCost = 0,
            };

            myitem.ApplyTemplate();
            Skill skill = ResourcesPrefabManager.Instance.GetItemPrefab(myitem.New_ItemID) as Skill;

            //Set the correct animation
            new SL_PlaySoundEffect()
            {
                Follow = true,
                OverrideCategory = EffectSynchronizer.EffectCategories.None,
                Delay = 0.0f,
                MinPitch = 1,
                MaxPitch = 1,
                SyncType = Effect.SyncTypes.OwnerSync,
                Sounds = new List<GlobalAudioManager.Sounds>() { GlobalAudioManager.Sounds.CS_Human_Yelling1, GlobalAudioManager.Sounds.CS_Human_Yelling2 }
            }.ApplyToTransform(TinyGameObjectManager.GetOrMake(skill.transform, "ActivationEffects", true, true));


            var effects = TinyGameObjectManager.GetOrMake(skill.transform, "Effects", true, true);
            effects.gameObject.AddComponent<CommandBeastEffect>();
            return skill;
        }
    }
}
