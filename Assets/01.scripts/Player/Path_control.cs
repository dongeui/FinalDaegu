using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path_control : MonoBehaviour {
    

    GameObject path;
    Transform[] path_tr;
    public Vector3[] path_vec;

    private void Awake()
    {
        Init_path();
    }   


    //경로 초기화(이걸로 경로가 몇개가 되어도 관리할 수 있다.)
    void Init_path()
    {
        path = GameObject.FindGameObjectWithTag("Path");
        int path_num = path.transform.childCount;
        path_tr = new Transform[path_num]; 

        for (int i = 0; i < path_num; i++)
        {
            path_tr[i] = path.transform.GetChild(i);//<- 이동할 경로의 실 정보가 담겨 있다.
        }
    }

    public void Set_path(string now_Track)
    {
        //현재 '트랙 1번'에 있는가?
        if (now_Track == "Track01")
        {
            //플레이어가 점프할 경로설정
            path_vec = new Vector3[path_tr.Length];
            for (int i = 0; i < path_vec.Length; i++)
            {
                path_vec[i] = path_tr[i].position;
            }
        }

        //현재 '트랙 2번'에 있는가?
        else if (now_Track == "Track02")
        {
            //플레이어가 점프할 경로설정
            path_vec = new Vector3[path_tr.Length];
            for (int i = 0; i < path_vec.Length; i++)
            {
                path_vec[i] = path_tr[path_vec.Length - 1 - i].position;
            }
        }

        //현재 트랙위에 있지 않으므로 아무작업도 하지 않고 탈출
        else
        {
            return;
        }
    }
}
