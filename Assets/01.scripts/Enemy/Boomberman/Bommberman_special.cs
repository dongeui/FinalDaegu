using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bommberman_special : MonoBehaviour {

    public Transform player_checker;
    public Enemy_attack info;
    

    private void Awake()
    {
        player_checker = gameObject.transform.parent.Find("playerchecker");
        info = GetComponent<Enemy_attack>();
    }

    public void Explosion()
    {
        Debug.Log("폭발한다!!!");
        LayerMask layer = LayerMask.GetMask("player");

        //플레이어가 범위내에 있으면 폭발해서 공격한다.
        Collider[] somthings = Physics.OverlapSphere(player_checker.position, 10f, layer);

        if (somthings.Length > 0)
        {
            somthings[0].GetComponent<Player_damaged>().Damaged(info.attack_Point_Special);
        }
    }
}
