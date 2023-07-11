using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class SelectRing : MonoBehaviour
{
    private MeshRenderer _renderer;
    private Color _originalColor;

    //private bool _isMoveable;
    public bool IsMoveable { get; set; }
    [SerializeField] private TowerHandler _currentTower;
    public TowerHandler CurrentTower { get { return _currentTower; } }


    [SerializeField] private float _moveSpeed = 5;
    public float MoveSpeed { get { return _moveSpeed; } }

    private GameManager _gameManager;

    public bool IsSelected { get; set; }

    #region Initialize
    private void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        _renderer = GetComponent<MeshRenderer>();
        _originalColor = _renderer.material.color;
        _gameManager = GameManager.instance;
        IsMoveable = true;
    }

    #endregion

    public void SetCurrentTower(TowerHandler tower)
    {
        _currentTower = tower;
    }

    #region Mouse Callbacks
    private void OnMouseDown()
    {
        if (!IsMoveable) return;
        if(IsSelected)
        {
            _gameManager.ClearSelectedRing();
        }
        else
        {
            _gameManager.SetSelectedRing(this);
        }
    }

    private void OnMouseEnter()
    {
        if (!IsMoveable) return;
        _renderer.material.color = Color.black;
    }

    private void OnMouseExit()
    {
        _renderer.material.color = _originalColor;
    }
    #endregion



}
