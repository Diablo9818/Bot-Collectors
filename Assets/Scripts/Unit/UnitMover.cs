using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    public void MoveTo(Transform target)
    {
        transform.position = Vector3.MoveTowards(transform.position,  
            target.transform.position, _speed * Time.deltaTime);
    }
}
