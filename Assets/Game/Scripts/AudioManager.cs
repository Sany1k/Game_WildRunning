using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private List<AudioClip> gameMusic;
    [SerializeField] private AudioClip jumpFX;
    [SerializeField] private AudioClip boomSFX;

    private AudioSource playAudio;

    private void Awake()
    {
        playAudio = GetComponent<AudioSource>();
    }

    public void PlayRandomGameMusic()
    {
        StartCoroutine(MusicLoopRoutine());
    }

    private IEnumerator MusicLoopRoutine()
    {
        while (true)
        {
            int rIndex = Random.Range(0, gameMusic.Count);
            playAudio.clip = gameMusic[rIndex];
            playAudio.Play();
            yield return new WaitForSeconds(gameMusic[rIndex].length);
        }
    }

    public void PlayBoomSFX() => playAudio.PlayOneShot(boomSFX, 3f);

    public void PlayJumpSFX() => playAudio.PlayOneShot(jumpFX, 3f);
}
