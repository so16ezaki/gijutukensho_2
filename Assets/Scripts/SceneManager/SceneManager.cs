using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
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
                break;
            case _SceneState.PLAY_SCENE:
                PlayScene();
                break;
            case _SceneState.END_SCENE:
                EndScene();
                break;
        }

        Debug.Log(_sceneState);
    }
    private void TitleScene()
    {
        if (Input.anyKey)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("PlayScene");
            _sceneState = _SceneState.PLAY_SCENE;
        }
    }
    private void PlayScene()
    {
        
    }

    private void EndScene()
    {

    }
}
