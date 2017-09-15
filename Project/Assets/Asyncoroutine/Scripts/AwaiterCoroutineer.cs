using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using UnityEngine;

namespace Asyncoroutine
{
    public class AwaiterCoroutineer : MonoBehaviour
    {
        private static AwaiterCoroutineer _instance;
        public static AwaiterCoroutineer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject("AwaiterCoroutineer").AddComponent<AwaiterCoroutineer>();
                    DontDestroyOnLoad(_instance);
                }

                return _instance;
            }
        }

        public SynchronizationContext SynchronizationContext { get; private set; }

        private void Awake()
        {
            SynchronizationContext = SynchronizationContext.Current;
        }

        public void StartAwaiterCoroutine<T>(AwaiterCoroutine<T> awaiterCoroutine)
        {
            StartCoroutine(awaiterCoroutine.Coroutine);
        }

        public void StopAwaiterCoroutine<T>(AwaiterCoroutine<T> awaiterCoroutine)
        {
            StopCoroutine(awaiterCoroutine.Coroutine);
        }
    }
}