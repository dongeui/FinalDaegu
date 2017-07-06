using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Audio : MonoBehaviour
{
    public static Enemy_Audio Instance;
    public AudioClip[] EnemyAudio = new AudioClip[2];

    private void Awake()
    {
        Instance = this;
    }

    public void EnemySound()
    {
        AudioSource enemy = new AudioSource();
        enemy.clip = EnemyAudio[0];
        enemy.Play();
    }

    public void BombSouond()
    {
        AudioSource enemy = new AudioSource();
        enemy.clip = EnemyAudio[1];
        enemy.Play();
    }

    public void GameOverSound()
    {
        AudioSource enemy = new AudioSource();
        enemy.clip = EnemyAudio[2];
        enemy.Play();
    }
}