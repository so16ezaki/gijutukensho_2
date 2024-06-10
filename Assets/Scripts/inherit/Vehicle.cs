using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Vehicle : MonoBehaviour,IDisplayable
{
    [SerializeField]
    protected int _Hp;
    [SerializeField]
    protected int _MoveSpeed;
    [SerializeField]
    protected float _GunInterval;
    [SerializeField]
    protected string _TankName;


    public abstract void disInfo();


}
