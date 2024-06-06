using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera1
    : MonoBehaviour
{
    float x, z;


    //float speed = 0.1f;

    public GameObject cam;
    public GameObject _Gun;
    Quaternion cameraRot, characterRot, gunRot;
    float Xsensityvity = 3f, Ysensityvity = 3f;

    bool cursorLock = true;

    //�ϐ��̐錾(�p�x�̐����p)

    // Start is called before the first frame update
    void Start()
    {
        cameraRot = cam.transform.localRotation;
        gunRot = _Gun.transform.localRotation;
        characterRot = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        Ray();
        ScrollAction();


        float xRot = Input.GetAxis("Mouse X") * Ysensityvity;
        float yRot = Input.GetAxis("Mouse Y") * Xsensityvity;

        cameraRot *= Quaternion.Euler(-yRot, 0, 0);
        // gunRot *= Quaternion.Euler(-yRot , 0, 0);
        characterRot *= Quaternion.Euler(0, xRot, 0);

        //Update�̒��ō쐬�����֐����Ă�
        cameraRot = ClampRotation(cameraRot, -90, 90);
        gunRot = ClampRotation(cameraRot, -30, 10);

        cam.transform.localRotation = cameraRot;
        //Debug.Log(cameraRot.eulerAngles.x);
        _Gun.transform.localRotation = gunRot;

        transform.localRotation = characterRot;


        UpdateCursorLock();


    }

    private void FixedUpdate()
    {

    }


    public void UpdateCursorLock()
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

    //�p�x�����֐��̍쐬
    public Quaternion ClampRotation(Quaternion q, float minX, float maxX)
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

    private void Ray()
    {
        //���C���J������̃}�E�X�|�C���^�̂���ʒu����ray���΂�
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        int enemyLayer = 1 << 8;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, enemyLayer))
        {
            //Ray�����������I�u�W�F�N�g�̖��O�ƈʒu�������O�ɕ\������
            //Debug.Log(hit.collider.gameObject.name);
            //Debug.Log(hit.collider.gameObject.transform.position);

            hit.collider.GetComponentInParent<IDisplayable>().disInfo();

        }
    }

    private void ScrollAction()
    {
        //�z�C�[�����擾���āA�ς��̂��߂�time.deltaTime�������Ă���
        var scroll = Input.mouseScrollDelta.y * Time.deltaTime * 100;
        //mainCam.orthographicSize��0���ƃG���[�o����ۂ��̂ŉ����
        float fov = Camera.main.fieldOfView;

        Camera.main.fieldOfView = Mathf.Clamp(fov -= scroll, 20, 90);

    }
}
