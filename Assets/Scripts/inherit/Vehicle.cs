using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Vehicle : MonoBehaviour,IDisplayable,IDamageable
{
    [SerializeField]
    protected int _CurrentHp;

    
    public abstract void Initialization(TankPM.VehicleEntity vehicleEntity);

    public abstract void AddDamage(int damage);

    public abstract void disInfo(TankPM.VehicleEntity vehicleEntity);


}
