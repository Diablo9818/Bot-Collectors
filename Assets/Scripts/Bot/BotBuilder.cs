using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBuilder : MonoBehaviour
{
    [SerializeField] private BotBase _base;

    public void Build()
    {
        Instantiate(_base, transform.position, transform.rotation);
    }
}
