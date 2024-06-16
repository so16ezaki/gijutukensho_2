using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MAtackTankBehaviour : MTankBehaviour
{
    private int _BurstShootNum = 3;
    private float _BurstInterval = 1f;
    private GameTimer _BurstShootTimer;
    private int shootedNum = 0;

    Renderer[] _RendererList;
    [SerializeField] private Material _TargetMaterial;

    public override void Initialization(TankPM.VehicleEntity vehicleEntity)
    {
        base.Initialization(vehicleEntity);
        _BurstShootTimer = new GameTimer(_BurstInterval);
        _RendererList = GetComponentsInChildren<Renderer>();
    }

    public override void UpdateBehaviour()
    {
        base.UpdateBehaviour();
        _BurstShootTimer.UpdateTimer();
        ChangeColor();
    }

    public override void Shoot()
    {
        if (GunIntervalTimer.IsTimeUp)
        {


            if (_BurstShootTimer.IsTimeUp && shootedNum <= _BurstShootNum)
            {
                Instantiate(_Shell, _Gun.transform.position, _Gun.transform.rotation);

                _BurstShootTimer.ResetTimer();
                shootedNum++;
            }
            else if (shootedNum >= _BurstShootNum)
            {
                GunIntervalTimer.ResetTimer();
                shootedNum = 0;
            }
        }
    }
    private void ChangeColor()
    {
        foreach (Renderer renderer in _RendererList)
        {
            renderer.material.color = Color.white * _GunIntervalTimer.InverseTimeRate + _TargetMaterial.color * _GunIntervalTimer.TimeRate;
        }
    }

    public override string disInfo(TankPM.VehicleEntity vehicleEntity)
    {
        return base.disInfo(vehicleEntity) + "\n ‘•“UŽžŠÔ:" + $"{_GunIntervalTimer.ElaspedTime:00.0}" + "/" + vehicleEntity.GunInterval;

    }
}
