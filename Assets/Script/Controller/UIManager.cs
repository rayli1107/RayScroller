using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private RectTransform _gameOverScreen;
    [SerializeField]
    private TextMeshProUGUI _textHP;
    [SerializeField]
    private TextMeshProUGUI _textCoin;
#pragma warning restore 0649

    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        GlobalGameContext.statUpdateAction += onStatUpdate;
    }

    // Start is called before the first frame update
    void Start()
    {
        onStatUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void onStatUpdate()
    {
        _textHP.text = GlobalGameContext.hp.ToString();
        _textCoin.text = GlobalGameContext.coin.ToString();
    }

    public void showGameOverScreen(bool show)
    {
        _gameOverScreen.gameObject.SetActive(show);
    }
}
