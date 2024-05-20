using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
	private List<Question> easyQuestions;
	private List<Question> mediumQuestions;
	private List<Question> hardQuestions;
	private GameController gameController;

	void Start()
	{
		gameController = GetComponent<GameController>();
		LoadQuestions();
	}

	void LoadQuestions()
	{
		easyQuestions = new List<Question>
		{
			new Question("En küçük il?", new List<string> {"İstanbul", "Bartın", "Sinop", "Artvin"}, 1),
			// Daha fazla kolay soru ekleyin
		};

		mediumQuestions = new List<Question>
		{
			new Question("İstanbul'un fethi?", new List<string> {"1453", "1461", "1444", "1458"}, 0),
			// Daha fazla orta düzey soru ekleyin
		};

		hardQuestions = new List<Question>
		{
			new Question("Işık hızı?", new List<string> {"300.000 km/s", "299.792 km/s", "310.000 km/s", "290.000 km/s"}, 1),
			// Daha fazla zor soru ekleyin
		};
	}

	public Question GetNextQuestion(int correctAnswers)
	{
		if (correctAnswers < 10)
		{
			return easyQuestions[Random.Range(0, easyQuestions.Count)];
		}
		else if (correctAnswers < 20)
		{
			return mediumQuestions[Random.Range(0, mediumQuestions.Count)];
		}
		else
		{
			return hardQuestions[Random.Range(0, hardQuestions.Count)];
		}
	}
}
