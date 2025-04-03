using TMPro;
using UnityEngine;

public class LeaderBoard : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI[] texts;

    [SerializeField]
    private TopScores topScores;

    private void Awake()
    {
        topScores.OnChanged += Refreash;
    }

    private void OnDestroy()
    {
        topScores.OnChanged -= Refreash;
    }

    private void Start()
    {
        Refreash();
    }

    public void Refreash()
    {
        var allScore = topScores.GetScores();
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].SetText($"#{i+1} ");
        }

        int counter = 0;
        for (int i = allScore.Count - 1; i >= 0; i--)
        {
            texts[counter].text += allScore[i].ToString();
            counter++;
        }
    }
}
