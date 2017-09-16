using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Asyncoroutine
{
    public class ExampleUnityCoroutine : MonoBehaviour
    {
        private IEnumerator Start()
        {
            yield return new WaitForSeconds(1f);
            Debug.Log("WaitForSeconds");

            yield return Task.Delay(1000).AsCoroutine();
            Debug.Log("AsCoroutine1 Delay");

            var taskYieldInstruction = Task.Run(() => LongTimeJob()).AsCoroutine();
            yield return taskYieldInstruction;

            Debug.LogFormat("AsCoroutine2 Task.Run Result : {0}", taskYieldInstruction.Result);
        }

        private long LongTimeJob()
        {             
            long sum = 0;
            for (long i = 0; i < 1000000000; ++i)
            {
                sum += i;
                if (i % 100000000 == 0)
                    Debug.LogFormat("Task is running on ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            }
            return sum;
        }
    }
}