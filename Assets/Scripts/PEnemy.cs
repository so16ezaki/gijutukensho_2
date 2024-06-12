using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PEnemy : MonoBehaviour
{
    [SerializeField] GameObject _TankPrefab;
    [SerializeField] MTankBehaviour _MTankBehaviour;
    float randam = 0;
    float i = 1;

    // Start is called before the first frame update


    [SerializeField]
    List<GameObject> Vehicles;

    TankPM.VehicleEntity tankEntity;



    void Start()
    {
        TankPM tankPM = (TankPM)Resources.Load<TankPM>("TankPM");tankEntity = tankPM.vehicles[0];
        

        GameObject gameObject= Instantiate(_TankPrefab, new Vector3(0, 1, 10), Quaternion.identity);
        Vehicles.Add(gameObject);
        _MTankBehaviour = gameObject.GetComponent<MTankBehaviour>();
        _MTankBehaviour.Initialization(tankEntity);
    }

    private void FixedUpdate()
    {


        if (i >= 2)
        {
            i = 0;
            randam = UnityEngine.Random.Range(-1f, 1f);
        }
        i += Time.fixedDeltaTime;

        _MTankBehaviour.Move(randam, 1f, tankEntity);
    }


}
