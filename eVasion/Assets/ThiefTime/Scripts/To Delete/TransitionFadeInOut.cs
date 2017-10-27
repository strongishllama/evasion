////-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
////  Author:         Taliesin Millhouse & Emma Cameron
////  Date Created:   28th October 2016
////  Brief:          Scene Transition: Fade In/Out
////-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

//using UnityEngine;
//using System.Collections;

////-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
////-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

//public class TransitionFadeInOut : MonoBehaviour
//{
//    // TRANSITION - FADE In/Out
//    // On Guard detect: Game Fades Out, Scene Reloads
//    // On Level Finish: Game Fades Out, New Scene Loads

//    // VARIABLES
//    // Image overlay that the screen will fade to: Black image or a loading graphic.
//    public Texture2D fadeOutTexture;

//    // Speed fade IN/OUT
//    private float fadeSpeed = 0.8f;
//    // Hierarchy Draw last, render on top of all other images
//    private int drawDepth = -1000;
//    // Alpha Texture default: Visible to nothing.
//    private float alpha = 1.0f;
//    // Direction to fade. =1 Fade In = 1 = Fade Out
//    private int fadeDirection = -1;

////-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
////-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

//    // FUNCTION
//    void OnGUI()
//    {
//        // Render graphics
//        // Fade In/OUT Direction, Speed, Delta Time(convert to seconds)
//        alpha += fadeDirection * fadeSpeed * Time.deltaTime;
//        // force (clamp) value to be between 0 and 1. alpha value
//        alpha = Mathf.Clamp01(alpha);

//        // Set Colour GUI texture/black image. Stay colour value the same, just change the alpha
//        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
//        // Draw texture on top
//        GUI.depth = drawDepth;
//        // Overlay entire texture across whole screen
//        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);
//    }

////-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
////-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

//    // Set fade IN/OUT
//    public float BeginFade (int direction)
//    {
//        fadeDirection = direction;
//        // fadeSpeed changes the timing of the SceneManager.LoadLevel()
//        return (fadeSpeed);
//    }

////-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
////-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

//    // Onlevelwasloaded called when level is loaded. to automate fading on each level.
//    void OnLevelWasLoaded ()
//    {
//        // Alpha -1 = fade IN
//        BeginFade(-1);
//    }

////-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
////-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//}
