using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "topScore", menuName ="ScriptableObjects/Top Score" )]
public class TopScores : ScriptableObject
{
    [SerializeField]
    private List<Score> scores = new List<Score>(11);

    public event Action OnChanged;

    public const int maxNumberOfScores = 10;
    
    public void Add(Score scoreToAdd)
    {

        if (scores.Count == 0)
        {
            scores.Add(scoreToAdd);
            OnChanged?.Invoke();
            return;
        }

        bool added = false;
        for (int i = 0; i < scores.Count; i++)
        {

            if (scoreToAdd.score <= scores[i].score)
            {
                // insert in right place
                scores.Insert(i, scoreToAdd);
                added = true;
                break;
            }

        }


        // it's higher than all other scores, add it
        if (!added)
            scores.Add(scoreToAdd);

        if (scores.Count > maxNumberOfScores)
        {
            // remove lowest score
            scores.RemoveAt(0);
        }

        OnChanged?.Invoke();
    }


    public void Save()
    {
        for (int i = 0; i < maxNumberOfScores; i++)
        {
            if (scores.Count > i)
            {
                string json = JsonUtility.ToJson(scores[i]);

                PlayerPrefs.SetString(i.ToString(), json);
            }
            else
                return;
        }
        PlayerPrefs.Save();
    }

    public void Load()
    {
        scores.Clear();
        for (int i = 0; i < maxNumberOfScores; i++)
        {
            string json = PlayerPrefs.GetString(i.ToString());
            Score score = JsonUtility.FromJson<Score>(json);

            if (score == null)
                return;

            scores.Add(score);
        }

        OnChanged?.Invoke();
    }

    public List<Score> GetScores()
    {
        return scores;
    }
}

[Serializable]
public class Score
{
    public string name;
    public int score;

    public Score (string name, int score)
    {
        this.name = name;
        this.score = score;
    }

    public override string ToString()
    {
        return name + ": " + score;
    }
}
