using System.Collections.Generic;
using UnityEngine;

namespace ai.fsm
{
    public class Fsm
    {
        private readonly Stack<FsmState> _stateStack = new Stack<FsmState>();

        public delegate void FsmState(Fsm fsm, GameObject gameObject);

        public void Update(GameObject gameObject)
        {
            if (_stateStack.Peek() != null)
                _stateStack.Peek().Invoke(this, gameObject);
        }

        public void PushState(FsmState state)
        {
            _stateStack.Push(state);
        }

        public void PopState()
        {
            _stateStack.Pop();
        }
    }
}