using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private SelectRing _selectedRing;

    [SerializeField] private List<TowerHandler> _towersList = new List<TowerHandler>();

    private Vector3 _originalRingPosition;

    private void OnEnable()
    {
        if (GameManager.instance != null)
            Destroy(this);
        else    
            instance = this; 
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
        if (_selectedRing.IsSelected)
        {
            _originalRingPosition = _selectedRing.transform.localPosition;
            _ = MoveSelectedRing(_selectedRing.CurrentTower.SelectedPoint.localPosition);
        }
        else
            _ = MoveSelectedRing(_originalRingPosition);
    }

    private async Task<bool> MoveSelectedRing(Vector3 newPosition)
    {
        //Debug.Log("ON MOVE RING");
        while (Vector3.Distance(_selectedRing.transform.localPosition, newPosition) >= 0.01f)
        {

            //Debug.Log("ON MOVE RING INSIDE WHILE LOOP");
            _selectedRing.transform.localPosition = Vector3.MoveTowards(_selectedRing.transform.localPosition, newPosition, Time.deltaTime * _selectedRing.MoveSpeed);
            await Task.Yield();
        }
        return true;
    }

    public void MoveRingToAnotherTower(TowerHandler anotherTower)
    {
        DeactivateTowers();
        MoveRingToAnotherTowerSequence(anotherTower);
    }

    private async void MoveRingToAnotherTowerSequence(TowerHandler anotherTower)
    {
        _selectedRing.SetCurrentTower(anotherTower);
        _selectedRing.transform.parent = anotherTower.ringsTransform.transform;
        await MoveSelectedRing(anotherTower.SelectedPoint.localPosition);
        await MoveSelectedRing(anotherTower.ringsTransform.localPosition);
        ClearSelectedRing();
  
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
