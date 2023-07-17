using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReturnToMainMenu : MonoBehaviour
{
    Button _returnButton;

    private void Start()
    {
        _returnButton = GetComponent<Button>();
        _returnButton.onClick.AddListener(() => ReturnToMain());
    }

    private void ReturnToMain()
    {
        SceneManager.LoadScene(0);
    }
}
