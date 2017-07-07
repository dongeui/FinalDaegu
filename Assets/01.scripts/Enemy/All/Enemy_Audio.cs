using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Audio : MonoBehaviour
{
    public static Enemy_Audio Instance;
    public AudioClip[] EnemyAudio = new AudioClip[5];
    private AudioSource enemy;

    private void Awake()
    {
        Instance = this;
        enemy = GetComponent<AudioSource>();
    }

    public void EnemySound()
    {
        enemy.clip = EnemyAudio[0];
        enemy.Play();
    }

    public void BombSouond()
    {
        enemy.clip = EnemyAudio[1];
        enemy.Play();
    }

    public void GameOverSound()
    {
        enemy.clip = EnemyAudio[2];
        enemy.Play();
    }

    public void Bosshit()
    {
        enemy.clip = EnemyAudio[3];
        enemy.Play();
    }

    public void GameWinSound()
    {
        enemy.clip = EnemyAudio[4];
        enemy.Play();
    }
}