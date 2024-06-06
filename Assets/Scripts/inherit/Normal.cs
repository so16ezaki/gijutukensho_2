using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Normal : Vehicle,IDisplayable
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
    void Start()
    {
        if (_BLHinge != null && _BRHinge != null && _PivotHinge != null)
        {
            _PivotSpring = _PivotHinge.spring;
            _PivotHinge.useSpring = true;
            _GunIntervalTimer = new GameTimer(1.0f);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_BLHinge != null && _BRHinge != null && _PivotHinge != null)
        {
            Move(25, 500, 1000, 100);

            _GunIntervalTimer.UpdateTimer();
            if (Input.GetMouseButton(0))
                Shoot();
        }
    }

    protected void Shoot()
    {

        if (_GunIntervalTimer.IsTimeUp)
        {
            Instantiate(_Shell, _Gun.transform.position, _Gun.transform.rotation);
            _GunIntervalTimer.ResetTimer();
        }

    }

    protected void Move(int maxPivotAngle, int motorForce, int targetSpeed, int cVFSpeed)
    {
        _PivotSpring.targetPosition = Input.GetAxis("Horizontal") * maxPivotAngle;
        _PivotHinge.spring = _PivotSpring;
        _PivotHinge.useSpring = true;

        float curveVelocityFade = (1 + Mathf.Abs(_PivotSpring.targetPosition) / cVFSpeed);

        _BRMotor.targetVelocity = Input.GetAxis("Vertical") * targetSpeed / curveVelocityFade;
        _BRMotor.force = motorForce;
        _BRHinge.motor = _BRMotor;

        _BLMotor.targetVelocity = Input.GetAxis("Vertical") * targetSpeed / curveVelocityFade;
        _BLMotor.force = motorForce;
        _BLHinge.motor = _BLMotor;
    }

    public override void disInfo()
    {
        Debug.Log("Normal�ԗ��ł�");
    }

}
