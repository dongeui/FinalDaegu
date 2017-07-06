using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class A10 : MonoBehaviour
{
    private Animator animator;
    public GameObject missiles;
    public Transform target;
    public GameObject effect;
    private GameObject obj;
    public float time;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger("TimeToKill");
    }

    private void Start()
    {
        create_bomb();
    }

    private void create_bomb()
    {
        obj = new GameObject();

        obj = Instantiate(missiles, new Vector3(40, 8, 15), Quaternion.identity);
        obj.transform.LookAt(target);
    }

    public void Bomb()
    {
        Debug.Log("폭격시작!!!");
        //DOTween을 이용하여 경로를 따라 미사일을 이동시킨다
        Sequence mySequence = DOTween.Sequence();

        mySequence.Append(obj.transform.DOLocalMove(target.position, time));
        StartCoroutine(Effect_start(time));
        //폭파소리재생
        Enemy_Audio.Instance.BombSouond();
        //게임오버소리재생
        Enemy_Audio.Instance.GameOverSound();
    }

    private IEnumerator Effect_start(float time)
    {
        yield return new WaitForSeconds(time);

        for (int i = 0; i < target.transform.childCount; i++)
        {
            target.transform.GetChild(i).gameObject.SetActive(true);
        }

        Destroy(obj);

        //그리고 플레이어 사망
        Player_control.Instance.PlayerIsDead();
    }
}