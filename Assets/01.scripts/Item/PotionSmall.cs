using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionSmall : MonoBehaviour {

    public int HealPoint;
    public GameObject Healeffect;
    

    public void Heal()
    {
        Player_health.Instance.HP_now += HealPoint;
        UI_control.Instance.HP_bar.value = Player_health.Instance.HP_now;

        Healeffect.transform.SetParent(Player_control.Instance.transform);
        Healeffect.SetActive(true);
        Destroy(Healeffect, 5f);
    }
    
}
