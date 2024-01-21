using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    public IEnumerator MoveTo(Transform target)
    {
        while (Vector3.Distance(transform.position, target.position) > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
            yield return null;
        }
    }
}
