using UnityEngine;
using System.Collections;

public class CameraMoverSwitcher : MonoBehaviour
{
    public Camera camera1;
    public Camera camera2;
    public float moveDuration = 2f; 

    private bool isMoving = false;

    public void SwitchCameraFrom1To2()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveAndSwitchFrom1To2());
        }
    }

    IEnumerator MoveAndSwitchFrom1To2()
    {
        isMoving = true;

        Vector3 startPosition = camera1.transform.position;
        Quaternion startRotation = camera1.transform.rotation;

        Vector3 targetPosition = camera2.transform.position;
        Quaternion targetRotation = camera2.transform.rotation;

        camera2.enabled = false;
        camera1.enabled = true;

        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / moveDuration;

            camera1.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            camera1.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);

            yield return null;
        }

        camera1.transform.position = targetPosition;
        camera1.transform.rotation = targetRotation;

        camera2.enabled = true;
        camera1.enabled = false;

        isMoving = false;
    }

}