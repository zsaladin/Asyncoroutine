using System;
using System.Collections;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Asyncoroutine
{
    public partial class AwaiterCoroutine<TInstruction> : INotifyCompletion
    {
        private Action _continuation;
        public TInstruction Instruction { get; private set; }
        public Enumerator Coroutine { get; private set; }

        private bool _isCompleted;
        public bool IsCompleted
        {
            get
            {
                return _isCompleted;
            }
            private set
            {
                _isCompleted = value;

                if (value && _continuation != null)
                    _continuation();
            }
        }

        public AwaiterCoroutine(TInstruction instruction)
        {
            Instruction = instruction;
            Coroutine = new Enumerator(this);

            AwaiterCoroutineer.Instance.StartAwaiterCoroutine(Coroutine);
        }

        public TInstruction GetResult()
        {
            return Instruction;
        }

        void INotifyCompletion.OnCompleted(Action continuation)
        {
            _continuation = continuation;
        }
        

        public static readonly AwaiterCoroutine<object> NextFrame = default(AwaiterCoroutine<object>);
    }
}