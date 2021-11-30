using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//**VRカメラのON/OFF切り替え**//
public class CameraModeSwScript : MonoBehaviour
{
    ManagerScript manager;
    [SerializeField] GameObject VR_camera = null;
    [SerializeField] GameObject main_camera = null;

    // Start is called before the first frame update
    void Start()
    {
        manager = gameObject.GetComponent<ManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.VR_mode)
        {
            VR_camera.SetActive(true);
            main_camera.SetActive(false);
        }
        else
        {
            VR_camera.SetActive(false);
            main_camera.SetActive(true);
        }
    }
}
