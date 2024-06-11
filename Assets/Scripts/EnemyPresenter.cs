using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPresenter : MonoBehaviour
{
    [SerializeField] GameObject _TankObj;
    [SerializeField] TankModel _Tank;
    float randam = 0;
    float i = 1;

    // Start is called before the first frame update


    [SerializeField]
    List<GameObject> Vehicles;
   

    void Start()
    {
        TankPM tankPM = (TankPM)Resources.Load<TankPM>("TankPM");

        

        TankPM.VehicleEntity tankEntity = tankPM.vehicles[0];

        Debug.Log(tankEntity.TankGameObj);

        _TankObj = Instantiate(tankEntity.TankGameObj,new Vector3(0,5,10), Quaternion.identity) as GameObject;

        _Tank = _TankObj.GetComponent<TankModel>();

        

        _Tank.Initialization(tankEntity);
    }

    private void FixedUpdate()
    {
        
        
        if (i >= 2)
        {
            i = 0;
            randam = UnityEngine.Random.Range(-1f,1f);
        }
        i += Time.fixedDeltaTime;

        Debug.Log(randam.ToString() + "   "+ i);
        _Tank.Move(randam,1f);
    }


}
