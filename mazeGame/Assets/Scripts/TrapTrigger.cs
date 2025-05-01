using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    public Animator minerAnimator;

    private bool triggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;
            minerAnimator.SetTrigger("Stab");
        }
    }
}
