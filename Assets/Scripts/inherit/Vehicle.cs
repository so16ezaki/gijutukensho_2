using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Vehicle : MonoBehaviour,IDisplayable
{
    private int _Hp;
    private int _MoveSpeed;
    private int _GunInterval;
    private string _Name;

    GameTimer _GunIntervalTimer;

    public abstract void disInfo();


}
