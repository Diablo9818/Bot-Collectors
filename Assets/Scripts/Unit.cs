using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Rigidbody))]
public class Unit : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private bool _isBusy = false;
    [SerializeField] private bool _isFull = false;

    [SerializeField] private Transform _basePosition;

    private Rigidbody _rigidbody;

    public Resourse Resource { get; private set; }
    public bool IsBusy => _isBusy;
    public bool IsFull => _isFull;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Resource != null && !_isFull)
        {
            Move();
        }

        if (_isFull)
        {
            MoveToBase();
        }
    }

    public void MoveToBase()
    {
        _rigidbody.velocity = (_basePosition.position - transform.position).normalized * _speed;
    }

    public void GiveResourseToBase()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        Resource = null;
        _isBusy = false;
        _isFull = false;
    }

    public void SetResourceTarget(Resourse newTarget)
    {
        Resource = newTarget;
        _isBusy = true;
    }

    private void Move()
    {
        _rigidbody.velocity = FindDirection() * _speed;
    }

    private Vector3 FindDirection()
    {
        return (Resource.transform.position - transform.position).normalized;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Resourse resourse))
        {
            if (resourse != Resource)
            {
                return;
            }

            resourse.transform.SetParent(transform);
            resourse.transform.position = new Vector3(transform.position.x, resourse.transform.position.y + 0.5f, transform.position.z);
            _isFull = true;
        }
    }
}