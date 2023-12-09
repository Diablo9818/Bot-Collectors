using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resourse : MonoBehaviour
{
    public bool IsReserved { get; private set; }

    public void Reserve()
    {
        IsReserved = true;
    }
}