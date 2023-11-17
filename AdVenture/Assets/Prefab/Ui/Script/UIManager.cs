using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] private HeaderUI Hearder;
    [SerializeField] private GameObject knob;
    [SerializeField] private Image ImagePlace;



    private enum State
    {
        NO_UI,
        UI_GAME,
        UI_PARAMETTRE,
        UI_BUFF,
        UI_PUB_ALL_PAGE
    }

    private State actualState;
    private State lastState;



    private void Update()
    {
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
