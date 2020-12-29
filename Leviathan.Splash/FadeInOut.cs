using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * gets image and then smoothly interpolates the Alpha 
 * Value from 0 to 255 then sitson screen for TimeOnScreen amount of seconds
 * before interpolatign the alpha value back to 0
 */
[RequireComponent(typeof(Image))]
public class FadeInOut : MonoBehaviour
{

   [Header("Settings")]
   [Range(.001f,.1f)]
   [Tooltip("amount of iterations per-second that image will fade into screen" +
            "the smaller eqauls faster fade in effect")]
   public float fadeIn = .02f;
   [Range(.001f, .1f)]
   [Tooltip("amount of iterations per-second when fading out")]
   public float fadeOut = .02f;
   [Range(.001f, 1f)]
   [Tooltip("the amount at which the image interpolates between values." +
            "the larger the number the more choppy the fade is")]
   public float Interpolation = .02f;
   [Tooltip("the amount of seconds the image speds on screen" +
             " before begining to fade away")]
   [Min(1)]
   public float TimeOnScreen = 5;

   //mutable values
   Image targetImage;
   Color Color;
   float alphaValue;     
  

    public bool finishedEffect { get; set; }

    //set refrences    
    void Start()
    {        
        targetImage = GetComponent<Image>();       
        Color = targetImage.color;
        Color.a = 0;
        targetImage.color = Color;          
    }

    public IEnumerator DisplayOnScreen()
    {
        finishedEffect = false;
        
        StartCoroutine(FadeIn());
        while (Color.a != 255)
            yield return new WaitForEndOfFrame();

        yield return new WaitForSeconds(TimeOnScreen);
        StartCoroutine(FadeOut());        
    }


    IEnumerator FadeIn()
    {
        Color.a = 0;
        alphaValue = 0;
        while (alphaValue <= 1)
        {            
            Color.a = alphaValue;
            yield return new WaitForSeconds(fadeIn);
            targetImage.color = Color;
            alphaValue += .01f;            
        }
        Color.a = 255;
        targetImage.color = Color;
    }

    IEnumerator FadeOut()
    {
        Color.a = 255;
        alphaValue = 1;
        while (alphaValue >= 0)
        {
            Color.a = alphaValue;
            yield return new WaitForSeconds(fadeOut);            
            targetImage.color = Color;
            alphaValue -= .01f;
        }
        Color.a = 0;
        targetImage.color = Color;
        finishedEffect = true;
    }
}
