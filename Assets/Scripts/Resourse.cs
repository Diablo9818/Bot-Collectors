using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resourse : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Unit unit))
        {
            transform.SetParent(unit.transform);
            unit.BecomeFull();
        }
    }
}
