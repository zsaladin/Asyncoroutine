using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Asyncoroutine.Example
{
    public class Basic : MonoBehaviour
    {
        async void Start()
        {
            await new WaitForSeconds(1f);
            Debug.Log("WaitForSeconds");

            await Task.Delay(1000);
            Debug.Log("Delay");

            WWW www = await new WWW("http://google.com");
            Debug.Log(www.text);     
        }
    }
}