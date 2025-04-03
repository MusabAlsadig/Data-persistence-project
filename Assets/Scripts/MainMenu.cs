using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements.Experimental;

public class MainMenu : MonoBehaviour
{

    #region UI methods

    public void ChangeName(string value)
    {
        DataManager.Instance.playerName = value;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    #endregion
}
