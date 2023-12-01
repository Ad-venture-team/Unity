using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.InputSystem.OnScreen;

public class UIManager : MonoBehaviour
{

    #region Singleton
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIManager>();
            }
            return _instance;
        }
    }
    #endregion


    public enum State
    {
        NO_UI = 0,
        UI_GAME = 1 ,
        UI_PARAMETTRE = 2,
        UI_BUFF = 3,
        UI_PUB_ALL_PAGE = 4,
    }


    [SerializeField] private UIGame UIGame;
    [SerializeField] private OnScreenStick Knob;
    [SerializeField] private Image ImagePlace;

    [Header("Ui Layer ")]
    [SerializeField] private GameObject ParameterUI;
    [SerializeField] private GameObject BuffUI;

    [HideInInspector] public State actualState = State.UI_GAME;
    private State lastState = State.UI_GAME;



    private void Update()
    {
        if(actualState != lastState)
        {
            if(actualState == State.NO_UI)
            {
                DiscardActualState();
                lastState = State.UI_GAME;
                DiscardActualState();
                ActivateActualState();
                lastState = actualState;
            }
            else if (actualState == State.UI_GAME)
            {
                DiscardActualState();
                ActivateActualState();
                lastState = actualState;
            }
            else if (actualState == State.UI_PARAMETTRE)
            {
                DiscardActualState();
                ActivateActualState();
                lastState = actualState;
            }
            else if (actualState == State.UI_BUFF)
            {
                DiscardActualState();
                ActivateActualState();
                lastState = actualState;
            }
        }
    }






    //Discard last UI et Create new UI
    private void DiscardActualState()
    {
        switch(lastState)
        {
            case State.UI_GAME: UIGame.gameObject.SetActive(false); break;
            case State.UI_PARAMETTRE : ParameterUI.SetActive(false); break;
            case State.UI_BUFF: BuffUI.SetActive(false); break;
            default: break;
        }
    }

    private void ActivateActualState()
    {
        switch (actualState)
        {
            case State.UI_GAME: UIGame.gameObject.SetActive(true); break;
            case State.UI_PARAMETTRE: ParameterUI.SetActive(true); break;
            case State.UI_BUFF: BuffUI.SetActive(true); break;
        }
    }


    //Button Controlle
    public void UnClickableGameButton()
    {
        Knob.gameObject.SetActive(false);
        UIGame.UnClickableGameButton();
    }

    public void ClickableGameButton()
    {
        Knob.gameObject.SetActive(true);
        UIGame.ClickableGameButton();
    }


    //Outside Script
    public void StartTimer()
    {
        UIGame.timeText.StartTimer();
    }

    public void ResetTimer()
    {
        UIGame.timeText.ResetTimer();
    }

    public void CreatNewMonsterList(List<Sprite> monsterList,Image image)
    {
        UIGame.ennemiBar.CreatNewMonsterList(monsterList, image);
    }

    public void ResetMonsterList()
    {
        UIGame.ennemiBar.ResetMonsterList();
    }

    public void KilledMonster(int monsterId)
    {
        UIGame.ennemiBar.KilledMonster(monsterId);
    }


    //HeaderUI Script
    public void SetNumberRoom(int number)
    {
        UIGame.SetNumberRoom(number);
    }

    public void FinalRoom()
    {
        UIGame.FinalRoom();
    }

    public void SetMaxWaveNumber(int number)
    {
        UIGame.SetMaxWaveNumber(number);
    }

    public void SetActualWaveNumber(int number)
    {
        UIGame.SetActualWaveNumber(number);
    }


}
