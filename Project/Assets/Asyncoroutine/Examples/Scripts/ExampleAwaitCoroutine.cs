using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Asyncoroutine.Example
{
    public class ExampleAwaitCoroutine : MonoBehaviour
    {
        async void Awake()
        {
            await new WaitForSeconds(1f);
            Debug.Log("WaitForSeconds");

            await new WaitForNextFrame();
            Debug.Log("WaitForNextFrame");

            await Task.Delay(1000);
            Debug.Log("Delay");

            WWW www = await new WWW("https://api.ipify.org?format=json");
            Debug.Log(www.text);

            await new WaitForSecondsRealtime(1f);
            Debug.Log("WaitForSecondsRealtime");

            await UnityCoroutine();
            Debug.Log("UnityCoroutine");
        }

        private IEnumerator UnityCoroutine()
        {
            Debug.Log("UnityCoroutine1");

            yield return new WaitForSecondsRealtime(1f);

            Debug.Log("UnityCoroutine2");

            if (Random.value < 0.5f)
            {
                yield return new WaitForSeconds(0.5f);
                Debug.Log("UnityCoroutine3");
            }
            else
            {
                yield return new WaitForSeconds(1.0f);
                Debug.Log("UnityCoroutine4");
            }
        }

        async void OnDisable()
        {
            await UnityCoroutine();
            WWW www = await new WWW("http://google.com");
            Debug.Log(www.text);
        }
    }
}