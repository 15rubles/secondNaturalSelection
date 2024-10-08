using UnityEngine;
using System.Collections;

public class MusicCenter : MonoBehaviour
{
    AudioClip[] playlist;
    AudioSource source;
    void Awake()
    {
        int count = FindObjectsOfType<MusicCenter>().Length;
        if (count != 1)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        playlist = Resources.LoadAll<AudioClip>("Music");
        source = gameObject.GetComponent<AudioSource>();
        source.clip = playlist[0];
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        source.Play();
    }

    void Update()
    {
        if (!source.isPlaying)
            playRandomMusic();
    }

    void playRandomMusic()
    {
        source.clip = playlist[Random.Range(0, playlist.Length)];
        source.Play();
    }
}