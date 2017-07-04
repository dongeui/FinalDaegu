using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonecrab_special : MonoBehaviour {


    public float interval;
    public GameObject bubble;
    Enemy_attack info;
    Animator animator;

    private void Awake()
    {
        info = GetComponent<Enemy_attack>();
        animator = GetComponent<Animator>();
        StartCoroutine(Attack(interval));
    }

    IEnumerator Attack(float interval)
    {
        while (true)
        {
            //공격 애니메이션 실행
            animator.SetTrigger("IsAttack");

            //무한 반복 도중 interval만큼만 쉰다.
            yield return new WaitForSeconds(interval);
        }
    }

    //에니메이션 도중에 호출한다.
    public void Shotbubble()
    {
        Debug.Log("거품광선!!!!!");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
