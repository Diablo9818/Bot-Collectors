using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceStorage : MonoBehaviour
{
    public int ResourcesCount { get; private set; }

    public void UpdateResourcesCount()
    {
        ResourcesCount ++;
    }
}
