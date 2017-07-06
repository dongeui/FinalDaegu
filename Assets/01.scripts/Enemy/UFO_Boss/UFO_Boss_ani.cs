using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO_Boss_ani : MonoBehaviour
{
    public Animator animator;
    public static UFO_Boss_ani Instance;    

    private void Awake()
    {
        animator = GetComponent<Animator>();
        Instance = this;
    }

    public void Spawn()
    {
        animator.SetTrigger("Spawn");
    }

    public void Damaged()
    {
        animator.SetTrigger("Damaged");
    }
    
}