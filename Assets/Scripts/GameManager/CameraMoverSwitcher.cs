using UnityEngine;
using DG.Tweening;

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
            isMoving = true;

            camera2.enabled = false;
            camera1.enabled = true;

            camera1.transform.DOMove(camera2.transform.position, moveDuration)
                .SetEase(Ease.InOutSine);

            camera1.transform.DORotateQuaternion(camera2.transform.rotation, moveDuration)
                .SetEase(Ease.InOutSine)
                .OnComplete(() =>
                {
                    camera2.enabled = true;
                    camera1.enabled = false;
                    isMoving = false;
                });
        }
    }
}
