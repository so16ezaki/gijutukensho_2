using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PView : MonoBehaviour
{
    [SerializeField]
    private GameObject _TitleView;
    [SerializeField]
    private GameObject _PlayView;
    [SerializeField]
    private GameObject _EndView;
    [SerializeField]
    private PEnemy _PEnemy;


    GameTimer _TitleTimer,_PlayTimer,_EndTimer;
    float a = 0;

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
        _TitleTimer = new GameTimer();
        _PlayTimer = new GameTimer(3);
        _EndTimer = new GameTimer(5);
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
                if (_PEnemy.isEndGame)
                {
                    ChangeState(_ViewState.END_View);
                }
                break;
            case _ViewState.END_View:
                UpdateEndScene();
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SceneManager.LoadScene("PlayScene");
                }
                break;
        }

        Debug.Log(a);
    }
    private void UpdateTitleScene()
    {
        _TitleTimer.UpdateTimer();
        _TitleView.GetComponentInChildren<Text>().color = new Color(1, 0, 0, Mathf.Abs(Mathf.Sin(_TitleTimer.ElaspedTime)));
        _TitleView.SetActive(true);
        _PlayView.SetActive(false);
        _EndView.SetActive(false);
    }


   
    

    private void UpdatePlayScene()
    {
        _PlayTimer.UpdateTimer();
        Time.timeScale = 1;
        _PlayView.SetActive(true);
        _TitleView.SetActive(false);
        _EndView.SetActive(false);
    }

    private void UpdateEndScene()
    {
        _EndTimer.UpdateTimer();
        _EndView.GetComponentInChildren<Image>().color = new Color(0.6f, 0.6f, 0.61f, _EndTimer.TimeRate*0.9f);
        _EndView.GetComponentInChildren<Text>().color = new Color(1, 0, 0, Mathf.Abs(Mathf.Sin(_EndTimer.ElaspedTime))*_EndTimer.TimeRate * 1f);
        _EndView.SetActive(true);
        _TitleView.SetActive(false);
        _PlayView.SetActive(false);
    }

    private void ChangeState(_ViewState next)
    {
        a = 0;
        _PlayTimer.ResetTimer();
        _EndTimer.ResetTimer();
        _sceneState = next;
    }
}
