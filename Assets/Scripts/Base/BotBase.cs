using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(ResourceScanner))]
[RequireComponent(typeof(ResourceStorage))]
[RequireComponent(typeof(BotFabric))]
public class BotBase : MonoBehaviour
{
    private readonly List<Bot> _bots = new List<Bot>();

    private ResourceScanner _resourceScanner;
    private ResourceStorage _resourceStorage;
    private BotFabric _botFabric;

     private Queue<Resource> _freeResources;
     private Queue<Bot> freeBots;

    private WaitForSeconds _waitScanDelay;

    private int _firstBotsAmmount = 3;

    private void Awake()
    {
        _resourceScanner = GetComponent<ResourceScanner>();
        _resourceStorage = GetComponent<ResourceStorage>();
        _botFabric = GetComponent<BotFabric>();

        _waitScanDelay = new WaitForSeconds(_resourceScanner.ScanDelay);
    }

    private void Start()
    {
        CreateBot(_firstBotsAmmount);

        StartCoroutine(RunAsync());
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
            freeBots = FindFreeBots();

            SendBots(_freeResources, freeBots);
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
}
