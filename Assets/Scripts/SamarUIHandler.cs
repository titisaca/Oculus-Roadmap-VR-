using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamarUIHandler : MonoBehaviour
{
    public GameObject scrollableContent;
    public AudioSource infoClip;


    public void ToggleObject()
    {
        scrollableContent.SetActive(!scrollableContent.activeSelf);
    }
   
   public void ToggleAudio()
   {
        if (infoClip.isPlaying)
            infoClip.Stop();
        else
            infoClip.Play();

   }
}
