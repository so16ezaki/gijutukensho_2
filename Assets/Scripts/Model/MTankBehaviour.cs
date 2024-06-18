
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;
using System;

[Serializable]
public class MTankBehaviour : Vehicle
{
    Rigidbody _Rb;

    [SerializeField] HingeJoint _BRHinge;
    [SerializeField] HingeJoint _BLHinge;
    [SerializeField] HingeJoint _PivotHinge;

    [SerializeField] JointMotor _BRMotor;
    [SerializeField] JointMotor _BLMotor;
    [SerializeField] JointSpring _PivotSpring;

    [SerializeField] protected GameObject _Shell;
    [SerializeField] protected GameObject _Gun;

    [SerializeField] private GameObject _CamPivot;
    [SerializeField] private GameObject _GunPivot;
    [SerializeField] private GameObject _TurretPivot;

    

    Quaternion cameraRot, turretRot, gunRot;



    protected GameTimer _GunIntervalTimer;

    public GameTimer GunIntervalTimer { get => _GunIntervalTimer; }

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
    public override void UpdateBehaviour()
    {

        GunIntervalTimer.UpdateTimer();
        

    }


    public virtual void Shoot()
    {

        if (GunIntervalTimer.IsTimeUp)
        {
            Instantiate(_Shell, _Gun.transform.position, _Gun.transform.rotation);
            GunIntervalTimer.ResetTimer();
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

        //Updateの中で作成した関数を呼ぶ
        cameraRot = ClampRotation(cameraRot, -90, 90);
        gunRot = ClampRotation(cameraRot, -30, 10);


        if (_CamPivot != null)
            _CamPivot.transform.localRotation = cameraRot;

        _GunPivot.transform.localRotation = gunRot;

        _TurretPivot.transform.localRotation = turretRot;


    }

    public void RotateTurret(GameObject targetGameObj)
    {
        Vector3 targetX = targetGameObj.transform.position - _TurretPivot.transform.position;
        Vector3 targetY = targetGameObj.transform.position - _GunPivot.transform.position;

        Quaternion targetXRot = Quaternion.LookRotation(targetX);
        Quaternion targetYRot = Quaternion.LookRotation(targetY);

        _TurretPivot.transform.rotation = Quaternion.Slerp(_TurretPivot.transform.rotation, targetXRot, Time.fixedDeltaTime * 100);
        _TurretPivot.transform.rotation = new Quaternion(0, _TurretPivot.transform.rotation.y, 0, _TurretPivot.transform.rotation.w);

        _GunPivot.transform.localRotation = Quaternion.Slerp(_GunPivot.transform.localRotation, targetYRot, Time.fixedDeltaTime * 100);
        _GunPivot.transform.localRotation = new Quaternion(_GunPivot.transform.localRotation.x, 0, 0, _GunPivot.transform.localRotation.w);

        _TurretPivot.transform.localRotation = ClampRotation( _TurretPivot.transform.localRotation,0, 0);
        _GunPivot.transform.localRotation = ClampRotation(_GunPivot.transform.localRotation, -30, 10);


    }

    public override string disInfo(TankPM.VehicleEntity vehicleEntity)
    {
        return vehicleEntity.TankName + "車両 HP: " + CurrentHp + "/" + vehicleEntity.MaxHp;
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


    //角度制限関数の作成
    private Quaternion ClampRotation(Quaternion q, float minX, float maxX)
    {
        //q = x,y,z,w (x,y,zはベクトル（量と向き）：wはスカラー（座標とは無関係の量）)

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
