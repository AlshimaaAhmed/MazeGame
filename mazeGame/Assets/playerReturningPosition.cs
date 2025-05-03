using UnityEngine;

public class PlayerReturnPosition : MonoBehaviour
{
    void Start()
    {

        Debug.Log("DatatoBeShared.ReturnPosition: " + DatatoBeShared.ReturnPosition);

        // áæ İí ãßÇä ÓÇÈŞ ãÊÓÌá
        if (DatatoBeShared.ReturnPosition != Vector3.zero)
        {
            Debug.Log("playerPosition: " + DatatoBeShared.ReturnPosition);

            // ÑÌøÚ ÇááÇÚÈ ááãæŞÚ ÇáŞÏíã
            transform.position = DatatoBeShared.ReturnPosition;

            // ÕİÑ ÇáŞíãÉ ÚáÔÇä ãÇ ÊÊßÑÑ ÇáãÑÉ ÇáÌÇíÉ
            DatatoBeShared.ReturnPosition = Vector3.zero;
        }
    }
}
