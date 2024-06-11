using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Vehicle : MonoBehaviour,IDisplayable,IDamageable
{
    [SerializeField]
    protected int _Hp;
    [SerializeField]
    protected int _MoveSpeed;
    [SerializeField]
    protected int _MoveForce;
    [SerializeField]
    protected float _GunInterval;
    [SerializeField]
    protected string _VehicleName;

    
    public abstract void Initialization(TankPM.VehicleEntity vehicleEntity);

    public abstract void AddDamage(int damage);

    public abstract void disInfo();


}
