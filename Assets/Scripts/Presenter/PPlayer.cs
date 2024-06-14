using System.Collections.Generic;
using UnityEngine;

public class PPlayer : MonoBehaviour
{
    [SerializeField] GameObject _GameObj;
    [SerializeField]MTankBehaviour _MTankBehaviour;
    [SerializeField]VGameView _VGameView;

    TankPM.VehicleEntity _Entity;
    List<TankPM.VehicleEntity> _Entitys;

    Rigidbody _Rigidbody;

    bool cursorLock = true;

    float Xsensityvity = 3f, Ysensityvity = 3f;
    float scroll = 0;

    // Start is called before the first frame update
    void Start()
    {
        TankPM tankPM = (TankPM)Resources.Load<TankPM>("TankPM");
        _Entitys = tankPM.vehicles;
        _Entity = _Entitys[0];

        
        _MTankBehaviour = _GameObj.GetComponent<MTankBehaviour>();
        _MTankBehaviour.Initialization(_Entity);

        _Rigidbody = _GameObj.GetComponent<Rigidbody>();

        _VGameView.EnemyRayDispInfo("");
    }


    // Update is called once per frame
    private void FixedUpdate()
    {

        if (Input.GetMouseButton(0))
            _MTankBehaviour.Shoot();

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        _MTankBehaviour.Move(horizontal,vertical,_Entity);


        
    }
    private void Update()
    {
        _VGameView.DispInfo(_Entity.TankName, _MTankBehaviour.CurrentHp, (int)_Rigidbody.velocity.sqrMagnitude);
        
        Ray();
        ScrollAction();
        UpdateCursorLock();

        
        scroll += Mathf.Clamp(1- 0.5f * Input.mouseScrollDelta.y,0.4f,1);

        float xRot = Input.GetAxis("Mouse X") * 0.5f;
        float yRot = Input.GetAxis("Mouse Y") * 0.5f;

        _MTankBehaviour.RotateTurret(xRot,yRot);
    }

    private void Ray()
    {
        //メインカメラ上のマウスポインタのある位置からrayを飛ばす
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        int VehicleLayer = 1 << 8;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, VehicleLayer))
        {
            //Rayが当たったオブジェクトの名前と位置情報をログに表示する
            //Debug.Log(hit.collider.gameObject.name);
            //Debug.Log(hit.collider.gameObject.transform.position);
            int entityId = hit.collider.GetComponentInParent<Vehicle>().EntityId;

            string Enemystatus = hit.collider.GetComponentInParent<IDisplayable>().disInfo(_Entitys[entityId]);
            _VGameView.EnemyRayDispInfo(Enemystatus);
        }

    }

    private void ScrollAction()
    {
        //ホイールを取得して、均しのためにtime.deltaTimeをかけておく
        var scroll = Input.mouseScrollDelta.y * Time.deltaTime * 100;
        //mainCam.orthographicSizeは0だとエラー出るっぽいので回避策
        float fov = Camera.main.fieldOfView;

        Camera.main.fieldOfView = Mathf.Clamp(fov -= scroll, 20, 90);

    }

    private void UpdateCursorLock()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cursorLock = false;
        }
        else if (Input.GetMouseButton(0))
        {
            cursorLock = true;
        }


        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (!cursorLock)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public bool isEndGame
    {
        get
        {
            bool end = false;
            if (_MTankBehaviour.IsDead)
                end = true;

            return end;
        }
    }
}
