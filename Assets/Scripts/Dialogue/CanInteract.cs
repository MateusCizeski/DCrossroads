using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using Yarn.Unity;

public class CanInteract : MonoBehaviour
{
    public CinemachineInputAxisController TalkZoomAxisController;
    public DialogueRunner dialogueRunner;
    public LookAtFunction LookAtScript;
    public Text InteractionText;
    private float InteractDistance = 2f;
    public bool CanInteraction = true;

    public CinemachineCamera PlayerVCam;
    public CinemachineCamera TalkZoomVCam;
    public FirstPersonController FpsController;

    void Update()
    {
        if (CanInteraction)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, InteractDistance))
            {
                if (hit.collider.CompareTag("Maneq"))
                {
                    InteractionText.text = "Talk to him";
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        InteractionText.text = "";
                        CanInteraction = false;
                        dialogueRunner.StartDialogue("Padre_Intro");
                    }
                }
                else InteractionText.text = "";
            }
            else InteractionText.text = "";
        }
    }

    public void OnDialogueStarted()
    {
        FpsController.enabled = false;
        TalkZoomVCam.enabled = true;
        PlayerVCam.enabled = false;
        LookAtScript.IKActive = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        TalkZoomAxisController.enabled = false;
    }

    public void OnDialogueEnded()
    {
        FpsController.enabled = true;
        TalkZoomVCam.enabled = false;
        PlayerVCam.enabled = true;
        LookAtScript.IKActive = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        CanInteraction = true;
    }
}