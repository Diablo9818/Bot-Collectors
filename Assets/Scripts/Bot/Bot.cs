using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BotMover))]
[RequireComponent(typeof(BotCollector))]
[RequireComponent(typeof(BotBuilder))]
public class Bot : MonoBehaviour
{
    private BotBase _botBase;
    private BotMover _mover;
    private BotCollector _collector;
    private BotBuilder _builder;

    private Transform _newBaseTarget;

    public bool IsBusy { get; private set; }

    public void Init(BotBase botBase, ResourceStorage storage)
    {
        _botBase = botBase;
        _mover = GetComponent<BotMover>();
        _collector = GetComponent<BotCollector>();
        _builder = GetComponent<BotBuilder>();
        _collector.GetStorage(storage);
    }

    public void Run()
    {
        StartCoroutine(RunCorutine());
    }

    public void RunToNewBase()
    {
        StopCoroutine(RunCorutine());
        StartCoroutine(RunToNewBaseCorutine());
    }

    public void SetTarget(Resource resource)
    {
        _collector.SetTarget(resource);
    }

    public void SetTarget(Transform target)
    {
        _newBaseTarget = target;
    }

    private IEnumerator RunCorutine()
    {
        if (_collector.Resource == null)
            yield break;

        IsBusy = true;
        yield return _mover.MoveTo(_collector.Resource.transform);
        _collector.Take();

        yield return _mover.MoveTo(_botBase.transform);
        _collector.Drop();
        IsBusy = false;
    }

    private IEnumerator RunToNewBaseCorutine()
    {
        if (_botBase.IsFlagPlaced() && !IsBusy)
        {
            IsBusy = true;
            yield return _mover.MoveTo(_newBaseTarget);
            _builder.Build();
        }

        yield break;
    }
}
