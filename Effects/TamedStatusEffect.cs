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

            if (SourceCharacter != null && _affectedCharacter.IsAI && !_affectedCharacter.InCombat && Vector3.Distance(_affectedCharacter.transform.position, SourceCharacter.transform.position) > 15)
            {
                var characterAI = _affectedCharacter.gameObject.GetComponentInChildren<CharacterAI>();

                foreach (var state in characterAI.AiStates)
                {
                    if (state is AISWander)
                    {
                        characterAI.SwitchAiState(state.StateID);
                        ((AISWander)state).SpeedModif = 1;
                        break;
                    }
                }
                characterAI.SetDestination(SourceCharacter.transform.position, false);
            }
        }
    }
}
