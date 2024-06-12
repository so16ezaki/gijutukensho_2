using System.Collections.Generic;
using UnityEngine;

public class PPlayer : MonoBehaviour
{
    [SerializeField] GameObject _TankGameObj;
    [SerializeField]MTankBehaviour _Tank;

    [SerializeField]
    List<GameObject> Vehicles;
    TankPM.VehicleEntity tankEntity;

    // Start is called before the first frame update
    void Start()
    {
        TankPM tankPM = (TankPM)Resources.Load<TankPM>("TankPM");

        tankEntity = tankPM.vehicles[0];

        
        _Tank = _TankGameObj.GetComponent<MTankBehaviour>();



        _Tank.Initialization(tankEntity);
    }


    // Update is called once per frame
    private void FixedUpdate()
    {

        if (Input.GetMouseButton(0))
            _Tank.Shoot();

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        _Tank.Move(horizontal,vertical,tankEntity);
    }
}
