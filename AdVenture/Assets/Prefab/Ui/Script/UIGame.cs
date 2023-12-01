using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGame : MonoBehaviour
{
    //new Layer Page
    [SerializeField] private Button buffButton;
    [SerializeField] private Button parameterButton;

    //Number In Game to change
    [SerializeField] private TextMeshProUGUI roomNumberText;
    [SerializeField] private TextMeshProUGUI waveNumberText;

    //Outside Script In Game Mechanics
    [SerializeField] public TimeCounter timeText;

    private int maxWave = 0;


    private void Start()
    {
        buffButton.onClick.AddListener(CallBuffPanel);
        parameterButton.onClick.AddListener(CallParametrePanel);
    }




    public void SetNumberRoom(int number)
    {
        roomNumberText.text = "Room " + number.ToString();
    }

    public void FinalRoom()
    {
        roomNumberText.text = "Final Room";
    }

    public void SetMaxWaveNumber(int number)
    {
        waveNumberText.text = "Wave: " + "0/" + number.ToString();
        maxWave = number;
    }

    public void SetActualWaveNumber(int number)
    {
        waveNumberText.text = "Wave: " + number.ToString() + "/" + maxWave.ToString();
    }




    //Button
    public void UnClickableGameButton()
    {
        buffButton.interactable = false;
        parameterButton.interactable = false;
    }

    public void ClickableGameButton()
    {
        buffButton.interactable = true;
        parameterButton.interactable = true;
    }

    private void CallBuffPanel()
    {
        UIManager.Instance.actualState = UIManager.State.UI_BUFF;
    }

    private void CallParametrePanel()
    {
        UIManager.Instance.actualState = UIManager.State.UI_PARAMETTRE;
    }
}
