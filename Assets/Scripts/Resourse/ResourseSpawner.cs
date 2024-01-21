using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ResourseSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private Resource _resourcePrefab;

    [SerializeField] private Vector2 _spawnAreaX;
    [SerializeField] private Vector2 _spawnAreaZ;
    [SerializeField] private float _yPosition;

    [SerializeField] private int _respawnCount;
    [SerializeField] private float _spawnDelay;

    private WaitForSeconds _wait;

    private void Awake()
    {
        _wait = new WaitForSeconds(_spawnDelay);
    }

    private void Start()
    {
        StartCoroutine(Create());
    }

    private IEnumerator Create()
    {
        for (int i = 0; i < _respawnCount; i++)
        {
            CreateResourse();
            yield return _wait;
        }
    }

    private void CreateResourse()
    {
        Resource resourse = Instantiate(_resourcePrefab, GetTransformPosition(), Quaternion.identity);
    }

    private Vector3 GetTransformPosition()
    {
        float newX = Random.Range(_spawnAreaX.x, _spawnAreaX.y);
        float newZ = Random.Range(_spawnAreaZ.x, _spawnAreaZ.y);
        float newY = _yPosition;

        return new Vector3(newX, newY, newZ);
    }
}
