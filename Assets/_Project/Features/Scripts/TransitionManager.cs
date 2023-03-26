using System.Collections;
using UnityEngine;
using System;
public class TransitionManager : MonoSingleton<TransitionManager>
{
    public CanvasGroup canvasGroup;
    public float transitionTime = 1f;
    Coroutine transitionRoutine;

    // Update is called once per frame
    void Update()
    {
        // testing
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            StartTransition(() => Debug.Log("hi"));
        }
    }

    public void StartTransition(Action callback = null)
    {
        StopTransition();

        transitionRoutine = StartCoroutine(C_startTransition(callback));
    }
    // adding delay so scene transition is not immediate
    IEnumerator C_startTransition(Action callback = null)
    {
        float timePassed = 0f;
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        
        while (canvasGroup.alpha < 1)
        {
            //Debug.Log(" ");
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, timePassed / transitionTime);
            
            yield return null; // wait until next frame
            timePassed += Time.deltaTime;
        }

        timePassed = 0f;
        callback?.Invoke();
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, timePassed / transitionTime);

            yield return null;
            timePassed += Time.deltaTime;

        }

        canvasGroup.alpha = 0;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

    }

    public void StopTransition()
    {
        if (transitionRoutine != null)
        {
            StopCoroutine(transitionRoutine);
        }


    }
}
