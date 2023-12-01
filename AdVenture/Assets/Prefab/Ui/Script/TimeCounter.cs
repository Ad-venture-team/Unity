using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class TimeCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;

    private bool TimerOn = false;
    private float upCounter;
    
    public void StartTimer()
    {
        ResetTimer();
        TimerOn = true;
    }

    public void ResetTimer()
    {
        textMeshProUGUI.text = "00:00";
    }
    
    private void Update()
    {
        if (TimerOn)
        {
            upCounter += Time.deltaTime;
            string minutes = Mathf.Floor(upCounter / 60).ToString("00");
            string seconds = (upCounter % 60).ToString("00");
            textMeshProUGUI.text = minutes + ":" + seconds;
        }
    }
}
