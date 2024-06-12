using System.Collections.Generic;
using UnityEngine;

public class PPlayer : MonoBehaviour
{
    [SerializeField] GameObject _GameObj;
    [SerializeField]MTankBehaviour _MTankBehaviour;
    [SerializeField]PlayView _PlayView;

    TankPM.VehicleEntity Entity;

    Rigidbody _Rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        TankPM tankPM = (TankPM)Resources.Load<TankPM>("TankPM");

        Entity = tankPM.vehicles[0];

        
        _MTankBehaviour = _GameObj.GetComponent<MTankBehaviour>();
        _MTankBehaviour.Initialization(Entity);

        _Rigidbody = _GameObj.GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    private void FixedUpdate()
    {

        if (Input.GetMouseButton(0))
            _MTankBehaviour.Shoot();

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        _MTankBehaviour.Move(horizontal,vertical,Entity);


        
    }
    private void Update()
    {
        _PlayView.DispInfo(Entity.TankName, _MTankBehaviour.CurrentHp, (int)_Rigidbody.velocity.sqrMagnitude);
    }
}
