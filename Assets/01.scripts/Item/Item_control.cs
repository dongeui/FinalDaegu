using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item_control : MonoBehaviour
{
    public float speed;

    private void Start()
    {
        StartCoroutine(Move());
    }

    public IEnumerator Move()
    {
        while (true)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("동전이 무언가와 부딪힘");

        switch (other.gameObject.tag)
        {
            case "Player":
                print("플레이어와 충돌");
                

                if (this.gameObject.tag == "SilverCoin")
                {
                    print("실버당!!!");
                    UI_control.Instance.CoinCountUpdate();
                    UI_control.Instance.ScoreUpdate(100);

                    //코인사운드재생
                    Coin_Audio.Instance.CoinSound();
                }
                if (this.gameObject.tag == "GoldCoin")
                {
                    print("골드당!!!");

                    //ui관리스크립트의 점수,코인카운트셋
                    UI_control.Instance.CoinCountUpdate();
                    UI_control.Instance.ScoreUpdate(300);

                    //코인사운드재생
                    Coin_Audio.Instance.CoinSound();
                }

                if (gameObject.tag == "potion")
                {
                    Debug.Log("포션!!!");
                    GetComponent<PotionSmall>().Heal();
                }

                Destroy(gameObject.transform.parent.gameObject);

                break;

            default:
                break;
        }
    }
}