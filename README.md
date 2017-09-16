# Asyncoroutine

Asyncoroutine is a unity asset that allows you to use Coroutine and async/await together.
You might face the situation that Coroutine and async/await get together. In this situation 'Asyncoroutine' is useful.
You don't need to switch your code style from Coroutine to async/await or from async/await to Coroutine by using 'Asyncoroutine'.
See below.

## How to use
### Coroutine in async/await
You can 'await' Coroutine in async/await.
```C#
using Asyncoroutine;

async void Awake()
{
    await new WaitForSeconds(1f);
    Debug.Log("WaitForSeconds");

    await Task.Delay(1000);
    Debug.Log("Delay");

    WWW www = await new WWW("http://google.com");
    Debug.Log(www.text);

    await new WaitForSecondsRealtime(1f);
    Debug.Log("WaitForSecondsRealtime");

    await UnityCoroutine();
    Debug.Log("UnityCoroutine");
}
```
As you can see, you just write 'await' at the front of Coroutine or YieldInstrunction like WaitForSeconds.
All the things will happen by 'using Asyncoroutine'.

Also it makes 'Awake' and 'OnEnable' use Coroutine. Unlike 'Start' we could not use Coroutine in them but you can from now.
(Actually there is an another alternative in above situation. See [link](https://github.com/zsaladin/AsCoroutine))

Moreover you can use Coroutine on 'OnDisable' by using it.

### Task in Coroutine
If you don't familiar with async/await then use original Coroutine style with async/await
```C#
using Asyncoroutine;

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
```
Just write 'AsCoroutine()' at the end of Task. It creates a proper YieldInstrunction which Coroutine can handle.

#### Note : You must have Unity 2017 or above and be sure that Scripting Runtime Version is '.Net 4.6'.

## Author
- Kim Daehee, Software engineer in Korea.
- zsaladinz@gmail.com

## License
- This asset is under MIT License.

