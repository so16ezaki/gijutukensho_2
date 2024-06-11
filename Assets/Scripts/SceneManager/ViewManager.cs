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

    enum _ViewState
    {
        TITLE_View,
        PLAY_View,
        END_View
    }
    _ViewState _sceneState = _ViewState.TITLE_View;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {


        switch (_sceneState)
        {
            case _ViewState.TITLE_View:
                UpdateTitleScene();
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    ChangeState(_ViewState.PLAY_View);
                }
                break;
            case _ViewState.PLAY_View:
                UpdatePlayScene();
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    ChangeState(_ViewState.END_View);
                }
                break;
            case _ViewState.END_View:
                UpdateEndScene();
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    ChangeState(_ViewState.TITLE_View);
                }
                break;
        }


    }
    private void UpdateTitleScene()
    {
        Time.timeScale = 0;
        _TitleView.SetActive(true);
        _PlayView.SetActive(false);
        _EndView.SetActive(false);
    }


   
    

    private void UpdatePlayScene()
    {
        Time.timeScale = 1;
        _PlayView.SetActive(true);
        _TitleView.SetActive(false);
        _EndView.SetActive(false);
    }

    private void UpdateEndScene()
    {
        Time.timeScale = 0;
        _EndView.SetActive(true);
        _TitleView.SetActive(false);
        _PlayView.SetActive(false);
    }

    private void ChangeState(_ViewState next)
    {
        _sceneState = next;
    }
}
