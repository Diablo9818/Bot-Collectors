using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourseSpawner : MonoBehaviour
{
    [SerializeField] private Resourse _resourcesToSpawn;

    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;
    [SerializeField] private float _maxZ;
    [SerializeField] private float _minZ;
    [SerializeField] private float _yPosition;

    [SerializeField] private float _count;

    private void Awake()
    {
        Spawn();
    }

    private void Spawn()
    {
        for (int i = 0; i <_count; i++)
        {
            Instantiate(_resourcesToSpawn, GetTransformPosition(),Quaternion.identity);
        }
    }

    private Vector3 GetTransformPosition()
    {
        float newX = Random.Range(_minX, _maxX);
        float newY = _yPosition;
        float newZ = Random.Range(_minZ, _maxZ);

        return new Vector3(newX, newY, newZ);
    }
}
