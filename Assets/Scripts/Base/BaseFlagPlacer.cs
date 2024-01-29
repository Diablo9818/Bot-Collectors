using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class BaseFlagPlacer : MonoBehaviour
{
    private const string LayerPlaceable = "Placeable";

    [SerializeField] private Flag _flagPrefab;

    private BaseFlagHandler _currentflagHandler;

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (Input.GetMouseButtonDown(0))
            {
                HandleMouseClick(hitInfo);
            }
        }
    }

    private void HandleMouseClick(RaycastHit hitInfo)
    {
        if (hitInfo.collider.gameObject.layer.Equals(LayerMask.NameToLayer(LayerPlaceable)))
        {
            HandlePlaceableClick(hitInfo);
        }
        else if (hitInfo.collider.TryGetComponent(out BaseFlagHandler flagHandler))
        {
            HandleFlagClick(flagHandler);
        }
    }


    private void HandlePlaceableClick(RaycastHit hitInfo)
    {
        if (_currentflagHandler == null)
            return;

        if (_currentflagHandler.IsFlagPlaced == false)
        {
            PlaceFlag(hitInfo);
        }
        else
        {
            ReplaceFlag(hitInfo);
        }

        _currentflagHandler = null;
    }

    private void HandleFlagClick(BaseFlagHandler flagHandler)
    {
        _currentflagHandler = flagHandler;
    }

    private void ReplaceFlag(RaycastHit hitInfo)
    {
        _currentflagHandler.Flag.transform.position = hitInfo.point;
    }

    private void PlaceFlag(RaycastHit hitInfo)
    {
        Flag flag = Instantiate(_flagPrefab, hitInfo.point, Quaternion.identity);
        _currentflagHandler.SetFlag(flag);
    }
}
