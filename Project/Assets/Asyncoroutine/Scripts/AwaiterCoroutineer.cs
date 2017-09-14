using System.Collections;
using System.Collections.Generic;
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

        public void StartAwaiterCoroutine<T>(AwaiterCoroutine<T>.Enumerator coroutine)
        {
            StartCoroutine(coroutine);
        }

        public void StopAwaiterCoroutine<T>(AwaiterCoroutine<T>.Enumerator coroutine)
        {
            StopCoroutine(coroutine);
        }
    }
}