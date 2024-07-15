using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vagabond
{
    using SideLoader;
    using TinyHelper;
    using UnityEngine;

    class TamedStatusEffect : Effect
    {
        public const string TAMING_EFFECT_NAME = "Tamed";
        public const string TAMING_DISPLAY_NAME = "Tamed";
        public const float TICK_RATE = 0.2f;
        protected override void ActivateLocally(Character _affectedCharacter, object[] _infos)
        {
            if (!_affectedCharacter.IsAlly(SourceCharacter))
            {
                _affectedCharacter.StatusEffectMngr.CleanseStatusEffect(Vagabond.Instance.Tamed);
            }

            if (SourceCharacter != null && _affectedCharacter.IsAI && !_affectedCharacter.InCombat && Vector3.Distance(_affectedCharacter.transform.position, SourceCharacter.transform.position) > 10)
            {
                var characterAI = _affectedCharacter.gameObject.GetComponentInChildren<CharacterAI>();
                characterAI.SetDestination(SourceCharacter.transform.position);
            }
        }
    }
}
