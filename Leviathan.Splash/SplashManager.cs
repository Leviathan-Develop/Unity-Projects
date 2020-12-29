using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * this class Handles all the settigns of multiple fadeInOut effects objects
 * and also controls the delay at the start and inbetween each fadeInOut effect 
 */
public class SplashManager : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("the fadeInOut effects will play from top to bottom.")]
    public List<FadeInOut> SplashScreens;
    [Tooltip("the amount of seconds the cahin of effects will wait" +
        "before srtating on laod")]
    public float IntroTime;
    [Tooltip("the time in seconds it will wait inbetween each efect.")]
    public float pauseBetween;
    public bool finishedSequence { get; set; }

    // Start is called before the first frame update
    private void Start()
    {
        finishedSequence = false;
        StartCoroutine(IntroWait());
    }

    public void DisplayInOrder(int index)
    {
        if (index > SplashScreens.Count - 1)
        {           
            finishedSequence = true;
        }
        else
        {           
            StartCoroutine(SplashScreens[index].DisplayOnScreen());            
            StartCoroutine(waitUntilFinished(SplashScreens[index], index));
        }
    }
    IEnumerator waitUntilFinished(FadeInOut Effect, int index)
    {       
        while (Effect.finishedEffect is false)
            yield return new WaitForSeconds(.1f);

        yield return new WaitForSeconds(pauseBetween);       
        DisplayInOrder(index + 1);          
    }
    IEnumerator IntroWait()
    {
        yield return new WaitForSeconds(IntroTime);
        DisplayInOrder(0);
    }
}

