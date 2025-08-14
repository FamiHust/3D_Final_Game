using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance;
    public GameObject attackEffectPrefab;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void PlayAttackEffectAtUI(RectTransform uiTransform, float destroyAfter = 3f)
    {
        GameObject fx = Instantiate(attackEffectPrefab, uiTransform.position, uiTransform.rotation);

        fx.transform.SetParent(uiTransform, worldPositionStays: true);
        Destroy(fx, destroyAfter);
    }

    public void PlayEffect(GameObject prefab, Vector3 position, Transform parent = null, float destroyAfter = 3f)
    {
        if (prefab == null)
        {
            return;
        }

        GameObject fx = Instantiate(prefab, position, Quaternion.identity);

        if (parent != null)
            fx.transform.SetParent(parent);

        Destroy(fx, destroyAfter);
    }
}
