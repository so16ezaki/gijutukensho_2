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

    [SerializeField] private GameObject _CamPivot;
    [SerializeField] private GameObject _GunPivot;
    [SerializeField] private GameObject _TurretPivot;

    Quaternion cameraRot, turretRot, gunRot;
    


    GameTimer _GunIntervalTimer;
    // Start is called before the first frame update

    public override void Initialization(TankPM.VehicleEntity vehicleEntity)
    {
        _CurrentHp = vehicleEntity.MaxHp;
        _EntityId = vehicleEntity.EntitiyId;

        if (_CamPivot != null)
            cameraRot = _CamPivot.transform.localRotation;

        gunRot = _GunPivot.transform.localRotation;
        turretRot = _TurretPivot.transform.localRotation;

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

    void Update()
    {
        
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

    public void RotateTurret(float xRot, float yRot)
    {
        cameraRot *= Quaternion.Euler(-yRot, 0, 0);
        // gunRot *= Quaternion.Euler(-yRot , 0, 0);
        turretRot *= Quaternion.Euler(0, xRot, 0);

        //Update�̒��ō쐬�����֐����Ă�
        cameraRot = ClampRotation(cameraRot, -90, 90);
        gunRot = ClampRotation(cameraRot, -30, 10);


        if (_CamPivot != null)
            _CamPivot.transform.localRotation = cameraRot;

        _GunPivot.transform.localRotation = gunRot;

        _TurretPivot.transform.localRotation = turretRot;


    }

    public override void disInfo(TankPM.VehicleEntity vehicleEntity)
    {
        Debug.Log(vehicleEntity.TankName + "�ԗ� HP: " + CurrentHp);
    }

    public override void AddDamage(int damage)
    {
        _CurrentHp -= damage;

        if (CurrentHp <= 0)
        {
            Joint[] joints = GetComponentsInChildren<Joint>();

            foreach (HingeJoint joint in joints)
            {
                Destroy(joint);
            }
            IsDead = true;
            Destroy(gameObject, 5);
        }
    }


    //�p�x�����֐��̍쐬
    private Quaternion ClampRotation(Quaternion q, float minX, float maxX)
    {
        //q = x,y,z,w (x,y,z�̓x�N�g���i�ʂƌ����j�Fw�̓X�J���[�i���W�Ƃ͖��֌W�̗ʁj)

        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1f;

        float angleX = Mathf.Atan(q.x) * Mathf.Rad2Deg * 2f;

        angleX = Mathf.Clamp(angleX, minX, maxX);

        q.x = Mathf.Tan(angleX * Mathf.Deg2Rad * 0.5f);

        return q;
    }
}
