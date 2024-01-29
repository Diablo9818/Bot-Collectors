using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceStorage : MonoBehaviour
{
    [field: SerializeField] public int ResourcesCount { get; private set; }

    public void IncreaseResourcesCount()
    {
        ResourcesCount ++;
    }

    public void DecreaseResourcesCount(int ammount)
    {
        ResourcesCount-=ammount;
    }
}
