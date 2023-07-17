using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class SetMinimumMovesUI : MonoBehaviour
{
    TMP_Text _minimumMovesText;
    [SerializeField] GlobalConfig _globalConfig;

    private void OnEnable()
    {
        _globalConfig.UpdateMinimumMoves += OnUpdateMinimumMovesUI;
    }

    private void OnDisable()
    {
        _globalConfig.UpdateMinimumMoves -= OnUpdateMinimumMovesUI;
    }

    private void Start()
    {
        _minimumMovesText = GetComponent<TMP_Text>();
        OnUpdateMinimumMovesUI();
    }

    private void OnUpdateMinimumMovesUI()
    {
        _minimumMovesText.text = _globalConfig.minimumMoves.ToString();
    }
}
