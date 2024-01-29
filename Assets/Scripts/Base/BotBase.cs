using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(ResourceScanner))]
[RequireComponent(typeof(ResourceStorage))]
[RequireComponent(typeof(BotFabric))]
[RequireComponent(typeof(BaseFlagHandler))]
public class BotBase : MonoBehaviour
{
    [SerializeField] private int _botPrice;
    [SerializeField] private int _basePrice;
    private readonly List<Bot> _bots = new List<Bot>();

    private ResourceScanner _resourceScanner;
    private ResourceStorage _resourceStorage;
    private BotFabric _botFabric;
    private BaseFlagHandler _baseFlagHandler;

     private Queue<Resource> _freeResources;
     private Queue<Bot> _freeBots;

    private WaitForSeconds _waitScanDelay;

    private int _firstBotsAmmount = 3;

    private void Awake()
    {
        _resourceScanner = GetComponent<ResourceScanner>();
        _resourceStorage = GetComponent<ResourceStorage>();
        _botFabric = GetComponent<BotFabric>();
        _baseFlagHandler = GetComponent<BaseFlagHandler>();

        _waitScanDelay = new WaitForSeconds(_resourceScanner.ScanDelay);
    }

    private void Start()
    {
        CreateBot(_firstBotsAmmount);

        StartCoroutine(RunAsync());
    }

    private void Update()
    {
        if (_baseFlagHandler.IsFlagPlaced)
        {
            if (_resourceStorage.ResourcesCount == _basePrice)
            {
                CreateBase();
            }
        }
        if(_resourceStorage.ResourcesCount == _botPrice && _baseFlagHandler.IsFlagPlaced == false)
        {
            CreateBot(1);
            _resourceStorage.DecreaseResourcesCount(_botPrice);
        }
    }

    public bool IsFlagPlaced()
    {
        return _baseFlagHandler.IsFlagPlaced;
    }

    private void CreateBase()
    {
        _freeBots = FindFreeBots();
        SendBot(_freeBots, _baseFlagHandler.Flag.transform);
        _resourceStorage.DecreaseResourcesCount(_basePrice);
    }

    private void CreateBot(int botCount)
    {
        for (int i = 0; i < botCount; i++)
        {
            Bot bot = _botFabric.SpawnBot();
            _bots.Add(bot);

            bot.Init(this, _resourceStorage);
        }
    }

    private IEnumerator RunAsync()
    {
        while (enabled)
        {
            _freeResources = _resourceScanner.Scan();
            _freeBots = FindFreeBots();

            SendBots(_freeResources, _freeBots);
            yield return _waitScanDelay;
        }
    }

    private Queue<Bot> FindFreeBots()
    {
        IEnumerable<Bot> enumerable = _bots.Where(bot => bot.IsBusy == false);

        return new Queue<Bot>(enumerable);
    }

    private void SendBots(Queue<Resource> freeResources, Queue<Bot> freeBots)
    {
        while (freeResources.TryPeek(out Resource resource) && freeBots.TryPeek(out Bot bot))
        {
            if (resource.IsOrdered)
            {
                freeResources.Dequeue();
                continue;
            }

            freeBots.Dequeue();

            resource.SetOrderedStatus();
            bot.SetTarget(resource);
            bot.Run();
        }
    }

    private void SendBot(Queue<Bot> freeBots, Transform target)
    {
        while(freeBots.TryPeek(out Bot bot))
        {
            freeBots.Dequeue();
            bot.SetTarget(target);
            bot.RunToNewBase();
        }
    }
}
