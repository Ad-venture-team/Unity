using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeaderUI : MonoBehaviour
{
    //new Layer Page
    [SerializeField] private Button buffButton;
    [SerializeField] private Button parameterButton;

    //Number In Game to change
    [SerializeField] private TextMeshProUGUI roomNumberText;
    [SerializeField] private TextMeshProUGUI waveNumberText;

    //Outside Script In Game Mechanics
    [SerializeField] public TimeCounter timeText;
    [SerializeField] public EnnemiBar ennemiBar;

    private int maxWave = 0;

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


}
