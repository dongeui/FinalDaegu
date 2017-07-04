using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    
    public string name;
    public int attack_point;
    public float speed_Comming;
    public bool IsFlying;
    public bool IsComming;
    public Collider coll;
    public Rigidbody rigidbody;
    Weapon_ani weapon_ani;


    private void Awake()
    {
        Init_weapon();
    }

    public void Init_weapon()
    {
        IsFlying = false;
        IsComming = true;
        coll = GetComponent<Collider>();
        rigidbody = GetComponent<Rigidbody>();
    }


    private void Start()
    {
        StartCoroutine(Comming());
        weapon_ani = gameObject.GetComponent<Weapon_ani>();
    }

    public void Suicide()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }

    public void Fly_anim()
    {
        weapon_ani.Rot_anim();
    }

    public void Holding_anim(bool play)
    {
        weapon_ani.Wave_anim(play);
    }

    public void Holding()
    {
        IsFlying = false;
        coll.isTrigger = true;
        Fly_anim();
        IsComming = false;
        rigidbody.useGravity = false;
        rigidbody.isKinematic = true;
        Holding_anim(true);
    }

    public void Throwing()
    {
        Holding_anim(false);
        IsFlying = true;
        coll.isTrigger = true;
        rigidbody.isKinematic = false;
    }

    
    IEnumerator Comming()
    {
        while (IsComming)
        {
            gameObject.transform.parent.transform.Translate(Vector3.left* speed_Comming * Time.deltaTime);
            yield return null;
        }
    }
    
}
