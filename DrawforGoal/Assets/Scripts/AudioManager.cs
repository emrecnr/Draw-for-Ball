using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance {  get; private set; }
    [SerializeField] AudioSource drawSFX;
    [SerializeField] AudioSource scoreSFX;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayDrawSFX(bool isPlay)
    {
        if (isPlay && !drawSFX.isPlaying)
        {
            drawSFX.Play();
        }
        else if (!isPlay)
            drawSFX.Pause();
    }
    public void PlayScoreSFX()
    {
        scoreSFX.Play();
    }


}
