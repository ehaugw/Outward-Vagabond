using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vagabond
{
    using SideLoader;
    using TinyHelper;
    using UnityEngine;

    class TamedEffect : Effect
    {
        public const string EFFECT_NAME = "Tamed";
        public const string DISPLAY_NAME = "Tamed";
        public const string DESCRIPTION = "Tamed";
        public const string IMAGE_PATH = @"\SideLoader\Texture2D\tamingStatusEffect.png";
        public const float TICK_RATE = 0.2f;

        public Character.Factions OriginalFaction;

        protected override void StopAffectLocally(Character _affectedCharacter)
        {
            _affectedCharacter.ChangeFaction(OriginalFaction);
        }

        protected override void ActivateLocally(Character _affectedCharacter, object[] _infos)
        {
            if (!_affectedCharacter.IsAlly(SourceCharacter))
            {
                OriginalFaction = _affectedCharacter.Faction;
                _affectedCharacter.ChangeFaction(SourceCharacter.Faction);
            }
        }
    }
}
