using System;
using System.Collections;
using System.Threading;
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

            AwaiterCoroutineer.Instance.StartAwaiterCoroutine(this);
        }

        public AwaiterCoroutine(Func<TInstruction> func)
        {
            AwaiterCoroutineer.Instance.SynchronizationContext.Send(state =>
            {
                Instruction = func();
                Coroutine = new Enumerator(this);

                AwaiterCoroutineer.Instance.StartAwaiterCoroutine(this);
            }, null);
        }

        public TInstruction GetResult()
        {
            return Instruction;
        }

        void INotifyCompletion.OnCompleted(Action continuation)
        {
            _continuation = continuation;
        }

        public AwaiterCoroutine<TInstruction> GetAwaiter()
        {
            return this;
        }
    }
}