using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]Tank _Tank;

    // Start is called before the first frame update
    void Start()
    {
        _Tank.Initialization();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        _Tank.Move(horizontal,vertical);
    }
}
