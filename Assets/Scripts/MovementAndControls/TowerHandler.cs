using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHandler : MonoBehaviour
{
    [SerializeField] private Transform _selectedPoint;
    public Transform SelectedPoint { get { return _selectedPoint; } }
    public Transform ringsTransform;
    public List<Transform> ringsPositions;

    //private bool _isClickable;
    public bool IsClickable { get; set; }

    public GameManager _gameManager;

    private MeshRenderer _renderer;
    private Color _originalColor;

    private void Start()
    {
        Initialization();
    }
    private void Initialization()
    {
        _renderer = GetComponent<MeshRenderer>();
        _originalColor = _renderer.material.color;
        _gameManager = GameManager.instance;
    }

    #region Mouse Callbacks
    private void OnMouseDown()
    {
        if (!IsClickable) return;
        _gameManager.MoveRingToAnotherTower(this);
    }

    private void OnMouseEnter()
    {
        if (!IsClickable) return;
        //Debug.Log("CHAGING MATERIAL COLOR");
        _renderer.material.color = Color.black;
    }

    private void OnMouseExit()
    {
        _renderer.material.color = _originalColor;
    }
    #endregion
}
