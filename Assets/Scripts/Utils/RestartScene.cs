using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartScene : MonoBehaviour
{
    Button _restartButton;

    private void Start()
    {
        _restartButton = GetComponent<Button>();
        _restartButton.onClick.AddListener(() => RestartGame());
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
