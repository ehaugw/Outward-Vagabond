namespace Vagabond
{
    using InstanceIDs;
    using System.Collections.Generic;
    using System.Linq;
    using TinyHelper;
    using UnityEngine;
    using static MapMagic.Matrix;

    class CommandBeastEffect : Effect
    {
        public const float COMMAND_RANGE = 10000f;
        public static void StaticActivate(Character character, object[] _infos, Effect instance)
        {
            List<Character> charsInRange = new List<Character>();
            CharacterManager.Instance.FindCharactersInRange(character.transform.position, CommandBeastEffect.COMMAND_RANGE, ref charsInRange);

            foreach (var otherCharacter in charsInRange)
            {

                if (otherCharacter?.StatusEffectMngr?.HasStatusEffect(TamedStatusEffect.TAMING_EFFECT_NAME) ?? false)
                {
                    if (otherCharacter?.StatusEffectMngr?.GetStatusEffectOfName(TamedStatusEffect.TAMING_EFFECT_NAME)?.SourceCharacter == character)
                    {
                        if (character.TargetingSystem?.LockedCharacter?.LockingPoint is LockingPoint lockingPoint)
                        {
                            var characterAI = otherCharacter.gameObject.GetComponentInChildren<CharacterAI>();
                            
                            foreach (var state in characterAI.AiStates)
                            {
                                if (state is AISCombat)
                                {
                                    characterAI.SwitchAiState(state.StateID);
                                    break;
                                }
                            }

                            characterAI.TargetingSystem.SetLockingPoint(lockingPoint);
                        }
                    }
                }
            }
        }
        protected override void ActivateLocally(Character character, object[] _infos)
        {
            StaticActivate(character, _infos, this);
        }
    }
}
