using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "TankPM", menuName = "TankPM")]
public class TankPM : ScriptableObject
{
    public List<TankPM.VehicleEntity> vehicles = new List<TankPM.VehicleEntity>();


    [System.Serializable]
    public class VehicleEntity 
    {
        [SerializeField]
        private int _EntitiyId ;
        [SerializeField]
        private string _TankName ;
        [SerializeField]
        private GameObject _Prefab;
        [SerializeField]
        private int _MaxHp ;
        [SerializeField]
        private int _MoveSpeed ;
        [SerializeField]
        private int _MoveForce ;
        [SerializeField]
        private float _GunInterval ;
        [SerializeField]
        private int _MaxPivotAngle ;
        [SerializeField]
        private int _CVFSpeed ;

        public string TankName { get => _TankName; }
        public int MaxHp { get => _MaxHp; }
        public int MoveSpeed { get => _MoveSpeed; }
        public int MoveForce { get => _MoveForce; }
        public float GunInterval { get => _GunInterval; }
        public int MaxPivotAngle { get => _MaxPivotAngle; }
        public int CVFSpeed { get => _CVFSpeed; }
        public GameObject Prefab { get => _Prefab;}
        public int EntitiyId { get => _EntitiyId; }
    }
}