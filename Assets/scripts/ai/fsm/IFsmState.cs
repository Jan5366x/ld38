using UnityEngine;

namespace ai.fsm
{
    public interface IFsmState
    {
        void Update(Fsm fsm, GameObject gameObject);
    }
}