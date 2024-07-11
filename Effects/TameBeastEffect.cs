namespace Vagabond
{
    using System.Collections.Generic;
    using System.Linq;
    using TinyHelper;
    using UnityEngine;
    using static MapMagic.Matrix;

    class TameBeastEffect : Effect
    {
        public static void StaticActivate(Character character, object[] _infos, Effect instance)
        {
            var foods = GameObject.FindObjectsOfType<Food>();
            var foodItems = foods.Select(x => x.transform.gameObject).ToList();

            foreach (var food in foodItems.Where(x => x.transform.parent == null && Vector3.Distance(character.transform.position, x.transform.position) <= TameBeastSkill.PLAYER_RANGE))
            {
                List<Character> charsInRange = new List<Character>();
                CharacterManager.Instance.FindCharactersInRange(food.transform.position, TameBeastSkill.FOOD_RANGE, ref charsInRange);

                float closestDistance = 0;
                Character closestBeast = null;

                foreach (var c in charsInRange.Where(c => c.Faction == Character.Factions.Hounds))
                {
                    var distance = Vector3.Distance(c.transform.position, character.transform.position);

                    if (closestBeast == null || distance < closestDistance)
                    {
                        closestBeast = c;
                        closestDistance = distance;
                    }
                }

                if (closestBeast != null)
                {
                    closestBeast.ChangeFaction(Character.Factions.Player);
                    Destroy(food);
                    CasualStagger.Stagger(closestBeast);
                }
            }


        }
        protected override void ActivateLocally(Character character, object[] _infos)
        {
            StaticActivate(character, _infos, this);
        }
    }
}
