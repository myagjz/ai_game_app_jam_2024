using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scene_loader : MonoBehaviour
{
    [SerializeField] private int scene_index;
    public int delayTime = 1;

    private Timer timer_; // Timer referansý

    private void Start()
    {
        timer_ = FindObjectOfType<Timer>(); // Her sahneye geçtiðinizde Timer referansýný bul
    }

    public static void Load_Level(int levelIndex)
    {
        if (levelIndex >= 0 && levelIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(levelIndex);
        }
    }

    IEnumerator LoadSceneAfterDelay(float delay, Button clickedButton)
    {
        yield return new WaitForSeconds(delay);
        clickedButton.GetComponent<Image>().color = Color.white;
    }

    public void Load_Next_Level(Button clickedButton)
    {
        clickedButton.GetComponent<Image>().color = Color.green;
        StartCoroutine(LoadSceneAfterDelay(delayTime, clickedButton));

        int next_sceneIndex = (SceneManager.GetActiveScene().buildIndex + 1)
            % SceneManager.sceneCountInBuildSettings;

        Load_Level(next_sceneIndex);
    }

    public void Wrong_answer(Button clickedButton)
    {
        clickedButton.GetComponent<Image>().color = Color.red;
        if (timer_ != null)
        {
            timer_.WrongAnswer();
        }
        StartCoroutine(LoadSceneAfterDelay(delayTime, clickedButton));
    }





    public void OnApplicationQuit()
    {
        Application.Quit();
    }
}
