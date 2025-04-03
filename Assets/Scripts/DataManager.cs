using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class DataManager : MonoBehaviour
{
    [SerializeField]
    private TopScores topScores;

    public static DataManager Instance { get; set; }

    public string playerName;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            topScores.Load();
        }
        else
            Destroy(gameObject);
    }

    public void AddScore(int score)
    {
        topScores.Add(new Score(playerName, score));
        topScores.Save();
    }

    [ContextMenu("Delete saved data ")]
    public void ClearData()
    {
        PlayerPrefs.DeleteAll();
        topScores.Load();
    }

    private void OnApplicationQuit()
    {
        topScores.Save();
    }
}
