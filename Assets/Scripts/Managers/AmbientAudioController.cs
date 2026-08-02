using UnityEngine;
using Yarn.Unity;

public class AmbientAudioController : MonoBehaviour
{
    public AudioSource ambientSource;

    private static AmbientAudioController instance;

    void Awake()
    {
        instance = this;
    }

    [YarnCommand("silence_ambient")]
    public static async YarnTask SilenceAmbient(float duration)
    {
        float originalVolume = instance.ambientSource.volume;
        instance.ambientSource.volume = 0f;
        await YarnTask.Delay((int)(duration * 1000));
        instance.ambientSource.volume = originalVolume;
    }
}