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
        public static StatusEffect MakeTamedStatusEffectPrefab()
        {
            var statusEffect = TinyEffectManager.MakeStatusEffectPrefab(
                effectName: TamedEffect.EFFECT_NAME,
                displayName: TamedEffect.DISPLAY_NAME,
                familyName: TamedEffect.EFFECT_NAME,
                description: TamedEffect.DESCRIPTION,
                lifespan: -1,
                refreshRate: TamedEffect.TICK_RATE,
                stackBehavior: StatusEffectFamily.StackBehaviors.Override,
                targetStatusName: "Bandage",
                isMalusEffect: false,
                modGUID: Vagabond.GUID,
                iconFileName: Vagabond.ModFolderName + TamedEffect.IMAGE_PATH
            );

            var effectSignature = statusEffect.StatusEffectSignature;
            var effectComponent = TinyGameObjectManager.MakeFreshObject("Effects", true, true, effectSignature.transform).AddComponent<TamedEffect>();
            effectComponent.UseOnce = false;
            effectSignature.Effects = new List<Effect>() { effectComponent };

            statusEffect.IsHidden = false;
            statusEffect.DisplayInHud = true;

            return statusEffect;
        }

        public static StatusEffect MakeAnimalCompanionStatusEffectPrefab()
        {
            var statusEffect = TinyEffectManager.MakeStatusEffectPrefab(
                effectName: AnimalCompanionEffect.EFFECT_NAME,
                displayName: AnimalCompanionEffect.DISPLAY_NAME,
                familyName: AnimalCompanionEffect.EFFECT_NAME,
                description: AnimalCompanionEffect.DESCRIPTION,
                lifespan: AnimalCompanionEffect.LIFESPAN,
                refreshRate: AnimalCompanionEffect.TICK_RATE,
                stackBehavior: StatusEffectFamily.StackBehaviors.Override,
                targetStatusName: "Bandage",
                isMalusEffect: false,
                modGUID: Vagabond.GUID,
                iconFileName: Vagabond.ModFolderName + AnimalCompanionEffect.IMAGE_PATH
            );

            var effectSignature = statusEffect.StatusEffectSignature;
            var effectComponent = TinyGameObjectManager.MakeFreshObject("Effects", true, true, effectSignature.transform).AddComponent<AnimalCompanionEffect>();
            effectComponent.UseOnce = false;
            effectSignature.Effects = new List<Effect>() { effectComponent };

            statusEffect.IsHidden = false;
            statusEffect.DisplayInHud = true;

            return statusEffect;
        }

        public static void MakeTamingStatusEffectPrefab()
        {
            var statusEffect = TinyEffectManager.MakeStatusEffectPrefab(
                effectName: TamingEffect.EFFECT_NAME,
                displayName: TamingEffect.DISPLAY_NAME,
                familyName: TamingEffect.EFFECT_NAME,
                description: TamingEffect.DESCRIPTION,
                lifespan: -1,
                refreshRate: TamingEffect.TICK_RATE,
                stackBehavior: StatusEffectFamily.StackBehaviors.Override,
                targetStatusName: "Bandage",
                isMalusEffect: false,
                modGUID: Vagabond.GUID,
                iconFileName: Vagabond.ModFolderName + TamedEffect.IMAGE_PATH
            );

            var effectSignature = statusEffect.StatusEffectSignature;
            var effectComponent = TinyGameObjectManager.MakeFreshObject("Effects", true, true, effectSignature.transform).AddComponent<TamingEffect>();
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
