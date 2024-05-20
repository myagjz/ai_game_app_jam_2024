using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Question
{
	public string questionText;
	public List<string> answers;
	public int correctAnswerIndex;

	public Question(string questionText, List<string> answers, int correctAnswerIndex)
	{
		this.questionText = questionText;
		this.answers = answers;
		this.correctAnswerIndex = correctAnswerIndex;
	}
}

public class GameController : MonoBehaviour
{
	public Text questionText;
	public List<Button> answerButtons;
	public Text timerText;
	public float timePerQuestion = 30f;
	public float penaltyTime = 5f;

	private int currentQuestionIndex;
	private float currentTime;
	private int correctAnswers;
	private AIController aiController;

	void Start()
	{
		aiController = GetComponent<AIController>(); // AIController bileşenini al
		correctAnswers = 0;
		StartGame();
	}

	void StartGame()
	{
		currentQuestionIndex = 0;
		currentTime = timePerQuestion;
		DisplayQuestion();
		InvokeRepeating("UpdateTimer", 1f, 1f);
	}

	void DisplayQuestion()
	{
		Question currentQuestion = aiController.GetNextQuestion(correctAnswers); // AI'den sonraki soruyu al
		questionText.text = currentQuestion.questionText;
		for (int i = 0; i < answerButtons.Count; i++)
		{
			answerButtons[i].GetComponentInChildren<Text>().text = currentQuestion.answers[i];
			int index = i;
			answerButtons[i].onClick.RemoveAllListeners(); // Önceki dinleyicileri temizle
			answerButtons[i].onClick.AddListener(() => OnAnswerSelected(index));
		}
	}

	void OnAnswerSelected(int index)
	{
		Question currentQuestion = aiController.GetNextQuestion(correctAnswers); // AI'den mevcut soruyu al
		if (index == currentQuestion.correctAnswerIndex)
		{
			correctAnswers++;
			DisplayQuestion();
		}
		else
		{
			currentTime -= penaltyTime;
			if (currentTime <= 0)
			{
				Debug.Log("Süreniz doldu, oyunu kaybettiniz!");
			}
		}
	}

	void UpdateTimer()
	{
		currentTime -= Time.deltaTime;
		timerText.text = currentTime.ToString("F2");
		if (currentTime <= 0)
		{
			Debug.Log("Süreniz doldu, oyunu kaybettiniz!");
		}
	}
}
