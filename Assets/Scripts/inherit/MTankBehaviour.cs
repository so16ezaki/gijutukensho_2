using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class MTankBehaviour : Vehicle
{
    Rigidbody _Rb;

    [SerializeField] HingeJoint _BRHinge;
    [SerializeField] HingeJoint _BLHinge;
    [SerializeField] HingeJoint _PivotHinge;

    [SerializeField] JointMotor _BRMotor;
    [SerializeField] JointMotor _BLMotor;
    [SerializeField] JointSpring _PivotSpring;

    [SerializeField] GameObject _Shell;
    [SerializeField] GameObject _Gun;



    
    GameTimer _GunIntervalTimer;
    // Start is called before the first frame update

    public override void Initialization(TankPM.VehicleEntity vehicleEntity)
    {
        _CurrentHp = vehicleEntity.MaxHp;

        if (_BLHinge != null && _BRHinge != null && _PivotHinge != null)
        {
            _PivotSpring = _PivotHinge.spring;
            _PivotHinge.useSpring = true;
            _GunIntervalTimer = new GameTimer(vehicleEntity.GunInterval);
            
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        _GunIntervalTimer.UpdateTimer();

    }

    public void Shoot()
    {

        if (_GunIntervalTimer.IsTimeUp)
        {
            Instantiate(_Shell, _Gun.transform.position, _Gun.transform.rotation);
            _GunIntervalTimer.ResetTimer();
        }

    }

    public void Move(float horizontal, float vertical, TankPM.VehicleEntity vehicleEntity)
    {


        if (_BLHinge != null && _BRHinge != null && _PivotHinge != null)
        {

            _PivotSpring.targetPosition = horizontal * vehicleEntity.MaxPivotAngle;
            _PivotHinge.spring = _PivotSpring;
            _PivotHinge.useSpring = true;

            float curveVelocityFade = (1 + Mathf.Abs(_PivotSpring.targetPosition) / vehicleEntity.CVFSpeed);
            float targetVelocity = vertical * vehicleEntity.MoveSpeed / curveVelocityFade;

            _BRMotor.targetVelocity = targetVelocity;
            _BRMotor.force = vehicleEntity.MoveForce;
            _BRHinge.motor = _BRMotor;

            _BLMotor.targetVelocity = targetVelocity;
            _BLMotor.force = vehicleEntity.MoveForce;
            _BLHinge.motor = _BLMotor;
        }
    }

    public override void disInfo(TankPM.VehicleEntity vehicleEntity)
    {
        Debug.Log(vehicleEntity.TankName + "ŽÔ—¼ HP: " + _CurrentHp);
    }

    public override void AddDamage(int damage)
    {
        _CurrentHp -= damage;

        if (_CurrentHp <= 0)
        {
            Joint[] joints = GetComponentsInChildren<Joint>();

            foreach (HingeJoint joint in joints)
            {
                Debug.Log(joint.name);
                Destroy(joint);
            }
            Destroy(gameObject, 5);
        }
    }
}
