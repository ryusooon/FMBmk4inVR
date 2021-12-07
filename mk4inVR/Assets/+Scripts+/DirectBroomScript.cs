using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectBroomScript : MonoBehaviour
{
    [SerializeField] GameObject ControllerR = null;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = ControllerR.transform.rotation;
    }
}
