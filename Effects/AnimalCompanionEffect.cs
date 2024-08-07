using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vagabond
{
    using SideLoader;
    using TinyHelper;
    using UnityEngine;

    class AnimalCompanionEffect : Effect
    {
        public const string EFFECT_NAME = "AnimalCompanionStatusEffect";
        public const string DISPLAY_NAME = "Animal companion";
        public const string DESCRIPTION = "You are an animal companion";
        public const string IMAGE_PATH = @"\SideLoader\Texture2D\animalCompanionStatusEffect.png";
        public const float TICK_RATE = 0.2f;
        public const float LIFESPAN = 240;

        protected override void ActivateLocally(Character _affectedCharacter, object[] _infos)
        {
            if (SourceCharacter != null && _affectedCharacter.IsAI && !_affectedCharacter.InCombat && Vector3.Distance(_affectedCharacter.transform.position, SourceCharacter.transform.position) > 15 && !_affectedCharacter.InCombat)
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
