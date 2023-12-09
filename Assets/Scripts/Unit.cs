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

    private Transform _target;
    private Rigidbody _rigidbody;

    public bool IsBusy { get { return _isBusy; } }
    public bool IsFull { get { return _isFull; } }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(_target != null && !_isFull)
        {
            Move();
        }
        if (_isFull)
        {
            MoveToBase();
        }
    }
    public void BecomeFull()
    {
        _isFull = true;
    }   

    public void BecomeEmpty()
    {
        _isFull = false;
    }   
    
    public void BecomeBusy()
    {
        _isBusy = true;
    }

    public void BecomeFree()
    {
        _isBusy = false;
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

        _target = null;
    }

    public void SetTarget(Transform newTarget)
    {
        _target = newTarget;
    }

    private void Move()
    {
        _rigidbody.velocity = FindDirection() * _speed;
    }

    private Vector3 FindDirection()
    {
        return (_target.position - transform.position).normalized;
    }

}
