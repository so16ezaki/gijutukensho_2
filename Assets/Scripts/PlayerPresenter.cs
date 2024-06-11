using System.Collections.Generic;
using UnityEngine;

public class PlayerPresenter : MonoBehaviour
{
    [SerializeField] GameObject _TankGameObj;
    [SerializeField]TankModel _Tank;

    [SerializeField]
    List<GameObject> Vehicles;

    // Start is called before the first frame update
    void Start()
    {
        TankPM tankPM = (TankPM)Resources.Load<TankPM>("TankPM");

        TankPM.VehicleEntity tankEntity = tankPM.vehicles[0];

        
        _Tank = _TankGameObj.GetComponent<TankModel>();



        _Tank.Initialization(tankEntity);
    }


    // Update is called once per frame
    private void FixedUpdate()
    {

        if (Input.GetMouseButton(0))
            _Tank.Shoot();

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        _Tank.Move(horizontal,vertical);
    }
}
