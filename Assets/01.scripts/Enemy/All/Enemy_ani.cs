using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ani : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();        
    }
    
    public void PlayerHit_anim()
    {
        animator.SetTrigger("Player_hit");
    }

    public void Enemy_die_anim()
    {
        animator.SetBool("IsAlive", false);
    }

    public void Enemy_hit_anim()
    {
        animator.SetTrigger("IsDamaged");
    }

}