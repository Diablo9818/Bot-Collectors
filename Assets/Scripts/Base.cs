using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Base : MonoBehaviour
{
    [SerializeField] private List<Unit> _units;

    private List<Resourse> _resourses;
    private int _resourseAmmount = 0;

    private void Start()
    {
        _resourses = FindObjectsOfType<Resourse>().ToList();
    }

    private void Update()
    {
        if (TryGetFreeUnit(out Unit unit))
        {
            if (TryGetFreeResourse(out Resourse resourse))
            {
                resourse.Reserve();
                unit.SetResourceTarget(resourse);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Unit unit))
        {
            var resource = unit.Resource;

            _resourses.Remove(resource);

            unit.GiveResourseToBase();

            _resourseAmmount++;
        }
    }

    private bool TryGetFreeUnit(out Unit freeUnit)
    {
        foreach (Unit unit in _units)
        {
            if (!unit.IsBusy)
            {
                freeUnit = unit;
                return true;
            }
        }

        freeUnit = null;
        return false;
    }

    private bool TryGetFreeResourse(out Resourse freeResourse)
    {
        foreach (Resourse resourse in _resourses)
        {
            if (resourse.IsReserved == false)
            {
                freeResourse = resourse;
                return true;
            }
        }

        freeResourse = null;
        return false;
    }
}