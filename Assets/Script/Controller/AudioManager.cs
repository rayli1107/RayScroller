using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private AudioClip _backgroundMusic;
    [SerializeField]
    private AudioClip _clipCoin;
    [SerializeField]
    private AudioClip _clipDamage;
    [SerializeField]
    private AudioClip _clipJump;
    [SerializeField]
    private AudioClip _clipGameOver;
#pragma warning restore 0649

    public static AudioManager Instance;
    private AudioSource _audio;
    private void Awake()
    {
        Instance = this;
        _audio = GetComponent<AudioSource>();
    }

    public void PlayCoin()
    {
        _audio.PlayOneShot(_clipCoin);
    }

    public void PlayDamage()
    {
        _audio.PlayOneShot(_clipDamage);
    }

    public void PlayJump()
    {
        _audio.PlayOneShot(_clipJump);
    }

    public void PlayGameOver()
    {
        _audio.PlayOneShot(_clipGameOver);
    }

    public void Stop()
    {
        _audio.Stop();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
