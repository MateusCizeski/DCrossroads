using UnityEngine;

public class LookAtFunction : MonoBehaviour
{
    public Animator animator;
    public Animator RedManeqAnimator;
    public bool IKActive = false;
    public Transform LookAtObj = null;
    public float LookWeight = 2f;
    public CanInteract CanInteract;

    private void OnAnimatorIK(int layerIndex)
    {
        if(this.gameObject.GetComponent<Animator>())
        {
            if(IKActive)
            {
                if(LookAtObj != null)
                {
                    LookWeight = Mathf.Lerp(LookWeight, 1, Time.deltaTime * 2);
                }
            }
            else
            {
                LookWeight = Mathf.Lerp(LookWeight, 0, Time.deltaTime * 2);
            }
        }
    }
}
