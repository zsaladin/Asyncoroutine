using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Asyncoroutine.Example
{
    public class ExampleSynchronization : MonoBehaviour
    {
        private async void Example1()
        {
            // CoroutineSync must be initialized on main thread before calling this.CoroutineSync.
            // It's  necessary just once.
            // If you use await coroutine on main thread like await WaitForSeoncds then it will be initialized implictly so initialization state is not needed.
            this.CoroutineSyncInit();

            Debug.Log("Example1");
            Debug.LogFormat("ThreadID : {0}", Thread.CurrentThread.ManagedThreadId);

            // It's not guaranteed that it will be completed on main thread because of 'ConfigureAwait(false)'
            await Task.Delay(1000).ConfigureAwait(false); 
            Debug.Log("Delay ConfigureAwait(false)");
            Debug.LogFormat("ThreadID : {0}", Thread.CurrentThread.ManagedThreadId);

            // This code below will throw an exception for 'WaitForSeconds'. It must be called on main thread.
            // await new WaitForSeconds(1f);

            // This 'new WaitForSeconds' will be executed on main thead.
            await this.CoroutineSync(() => new WaitForSeconds(1f));
            Debug.Log("AwaiterCoroutine.Sync WaitForSeconds");
            Debug.LogFormat("ThreadID : {0}", Thread.CurrentThread.ManagedThreadId);
        }

        private async void Example2()
        {
            // It's not necessary to initialize CoroutineSync at this moment.
            // Because these codes below use await coroutine on main thread(await new WaitForSeconds).
            // At that time CoroutineSync will be initialized implictly.
            // this.CoroutineSyncInit();

            Debug.Log("Example2");
            Debug.LogFormat("ThreadID : {0}", Thread.CurrentThread.ManagedThreadId);

            await new WaitForSeconds(1f);
            Debug.Log("WaitForSeconds");
            Debug.LogFormat("ThreadID : {0}", Thread.CurrentThread.ManagedThreadId);

            // It's not guaranteed that it will be completed on main thread because of 'ConfigureAwait(false)'
            await Task.Delay(1000).ConfigureAwait(false);
            Debug.Log("Delay ConfigureAwait(false)");
            Debug.LogFormat("ThreadID : {0}", Thread.CurrentThread.ManagedThreadId);

            // This code below will make an exception 'WaitForSeconds'. It must be called on main thread.
            // await new WaitForSeconds(1f);

            // This 'new WaitForSeconds' will be executed on main thead.
            await this.CoroutineSync(() => new WaitForSeconds(1f));
            Debug.Log("AwaiterCoroutine.Sync WaitForSeconds");
            Debug.LogFormat("ThreadID : {0}", Thread.CurrentThread.ManagedThreadId);
        }

        private void OnGUI()
        {
            if (GUI.Button(GetRect(1, 2), "Example1"))
                Example1();

            if (GUI.Button(GetRect(2, 2), "Example2"))
                Example2();
        }

        private Rect GetRect(int order, int totalOrder)
        {
            float width = Screen.width;
            float x = 0f;

            float height = Screen.height / totalOrder;
            float y = (order - 1) * height;

            return new Rect(x, y, width, height);
        }
    }
}