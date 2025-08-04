using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance;

    [Header("Effect Prefabs")]
    public GameObject attackEffectPrefab;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Dành cho RectTransform (UI)
    // public void PlayAttackEffectAtUI(RectTransform uiTransform, Camera uiCamera, Transform parent = null, float destroyAfter = 2f)
    // {
    //     Vector3 worldPos = uiCamera.WorldToScreenPoint(uiTransform.position);  // Lấy screen position
    //     worldPos.z = 0f;

    //     Vector3 worldEffectPos = uiCamera.ScreenToWorldPoint(worldPos);        // Chuyển lại về world space
    //     worldEffectPos.z = 0f;

    //     PlayEffect(attackEffectPrefab, worldEffectPos, parent, destroyAfter);
    // }
    public void PlayAttackEffectAtUI(RectTransform uiTransform, float destroyAfter = 3f)
    {
        // Instantiate với đúng position & rotation của UI element
        GameObject fx = Instantiate(attackEffectPrefab, uiTransform.position, uiTransform.rotation);

        // Gắn làm con để kế thừa scale/rotation chính xác
        fx.transform.SetParent(uiTransform, worldPositionStays: true);
        Destroy(fx, destroyAfter);
    }



    public void PlayEffect(GameObject prefab, Vector3 position, Transform parent = null, float destroyAfter = 3f)
    {
        if (prefab == null)
        {
            Debug.LogWarning("Prefab effect bị null!");
            return;
        }

        GameObject fx = Instantiate(prefab, position, Quaternion.identity);

        if (parent != null)
            fx.transform.SetParent(parent);

        Destroy(fx, destroyAfter);
    }
}
