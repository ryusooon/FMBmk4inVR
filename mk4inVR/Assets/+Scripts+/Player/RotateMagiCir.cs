using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMagiCir : MonoBehaviour
{
    [SerializeField] private float spin_speed_A = 5.0f;
    private Quaternion rot;
    private Quaternion now_q;

    [SerializeField] ManagerScript manager = null;

    void Update()
    {
        if (!manager.brake_event)
        {
            rot = Quaternion.AngleAxis(spin_speed_A, Vector3.up/*(軸)*/);
            now_q = this.transform.rotation;

            this.transform.rotation = now_q * rot;
        }
    }
}
