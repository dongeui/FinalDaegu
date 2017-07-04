using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ani : MonoBehaviour {

    public static Player_ani Instance;

    public Animator animator;

    private void Awake()
    {        
        animator = GetComponent<Animator>();
        Init_anim();
        Instance = this;
    }

    void Init_anim()
    {
        animator.SetBool("IsJumping", false);
        animator.SetBool("IsAlive", true);
    }


    public void Win_anim()
    {
        animator.SetTrigger("Win");
    }

    public void Jump_anim()
    {
        animator.SetBool("IsJumping", true);
    }

    public void Damage_anim()
    {
        animator.SetTrigger("Damage");
    }

    public void Attack_anim()
    {
        animator.SetTrigger("Attack"); 
    }

    public void Die_anim()
    {
        animator.SetBool("IsAlive", false);
    }

    public void Immortal_anim(bool play)
    {
        animator.SetBool("Immortal_tmp", play);
    }
    
}
