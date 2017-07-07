using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Audio : MonoBehaviour
{
    public static Player_Audio Instance;
    public AudioClip[] PlayerAudio = new AudioClip[3];
    public AudioSource player;

    //0번째가 플레이어 점프
    //1번째가 플레이어 피격
    //2번째가 플레이어 죽음
    private void Awake()
    {
        Instance = this;
        player = GetComponent<AudioSource>();
    }

    public void JumpSound()
    {
        player.clip = PlayerAudio[0];
        player.Play();
    }

    public void HitSound()
    {
        player.clip = PlayerAudio[1];
        player.Play();
    }

    public void DieSound()
    {
        player.clip = PlayerAudio[2];
        player.Play();
    }
}