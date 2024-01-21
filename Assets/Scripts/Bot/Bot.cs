using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BotMover))]
[RequireComponent(typeof(BotCollector))]
public class Bot : MonoBehaviour
{
    private BotBase _botBase;
    private BotMover _mover;
    private BotCollector _collector;

    public bool IsBusy { get; private set; }

    public void Init(BotBase botBase, ResourceStorage storage)
    {
        _botBase = botBase;
        _mover = GetComponent<BotMover>();
        _collector = GetComponent<BotCollector>();
        _collector.GetStorage(storage);
    }

    public void Run()
    {
        StartCoroutine(RunAsync());
    }

    public void SetTarget(Resource resource)
    {
        _collector.SetTarget(resource);
    }

    private IEnumerator RunAsync()
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
}
