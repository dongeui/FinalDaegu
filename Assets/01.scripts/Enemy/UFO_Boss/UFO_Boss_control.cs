using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO_Boss_control : MonoBehaviour
{
    public float BossLife = 100f;
    UFO_Start_spwan start_spwan;    
   
    public ParticleSystem HitEffect;
    public float speed;
    public static UFO_Boss_control Instance;
    public GameObject[] bossPos;
    string Track_boss;
    bool Changing;
    bool IsAlive;
    public bool IsImmortal;

    private void Awake()
    {
        Instance = this;
        IsAlive = true;
        Track_boss = "Track01";
        IsImmortal = true;
    }

    public void Start()
    {
        start_spwan = GetComponentInParent<UFO_Start_spwan>();
        StartCoroutine(Check_player());
    }

    //플레이어의 위치에 따라 보스의 위치를 바꾼다.
    IEnumerator Check_player()
    {
        while (true)
        {
            if (!Changing)
            {
                yield return null;
            }

            //플레이어가 달리는 트랙과 보스의 위치가 서로 다르다.
            if (Player_control.Instance.now_Track != Track_boss)
            {
                StartCoroutine(Change_track(Player_control.Instance.now_Track));
                
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator Change_track(string player_track)
    {
        
        Changing = true;
        while (Changing)
        {
            if (!IsAlive)
            { yield break; }

            switch (player_track)
            {
                case "Track01":
                    GoTo_track01();
                    break;

                case "Track02":
                    Goto_track02();
                    break;
            }


            yield return null;
        }        
    }

    public void GoTo_track01()
    {
        UFO_Start_spwan.Instance.IsPossible = false;
        UFO_Start_spwan.Instance.StopAllCoroutines();
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, bossPos[0].transform.position, speed * Time.deltaTime);
        Debug.Log("트랙01로 이동중");
        //충분히 가깝다.
        if (Vector3.Distance(gameObject.transform.position, bossPos[0].transform.position)<0.1f)
        {
            gameObject.transform.position = bossPos[0].transform.position;
            Track_boss = "Track01";
            UFO_Start_spwan.Instance.IsPossible = true;
            UFO_Start_spwan.Instance.Start_spawn();
            Changing = false;
        }
    }

    public void Goto_track02()
    {
        UFO_Start_spwan.Instance.IsPossible = false;
        UFO_Start_spwan.Instance.StopAllCoroutines();
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, bossPos[1].transform.position, speed * Time.deltaTime);
        Debug.Log("트랙02로 이동중");

        //충분히 가깝다.
        if (Vector3.Distance(gameObject.transform.position, bossPos[1].transform.position) < 0.1f)
        {
            gameObject.transform.position = bossPos[1].transform.position;
            Track_boss = "Track02";
            UFO_Start_spwan.Instance.IsPossible = true;
            UFO_Start_spwan.Instance.Start_spawn();
            Changing = false;
        }
    }


    //무기에 공격당했을 때 움직이는거는 애니가 없으니까 파티클로 대체
    private void OnTriggerEnter(Collider other)
    {
        //일시 무적 상태면 충돌 무시...
        if (IsImmortal)
            return;

        else if (other.gameObject.tag == "weapon")
        {
            //무기(collision)에서 무기데미지가져와서 BossLife에서 빼서 일정넘어가면 Destroy불러버림
            //이 자리에 hitparticle  재생되게 만들어야함

            //부딫힌 무기를 관리하는 클래스를 참조한다
            Weapon weapon = other.gameObject.GetComponent<Weapon>();

            if (!weapon.IsFlying)
            { return; }

            //무기의 공격력 만큼 보스의 체력을 깍는다.
            BossLife -= weapon.attack_point;

            //체력이 떨어 져서 사망...
            if (BossLife <= 0)
            {
                Destroy(gameObject.transform.parent.gameObject, 5f);
                IsAlive = false;
                UFO_Start_spwan.Instance.Boss_Die();
            }

            //죽지는 않고 데미지만 입었다. 임시 무적
            else
            {
                Debug.Log("보스가 아파합니다.");
                UFO_Boss_ani.Instance.Damaged();
            }

            //보스와 부딫힌 무기는 사망...
            weapon.coll.enabled = false;
        }
    }

    

    public void Start_drop()
    {
        start_spwan.Drop();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}