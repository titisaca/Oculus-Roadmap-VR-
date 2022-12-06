using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
public class TourObject : MonoBehaviour
{
    [SerializeField] ParticleSystem explode = null;
    [SerializeField] private string itemNameTxt;
    [SerializeField] private TextMeshPro itemNameTM = null;
    [SerializeField] private string itemDescriptionTxt;
    [SerializeField] private TextMeshPro itemDescriptionTM = null;

    [SerializeField] private GameObject parent;
    [SerializeField] private MeshRenderer meshRenderer;
    private Material material;

    private void Start() {
            if (parent != null)
	    {
            	meshRenderer = parent.GetComponent<MeshRenderer>();
	    }
            else
            {
  		meshRenderer = GetComponent<MeshRenderer>();
	    }
            material = meshRenderer.material;
            itemNameTM.text = itemNameTxt;
            Debug.Log("aaaaaaa" + itemNameTxt);

    }

    // Start is called before the first frame update
    void OnTriggerEnter(Collider other) {
	Debug.Log("aaaaaaa " + itemNameTxt + "   "+ other.transform.name);
        if(other.CompareTag("Player")) {
            itemNameTM.text = "";
            explode.Play();
            // pointsText.text = map.Any() ? map[tourItem] : "No data";
            // itemDescriptionTM.text = map.Any() ? map[itemNameTxt] : "No data";
            if (itemDescriptionTM != null)
	    {
	    	itemDescriptionTM.text = itemDescriptionTxt;
	    }
            StartCoroutine(objectExplodeDestroy());         
           
        }
    }


    IEnumerator objectExplodeDestroy() {
        
        for(int i = 0; i < 10; i++) {
            Color color = material.color;
            color.a = (10f-(float)i )/10f;

            Debug.Log(color.a.ToString());
            material.color = color;
            yield return new WaitForSeconds(.1f);
        }
        yield return new WaitForSeconds(3f);
        itemDescriptionTM.text = "";
        Destroy(parent, 1.5f);
    }
}
