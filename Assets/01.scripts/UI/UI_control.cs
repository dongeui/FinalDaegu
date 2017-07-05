using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_control : MonoBehaviour
{
    static public UI_control Instance;

    public Slider HP_bar;
    private Text coin_txt;
    private Text score_txt;

    public int score_best;
    public int score_now;
    public int CoinCount;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Find_components();
        Init_UI();
        score_now = 0;
        CoinCount = 0;
    }

    //UI 컨트롤러에서 활용할 컴포넌트들을 참조한다.
    private void Find_components()
    {
        HP_bar = gameObject.GetComponentInChildren<Slider>();
        coin_txt = GameObject.Find("coinText").GetComponent<Text>();
        score_txt = GameObject.Find("ScoreText").GetComponent<Text>();
    }

    //UI컨트롤러의 정보들을 초기화 한다.
    //체력바는 최대치, 코인과 점수는 각각 0점. 혹은 필요에 따라 최대점수를 미리 불러 온다.
    private void Init_UI()
    {
        HP_bar.maxValue = Player_health.Instance.HP_max;
        HP_bar.minValue = 0f;
        HP_bar.value = Player_health.Instance.HP_now;
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void CoinCountUpdate()
    {
        CoinCount++;
        SetCoinCount();
    }

    public void ScoreUpdate(int value)
    {
        score_now += value;
        SetScore();
    }

    public void SetScore()
    {
        score_txt.text = score_now.ToString();
    }

    public void SetCoinCount()
    {
        coin_txt.text = CoinCount.ToString();
    }
}