// using UnityEngine;
// using System.Collections;

// public class CameraShake : MonoBehaviour
// {
//     public static CameraShake instance;

//     [Header("Shake Settings")]
//     public float shakeDuration = 0.2f;
//     public float shakeMagnitude = 0.1f;

//     private Vector3 originalPos;

//     private void Awake()
//     {
//         // Singleton pattern
//         if (instance == null) instance = this;
//         else Destroy(gameObject);

//         originalPos = transform.localPosition;
//     }

//     public void Shake()
//     {
//         StopAllCoroutines();
//         StartCoroutine(ShakeCoroutine());
//     }

//     private IEnumerator ShakeCoroutine()
//     {
//         float elapsed = 0f;

//         while (elapsed < shakeDuration)
//         {
//             Vector3 randomOffset = Random.insideUnitSphere * shakeMagnitude;
//             transform.localPosition = originalPos + randomOffset;

//             elapsed += Time.deltaTime;
//             yield return null;
//         }

//         transform.localPosition = originalPos;
//     }
// }
using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;

    [Header("Shake Settings")]
    public float shakeDuration = 0.2f;
    public float shakeMagnitude = 0.1f;

    [Header("Recoil Settings")]
    public float recoilDistance = 0.3f;     // Độ dài giật lùi (có thể chỉnh trên Inspector)
    public float recoilDuration = 0.1f;     // Thời gian giật lùi

    private Vector3 originalPos;

    private void Awake()
    {
        // Singleton pattern
        if (instance == null) instance = this;
        else Destroy(gameObject);

        originalPos = transform.localPosition;
    }

    public void Shake()
    {
        StopAllCoroutines();
        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            Vector3 randomOffset = Random.insideUnitSphere * shakeMagnitude;
            transform.localPosition = originalPos + randomOffset;

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
    }

    /// <summary>
    /// Hiệu ứng giật lùi camera (Recoil)
    /// </summary>
    /// <param name="direction">Hướng giật lùi (mặc định lùi về phía sau local Z)</param>
    public void Recoil(Vector3? direction = null)
    {
        StopAllCoroutines();
        Vector3 dir = direction ?? -transform.forward; // Lùi về phía sau local Z
        StartCoroutine(RecoilCoroutine(dir.normalized));
    }

    private IEnumerator RecoilCoroutine(Vector3 direction)
    {
        float elapsed = 0f;

        Vector3 startPos = originalPos;
        Vector3 recoilPos = originalPos + direction * recoilDistance;

        // Di chuyển camera về phía sau
        while (elapsed < recoilDuration)
        {
            transform.localPosition = Vector3.Lerp(startPos, recoilPos, elapsed / recoilDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = recoilPos;

        // Trả camera về vị trí cũ
        elapsed = 0f;
        while (elapsed < recoilDuration)
        {
            transform.localPosition = Vector3.Lerp(recoilPos, originalPos, elapsed / recoilDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPos;
    }
}
