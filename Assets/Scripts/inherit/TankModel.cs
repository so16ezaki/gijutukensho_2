using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class TankModel : Vehicle
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

    [SerializeField] int _MaxPivotAngle = 25;
    [SerializeField] int _CVFSpeed = 100;


    GameTimer _GunIntervalTimer;
    // Start is called before the first frame update

    public override void Initialization(TankPM.VehicleEntity vehicleEntity)
    {
        _Hp = vehicleEntity.Hp;
        _MoveSpeed = vehicleEntity.MoveSpeed;
        _MoveForce = vehicleEntity.MoveForce;
        _VehicleName = vehicleEntity.TankName;

        if (_BLHinge != null && _BRHinge != null && _PivotHinge != null)
        {
            _PivotSpring = _PivotHinge.spring;
            _PivotHinge.useSpring = true;
            _GunIntervalTimer = new GameTimer(_GunInterval);
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

    public void Move(float horizontal, float vertical)
    {
        if (_BLHinge != null && _BRHinge != null && _PivotHinge != null)
        {

            _PivotSpring.targetPosition = horizontal * _MaxPivotAngle;
            _PivotHinge.spring = _PivotSpring;
            _PivotHinge.useSpring = true;

            float curveVelocityFade = (1 + Mathf.Abs(_PivotSpring.targetPosition) / _CVFSpeed);
            float targetVelocity = vertical * _MoveSpeed / curveVelocityFade;

            _BRMotor.targetVelocity = targetVelocity;
            _BRMotor.force = _MoveForce;
            _BRHinge.motor = _BRMotor;

            _BLMotor.targetVelocity = targetVelocity;
            _BLMotor.force = _MoveForce;
            _BLHinge.motor = _BLMotor;
        }
    }

    public override void disInfo()
    {
        Debug.Log(_VehicleName + "ŽÔ—¼ HP: " + _Hp);
    }

    public override void AddDamage(int damage)
    {
        _Hp -= damage;

        if (_Hp <= 0)
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
