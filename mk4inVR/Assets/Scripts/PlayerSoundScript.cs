using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤー中心で鳴る音の制御
public class PlayerSoundScript : MonoBehaviour
{
    public AudioClip fly_mp3; //飛行時のSE
    AudioSource audioSource;

    [SerializeField] PlayerScript player_sc;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //飛行音は常に鳴らす
        audioSource.PlayOneShot(fly_mp3);
        audioSource.loop = true;
    }

    void Update()
    {
        //ブレーキ時にミュート
        if (player_sc.brake_on)
        {
            audioSource.mute = true;
        }
        else
        {
            audioSource.mute = false;
        }
    }
}
