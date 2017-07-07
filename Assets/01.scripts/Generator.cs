using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {

    public GameObject creature;

    private void Awake()
    {
    }

    private void Start()
    {
       // Creature_generate();
    }

    public void Creature_generate()
    {
        GameObject obj = Instantiate(creature);
        obj.transform.position = gameObject.transform.position;
    }

    public void AutoReady(GameObject obj)
    {
        creature = obj;
    }
}
