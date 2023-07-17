using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGame : MonoBehaviour
{
    [SerializeField] GlobalConfig _globalConfig;
    [SerializeField] MovesCounter _moves;

    [SerializeField] TMP_Text _endGameMessageText;

    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _perfectSolveBGM;
    [SerializeField] AudioClip _solveBGM;

    private void Awake()
    {
        if(_moves.GetMoves() == _globalConfig.minimumMoves)
        {
            _audioSource.PlayOneShot(_perfectSolveBGM);
            _endGameMessageText.text = "CONGRATULATIONS! You solved it with minimum Moves";
        }
        else
        {
            _audioSource.PlayOneShot(_solveBGM);
            _endGameMessageText.text = "WELL DONE! You solved the game";
        }
    }
}
