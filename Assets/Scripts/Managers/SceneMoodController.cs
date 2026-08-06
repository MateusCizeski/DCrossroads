using UnityEngine;
using Yarn.Unity;

public class SceneMoodController : MonoBehaviour
{
    public GameObject padreNPC;
    public Light directionalLight;
    public float fogTargetDensity = 0.08f;
    public AudioSource ambientSource;
    public AudioClip somberAmbientClip;
    public CanvasGroup fadeOverlay;
    public Transform playerTransform;
    public Transform benchSeatPosition;
    public Animator playerAnimator;
    public GameObject newspaperObject;

    private static SceneMoodController instance;

    void Awake()
    {
        instance = this;
        fadeOverlay.blocksRaycasts = false;
        fadeOverlay.interactable = false;
    }

    [YarnCommand("timeskip_3dias")]
    public static async YarnTask TimeskipTresDias()
    {
        await Fade(instance.fadeOverlay, 0f, 1f, 1f);

        instance.padreNPC.SetActive(false);

        instance.playerTransform.position = instance.benchSeatPosition.position;
        instance.playerTransform.rotation = instance.benchSeatPosition.rotation;

        instance.playerAnimator.SetTrigger("Sit");

        RenderSettings.fog = true;
        RenderSettings.fogDensity = instance.fogTargetDensity;
        instance.directionalLight.intensity *= 0.4f;
        instance.directionalLight.color = new Color(0.6f, 0.65f, 0.8f);

        if (instance.somberAmbientClip != null)
        {
            instance.ambientSource.clip = instance.somberAmbientClip;
            instance.ambientSource.Play();
        }

        if (instance.newspaperObject != null)
            instance.newspaperObject.SetActive(true);

        await YarnTask.Delay(500);
        await Fade(instance.fadeOverlay, 1f, 0f, 1f);
    }

    [YarnCommand("end_demo_fade")]
    public static async YarnTask EndDemoFade()
    {

        await YarnTask.Delay(800);
        await Fade(instance.fadeOverlay, 0f, 1f, 2f);
    }

    static async YarnTask Fade(CanvasGroup group, float from, float to, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            group.alpha = Mathf.Lerp(from, to, elapsed / duration);
            await YarnTask.Yield();
        }

        group.alpha = to;
    }
}