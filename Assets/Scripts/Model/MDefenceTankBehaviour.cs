using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MDefenceTankBehaviour : MTankBehaviour
{
    [SerializeField] int _MaxShieldHp = 20;
    [SerializeField] int _CurrentShieldHp = 0;
    [SerializeField] int _ShieldInterval = 15;
    [SerializeField] GameTimer _NoDamageTimer;

    Renderer[] _RendererList;
    [SerializeField] private Material _TargetMaterial;

    public override void Initialization(TankPM.VehicleEntity vehicleEntity)
    {
        base.Initialization(vehicleEntity);
        _NoDamageTimer = new GameTimer(_ShieldInterval);
        _CurrentShieldHp = _MaxShieldHp;
        _RendererList = GetComponentsInChildren<Renderer>();
    }

    public override void UpdateBehaviour()
    {
        base.UpdateBehaviour();
        RecoveryShield();
        ChangeColor();

    }
    public override void AddDamage(int damage)
    {
        
        _NoDamageTimer.ResetTimer();

        if (_CurrentShieldHp - damage <= 0)
        {
            if (_CurrentShieldHp <= 0)
                _CurrentHp -= damage;

            _CurrentShieldHp = 0;
        }
        else
        {
            _CurrentShieldHp -= damage;
        }


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

    private void RecoveryShield()
    {
        _NoDamageTimer.UpdateTimer();
        if (_NoDamageTimer.IsTimeUp)
        {
            _MaxShieldHp *= 1;
            _CurrentShieldHp = _MaxShieldHp;
            _NoDamageTimer.ResetTimer();
        }



    }

    private void ChangeColor()
    {
        float shieldRate = (float)_CurrentShieldHp / _MaxShieldHp;
        float inverseShieldRate = 1 - shieldRate;

        foreach (Renderer renderer in _RendererList)
        {
            renderer.material.color = Color.white * inverseShieldRate + _TargetMaterial.color * shieldRate;
        }
    }
    public override string disInfo(TankPM.VehicleEntity vehicleEntity)
    {
        return base.disInfo(vehicleEntity) + "\n ÉVÅ[ÉãÉhHP:" + $"{_CurrentShieldHp:00}" + "/" + _MaxShieldHp;

    }


}
