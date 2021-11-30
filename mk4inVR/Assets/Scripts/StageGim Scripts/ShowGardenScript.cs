using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//花畑に近づいたときの表示切り替え
public class ShowGardenScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Garden")
        {
            Renderer renderer = other.gameObject.GetComponent<Renderer>();
            renderer.enabled = true;
            //other.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Garden")
        {
            Renderer renderer = other.gameObject.GetComponent<Renderer>();
            renderer.enabled = false;
            //other.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
