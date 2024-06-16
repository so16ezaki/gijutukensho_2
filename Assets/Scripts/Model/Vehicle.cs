using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Vehicle : MonoBehaviour,IDisplayable,IDamageable
{
    [SerializeField]
    protected int _CurrentHp;
    [SerializeField]
    protected int _EntityId;

    private bool isDead = false;

    public int EntityId { get => _EntityId;}
    public int CurrentHp { get => _CurrentHp;}
    public bool IsDead { get => isDead; set => isDead = value; }

    public abstract void Initialization(TankPM.VehicleEntity vehicleEntity);

    public abstract void UpdateBehaviour();

    public abstract void AddDamage(int damage);

    public abstract string disInfo(TankPM.VehicleEntity vehicleEntity);


}
