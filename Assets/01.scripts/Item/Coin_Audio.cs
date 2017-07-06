using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_Audio : MonoBehaviour
{
    public static Coin_Audio Instance;
    public AudioClip CoinAudio = new AudioClip();

    private void Awake()
    {
        Instance = this;
    }

    public void CoinSound()
    {
        AudioSource coin = new AudioSource();
        coin.clip = CoinAudio;
        coin.Play();
    }
}