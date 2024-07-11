using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vagabond
{
    using SideLoader;
    using TinyHelper;
    using UnityEngine;

    class TameBeastStatusEffect : Effect
    {
        public const string TAMING_EFFECT_NAME = "TamingBeastBySitting";
        public const string TAMING_DISPLAY_NAME = "Taming Nearby Beasts";
        public const float TICK_RATE = 0.2f;
        protected override void ActivateLocally(Character _affectedCharacter, object[] _infos)
        {
            if ((((_affectedCharacter?.Animator?.velocity != null) && (_affectedCharacter.Animator.velocity.sqrMagnitude > 0.1)) || ((_affectedCharacter?.AnimMoveSqMagnitude ?? 0) > 0.1 && this.m_parentStatusEffect.Age > 0.5)) && this.m_parentStatusEffect.Age > 1)
            {
                _affectedCharacter.StatusEffectMngr.CleanseStatusEffect(TameBeastStatusEffect.TAMING_EFFECT_NAME);
            }

            var character = _affectedCharacter;
            var foods = GameObject.FindObjectsOfType<Food>();
            var foodItems = foods.Select(x => x.transform.gameObject).ToList();

            TameBeastEffect.StaticActivate(_affectedCharacter, null, null);
            Console.WriteLine("tick");
            Debug.Log("Tick");
        }
    }
}
