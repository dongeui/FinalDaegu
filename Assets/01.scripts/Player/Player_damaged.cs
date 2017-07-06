using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_damaged : MonoBehaviour
{
    private Transform[] player_pos;
    static public Player_damaged Instance;
    private Effect_control effect_control;

    private void Awake()
    {
        effect_control = GetComponent<Effect_control>();
        Instance = this;
    }

    // Use this for initialization
    private void Start()
    {
        player_pos = new Transform[2];
        effect_control.Set_transform(0, gameObject.transform.position);
        player_pos[0] = GameObject.FindGameObjectWithTag("Track01").transform;
        player_pos[1] = GameObject.FindGameObjectWithTag("Track02").transform;
    }

    public void Damaged(int damage = 10)
    {
        //이미 사망했으면 아무것도 할게 없지
        if (!Player_control.Instance.IsAlive)
        {
            return;
        }

        //적의 공격을 받고 일시적으로 무적 상태면 적의 공격을 잠시 무시한다.
        if (!Player_control.Instance.IsImmortal)
        {
            //데미지 만큼 플레이어의 체력을 깎는다.
            Player_health.Instance.HP_now -= damage;
            UI_control.Instance.HP_bar.value = Player_health.Instance.HP_now;

            //플레이어의 체력이 0보다 작다면 사망...
            if (Player_health.Instance.HP_now <= 0)
            {
                //플레이어 죽는사운드 재생
                Player_Audio.Instance.DieSound();
                Player_control.Instance.PlayerIsDead();
            }

            //데미지 애니메이션 재생
            Player_ani.Instance.Damage_anim();
            effect_control.Show_effect(0);
            // 피격 사운드 재생
            Player_Audio.Instance.HitSound();
            //약간 뒤로 밀리는 모션과 잠시 무적시간 부여
            StopCoroutine(Knockback());
            StartCoroutine(Knockback());
        }
    }

    //적의 공격으로 잠시 뒤로 밀려난다. 그리고 짧은 시간동안 무적상태가 된다.
    private IEnumerator Knockback()
    {
        Player_control.Instance.IsImmortal = true;
        Player_ani.Instance.Immortal_anim(true);

        //물리로 힘을 줘서 뒤로 밀려난다.
        gameObject.transform.GetComponent<Rigidbody>().AddForce(new Vector3(-300f, 0, 0));
        yield return new WaitForSeconds(0.5f);
        gameObject.transform.GetComponent<Rigidbody>().AddForce(new Vector3(200f, 0, 0));
        Debug.Log("전진 앞으로");

        //목표 위치로 조금씩 가까워 지다가 충분히 가까워 지면 바로 대입. 무적 상태도 풀린다.
        while (true)
        {
            //뒤로 밀려나고 다시 원위치에 돌아가야할 위치

            Vector3 target = new Vector3();

            if (Player_control.Instance.now_Track == "Track01")
            {
                target = player_pos[0].localPosition;
            }
            else if (Player_control.Instance.now_Track == "Track02")
            {
                target = player_pos[1].localPosition;
            }

            gameObject.transform.localPosition = Vector3.Lerp(gameObject.transform.localPosition, target, 3f * Time.deltaTime);
            if (Vector3.Distance(gameObject.transform.localPosition, target) < 0.05f)
            {
                gameObject.transform.localPosition = target;
                Player_control.Instance.IsImmortal = false;
                Player_ani.Instance.Immortal_anim(false);
                Debug.Log("뒤로 밀렸다가 제위치 확보");
                effect_control.Hide_effect(0);
                break;
            }
            yield return null;
        }
    }
}