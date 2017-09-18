using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Asyncoroutine.Example
{
    public class ExampleAwaitCoroutineSync : MonoBehaviour
    {
        private async void Example1()
        {
            // CoroutineSync must be initialized on main thread before awaiting YieldInstruction.
            // It's necessary just once in runtime.
            // If you use await coroutine on main thread like await WaitForSeoncds then it will be initialized implictly so initialization state is not needed.
            Asyncoroutine.Ready();

            Debug.Log("Example1");
            Debug.LogFormat("ThreadID : {0}", Thread.CurrentThread.ManagedThreadId);

            // It's not guaranteed that it will be completed on main thread because of 'ConfigureAwait(false)'
            await Task.Delay(1000).ConfigureAwait(false); 
            Debug.Log("Delay ConfigureAwait(false)");
            Debug.LogFormat("ThreadID : {0}", Thread.CurrentThread.ManagedThreadId);

            await new WaitForMainThread();
            WWW www = await new WWW("https://api.github.com/users/zsaladin/repos");
            Debug.LogFormat("ThreadID : {0}", Thread.CurrentThread.ManagedThreadId);
        }

        private async void Example2()
        {
            // It's not necessary to initialize CoroutineSync at this moment.
            // Because these codes below use await coroutine on main thread(await new WaitForSeconds).
            // At that time Asyncoroutine will be initialized implictly.
            //Asyncoroutine.Ready();

            Debug.Log("Example2");
            Debug.LogFormat("ThreadID : {0}", Thread.CurrentThread.ManagedThreadId);

            await new WaitForSeconds(1f);
            Debug.Log("WaitForSeconds");
            Debug.LogFormat("ThreadID : {0}", Thread.CurrentThread.ManagedThreadId);

            // It's not guaranteed that it will be completed on main thread because of 'ConfigureAwait(false)'
            await Task.Delay(1000).ConfigureAwait(false);
            Debug.Log("Delay ConfigureAwait(false)");
            Debug.LogFormat("ThreadID : {0}", Thread.CurrentThread.ManagedThreadId);

            // This code below will make an exception 'WWW'. It must be called on main thread.
            // await new WWW(""https://api.github.com/users/zsaladin/repos");

            //await new WaitForMainThread();
            //WWW www = await new WWW("https://api.github.com/users/zsaladin/repos");

            // It's same as above.
            WWW www = await new WaitForMainThread().Awaiter(new WWW("https://api.github.com/users/zsaladin/repos"));

            Debug.LogFormat("Asyncoroutine WWW {0}", www.text);
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