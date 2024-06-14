using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Text = UnityEngine.UI.Text;

public class PViewManager : MonoBehaviour
{

    //[SerializeField]
    //private GameObject[] _Views;

    [SerializeField]
    private PEnemy _PEnemy;
    [SerializeField]
    private PPlayer _PPlayer;

    [SerializeField]
    private VGameView _VGameView;



    GameTimer _TitleTimer, _PlayTimer, _EndTimer;

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
                _VGameView.UpdateTitleScene(_TitleTimer);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    ChangeState(_ViewState.PLAY_View);
                }
                break;
            case _ViewState.PLAY_View:
                _VGameView.UpdatePlayScene(_PlayTimer);
                if (_PEnemy.isEndGame || _PPlayer.isEndGame)
                {
                    ChangeState(_ViewState.END_View);
                }
                break;
            case _ViewState.END_View:
                _VGameView.UpdateEndScene(_EndTimer);

                if (_EndTimer.TimeRate > 0.9)
                    SceneManager.LoadScene("PlayScene");

                break;
        }

    }
    //private void UpdateTitleScene(GameObject[] views,GameTimer timer)
    //{
    //    Time.timeScale = 0;
    //    timer.UpdateTimer();

    //    views[0].SetActive(true);
    //    views[1].SetActive(false);
    //    views[2].SetActive(false);
    //}

    //private void UpdatePlayScene(GameObject[] views, GameTimer timer)
    //{
    //    Time.timeScale = 1;
    //    _PlayTimer.UpdateTimer();

    //    views[0].SetActive(false);
    //    views[1].SetActive(true);
    //    views[2].SetActive(false);
    //}

    //private void UpdateEndScene(GameObject[] views, GameTimer timer)
    //{
    //    _EndTimer.UpdateTimer();

    //    views[2].GetComponentInChildren<Image>().color = new Color(0.6f, 0.6f, 0.61f, _EndTimer.TimeRate * 0.9f);
    //    views[2].GetComponentInChildren<Text>().color = new Color(1, 0, 0, Mathf.Abs(Mathf.Sin(_EndTimer.ElaspedTime)) * _EndTimer.TimeRate * 1f);

    //    views[0].SetActive(false);
    //    views[1].SetActive(false);
    //    views[2].SetActive(true);

        
    //}


    private void ChangeState(_ViewState next)
    {
        _PlayTimer.ResetTimer();
        _EndTimer.ResetTimer();
        _sceneState = next;
    }


}
