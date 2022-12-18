using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using System;
public class TourObject : MonoBehaviour
{
    //particle system for the tour object
    [SerializeField] ParticleSystem explode = null;
    //item labels + descriptions  and their corrosponding Text Mesh UI
    [SerializeField] private string itemNameTxt;
    [SerializeField] private TextMeshPro itemNameTM = null;
    [SerializeField] private string itemDescriptionTxt; //item description
    [SerializeField] private string itemLinkTxt; //Url for the item
    [SerializeField] private TextMeshPro itemDescriptionTM = null;
    //Audio clip for the tour object
    [SerializeField] public AudioSource audioSource;
    [SerializeField] public AudioClip infoClip;
    private float audioDuration;
    private int audioSeconds;
    //Tour Icon Parent Object (Must have a mesh collider)
    [SerializeField] private GameObject tourIcon = null;
    [SerializeField] private MeshRenderer meshRenderer;
    private CapsuleCollider meshCollider;
    //Material used for albedo color fading
    private Material material;
   
  

    private void start() {
        if (tourIcon != null){
            meshRenderer = tourIcon.GetComponent<MeshRenderer>();    
        }
        else
        {
            meshRenderer = GetComponent<MeshRenderer>();    

        }
            audioDuration = infoClip.length;
            audioSeconds = (int)Math.Ceiling(audioDuration);
            Debug.Log("Cip Name: " + itemNameTxt + " Clip Length " + audioSeconds);
            meshCollider = tourIcon.GetComponent<CapsuleCollider>();
            material = tourIcon.GetComponent<Renderer>().material;
            material = meshRenderer.material;
            itemNameTM.text = itemNameTxt;

    }

    // Start is called before the first frame update
    void OnTriggerEnter(Collider other) {
        
//        meshCollider.enabled = false;
        if(other.CompareTag("Player")) {
            itemNameTM.text = "";
            explode.Play(); 
                   
            //samar play the information audio
            audioSource.Stop();

            audioSource.PlayOneShot(infoClip);
            StartCoroutine(clearText());
            // pointsText.text = map.Any() ? map[tourItem] : "No data";
            // itemDescriptionTM.text = map.Any() ? map[itemNameTxt] : "No data";
             if (itemDescriptionTM != null){
                itemDescriptionTM.text = itemDescriptionTxt;
            }   
            StartCoroutine(iconFade());
           
        }
    }


   private IEnumerator iconFade() {
        
        for(int i = 0; i < 10; i++) {
            Color color = material.color;
            color.a = (10f-(float)i )/10f;
            // Debug.Log(color.a.ToString());
            material.color = color;
            yield return new WaitForSeconds(.1f);
        }

    }

    private IEnumerator clearText() {
        yield return new WaitForSeconds(audioDuration);
        itemDescriptionTM.text = "";
        Destroy(tourIcon, 1.5f);
        
        
    }
}
