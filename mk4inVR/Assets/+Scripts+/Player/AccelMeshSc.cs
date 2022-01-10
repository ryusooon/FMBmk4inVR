using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelMeshSc : MonoBehaviour
{
    [SerializeField] ManagerScript manager = null;
    Renderer renderer_;

    void Start()
    {
        renderer_ = this.gameObject.GetComponent<Renderer>();
    }

    void Update()
    {
        if (manager.accel_on)
        {
            renderer_.enabled = true;
        }
        else
        {
            renderer_.enabled = false;
        }
    }
}
