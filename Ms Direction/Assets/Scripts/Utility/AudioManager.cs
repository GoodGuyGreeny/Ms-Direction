using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Tooltip("Leave NULL. Scripts will take care of track management. Index 0 of \"tracks\" will be the first song played in the scene.")]
    public AudioSource currentTrack;
    private GameObject currentTrackObject;

    [SerializeField]
    private GameObject[] tracks;
    private int currentTrackIndex = 0;
    private bool paused = false;

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
        currentTrack.transform.parent = Camera.main.transform;
        currentTrack.transform.localPosition = new Vector3(0, 0, 0);
        currentTrack = currentTrack.GetComponent<AudioSource>();
        
        if(currentTrack = null)
        {
            Destroy(currentTrack);
            Debug.LogError("ERROR in AudioManager.cs: Passed track object does not have an audio source attached!");
            paused = true;
            return;
        }

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

    IEnumerator WaitToPlayTrack(int waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        PlayCurrentTrack();
    }
}
