using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO_Start_spwan : MonoBehaviour {

    public bool IsPossible;
    public float time;    
    public GameObject Bonecrab;
    public GameObject[] weapon;
    private Animator animator;

    UFO_Boss_ani ani;
    public static UFO_Start_spwan Instance;

    private void Awake()
    {
        IsPossible = true;
        Instance = this;
        ani = GetComponentInChildren<UFO_Boss_ani>();
        animator = GetComponent<Animator>();
    }

    public void Start_spawn()
    {
        //게 알까기 시작!!
        Debug.Log("게 알까기 시작");
        StartCoroutine((Spawn(time)));
        UFO_Boss_control.Instance.IsImmortal = false;
    }

    IEnumerator Spawn(float time)
    {
        while (IsPossible)
        {            
            ani.Spawn();
            yield return new WaitForSeconds(time);
        }
    }


    //빵개 드롭 x축 자기 밑에 와이영제트영포지션에 빵개 투하
    public void Drop()
    {
        //Instantiate(Bonecrab, new Vector3(transform.position.x, 0, 0), Quaternion.identity);
        if (!IsPossible)
        { return; }

        int rnd = Random.Range(0, 10);


        //확률에 의해 무기 드랍
        if (rnd < 4)
        {
            int index = Random.Range(0, weapon.Length);
            Instantiate(weapon[index], gameObject.transform.GetChild(0).position + Vector3.down * 4.5f, Quaternion.Euler(0, 0, 0));
        }

        else
        {
            Instantiate(Bonecrab, gameObject.transform.GetChild(0).position, Quaternion.Euler(0, 0, 0));
        }
        
    }

    public void Boss_Die()
    {
        StopAllCoroutines();
        StartCoroutine(Goto_base());
    }

    IEnumerator Goto_base()
    {
        Transform child = gameObject.transform.GetChild(0).transform;
        Debug.Log("원래 위치로 돌아가야 한다");
        while (true)
        {
            child.localPosition = Vector3.Lerp(child.localPosition, Vector3.zero, 2f * Time.deltaTime);

            if (Vector3.Distance(child.localPosition, Vector3.zero) <0.1f)
            {
                child.localPosition = Vector3.zero;
            }
            animator.SetBool("IsAlive", false);
            yield return null;
        }
    }
}
