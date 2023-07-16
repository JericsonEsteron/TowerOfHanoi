using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GlobalConfig _globalConfig;

    [SerializeField] TMP_Text _numberOfRingText;
    [SerializeField] Button _decreaseNumberButton;
    [SerializeField] Button _increaseNumberButton;
    [SerializeField] Button _StartGame;

    private void Start()
    {
        _numberOfRingText.text = _globalConfig.numberOfRings.ToString();
        _decreaseNumberButton.onClick.AddListener(() => DecreaseRings());
        _increaseNumberButton.onClick.AddListener(() => IncreaseRings());
        _StartGame.onClick.AddListener(() => StartGame());
    }

    private void IncreaseRings()
    {
        _increaseNumberButton.interactable = false;
        if (_globalConfig.numberOfRings < 8)
        {
            _globalConfig.numberOfRings += 1;
            _numberOfRingText.text = _globalConfig.numberOfRings.ToString();
        }
        _globalConfig.CalculateMinimumMoves();
        StartCoroutine( ResetButton(_increaseNumberButton));
    }

    private void DecreaseRings()
    {
        _decreaseNumberButton.interactable = false;
        if (_globalConfig.numberOfRings > 3)
        {
            _globalConfig.numberOfRings -= 1;
            _numberOfRingText.text = _globalConfig.numberOfRings.ToString();
        }
        _globalConfig.CalculateMinimumMoves();
        StartCoroutine(ResetButton(_decreaseNumberButton));
    }

    IEnumerator ResetButton(Button button)
    {
        yield return new WaitForEndOfFrame();
        button.interactable = true;
    }

    private void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
