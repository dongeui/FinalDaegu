using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator_holder : MonoBehaviour {
    
    public bool automatic_Generate = false;
    public control controler;

    private int[] creation_encount;
    private Generator[] children_class;

    private void Awake()
    {
        
    }

    void Start() {
        Find_All_Generator();
        Check_mode();
    }
	
	void Update () {
		
	}


    //생성기에 담겨있는 모든 자식들을 참조한다. 
    //이 자식들은 무언가가 만들어질 위치정보를 담고 있다.
    void Find_All_Generator()
    {
        controler.position = new GameObject[transform.childCount];
        children_class = new Generator[transform.childCount];

        for (int i = 0; i < controler.position.Length; i++)
        {
            controler.position[i] = gameObject.transform.GetChild(i).gameObject;
            children_class[i] = controler.position[i].GetComponent<Generator>();
        }
    }

    //보스를 제외한 일반 몬스터와 아이템을 자동으로 떨군다.
    IEnumerator Generate_Auto()
    {
        while (automatic_Generate)
        {
            yield return new WaitForSeconds(controler.time);
            int rnd = Random.Range(0, 101);
            Generate(rnd);
        }
    }

    //자동생성이 활성화 되었는가? 그렇다면 자동생성을 시작한다.    
    void Check_mode()
    {
        if (automatic_Generate)
        {
            StartCoroutine(Generate_Auto());
        }
    }

    void Creation_set()
    {
        creation_encount = new int[controler.position.Length];
        int rnd = 0;

        for (int i = 0; i < creation_encount.Length; i++)
        {
            if (controler.position[i].name.Contains("enemy"))
            {
                creation_encount[i] = controler.enemy_encount;
                rnd = Random.Range(0, controler.enemies.Length);
                children_class[i].creature = controler.enemies[rnd];
            }

            else if (controler.position[i].name.Contains("weapon"))
            {
                creation_encount[i] = controler.weapon_encount;
                rnd = Random.Range(0, controler.weapons.Length);
                children_class[i].creature = controler.weapons[rnd];
            }

            else if (controler.position[i].name.Contains("coin"))
            {
                creation_encount[i] = controler.coin_encount;
                rnd = Random.Range(0, controler.coins.Length);
                children_class[i].creature = controler.coins[rnd];
            }

            else if (controler.position[i].name.Contains("item"))
            {
                creation_encount[i] = controler.item_encount;
                rnd = Random.Range(0, controler.items.Length);
                children_class[i].creature = controler.items[rnd];
            }
        }        
    }

    void Generate(int percent)
    {
        Creation_set();
        Debug.Log("무언가를 생성한다! : " + percent);

        for (int i=0;i< children_class.Length; i++)
        {
            if (creation_encount[i] > percent)
            {
                children_class[i].Creature_generate();
            }
        }
    }

    [System.Serializable]
    public class control
    {
        public GameObject[] position;

        public GameObject[] enemies;
        public GameObject[] weapons;
        public GameObject[] coins;
        public GameObject[] items;

        [Range(0, 100)]
        public int enemy_encount = 0;

        [Range(0, 100)]
        public int weapon_encount = 0;

        [Range(0, 100)]
        public int coin_encount = 0;

        [Range(0, 100)]
        public int item_encount = 0;

        [Range(1, 10)]
        public float time = 1f;
    }
}