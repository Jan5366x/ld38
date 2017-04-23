using System.Collections.Generic;
using UnityEngine;

namespace ai.goap
{
    public abstract class GoapAction : MonoBehaviour
    {
        private bool _inRange = false;

        public float Cost = 1f;

        public GameObject Target;

        protected GoapAction()
        {
            Preconditions = new HashSet<KeyValuePair<string, object>>();
            Effects = new HashSet<KeyValuePair<string, object>>();
        }

        public void DoReset()
        {
            _inRange = false;
            Target = null;
            Reset();
        }

        public abstract void Reset();

        public abstract bool IsDone();

        public abstract bool CheckProceduralPrecondition(GameObject agent);

        public abstract bool Perform(GameObject agent);

        public abstract bool RequiresInRange();

        public bool IsInRange()
        {
            return _inRange;
        }

        public void SetInRange(bool inRange)
        {
            _inRange = inRange;
        }

        public void AddPrecondition(string key, object value)
        {
            Preconditions.Add(new KeyValuePair<string, object>(key, value));
        }

        public void RemovePrecondition(string key)
        {
            var remove = default(KeyValuePair<string, object>);
            foreach (var precondition in Preconditions)
            {
                if (precondition.Key.Equals(key))
                    remove = precondition;
            }
            if (!default(KeyValuePair<string, object>).Equals(remove))
                Preconditions.Remove(remove);
        }

        public void AddEffect(string key, object value)
        {
            Effects.Add(new KeyValuePair<string, object>(key, value));
        }

        public void RemoveEffect(string key)
        {
            var remove = default(KeyValuePair<string, object>);
            foreach (var effect in Effects)
            {
                if (effect.Key.Equals(key))
                    remove = effect;
            }
            if (!default(KeyValuePair<string, object>).Equals(remove))
                Effects.Remove(remove);
        }

        public HashSet<KeyValuePair<string, object>> Preconditions { get; private set; }

        public HashSet<KeyValuePair<string, object>> Effects { get; private set; }
    }
}