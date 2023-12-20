using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(UnitMover))]
[RequireComponent (typeof(UnitCollecter))]
public class Unit : MonoBehaviour
{
    [SerializeField] private Transform _basePosition;

    private UnitMover _mover;
    private UnitCollecter _collecter;

    public UnitCollecter Collecter => _collecter;

    private void Awake()
    {
        _mover = GetComponent<UnitMover>();
        _collecter = GetComponent<UnitCollecter>();
    }

    private void Update()
    {
        if (_collecter.Resource != null && !_collecter.IsFull)
        {
            _mover.MoveTo(_collecter.Resource.transform);
        }

        if (_collecter.IsFull)
        {
            _mover.MoveTo(_basePosition);
        }
    }

    public void SetResourceTarget(Resourse newTarget)
    {
        _collecter.SetResourceTarget(newTarget);
    }
}