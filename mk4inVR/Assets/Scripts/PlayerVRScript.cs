/*
【概要】
VIVEコントローラで前後左右の移動と回転をするスクリプト

左手：タッチパッドの入力で『上下左右』を判定し↑←↓→の移動をする
右手：タッチパッドの入力で『左右』を判定し右回転と左回転をする
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerVRScript : MonoBehaviour
{
    // コントローラー操作関連の変数
    public SteamVR_Input_Sources lefthandType;
    public SteamVR_Input_Sources righthandType;
    public SteamVR_Action_Boolean teleport;
    public SteamVR_Action_Vector2 direction;
    public Transform _VRCamera;
    public float _Rotspeed = 5f;

    [SerializeField] Transform E;
    private Quaternion offsetRotation;
    // トリガー入力に使用する変数
    private SteamVR_Action_Boolean Trigger = SteamVR_Actions._default.InteractUI;

    public static Rigidbody CameraRb { set; get; }
    [SerializeField] Rigidbody PlayerRb;

    // プレイヤーのステータス関係の変数
    [SerializeField] float PlayerSpeed = 20.0f;
    public static int PlayerVectMag;
    public static float Axel;
    [SerializeField] float MoveSpeed = 15.0f;

    public float Dir_y = 0;
    [SerializeField] GameObject gameManagerOb;
    ManagerScript manager;

    // Tracker関連の変数
    [SerializeField] VelocityEstimator TrackerVe;
    float AbsoluVecX, AbsoluVecY, AbsoluVecZ;

    // コントローラー関連の変数
    [SerializeField] GameObject Controller;
    [SerializeField] VelocityEstimator ControllerVector;

    // 箒のオブジェクト
    [SerializeField] GameObject Broom;

    float Xvec, Yvec, Zvec;

    private void Start()
    {
        CameraRb = this.gameObject.transform.GetComponent<Rigidbody>();

        PlayerVectMag = 0;
        Axel = 0.0f;

        manager = gameManagerOb.gameObject.GetComponent<ManagerScript>();

        //Vector = GetComponent<VelocityEstimator>();

        offsetRotation = Quaternion.Inverse(E.transform.rotation) * transform.rotation;
    }

    void Update()
    {
        transform.rotation = E.transform.rotation * offsetRotation;

        // コントローラーの加速度を各変数に代入
        AbsoluVecX = ControllerVector.GetAngularVelocityEstimate().x;
        AbsoluVecY = ControllerVector.GetAngularVelocityEstimate().y;
        AbsoluVecZ = ControllerVector.GetAngularVelocityEstimate().z;

        // 加速度の値が負の場合、マイナスを掛けて絶対値にする
        if (AbsoluVecX < 0.0f)
        {
            AbsoluVecX *= -1;
        }
        if (AbsoluVecY < 0.0f)
        {
            AbsoluVecY *= -1;
        }
        if (AbsoluVecZ < 0.0f)
        {
            AbsoluVecZ *= -1;
        }

        Debug.Log(" AbsoluVecX = " + AbsoluVecX + " AbsoluVecY = " + AbsoluVecY + " AbsoluVecZ = " + AbsoluVecZ);

        if (AbsoluVecX >= 1.0f && AbsoluVecZ >= 1.0f)
        {
            Stop();
        }
        else
        {
            Move();
        }

    }

    void FixedUpdate()
    {
        //CameraRigを随時箒の方向に向けて移動、PlayerRigも同様に連動して移動

        CameraRb.AddForce(Broom.transform.forward * PlayerSpeed * Axel, ForceMode.Force);
        PlayerRb.AddForce(Broom.transform.forward * PlayerSpeed * Axel, ForceMode.Force);

        PlayerVectMag = Mathf.FloorToInt(CameraRb.velocity.magnitude);

    }

    void LateUpdate()
    {

    }

    private void Move()
    {
        Axel = MoveSpeed;
        CameraRb.drag = 1;
        Debug.Log("PadOn");
    }

    private void HalfMove()
    {
        Axel = MoveSpeed / 2;
        CameraRb.drag = 5;
        Debug.Log("HalfOn");
    }

    private void Stop()
    {
        Axel = 0f;
        CameraRb.drag = 2;
        Debug.Log("StopNow");
    }

    private void Brake()
    {
        Axel = 0.0f;
        Debug.Log("TriggerOn");
    }

    private void Rotate()
    {
        // HMD(=カメラ)位置を中心として左右に回転する
        // 左右の判定はCheckDirectionRight()で計算する
        this.transform.RotateAround(_VRCamera.position, Vector3.up, _Rotspeed * CheckDirectionLeft());
    }

    private bool CheckGrabLeft()
    {
        return teleport.GetState(lefthandType);
    }
    private bool CheckGrabRight()
    {
        return teleport.GetState(righthandType);
    }

    private Vector3 CheckDirectionRight()
    {
        // タッチパッドのタッチ位置をVector2で取得
        Vector2 dir = direction.GetAxis(righthandType);
        Dir_y = dir.y;

        //manager.Dir_y = Dir_y;

        if (Mathf.Abs(dir.y) >= Mathf.Abs(dir.x))
        {
            // Y方向の絶対値の方が大きければ、HMD(=カメラ)に対して前か後ろ方向を返す
            return Mathf.Sign(dir.y) * Vector3.RotateTowards(new Vector3(0f, 0f, 1f), _VRCamera.forward, 360f, 360f);
        }
        else
        {
            // X方向の絶対値の方が大きければ、HMD(=カメラ)に対して右か左方向を返す
            return Mathf.Sign(dir.x) * Vector3.RotateTowards(new Vector3(1f, 0f, 0f), _VRCamera.right, 360f, 360f);
        }
    }


    private float CheckDirectionLeft()
    {
        // タッチパッドのタッチ位置をVector2で取得
        Vector2 dir = direction.GetAxis(lefthandType);
        if (Mathf.Abs(dir.y) >= Mathf.Abs(dir.x))
        {
            // Y方向の絶対値の方が大きければ、回転量=0を返す
            return 0f;
        }
        else
        {
            // X方向の絶対値の方が大きければ、回転量= 1か -1を返す
            return Mathf.Sign(dir.x);
        }
    }

}