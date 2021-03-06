﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBall : MonoBehaviour
{
    private GameObject hitEffect;
    public int attackPoint;
    public float speed;
    public bool IsAlive;
    public float lifetime;

    private void Awake()
    {
        IsAlive = true;
        StartCoroutine(Move());
    }

    // Use this for initialization
    private void Start()
    {
        hitEffect = gameObject.transform.FindChild("WaterBall_hit").gameObject;
        StartCoroutine(Kill_self(lifetime));
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private IEnumerator Move()
    {
        while (IsAlive)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);            
            yield return null;
        }
    }

    //무언가와 부딪혔다.
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(Kill_self());            

            //플레이어 피격사운드
            Player_Audio.Instance.HitSound();           

            Player_damaged.Instance.Damaged(attackPoint);
        }
    }

    IEnumerator Kill_self(float time = 0)
    {
        yield return new WaitForSeconds(time);
        hitEffect.SetActive(true);
        Destroy(hitEffect, 2f);
        gameObject.SetActive(false);
        Destroy(gameObject.transform.GetChild(0).gameObject);
        transform.DetachChildren();
        Destroy(gameObject);
    }
}