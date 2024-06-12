using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class PlayView : MonoBehaviour
{
    [TextArea]
    [SerializeField] string text;

    [SerializeField]
    Text _StatusText;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        _StatusText.text = text;
    }

    public void DispInfo(string name, int hp, int speed)
    {
        text =
            "Name  :" + name + "\r\n" +
            "HP    :  " + $"{hp:000}" + "\r\n" +
            "Speed :  " +$"{speed * 3.6:000}" + "km/h\r\n" +
            "Ammo  :      ";
        
        _StatusText.text = text;
    }
}
