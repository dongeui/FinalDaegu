using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_attack : MonoBehaviour {

    public static Player_attack Instance;

    string state_weapon = string.Empty;
    bool Ispulling = false;
    public Transform[] weapon_checker;
    public Transform weapon_holder;
    GameObject weapon_now;
    Effect_control effect_control;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        weapon_now = new GameObject();
        effect_control = GetComponent<Effect_control>();
    }
    
    //무기를 들고 있는지 확인해서 다음 행동을 실행한다.
    public void check_arm ()
    {
        //죽었다면 아무것도 하지 말아야지
        if (!Player_control.Instance.IsAlive)
        { return; }

        //자석으로 이미 무기를 갖고 있다. 남은 일은 무기를 던지는 것이다.
        if (state_weapon != string.Empty)
        {
            //물론 트랙을 이동중이라면 공격할 수 없지
            if (Player_control.Instance.Isjumping)
                return;

            //공격한다!!!
            Debug.Log("공격한다!");

            Player_ani.Instance.Attack_anim();
            state_weapon = string.Empty;
            StartCoroutine(move(weapon_now, Vector3.right, 30f));
            weapon_holder.DetachChildren();
            effect_control.Hide_effect(1);
            effect_control.effects[1].transform.SetParent(weapon_holder);
        }

        //무기를 갖고 있지 않음.
        else
        {
            //전방에 범위내에 무기가 있는지 확인하고, 가장 가까운 무기를 가져온다.
            LayerMask layer = LayerMask.GetMask("weapon");
            Collider[] weapons = Physics.OverlapCapsule(weapon_checker[0].position, weapon_checker[1].position, 2.5f, layer);

            //전방에 무기가 없으므로 더이상 진행하지 않아도 된다.
            if (weapons.Length < 1)
            {
                return;
            }

            //전방에 무기가 최소 1개 이상은 있다. 1개 이상이라면 이중에서 가장 가까운 무기를 선택해야 한다.
            int index_base = 0;

            //무기가 2개 이상이다. 그럼 가장 가까운 것을 구한다.
            if (weapons.Length > 0)
            {
                float length_pre = Vector3.Distance(weapons[0].transform.position, gameObject.transform.position);

                for (int i = 0; i < weapons.Length; i++)
                {
                    float length_now = Vector3.Distance(weapons[i].transform.position, gameObject.transform.position);

                    //그런데 이미 날아가고 있는 무기다. 그렇다면 이 무기는 무시해야 겠지.
                    bool Isflying = weapons[i].gameObject.GetComponentInChildren<Weapon>().IsFlying;
                    if (Isflying)
                    {
                        return;
                    }

                    else if (length_pre > length_now)
                    {
                        length_pre = length_now;
                        index_base = i;
                    }
                }
            }
            weapon_now = weapons[index_base].transform.gameObject;
            Debug.Log("전방 무기 확인 : " + weapon_now.name);

            //전방에 무기가 있는 것을 확인했다. 그럼 이것을 끌어와야지.
            //이미 무기를 자석으로 당기는 중인가? 그렇다면 하던 일만 계속 한다.
            if (!Ispulling)
            {                
                Ispulling = true;
                StartCoroutine(pull_weapon());
            }
        }
    }

        
	
	// Update is called once per frame
	void Update () {
		
	}

    void Set_arm(string name)
    {
        state_weapon = name;
    }


    //자석으로 무기를 당긴다.
    IEnumerator pull_weapon()
    {
        bool roop = true;

        //무기정보가 있는 클래스를 참조한다.
        Weapon weapon = weapon_now.GetComponent<Weapon>();
        weapon.Holding();
        effect_control.Show_effect(1);

        while (roop)
        {
            //계속 Lerp를 하면서 자석에 가까이 가져간다.
            //이때 충분히 가까워 지면 Lerp는 그만하고 바로 자석에 붙인다.
            weapon_now.transform.parent.position = Vector3.Lerp(weapon_now.transform.parent.position, weapon_holder.position, 10f * Time.deltaTime);

            if (Vector3.Distance(weapon_now.transform.parent.position, weapon_holder.position) < 0.2f)
            {
                weapon_now.transform.parent.position = weapon_holder.position;
                weapon_now.transform.parent.SetParent(weapon_holder);
                Set_arm(weapon_now.name);
                Ispulling = false;
                
                Debug.Log("현재무장 : " + weapon_now.name);
                break;
            }
            yield return null;
        }
    }



    // 자석으로 붙인 무기를 전방으로 날린다!
    // 무기/방향/스피드
    IEnumerator move(GameObject target, Vector3 dir, float speed)
    {
        bool loop = true;
        float time = 0f;

        Weapon weapon = target.GetComponent<Weapon>();
        weapon.Throwing();

        while (loop)
        {
            time += Time.deltaTime;
            target.transform.parent.Translate(dir * speed * Time.deltaTime);
            yield return null;

            if (time > 1.5f)
            {
                loop = false;
            }            
        }
        weapon.Suicide();
    }
}
