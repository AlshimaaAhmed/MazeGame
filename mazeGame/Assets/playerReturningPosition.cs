using UnityEngine;

public class PlayerReturnPosition : MonoBehaviour
{
    void Start()
    {
        Debug.Log("DatatoBeShared.ReturnPosition: " + DatatoBeShared.ReturnPosition);

        if (DatatoBeShared.ReturnPosition != Vector3.zero)
        {
            Debug.Log("playerPosition: " + DatatoBeShared.ReturnPosition);

            transform.position = DatatoBeShared.ReturnPosition;
            DatatoBeShared.ReturnPosition = Vector3.zero;

            // ✅ أوقف الأنيميشن
            Animator animator = GetComponent<Animator>();
            if (animator != null)
            {
                animator.ResetTrigger("Sink");
               // animator.SetBool("isFalling", false); // لو بتستخدمه
                animator.Play("Idle"); // أو أي اسم Animation عايز تبدأ بيه
            }
        }
    }
}
