using UnityEngine;
using TMPro;

public class TimerDisplay : MonoBehaviour
{
    public TextMeshProUGUI timerText;  

    void Update()
    {
        // Get the formatted time from the Timer class and update the UI Text
        timerText.text = Timer.Instance.GetTimeElapsed();
    }
}