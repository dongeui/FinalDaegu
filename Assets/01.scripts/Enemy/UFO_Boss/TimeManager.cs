using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {

    public float time_Boss;
    public float time_remains;
    public GameObject bomber;

    public void Awake()
    {
        time_remains = time_Boss;
        StartCoroutine(TimeCount());
    }
    
    IEnumerator TimeCount()
    {
        while (time_remains > 0) 
        {
            yield return new WaitForSeconds(1f);
            time_remains--;
            UFO_Boss_control.Instance.Show_time();
        }

        Debug.Log("UFO 퇴장");
        UFO_Boss_control.Instance.GetBack();
        yield return new WaitForSeconds(2f);

        Debug.Log("폭격기 소환!!!");
        Instantiate(bomber);
    }
}
