using UnityEngine;
using Yarn.Unity;

public class ShadowController : MonoBehaviour
{
    public Animator shadowAnimator;
    private static ShadowController instance;

    void Awake()
    {
        instance = this;
    }

    [YarnCommand("gesture_pacto")]
    public static void GesturePacto()
    {
        instance.shadowAnimator.SetTrigger("Pacto");
    }

    [YarnCommand("shadow_gesture")]
    public static void ShadowGesture(string gestureName)
    {
        instance.shadowAnimator.SetTrigger(gestureName);
    }

    [YarnCommand("shadow_diverge_timed")]
    public static async YarnTask DivergeTimed(string gestureName, float duration)
    {
        instance.shadowAnimator.SetTrigger(gestureName);
        await YarnTask.Delay((int)(duration * 1000));
    }

    [YarnCommand("shadow_sync")]
    public static void Sync()
    {
        instance.shadowAnimator.SetTrigger("Sync");
    }
}