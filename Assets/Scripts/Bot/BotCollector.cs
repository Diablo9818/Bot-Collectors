using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotCollector : MonoBehaviour
{
    [SerializeField] private Transform _bag;

    private ResourceStorage _storage;
    private Resource _targetResource;

    public Resource Resource => _targetResource;

    public void GetStorage(ResourceStorage storage)
    {
        _storage = storage;
    }

    public void SetTarget(Resource resource)
    {
        _targetResource = resource;
    }

    public void Take()
    {
        _targetResource.transform.SetParent(transform);
        _targetResource.transform.position = _bag.position;
    }

    public void Drop()
    {
        _storage.IncreaseResourcesCount();
        Destroy(_targetResource.gameObject);
    }
}
