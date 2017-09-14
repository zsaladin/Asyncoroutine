using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Asyncoroutine
{
    public static class AwaiterCoroutineExtension
    {
        public static AwaiterCoroutine<object> GetAwaiter(this AwaiterCoroutine<object> nextFrame)
        {
            return new AwaiterCoroutine<object>(null);
        }

        public static AwaiterCoroutine<IEnumerator> GetAwaiter(this IEnumerator coroutine)
        {
            return new AwaiterCoroutine<IEnumerator>(coroutine);
        }

        public static AwaiterCoroutine<WaitForSeconds> GetAwaiter(this WaitForSeconds waitForSeconds)
        {
            return new AwaiterCoroutine<WaitForSeconds>(waitForSeconds);
        }

        public static AwaiterCoroutine<WWW> GetAwaiter(this WWW www)
        {
            return new AwaiterCoroutine<WWW>(www);
        }
    }
}