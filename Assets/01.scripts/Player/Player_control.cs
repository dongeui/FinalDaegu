using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player_control : MonoBehaviour {


    static public Player_control Instance;

    GameObject path;
    Path_control path_control;

    public string now_Track;
    public string pre_Track;
    public bool Isjumping = false;
    public bool IsAlive = true;
    public bool IsImmortal = false;
    
    

    private void Awake()
    {        
        Init_Player();
        Instance = this;        
    }


    private void Start()
    {
        path = GameObject.FindWithTag("Path");
        path_control = path.GetComponent<Path_control>();
    }
   

    //변수 초기화
    void Init_Player()
    {
        now_Track = "Track01";
        pre_Track = "Track01";
        Isjumping = false;
        Isjumping = false;
        IsAlive = true;
        IsImmortal = false;
        //혹시모를 애니메이션 변수 초기화는 애니메이션 컨트롤러에서 실행하고 있다.
    }

    public void Attack()
    {
        Player_attack.Instance.check_arm();
    }

    public void Jump()
    {
        //현재 뛰고 있는 중인가?
        if (Isjumping)
            return;

        //살아는 있나?
        if (!IsAlive)
            return;

        switch (now_Track)
        {
            case "Track01": //현재 '트랙 1번'에 있는가?
            case "Track02": //현재 '트랙 2번'에 있는가?
                path_control.Set_path(now_Track);   //현재 트랙에 맞는 플레이어의 이동경로를 설정한다.
                break;

            default: //현재 트랙위에 있지 않으므로 아무작업도 하지 않고 탈출
                return;
        }

        Debug.Log("점프할거야!");

        //너무 빠른 동작으로 이전 점프에서 제위치로 돌아오지 않았을 경우를 대비해
        //플레이어가 앞을 바라보게 한다.
        gameObject.transform.rotation = Quaternion.Euler(0f, 90f, 0f);

        //DOTween을 이용하여 경로를 따라 플레이어를 이동시킨다
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(gameObject.transform.DOPath(path_control.path_vec, 0.9f));

        //점프를 하고 있으니, 이에 맞는 애니메이션을 실행
        Player_ani.Instance.Jump_anim();
        Isjumping = true;
        
    }

    //플레이어가 점프를 시작해서 회전하기 시작
    public void Turn_start()
    {
        //트랙 1에서 트랙 2로 넘어가는 중이다.
        if (pre_Track == "Track01")
            Turn_left();

        //트랙 2에서 트랙 1로 넘어가는 중이다.
        else if(pre_Track == "Track02")
            Turn_right();
    }

    //플레이어가 점프후 회전을 거의 끝냈다.
    public void Turn_end()
    {
        //트랙 1에서 트랙 2로 넘어가는 중이다.
        if (pre_Track == "Track01")
            Turn_right();

        //트랙 2에서 트랙 1로 넘어가는 중이다.
        else if (pre_Track == "Track02")
            Turn_left();
    }
    
    void Turn_left()
    {
        Debug.Log("왼쪽 회전");
        Sequence mySequence = DOTween.Sequence();
        Vector3 rot = (gameObject.transform.localRotation.eulerAngles + new Vector3(0f, -90f, 0));
        mySequence.Join(gameObject.transform.DORotate(rot, 0.15f));
    }

    void Turn_right()
    {
        Debug.Log("오른쪽 회전");
        Sequence mySequence = DOTween.Sequence();
        Vector3 rot = (gameObject.transform.localRotation.eulerAngles + new Vector3(0f, 90f, 0));
        mySequence.Join(gameObject.transform.DORotate(rot, 0.15f));
    }


    private void OnTriggerEnter(Collider other)
    {
        //현재 달리고 있는 트랙을 파악.
        LayerMask layer = LayerMask.NameToLayer("player_path");
        if (other.gameObject.layer == layer)
        {
            if (other.gameObject.tag == "Track01")
            {
                now_Track = "Track01";
            }

            else if (other.gameObject.tag == "Track02")
            {
                now_Track = "Track02";
            }

            Isjumping = false;
            Player_ani.Instance.animator.SetBool("IsJumping", false);
            Debug.Log("현재 달리고 있는 트랙 : " + now_Track);

            if(now_Track!= pre_Track)
            {
                Turn_end();
            }
        }        
    }

    private void OnTriggerExit(Collider other)
    {
        //현재 탈출한 트랙을 파악.
        LayerMask layer = LayerMask.NameToLayer("player_path");
        if (other.gameObject.layer == layer)
        {
            if (other.gameObject.tag == "Track01")
            {
                pre_Track = "Track01";
                now_Track = string.Empty;
            }

            else if (other.gameObject.tag == "Track02")
            {
                pre_Track = "Track02";
                now_Track = string.Empty;
            }
            
            Debug.Log("현재 탈출한 트랙 : " + other.gameObject.tag);


            if (now_Track != pre_Track)
            {
                Turn_start();
            }
        }

    }

    public void PlayerIsDead()
    {
        IsAlive = false;
        GetComponent<Collider>().isTrigger = true;
        GetComponent<Rigidbody>().isKinematic = true;
        Player_ani.Instance.Die_anim();
    }

}
