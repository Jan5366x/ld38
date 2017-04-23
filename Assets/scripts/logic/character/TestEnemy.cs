using System.Collections.Generic;

namespace logic.character
{
    public class TestEnemy : Enemy
    {
        public override HashSet<KeyValuePair<string, object>> CreateGoalState()
        {
            var goal = new HashSet<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("damagePlayer", true),
                new KeyValuePair<string, object>("returnToInfector", true)
            };

            return goal;
        }
    }
}