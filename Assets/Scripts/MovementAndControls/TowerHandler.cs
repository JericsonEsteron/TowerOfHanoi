using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHandler : MonoBehaviour
{
    [SerializeField] private Transform _selectedPoint;
    public Transform SelectedPoint { get { return _selectedPoint; } }
    public Transform ringsContainerTransform;
    public List<SelectRing> ringList;

    public SelectRing topRing;

    //private bool _isClickable;
    public bool IsClickable { get; set; }

    private GameManager _gameManager;

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

        if(topRing != null)
            topRing.IsTopRing = true;
    }

    public void ChangeTopRing(SelectRing ring)
    {
        if(topRing != null)
            topRing.IsTopRing = false;

        topRing = ring;
        ring.IsTopRing = true;
    }

    public void UpdateTopRing()
    {
        ArrangeRingList();
        if(!ringList.Contains(topRing) && ringList.Count > 0)
        {
            Debug.Log("CHANGING TOP RING");
            topRing = ringList[0];
            topRing.IsTopRing = true;
        }
        else
        {
            topRing = null;
        }
    }

    public void RemoveRingInList(SelectRing ring)
    {
        ringList.Remove(ring);
        UpdateTopRing();
    }

    public void ArrangeRingList()
    {
        ShowRingList();
        ringList.Sort((r1, r2) => r1.RingSize.CompareTo(r2.RingSize));

        /*for(int x = 0; x < ringList.Count; x++)
        {
            for(int y = x + 1; y < ringList.Count; y++)
            {
                if (x >= ringList.Count) continue;
                if(ringList[x].RingSize > ringList[x + 1].RingSize)
                {
                    SelectRing tempRing = ringList[x];
                    ringList[x] = ringList[x + 1];
                    ringList[x + 1] = tempRing;
                }
            }
        }*/
        ShowRingList();
    }

    public bool CheckRingsCount(int maxRings)
    {
        if(maxRings == ringList.Count)
        {
            topRing.IsTopRing = false;
            return true;
        }

        return false;
    }

    private void ShowRingList()
    {
        string ringListString = "";
        foreach(SelectRing ring in ringList)
        {
            ringListString += ring.name + "\n";
        }
        Debug.Log(ringListString);
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
