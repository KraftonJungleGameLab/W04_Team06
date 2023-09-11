using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //----------------------------------------------------
    private static SoundManager _instance;
    public static SoundManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        //DontDestroyOnLoad(this.gameObject);
    }
    //----------------------------------------------------

    private Animator anim;
    public List<AudioSource> audioSources;
    public int currentAudioIndex;

    void Start()
    {
        currentAudioIndex = 0;
        StartCoroutine(StartAudio(currentAudioIndex, 1.0f));
    }

    public void SwitchSound(int idx, float blendSeconds)
    {
        StartCoroutine(StopAudio(currentAudioIndex, blendSeconds));
        currentAudioIndex = idx;
        StartCoroutine(StartAudio(idx, blendSeconds));
    }

    public IEnumerator StartAudio(int idx, float blendSeconds)
    {
        float seconds = 0.0f;
        while (seconds < blendSeconds)
        {
            audioSources[idx].volume += Time.deltaTime / blendSeconds;
            seconds += Time.deltaTime;
            yield return null;
        }
        audioSources[idx].volume = 0.2f;
    }

    public IEnumerator StopAudio(int idx, float blendSeconds)
    {
        float seconds = 0.0f;
        while (seconds < blendSeconds)
        {
            audioSources[idx].volume -= Time.deltaTime / blendSeconds;
            seconds += Time.deltaTime;
            yield return null;
        }
        audioSources[idx].volume = 0.0f;
    }
}
