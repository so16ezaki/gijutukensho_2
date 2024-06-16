using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static TankPM;
using static UnityEngine.EventSystems.EventTrigger;

[Serializable]
public class VGameView : MonoBehaviour
{
    [TextArea]
    [SerializeField] string text;

    [SerializeField]
    Text _Status;

    [SerializeField]
    Text _EnemyStatus;

    [SerializeField]
    Text _EnemyStatusList;

    [SerializeField]
    private GameObject[] _Views;

    // Start is called before the first frame update
    public void DispInfo(string name, int hp, int speed)
    {
        text =
            "Name  :" + name + "\r\n" +
            "HP    :  " + $"{hp:000}" + "\r\n" +
            "Speed :  " +$"{speed * 3.6:000}" + "km/h\r\n" +
            "Ammo  :      ";
        
        _Status.text = text;
    }

    public void EnemyRayDispInfo(string dispString)
    {
        text = dispString;

        _EnemyStatus.text = text;
    }

    public void EnemyListDispInfo(List<TankPM.VehicleEntity> entities,List<GameObject> vehicles)
    {
        String text = vehicles.Count + "/" + entities.Count +"\n";
        foreach (GameObject vehicle in vehicles)
        {
            int entityId  = vehicle.GetComponentInParent<Vehicle>().EntityId;
            Vehicle vehicleBehavior = vehicle.GetComponentInParent<Vehicle>();
            TankPM.VehicleEntity entity = entities[entityId];
            text += entity.TankName+ "ŽÔ—¼" + vehicleBehavior.CurrentHp + "/" + entity.MaxHp + "\n";
        }

        _EnemyStatusList.text = text;
    }

    public void UpdateTitleScene(GameTimer timer)
    {
        Time.timeScale = 0;
        timer.UpdateTimer();

        _Views[0].SetActive(true);
        _Views[1].SetActive(false);
        _Views[2].SetActive(false);
    }

    public void UpdatePlayScene(GameTimer timer)
    {
        Time.timeScale = 1;
        timer.UpdateTimer();

        _Views[0].SetActive(false);
        _Views[1].SetActive(true);
        _Views[2].SetActive(false);
    }

    public void UpdateEndScene( GameTimer timer)
    {
        timer.UpdateTimer();

        _Views[2].GetComponentInChildren<Image>().color = new Color(0.6f, 0.6f, 0.61f, timer.TimeRate * 0.9f);
        _Views[2].GetComponentInChildren<Text>().color = new Color(1, 0, 0, Mathf.Abs(Mathf.Sin(timer.ElaspedTime)) * timer.TimeRate * 1f);

        _Views[0].SetActive(false);
        _Views[1].SetActive(false);
        _Views[2].SetActive(true);


    }

   

}
