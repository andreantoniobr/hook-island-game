using UnityEngine;

public class AudioServices : AudioPlayerMain
{
    [SerializeField] private AudioClip scoreAudioClip;
    [SerializeField] private AudioClip retractingAudioClip;
    

    private void Awake()
    {
        Target.OnTargetActivedEvent += OnTargetActived;

        PlayerController.OnRetractingEvent += OnHookRetracting;
    }

    private void OnDestroy()
    {
        Target.OnTargetActivedEvent -= OnTargetActived;

        PlayerController.OnRetractingEvent -= OnHookRetracting;
    }

    private void OnTargetActived()
    {
        PlayAudioCue(scoreAudioClip);
    }

    private void OnHookRetracting()
    {
        PlayAudioCue(retractingAudioClip);
    }
}
