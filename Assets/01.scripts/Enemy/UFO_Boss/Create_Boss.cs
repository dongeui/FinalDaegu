using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create_Boss : MonoBehaviour {

    public GameObject Boss;
    public float Time;
    public GameObject[] generators;
        


    private void Awake()
    {
        StartCoroutine(BossIsComming());
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator BossIsComming()
    {
        yield return new WaitForSeconds(Time);
        Instantiate(Boss);

        for (int i=0; i< generators.Length; i++)
        {
            generators[i].SetActive(false);
        }
    }
}
