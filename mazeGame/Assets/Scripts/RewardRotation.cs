using UnityEngine;

public class RewardRotation : MonoBehaviour
{
    public float rotationSpeed = 90f;
    public float rotationDuration = 2f;
    public Light glowLight;
    public Transform spotLightTransform;
    public float moveSpeed = 200f;

    private Vector3 targetPosition;
    private bool rotating = false;
    private bool movingLight = false;
    private float timer = 0f;

    void Start()
    {
        if (glowLight != null)
            glowLight.enabled = false;
    }

    public void StartRotation()
    {
        rotating = true;
        timer = rotationDuration;
    }

    void Update()
    {
        if (rotating)
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                rotating = false;

                if (glowLight != null)
                    glowLight.enabled = true;

                // äÍÑß ÇáÖæÁ ÈÚÏ ÅíÞÇÝ ÇáÏæÑÇä
                if (spotLightTransform != null)
                {
                    float targetX = (gameObject.name.ToLower().Contains("GoldenBox")) ? -40f : -440f;
                    targetPosition = new Vector3(targetX, spotLightTransform.position.y, spotLightTransform.position.z);
                    movingLight = true;
                }
            }
        }

        if (movingLight && spotLightTransform != null)
        {
            spotLightTransform.position = Vector3.MoveTowards(
                spotLightTransform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );

            if (Vector3.Distance(spotLightTransform.position, targetPosition) < 0.01f)
            {
                movingLight = false;
            }
        }
    }
}
