using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGControl : MonoBehaviour
{
    public float speedX = 0.1f;

    // Use this for initialization
    private void Start()

    {
    }

    // Update is called once per frame
    private void Update()
    {
        //transform.Translate()
        this.transform.Translate(new Vector3(speedX, 0, 0));
    }
}