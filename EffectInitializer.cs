using System.Collections.Generic;
using TinyHelper;
using InstanceIDs;
using SideLoader;
using UnityEngine;
using System.Linq;

namespace Vagabond
{
    class EffectInitializer
    {
        public static void MakeSittingPrefab()
        {
            var statusEffect = TinyEffectManager.MakeStatusEffectPrefab(
                effectName: TameBeastStatusEffect.TAMING_EFFECT_NAME,
                displayName: TameBeastStatusEffect.TAMING_DISPLAY_NAME,
                familyName: TameBeastStatusEffect.TAMING_EFFECT_NAME,
                description: "Wait for nearby beasts to eat the meat you left behind.",
                lifespan: -1,
                refreshRate: TameBeastStatusEffect.TICK_RATE,
                stackBehavior: StatusEffectFamily.StackBehaviors.Override,
                targetStatusName: "Bandage",
                isMalusEffect: false,
                modGUID: Vagabond.GUID,
                iconFileName: Vagabond.ModFolderName + @"\SideLoader\Texture2D\tamingEffect.png"
            );;

            var effectSignature = statusEffect.StatusEffectSignature;
            var effectComponent = TinyGameObjectManager.MakeFreshObject("Effects", true, true, effectSignature.transform).AddComponent<TameBeastStatusEffect>();
            effectComponent.UseOnce = false;
            effectSignature.Effects = new List<Effect>() { effectComponent };

            statusEffect.IsHidden = false;
            statusEffect.DisplayInHud = true;
        }

        public static ImbueEffectPreset MakeHonedBladeInfusion()
        {
            ImbueEffectPreset effectPreset = TinyEffectManager.MakeImbuePreset(
                imbueID: IDs.honedBladeImbueID,
                name: "Honed Blade",
                description: "Weapon deals some Physical damage.",
                //iconFileName: Crusader.ModFolderName + @"\SideLoader\Texture2D\impendingDoomImbueIcon.png",
                visualEffectID: IDs.windImbueID
            );

            Transform effectTransform;

            effectTransform = TinyGameObjectManager.MakeFreshObject("Effects", true, true, effectPreset.transform).transform;
            TinyEffectManager.MakeWeaponDamage(effectTransform, 0, 0.20f, DamageType.Types.Physical, 0f);

            var fx = effectPreset.ImbueFX;

            fx = Object.Instantiate(fx);
            fx.gameObject.SetActive(false);
            Object.DontDestroyOnLoad(fx);
            effectPreset.ImbueFX = fx;

            GameObject.Destroy(fx.Find("BlessParticlesSparkles").gameObject);

            return effectPreset;
        }
    }
}
