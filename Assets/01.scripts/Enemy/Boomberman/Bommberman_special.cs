using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bommberman_special : MonoBehaviour {

    public Transform player_checker;
    public Enemy_attack info;
    public Effect_control effect_control;

    private void Awake()
    {
        player_checker = gameObject.transform.parent.Find("playerchecker");
        info = GetComponent<Enemy_attack>();
        effect_control = GetComponent<Effect_control>();
    }

    public void Explosion()
    {
        Debug.Log("폭발한다!!!");
        LayerMask layer = LayerMask.GetMask("player");
        effect_control.Set_transform(1, gameObject.transform.position);
        effect_control.Show_effect(1);
        effect_control.effects[1].transform.DetachChildren();

        //플레이어가 범위내에 있으면 폭발해서 공격한다.
        Collider[] somthings = Physics.OverlapSphere(player_checker.position, 10f, layer);

        if (somthings.Length > 0)
        {
            somthings[0].GetComponent<Player_damaged>().Damaged(info.attack_Point_Special);
        }
    }
}
