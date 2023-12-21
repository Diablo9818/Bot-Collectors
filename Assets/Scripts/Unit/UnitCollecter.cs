using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCollecter : MonoBehaviour
{
    [SerializeField] private bool _isBusy = false;
    [SerializeField] private bool _isFull = false;
    [SerializeField] private float _resoursePositionYOffset = 0.5f;

    public Resourse Resource { get; private set; }
    public bool IsBusy => _isBusy;
    public bool IsFull => _isFull;

    public void SetResourceTarget(Resourse newTarget)
    {
        Resource = newTarget;
        _isBusy = true;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Resourse resourse))
        {
            if (resourse != Resource)
            {
                return;
            }

            resourse.transform.SetParent(transform);
            resourse.transform.position = new Vector3(transform.position.x, resourse.transform.position.y + _resoursePositionYOffset, transform.position.z);
            _isFull = true;
        }
    }
}
