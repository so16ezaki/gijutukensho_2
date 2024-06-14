using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Randam = UnityEngine.Random;

public class PEnemy : MonoBehaviour
{
    [SerializeField] GameObject _TankPrefab;
    [SerializeField] MTankBehaviour _MTankBehaviour;
    [SerializeField] GameObject _Player;
    [SerializeField] VGameView _VGameView;

    float randam = 0;
    float i = 1;

    // Start is called before the first frame update
    int _SpawnInter = 10;
    GameTimer spawnTimer;


    [SerializeField]
    List<GameObject> _Vehicles;

    TankPM.VehicleEntity _Entity;
    List<TankPM.VehicleEntity> _Entitys;

    int _Num;
    int _DestroyCount;



    void Start()
    {
        TankPM tankPM = (TankPM)Resources.Load<TankPM>("TankPM");
        //_Entity = tankPM.vehicles[0];
        _Entitys = tankPM.vehicles;

        spawnTimer = new GameTimer(_SpawnInter);

        _Num = 0;


        //GameObject gameObject= Instantiate(_TankPrefab, new Vector3(0, 1, 10), Quaternion.identity);
        //Vehicles.Add(gameObject);
        //_MTankBehaviour = gameObject.GetComponent<MTankBehaviour>();
        //_MTankBehaviour.Initialization(_Entity);
    }

    private void Update()
    {
        Debug.Log(_Num);

        List<GameObject> vehiclesToRemove = new List<GameObject>();

        foreach (GameObject vehicle in _Vehicles)
        {
            if (vehicle.GetComponent<MTankBehaviour>().IsDead)
            {
                vehiclesToRemove.Add(vehicle);
            }
        }

        foreach (GameObject vehicleToRemove in vehiclesToRemove)
        {
            _Vehicles.Remove(vehicleToRemove);
            _DestroyCount++;
        }




        //Debug.Log(_Entitys.Count);
        spawnTimer.UpdateTimer();

        if (spawnTimer.IsTimeUp)
        {
            int entityId = _Num;

            if (_Num < _Entitys.Count)
            {
                _Entity = _Entitys[entityId];
                GameObject gameObject = Instantiate(_Entity.Prefab, new Vector3(20,0,Randam.Range(-50,50)),Quaternion.identity);
                _Vehicles.Add(gameObject);
                _MTankBehaviour = gameObject.GetComponent<MTankBehaviour>();
                _MTankBehaviour.Initialization(_Entity);
                _Num++;
                spawnTimer.ResetTimer();
            }

        }

        _VGameView.EnemyListDispInfo(_Entitys,_Vehicles);
    }

    private void FixedUpdate()
    {


        if (i >= 2)
        {
            i = 0;
            randam = UnityEngine.Random.Range(-1f, 1f);
        }
        i += Time.fixedDeltaTime;

        foreach (GameObject vehicle in _Vehicles)
        {
            _MTankBehaviour = vehicle.GetComponent<MTankBehaviour>();
            int entityId = _MTankBehaviour.EntityId;
            //Debug.Log(entityId);
            _Entity = _Entitys[entityId];

            _MTankBehaviour.Move(randam, 1f, _Entity);

            _MTankBehaviour.RotateTurret(_Player);

            _MTankBehaviour.Shoot();
        }
    }

    public bool isEndGame
    {
        get
        {
            bool end = false;
            if (_Entitys.Count <= _DestroyCount)
                end = true;

            return end;
        }
    }



}
