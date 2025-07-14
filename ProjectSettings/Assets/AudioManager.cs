using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("AudioSources")]
    [SerializeField] private AudioSource starsCollect;
    [SerializeField] private AudioSource cubeMovement;

    public static AudioManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GameManager.Instance.starsCollected += PlayStarCollectedAudio;
        GameManager.Instance.cubeMovement += PlayCubeMovementAudio;
    }

    public void PlayStarCollectedAudio()
    {
       starsCollect.Play();  
       StartCoroutine(stopAudioSource(starsCollect));  
    }


    public void PlayCubeMovementAudio()
    {
        cubeMovement.Play();
        StartCoroutine(stopAudioSource(cubeMovement));
    }

    private IEnumerator stopAudioSource(AudioSource _audioSource)
    {
        yield return new WaitForSeconds(_audioSource.clip.length);
        _audioSource.Stop();    
    }

    private void OnDisable()
    {
        GameManager.Instance.starsCollected -= PlayStarCollectedAudio;
        GameManager.Instance.cubeMovement -= PlayCubeMovementAudio;
    }
}
