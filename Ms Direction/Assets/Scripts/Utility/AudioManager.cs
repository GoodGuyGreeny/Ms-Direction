using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [HideInInspector]
    public AudioSource currentTrack;
    private GameObject currentTrackObject;

    [HideInInspector]
    public enum AudioType { Music, SFX };

    [SerializeField]
    private GameObject[] tracks;
    private int currentTrackIndex = 0;
    private bool paused = false;
    private float musicVolume = 100;
    private float sfxVolume = 100;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (tracks.Length > 0)
            StartMusic();
    }

    private void Update()
    {
        if (tracks.Length <= 0)
            return;

        if(currentTrack.isPlaying == false && paused == false)
        {
            currentTrackIndex++;
            StartMusic();
        }
    }

    void StartMusic()
    {
        if (currentTrackIndex >= tracks.Length)
            currentTrackIndex = 0;

        if (currentTrackObject != null)
            Destroy(currentTrackObject);

        currentTrackObject = Instantiate(tracks[currentTrackIndex]);

        currentTrackObject.transform.parent = Camera.main.transform;
        currentTrackObject.transform.localPosition = new Vector3(0, 0, 0);
        currentTrack = currentTrackObject.GetComponent<AudioSource>();
        
        if(currentTrack == null)
        {
            Destroy(currentTrackObject);
            Debug.LogError("ERROR in AudioManager.cs: Passed track object does not have an audio source attached!");
            paused = true;
            return;
        }

        currentTrack.volume = musicVolume / 100.0f;
        currentTrack.Play();
    }

    public void PauseCurrentTrack(int restartInSeconds = 0)
    {
        currentTrack.Pause();
        paused = true;

        if(restartInSeconds > 0)
            StartCoroutine("WaitToPlayTrack", restartInSeconds);
    }

    public void PlayCurrentTrack()
    {
        currentTrack.Play();
        paused = false;
    }

    public void SetMusicVolume(float value)
    {
        musicVolume = value;
        if(currentTrack != null)
            currentTrack.volume = musicVolume / 100.0f;
    }

    public void SetSFXVolume(float value)
    {
        sfxVolume = value;
    }

    public void ApplyVolume(AudioSource audio, AudioType type)
    {
        if (type == AudioType.SFX)
            audio.volume = sfxVolume / 100.0f;
        else if (type == AudioType.Music)
            audio.volume = musicVolume / 100.0f;
        else
            Debug.LogError("ERROR in AudioManager.cs: Invalid type somehow passed to ApplyVolume function!");
    }

    IEnumerator WaitToPlayTrack(int waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        PlayCurrentTrack();
    }


}
