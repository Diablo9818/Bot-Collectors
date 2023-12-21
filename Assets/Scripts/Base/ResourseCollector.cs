using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Base))]
public class ResourseCollector : MonoBehaviour
{
    private Queue <Resourse> _resourses;
    private int _resourseAmmount = 0;
    private Base _base;

    private void Awake()
    {
        _base = GetComponent<Base>();
    }

    private void Start()
    {
         List<Resourse> _resoursesList = FindObjectsOfType<Resourse>().ToList();
        _resourses = new Queue<Resourse>(_resoursesList); 
    }

    private void Update()
    {
        if (_base.TryGetFreeUnit(out Unit unit))
        {
            if (TryGetResourse(out Resourse resourse))
            {
                unit.SetResourceTarget(resourse);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Unit unit))
        {
            var resource = unit.Collecter.Resource;

            unit.Collecter.GiveResourseToBase();

            _resourseAmmount++;
        }
    }

    private bool TryGetResourse(out Resourse resourseToGet)
    {
        foreach (Resourse resourse in _resourses)
        {
            resourseToGet = _resourses.Dequeue();
            return true;
        }

        resourseToGet = null;
        return false;
    }
}
