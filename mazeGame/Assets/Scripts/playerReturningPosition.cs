using UnityEngine;

public class PlayerReturnPosition : MonoBehaviour
{
    void Start()
    {
        // فقط لو كان فيه مكان راجع ليه
        if (DatatoBeShared.ReturnPosition != Vector3.zero)
        {
            Debug.Log("⏪ رجوع اللاعب للمكان السابق: " + DatatoBeShared.ReturnPosition);
            transform.position = DatatoBeShared.ReturnPosition;

            DatatoBeShared.ReturnPosition = Vector3.zero;

            Animator animator = GetComponent<Animator>();
            if (animator != null)
            {
                animator.ResetTrigger("Sink"); // لو كان في ترجر شغال
                animator.Play("Idle");         // حط اسم أنيميشن الوقوف هنا
            }
        }
    }
}
