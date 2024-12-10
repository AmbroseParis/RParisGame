using UnityEngine;

public class Timer : MonoBehaviour
{
    public float timeElapsed = 0f;  
    public bool timerIsRunning = true;  

    private static Timer instance;

    
    public static Timer Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Timer>();
                if (instance == null)
                {
                    GameObject timerObject = new GameObject("Timer");
                    instance = timerObject.AddComponent<Timer>();
                }
            }
            return instance;
        }
    }

    void Awake()
    {
        // to ensure it persists across scenes
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (timerIsRunning)
        {
            timeElapsed += Time.deltaTime;  // Count up
        }
    }

    // this method stops the timer
    public void StopTimer()
    {
        timerIsRunning = false;
    }
    

    // return the time (minutes:seconds)
    public string GetTimeElapsed()
    {
        float minutes = Mathf.FloorToInt(timeElapsed / 60);
        float seconds = Mathf.FloorToInt(timeElapsed % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}