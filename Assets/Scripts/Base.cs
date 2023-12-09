using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Base : MonoBehaviour
{
    [SerializeField] private List<Unit> _units;
    [SerializeField] private List<Resourse> _resourses;

    private Transform _resourseTransform;
    private int _resourseCount = 0;
    private int _currenResourseIndex = 0;
    private Resourse _resourse;

    private void Start()
    {
        _resourses = GameObject.FindObjectsOfType<Resourse>().ToList();
    }

    private void Update()
    {
        _resourse = _resourses[_currenResourseIndex];

        if (_resourse != null)
        {
            _resourseTransform = _resourse.transform;

            Unit unit = _units.FirstOrDefault(unit => !unit.IsBusy);

            if (unit != null)
            {
                unit.SetTarget(_resourseTransform);
                unit.BecomeBusy();

                _currenResourseIndex = (_currenResourseIndex + 1) % _resourses.Count;
            }
        }

        Debug.Log(_resourses.Count);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Unit unit))
        {
            unit.BecomeFree();
            unit.GiveResourseToBase();
            unit.BecomeEmpty();
            _resourseCount++;
            _resourse = null;
            _resourseTransform = null;
            _resourses.RemoveAt(_currenResourseIndex);
            Debug.Log("Resourse Count:" + _resourses.Count);
        }
    }

    private void SendUnit(Transform target)
    {
        Unit unit = _units.First( unit => !unit.IsBusy);
        unit.SetTarget(target);
        unit.BecomeBusy();
    }

    private Resourse GetRandomResourse()
    {
        int index = Random.Range(0, _resourseCount);
        _currenResourseIndex = index;
        Debug.Log(index);
        return _resourses[index];
    }
}
