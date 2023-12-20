using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Base : MonoBehaviour
{
    [SerializeField] private List<Unit> _units;

    public bool TryGetFreeUnit(out Unit freeUnit)
    {
        foreach (Unit unit in _units)
        {
            if (!unit.Collecter.IsBusy)
            {
                freeUnit = unit;
                return true;
            }
        }

        freeUnit = null;
        return false;
    }
}