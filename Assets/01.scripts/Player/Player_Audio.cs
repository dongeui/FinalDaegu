using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Audio : MonoBehaviour
{
    public static Player_Audio Instance;
    public AudioClip[] PlayerAudio = new AudioClip[3];

    //0번째가 플레이어 점프
    //1번째가 플레이어 피격
    //2번째가 플레이어 죽음
    private void Awake()
    {
        Instance = this;
    }

    public void JumpSound()
    {
        AudioSource jump = new AudioSource();
        jump.clip = PlayerAudio[0];
        jump.Play();
    }

    public void HitSound()
    {
        AudioSource hit = new AudioSource();
        hit.clip = PlayerAudio[1];
        hit.Play();
    }

    public void DieSound()
    {
        AudioSource die = new AudioSource();
        die.clip = PlayerAudio[2];
        die.Play();
    }
}