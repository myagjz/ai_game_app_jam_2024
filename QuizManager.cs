using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class QuizManager : MonoBehaviour
{
    public Text questionText; // Soru metni için Text
    public Text timerText; // Zamanlayıcı için Text
    public Button[] answerButtons; // Cevap butonları
    public List<Question> questions; // Sorular listesi
    private Question currentQuestion; // Şu anki soru
    private int currentQuestionIndex; // Şu anki sorunun indeksi
    private float timeRemaining; // Kalan süre
    public float timePerQuestion = 30f; // Soru başına süre
    public float penaltyTime = 5f; // Yanlış cevap cezası

    void Start()
    {
        timeRemaining = timePerQuestion; // Süreyi başlat
        GenerateRandomQuestion(); // Rastgele soru oluştur
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime; // Süreyi azalt
            timerText.text = "Time: " + Mathf.Round(timeRemaining).ToString(); // Süreyi güncelle
        }
        else
        {
            // Oyun bitti
            Debug.Log("Game Over!");
        }
    }

    void GenerateRandomQuestion()
    {
        if (questions.Count > 0)
        {
            currentQuestionIndex = Random.Range(0, questions.Count); // Rastgele bir soru seç
            currentQuestion = questions[currentQuestionIndex];
            DisplayQuestion(); // Soruyu göster
        }
        else
        {
            Debug.Log("No more questions!");
        }
    }

    void DisplayQuestion()
    {
        questionText.text = currentQuestion.questionText;
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<Text>().text = currentQuestion.options[i];
            int index = i; // İndeksi yakala
            answerButtons[i].onClick.AddListener(() => CheckAnswer(index));
        }
    }

    void CheckAnswer(int index)
    {
        if (index == currentQuestion.correctAnswerIndex)
        {
            // Doğru cevap
            questions.RemoveAt(currentQuestionIndex); // Soruyu listeden çıkar
            if (questions.Count > 0)
            {
                GenerateRandomQuestion(); // Yeni bir soru seç
            }
            else
            {
                Debug.Log("You answered all questions!");
                // Oyunu kazandınız
            }
        }
        else
        {
            // Yanlış cevap
            timeRemaining -= penaltyTime;
            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                Debug.Log("Game Over!");
                // Oyun bitti
            }
        }
    }
}
