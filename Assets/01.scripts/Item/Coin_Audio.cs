using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_Audio : MonoBehaviour
{
    public static Coin_Audio Instance;

    public AudioSource coin;

    private void Awake()
    {
        Instance = this;
        coin = GetComponent<AudioSource>();
    }

    public void CoinSound()
    {
        coin.Play();
    }
}