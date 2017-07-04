using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_ani : MonoBehaviour {

    Animator anim;

    public void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Rot_anim()
    {
        anim.SetTrigger("rot");
    }

    public void Wave_anim(bool play)
    {
        anim.SetBool("Holding", play);
    }
}
