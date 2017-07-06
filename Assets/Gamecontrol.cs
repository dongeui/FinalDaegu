using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamecontrol : MonoBehaviour
{
    public GameObject[] SpawnPoint = new GameObject[2];
    public GameObject[] ResponPattern = new GameObject[5];
    public GameObject[] RandomObject = new GameObject[7];

    public GameObject[] RandomResponPoint = new GameObject[2];

    private int PatternResponCount;
    private int RandomResponCount;

    // Use this for initialization
    private void Start()
    {
        RandomResponCount = 0; // 랜덤리스폰카운트 초기화
        PatternResponCount = 0; //랜덤패턴리스폰카운트 초기화
        ResponFirst(); //첫 패턴 몬스터무리 생성
        InvokeRepeating("Respon", 15f, 15f);
        InvokeRepeating("RandomRespon", 75f, 2f);
    }

    private void Update()
    {
    }

    public void ResponFirst()
    {
        print("FirstRespon");
        int RandomNum = Random.Range(0, 5);
        //화면에 보이는 위치에 첫 생성
        Instantiate(ResponPattern[RandomNum], SpawnPoint[0].transform);
        int RandomNum1 = Random.Range(0, 5);
        //화면에 보이지 위치에 생성
        Instantiate(ResponPattern[RandomNum1], SpawnPoint[1].transform);
    }

    public void Respon()
    {
        if (PatternResponCount < 4)
        {
            int RandomNum = Random.Range(0, 5);

            Instantiate(ResponPattern[RandomNum], SpawnPoint[1].transform);
            PatternResponCount++;
            print("PatternRespon" + PatternResponCount);
        }
    }

    public void RandomRespon()
    {
        if (PatternResponCount >= 4 && RandomResponCount < 12)
        {
            int RandomResNum = Random.Range(0, 7);
            int RandomResPointNum = Random.Range(0, 2);

            Instantiate(RandomObject[RandomResNum], RandomResponPoint[RandomResPointNum].transform);
            RandomResponCount++;
            print("RandomRespon" + RandomResponCount);
        }
    }
}