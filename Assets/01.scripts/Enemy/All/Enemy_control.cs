using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_control : MonoBehaviour
{
    public int health;
    public float speed;
    public bool hit;
    public bool IsAlive;
    public Enemy_ani ani;
    public Rigidbody rigidbody;
    public Collider[] colls;
    public Enemy_attack enemy_attack;
    public Effect_control effect_control;

    private void Awake()
    {
        Init_monster();
    }

    //몬스터 초기화하기
    public void Init_monster()
    {
        hit = false;
        IsAlive = true;
        ani = GetComponent<Enemy_ani>();
        rigidbody = GetComponent<Rigidbody>();
        colls = GetComponents<Collider>();
        enemy_attack = GetComponent<Enemy_attack>();
        effect_control = GetComponent<Effect_control>();
    }

    private void Start()
    {
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        Debug.Log("움직임 : " + gameObject.name);

        while (IsAlive)
        {
            transform.parent.Translate(Vector3.left * speed * Time.deltaTime);
            yield return null;
        }
        Debug.Log("멈췄다 : " + gameObject.name);
    }

    private IEnumerator FlyAway()
    {
        Debug.Log("맞아서 날아간다!!!");
        Vector3 vec = new Vector3(Random.Range(5, 20), Random.Range(1, 20), 0);

        while (!IsAlive)
        {
            transform.parent.Translate(vec * Time.deltaTime);
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Check_identity(other.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Check_identity(collision.gameObject);
    }

    private void Check_identity(GameObject obj)
    {
        switch (obj.gameObject.tag)
        {
            case "Player":
                Enemy_hit_player(obj.gameObject);
                break;

            case "weapon":
                Enemy_damage_check(obj.gameObject);
                break;

            default:
                break;
        }
    }

    public void Destroy_enemy(float time = 3)
    {
        Destroy(gameObject.transform.parent.gameObject);
    }

    //적이 플레이어와 부딪혔다.
    public void Enemy_hit_player(GameObject player)
    {
        //죽음소리 재생
        GetComponent<Enemy_Audio>().EnemySound();
        IsAlive = false;
        for (int i = 0; i < colls.Length; i++)
        {
            colls[i].isTrigger = true;
        }
        rigidbody.isKinematic = true;
        ani.PlayerHit_anim();
        player.GetComponent<Player_damaged>().Damaged();
        Debug.Log("플레이어와 부딪힘");
    }

    //적이 무기와 부딪혔다.
    public void Enemy_damage_check(GameObject obj)
    {
        //죽음소리 재생
        GetComponent<Enemy_Audio>().EnemySound();
        //부딪힌 무기를 관리하는 클래스를 참조한다
        Weapon weapon = obj.GetComponentInChildren<Weapon>();
        Debug.Log("무기와 부딪힘");
        if (!weapon.IsFlying)
        { return; }

        health -= weapon.attack_point;

        effect_control.Set_transform(0, gameObject.transform.position);
        effect_control.Show_effect(0);
        effect_control.Hide_effect_delay(0, 1f);

        if (health <= 0)
        {
            for (int i = 0; i < colls.Length; i++)
            {
                colls[i].isTrigger = true;
            }
            IsAlive = false;
            rigidbody.isKinematic = true;
            ani.Enemy_die_anim();
            StartCoroutine(FlyAway());
            UI_control.Instance.ScoreUpdate(100);
            Destroy(gameObject.transform.parent.gameObject, 3f);
        }

        //죽지는 않고 데미지만 입었다.
        else
        {
            ani.Enemy_hit_anim();
        }
    }
}