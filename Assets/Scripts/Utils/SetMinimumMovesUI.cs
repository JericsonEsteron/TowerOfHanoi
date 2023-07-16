using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class SetMinimumMovesUI : MonoBehaviour
{
    [SerializeField] TMP_Text _minimumMovesText;
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
        OnUpdateMinimumMovesUI();
    }

    public void OnUpdateMinimumMovesUI()
    {
        _minimumMovesText.text = _globalConfig.minimumMoves.ToString();
    }
}
