using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GlobalConfig _globalConfig;
    [SerializeField] private TowerHandler _initialTower;
    [SerializeField] private TowerHandler _lastTower;

    [Header("")]
    [SerializeField] private SelectRing _selectedRing;

    [SerializeField] private List<TowerHandler> _towersList = new List<TowerHandler>();

    private Vector3 _originalRingPosition;

    [SerializeField] private float _ringPositionOffset = 0.108f;

    [SerializeField] private GameObject _endGamePopUp;

    public bool isInteractableOn { get; set; }

    public event Action UpdateMovesCounter;

    private void OnEnable()
    {
        if (GameManager.instance != null)
            Destroy(this);
        else    
            instance = this; 
    }

    private void Start()
    {
        isInteractableOn = true;
        InstantiateRings();
    }

    private void InstantiateRings()
    {
        float initialOffset = 0;
        for (int i = _globalConfig.numberOfRings - 1; i >= 0; i--)
        {
            GameObject ring = Instantiate(
                _globalConfig.ringsPrefabList[i], 
                _initialTower.ringsContainerTransform.transform);

            ring.transform.localPosition = new Vector3(0, 
                _initialTower.ringsContainerTransform.localPosition.y + initialOffset, 0);

            ring.isStatic = false;

            initialOffset += _ringPositionOffset;

            SelectRing selectRing = ring.GetComponent<SelectRing>();
            selectRing.SetCurrentTower(_initialTower);

            _initialTower.ringList.Add(ring.GetComponent<SelectRing>());
        }
        _initialTower.UpdateTopRing();
    }

    public void SetSelectedRing(SelectRing selectedRing)
    {
        _selectedRing = selectedRing;
        _selectedRing.IsSelected = true;
        ActivateTowers(selectedRing.CurrentTower);
        MoveRing();
    }

    public void ClearSelectedRing()
    {
        _selectedRing.IsSelected = false;
        _selectedRing = null;
        _originalRingPosition = Vector3.zero;
        DeactivateTowers();
    }

    #region Ring Movements

    private void MoveRing()
    {
        isInteractableOn = false;
        if (_selectedRing.IsSelected)
        {
            _originalRingPosition = _selectedRing.transform.localPosition;
            _ = MoveSelectedRing(_selectedRing.CurrentTower.SelectedPoint.localPosition);
        }
        else
            _ = MoveSelectedRing(_originalRingPosition);

        isInteractableOn = true;
    }

    private async Task<bool> MoveSelectedRing(Vector3 newPosition)
    {

        //Debug.Log("ON MOVE RING");
        _selectedRing.PlayMoveSFX();
        while (Vector3.Distance(_selectedRing.transform.localPosition, newPosition) >= 0.01f)
        {

            //Debug.Log("ON MOVE RING INSIDE WHILE LOOP");
            _selectedRing.transform.localPosition = Vector3.MoveTowards(
                _selectedRing.transform.localPosition, 
                newPosition, 
                Time.deltaTime * _selectedRing.MoveSpeed);

            await Task.Yield();
        }
        
        return true;
    }

    public void ReturnSelectedRing()
    {
        MoveRing();
    }

    public void MoveRingToAnotherTower(TowerHandler anotherTower)
    {
        if(anotherTower.topRing != null)
        {
            if (_selectedRing.RingSize > anotherTower.topRing.RingSize)
            {
                Debug.Log("SELECTED RIN CANNOT BE PUT IN THIS TOWER");
                return;
            }
        }

        DeactivateTowers();
        MoveRingToAnotherTowerSequence(anotherTower);
        
    }

    private void EndGame()
    {
        _endGamePopUp.gameObject.SetActive(true);
    }

    private async void MoveRingToAnotherTowerSequence(TowerHandler anotherTower)
    {
        isInteractableOn = false;
        _selectedRing.CurrentTower.RemoveRingInList(_selectedRing);
        _selectedRing.SetCurrentTower(anotherTower);
        _selectedRing.transform.parent = anotherTower.ringsContainerTransform.transform;
        await MoveSelectedRing(anotherTower.SelectedPoint.localPosition);
        if (anotherTower.topRing != null)
        {
            float newRingPositionY = anotherTower.topRing.transform.localPosition.y + _ringPositionOffset;
            Vector3 newRingTransformPosition = new Vector3(
                anotherTower.topRing.transform.localPosition.x, 
                newRingPositionY, 
                anotherTower.topRing.transform.localPosition.z);

            await MoveSelectedRing(newRingTransformPosition);
        }
        else
            await MoveSelectedRing(anotherTower.ringsContainerTransform.localPosition);

        isInteractableOn = true;
        anotherTower.ringList.Add(_selectedRing);
        anotherTower.ChangeTopRing(_selectedRing);
        UpdateMovesCounter?.Invoke();
        ClearSelectedRing();

        bool isGameFinish = _lastTower.CheckRingsCount(_globalConfig.numberOfRings);

        if (isGameFinish)
            EndGame();

    }

    #endregion

    private void ActivateTowers(TowerHandler excludedTower)
    {
        foreach(TowerHandler tower in _towersList)
        {
            if (tower == excludedTower) continue;
            tower.IsClickable = true;
            //Debug.Log(tower.name + " is Clickable");
        }
    }

    private void DeactivateTowers()
    {
        foreach (TowerHandler tower in _towersList)
        {
            tower.IsClickable = false;
        }
    }

}
