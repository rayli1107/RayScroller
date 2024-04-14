using System;
using UnityEngine;

public static class GlobalGameContext
{
    public static Action statUpdateAction;

    private static int _hp;
    public static int hp
    {
        get => _hp;
        set
        {
            _hp = value;
            statUpdateAction?.Invoke();
        }
    }

    private static int _coin;
    public static int coin
    {
        get => _coin;
        set
        {
            _coin = value;
            statUpdateAction?.Invoke();
        }
    }

    public static void Initialize()
    {
        hp = 10;
        coin = 0;
    }
}

public class GameController : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private ScrollingObjectController _prefabPlatformSafe;
    [SerializeField]
    private ScrollingObjectController _prefabPlatformDamage;
    [SerializeField]
    private ScrollingObjectController _prefabCoin;
    [SerializeField]
    private int _numPlatforms = 15;
    [SerializeField]
    private float _startY = -6.0f;
    [SerializeField]
    private float _endY = 6.0f;
    [SerializeField]
    private Vector2 _startX = new Vector2(-3f, 3f);
    [SerializeField]
    private float _speedPlatform = 100f;
    [SerializeField]
    private float _speedCoin = 200f;

#pragma warning restore 0649

    private System.Random _random;
    private float _lastPlatformCreateTime = 0f;
    private float _lastCoinCreateTime = 0f;

    private void Awake()
    {
        _random = new System.Random(System.Guid.NewGuid().GetHashCode());
    }

    // Start is called before the first frame update
    void Start()
    {
        _lastPlatformCreateTime = -1.0f;
        _lastCoinCreateTime = -1.0f;
        GlobalGameContext.Initialize();
        GlobalGameContext.statUpdateAction += onStatUpate;
    }

    private void onStatUpate()
    {
        if (GlobalGameContext.hp == 0)
        {
            Time.timeScale = 0;
            UIManager.Instance.showGameOverScreen(true);
            AudioManager.Instance.Stop();
            AudioManager.Instance.PlayGameOver();
        }
    }

    private void createScrollable(ScrollingObjectController prefab, float speed)
    {
        ScrollingObjectController scrollingObject = Instantiate(prefab, transform);
        scrollingObject.transform.position = new Vector3(
            (float)_random.NextDouble() * (_startX.y - _startX.x) + _startX.x,
            _startY,
            scrollingObject.transform.position.z);
        scrollingObject.endY = _endY;
        scrollingObject.speed = speed;
        scrollingObject.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        float timeNow = Time.time;
        if (_lastPlatformCreateTime < 0f || timeNow - _lastPlatformCreateTime >= 1f)
        {
            createScrollable(
                _random.Next(2) == 0 ? _prefabPlatformDamage : _prefabPlatformSafe,
                _speedPlatform);
            _lastPlatformCreateTime = timeNow;
        }


        if (_lastCoinCreateTime < 0f || timeNow - _lastCoinCreateTime >= 2f)
        {
            createScrollable(_prefabCoin, _speedCoin);
            _lastCoinCreateTime = timeNow;
        }
    }
}
