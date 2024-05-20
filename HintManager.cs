using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HintManager : MonoBehaviour
{
    public Button hintButton;
    public Text hintText;
    public Text questionText; // Text bile�eni kullan�rsan�z
    // public InputField questionInput; // InputField bile�eni kullan�rsan�z

    private static readonly string appId = "YOUR_WOLFRAM_ALPHA_APP_ID";

    void Start()
    {
        hintButton.onClick.AddListener(OnHintButtonClicked);
    }

    async void OnHintButtonClicked()
    {
        // Kullan�c�n�n girdi�i soruyu oku
        string question = questionText.text; // Text bile�eni kullan�rsan�z
        // string question = questionInput.text; // InputField bile�eni kullan�rsan�z

        string hint = await GetHint(question);
        hintText.text = $"Hint: {hint}";
    }

    static async Task<string> GetHint(string question)
    {
        using (HttpClient client = new HttpClient())
        {
            string url = $"http://api.wolframalpha.com/v2/query?input={Uri.EscapeDataString(question)}&appid={appId}&format=plaintext";

            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            XDocument xmlDoc = XDocument.Parse(responseBody);
            string hint = null;

            foreach (var pod in xmlDoc.Descendants("pod"))
            {
                string title = pod.Attribute("title")?.Value;
                if (title == "Result"  title == "Solution"  title == "Answer")
                {
                    hint = pod.Descendants("plaintext").FirstOrDefault()?.Value;
                    break;
                }
            }

            return hint;
        }
    }
}