using System;
using System.Collections;
using System.Threading;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Asyncoroutine
{
    public partial class AwaiterCoroutine<TInstruction> : INotifyCompletion
    {
        protected Action Continuation { get; private set; }
        public TInstruction Instruction { get; protected set; }
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

                if (value && Continuation != null)
                    Continuation();
            }
        }

        public AwaiterCoroutine()
        {

        }

        public AwaiterCoroutine(TInstruction instruction)
        {
            ProcessCoroutine(instruction);
        }

        private void ProcessCoroutine(TInstruction instruction)
        {
            Instruction = instruction;
            Coroutine = new Enumerator(this);

            AwaiterCoroutineer.Instance.StartAwaiterCoroutine(this);
        }

        public TInstruction GetResult()
        {
            return Instruction;
        }

        void INotifyCompletion.OnCompleted(Action continuation)
        {
            Continuation = continuation;
        }

        public AwaiterCoroutine<TInstruction> GetAwaiter()
        {
            return this;
        }
    }

    public class AwaiterCoroutineWaitForMainThread : AwaiterCoroutine<WaitForMainThread>
    {
        public AwaiterCoroutineWaitForMainThread()
        {
            Instruction = default(WaitForMainThread);

            if (SynchronizationContext.Current != null)
            {
                Continuation();
            }
            else
            {
                AwaiterCoroutineer.Instance.SynchronizationContext.Post(state =>
                {
                    Continuation();
                }, null);
            }
        }
    }
}