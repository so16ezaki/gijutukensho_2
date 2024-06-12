using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PEnemy : MonoBehaviour
{
    [SerializeField] GameObject _TankPrefab;
    [SerializeField] MTankBehaviour _MTankBehaviour;
    float randam = 0;
    float i = 1;

    // Start is called before the first frame update
    int _SpawnInter = 10;
    GameTimer spawnTimer;


    [SerializeField]
    List<GameObject> _Vehicles;

    TankPM.VehicleEntity _Entity;
    List<TankPM.VehicleEntity> _Entitys;

    int num;



    void Start()
    {
        TankPM tankPM = (TankPM)Resources.Load<TankPM>("TankPM");
        //_Entity = tankPM.vehicles[0];
        _Entitys = tankPM.vehicles;

        spawnTimer = new GameTimer(_SpawnInter);

        num = 0;


        _Entity = _Entitys[0];
        GameObject gameObject = Instantiate(_Entity.Prefab, new Vector3(0, 1, 10), Quaternion.identity);
        _Vehicles.Add(gameObject);
        _MTankBehaviour = gameObject.GetComponent<MTankBehaviour>();
        _MTankBehaviour.Initialization(_Entity);

        //GameObject gameObject= Instantiate(_TankPrefab, new Vector3(0, 1, 10), Quaternion.identity);
        //Vehicles.Add(gameObject);
        //_MTankBehaviour = gameObject.GetComponent<MTankBehaviour>();
        //_MTankBehaviour.Initialization(_Entity);
    }

    private void Update()
    {

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
        }




        //Debug.Log(_Entitys.Count);
        spawnTimer.UpdateTimer();

        if (spawnTimer.IsTimeUp && num < 1)
        {
            int entityId = num;

            if (num >= _Entitys.Count)
            {
                entityId = 0;
            }

            _Entity = _Entitys[entityId];
            GameObject gameObject = Instantiate(_Entity.Prefab, new Vector3(0, 1, 10), Quaternion.identity);
            _Vehicles.Add(gameObject);
            _MTankBehaviour = gameObject.GetComponent<MTankBehaviour>();
            _MTankBehaviour.Initialization(_Entity);
            num++;
            spawnTimer.ResetTimer();


        }
    }

    private void FixedUpdate()
    {


        if (i >= 2)
        {
            i = 0;
            randam = UnityEngine.Random.Range(-1f, 1f);
        }
        i += Time.fixedDeltaTime;

        _MTankBehaviour.Move(randam, 1f, _Entity);
    }

    public bool isEndGame
    {
        get{
            bool end = false;
            if (_Vehicles.Count <= 0)
                end = true;

            return end;
        }
    }



}
