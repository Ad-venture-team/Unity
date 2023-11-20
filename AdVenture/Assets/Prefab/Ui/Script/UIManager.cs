using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    private enum State
    {
        NO_UI = 0,
        UI_GAME = 1 ,
        UI_PARAMETTRE = 2,
        UI_BUFF = 3,
        UI_PUB_ALL_PAGE = 4,
    }


    [SerializeField] private HeaderUI Hearder;
    [SerializeField] private GameObject Knob;
    [SerializeField] private Image ImagePlace;

    [Header("Ui Layer ")]
    [SerializeField] private GameObject ParameterUI;
    [SerializeField] private GameObject BuffUI;

    private State actualState = State.UI_GAME;
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
            case State.UI_GAME: Hearder.gameObject.SetActive(false); break;
            case State.UI_PARAMETTRE : ParameterUI.SetActive(false); break;
            case State.UI_BUFF: BuffUI.SetActive(false); break;
            default: break;
        }
    }

    private void ActivateActualState()
    {
        switch (actualState)
        {
            case State.UI_GAME: Hearder.gameObject.SetActive(true); break;
            case State.UI_PARAMETTRE: ParameterUI.SetActive(true); break;
            case State.UI_BUFF: BuffUI.SetActive(true); break;
        }
    }






    //Outside Script
    public void StartTimer()
    {
        Hearder.timeText.StartTimer();
    }

    public void ResetTimer()
    {
        Hearder.timeText.ResetTimer();
    }

    public void CreatNewMonsterList(List<Sprite> monsterList,Image image)
    {
        Hearder.ennemiBar.CreatNewMonsterList(monsterList, image);
    }

    public void ResetMonsterList()
    {
        Hearder.ennemiBar.ResetMonsterList();
    }

    public void KilledMonster(int monsterId)
    {
        Hearder.ennemiBar.KilledMonster(monsterId);
    }


    //HeaderUI Script
    public void SetNumberRoom(int number)
    {
        Hearder.SetNumberRoom(number);
    }

    public void FinalRoom()
    {
        Hearder.FinalRoom();
    }

    public void SetMaxWaveNumber(int number)
    {
        Hearder.SetMaxWaveNumber(number);
    }

    public void SetActualWaveNumber(int number)
    {
        Hearder.SetActualWaveNumber(number);
    }


}
