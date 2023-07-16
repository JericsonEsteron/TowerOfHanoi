using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "GlobalConfig", menuName = "GlobalData")]
public class GlobalConfig : ScriptableObject
{
    public int numberOfRings;

    public List<GameObject> ringsPrefabList = new List<GameObject>();

    public float minimumMoves;

    public event Action UpdateMinimumMoves;

    public void CalculateMinimumMoves()
    {
        minimumMoves = Mathf.Pow(2, numberOfRings - 1f);
        UpdateMinimumMoves?.Invoke();
    }
}
