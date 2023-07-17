using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MovesCounter : MonoBehaviour
{
    TMP_Text _movesCounterText;
    int _movesCounter;

    private void OnDestroy()
    {
        GameManager.instance.UpdateMovesCounter -= OnUpdateMoves;
    }

    private void Start()
    {
        _movesCounterText = GetComponent<TMP_Text>();
        GameManager.instance.UpdateMovesCounter += OnUpdateMoves;
        _movesCounter = 0;
    }

    private void OnUpdateMoves()
    {
        _movesCounter++;
        _movesCounterText.text = _movesCounter.ToString();
    }

    public int GetMoves()
    {
        return _movesCounter;
    }
}
