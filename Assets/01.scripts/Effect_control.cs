using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_control : MonoBehaviour {

    public GameObject[] effects;    


    public void Show_effect(int index)
    {
        effects[index].SetActive(false);
        effects[index].SetActive(true);
    }

    public void Hide_effect(int index)
    {
        effects[index].SetActive(false);
    }

    public void Hide_effect_delay(int index, float delayTime)
    {
        StopAllCoroutines();
        StartCoroutine(Delay_hide(index, delayTime));
    }


    IEnumerator Delay_hide(int index, float time)
    {
        yield return new WaitForSeconds(time);
        effects[index].SetActive(false);
    }

    public void Set_transform(int index, Vector3 position)
    {
        effects[index].transform.position = position;
    }


    public void Set_loop_All(int index)
    {
        effects[index].GetComponent<ParticleSystem>().loop = true;
        for (int i=0;i< effects[index].transform.childCount; i++)
        {
            effects[index].transform.GetChild(i).GetComponent<ParticleSystem>().loop = true;
        }
    }
}
