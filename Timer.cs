using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private Canvas canvas; // Canvas referansý

    public float timeRemaining = 85500; // 23:45
    public bool timerIsRunning = false;
    public int sceneToLoad = 0; // kaybetme ekranýna gidecek

    private static Timer instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        timerIsRunning = true;
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining < 24 * 60 * 60) // 24 saat kontrolü
            {
                timeRemaining += Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Kaybettin");
                timerIsRunning = false;
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float hours = Mathf.FloorToInt(timeToDisplay / 3600);
        float minutes = Mathf.FloorToInt((timeToDisplay % 3600) / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }

    public void WrongAnswer()
    {
        timeRemaining += 60;
    }
}
