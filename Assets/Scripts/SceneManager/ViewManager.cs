using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class ViewManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _TitleView;
    [SerializeField]
    private GameObject _PlayView;
    [SerializeField]
    private GameObject _EndView;

    enum _SceneState
    {
        TITLE_SCENE,
        PLAY_SCENE,
        END_SCENE
    }
    _SceneState _sceneState = _SceneState.TITLE_SCENE;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {


        switch (_sceneState)
        {
            case _SceneState.TITLE_SCENE:
                TitleScene();
                if (Input.GetKeyDown(KeyCode.Space))
                    ChangeState(_SceneState.PLAY_SCENE);
                break;
            case _SceneState.PLAY_SCENE:
                PlayScene();
                if (Input.GetKeyDown(KeyCode.Alpha1))
                    ChangeState(_SceneState.END_SCENE);
                break;
            case _SceneState.END_SCENE:
                EndScene();
                if (Input.GetKeyDown(KeyCode.Alpha2))
                    ChangeState(_SceneState.TITLE_SCENE);
                break;
        }

        Debug.Log(_sceneState);
    }
    private void TitleScene()
    {
        Time.timeScale = 0;
        _TitleView.SetActive(true);
        _PlayView.SetActive(false);
        _EndView.SetActive(false);
    }
    private void PlayScene()
    {
        Time.timeScale = 1;
        _PlayView.SetActive(true);
        _TitleView.SetActive(false);
        _EndView.SetActive(false);
    }

    private void EndScene()
    {
        Time.timeScale = 0;
        _EndView.SetActive(true);
        _TitleView.SetActive(false);
        _PlayView.SetActive(false);
    }

    private void ChangeState(_SceneState next)
    {
        _sceneState = next;
    }
}
