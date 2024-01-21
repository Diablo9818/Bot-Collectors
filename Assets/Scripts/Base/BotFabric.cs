using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotFabric : MonoBehaviour
{
    [SerializeField] private Bot _botPrefab;

    public Bot SpawnBot()
    {
        return Instantiate(_botPrefab, transform.position, transform.rotation);
    }
}
