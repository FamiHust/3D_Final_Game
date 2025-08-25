using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SimpleParticleManager : MonoBehaviour
{
	public static SimpleParticleManager Instance;
	
	[Header("Particle Effects")]
	[SerializeField] private ParticleSystem damageEffect; // Hiệu ứng chịu sát thương
	[SerializeField] private ParticleSystem healEffect; // Hiệu ứng hồi máu
	[SerializeField] private ParticleSystem targetSelectEffect; // Hiệu ứng chọn mục tiêu (bật khi có thể đánh thẳng HP)
	[SerializeField] private Color attackLineColor = Color.white; // Màu line tấn công
	
	[Header("Effect Settings")]
	[SerializeField] private float effectHeightOffset = 1f; // Độ cao hiệu ứng so với object
	[SerializeField] private bool targetEffectLoops = true; // Effect chọn mục tiêu chạy lặp
	[SerializeField] private Vector3 targetEffectRotationEuler = new Vector3(90f, 0f, 0f); // Rotation mặc định cho effect chọn mục tiêu
	
	[Header("Anchors (Optional)")]
	[SerializeField] private Transform enemyHpAnchor; // Kéo thả Transform của Enemy HP (ví dụ: Health_Bar)
	[SerializeField] private Vector3 enemyHpAnchorOffset = Vector3.zero; // Offset cho anchor HP
	[SerializeField] private Transform enemyModelAnchor; // Kéo thả Transform của Enemy Model (giống 2 hiệu ứng trước)
	[SerializeField] private Vector3 enemyModelAnchorOffset = Vector3.zero; // Offset cho anchor Model
	
	private ParticleSystem activeTargetEffect;
	private Transform targetEffectFollow; // Theo dõi theo Transform (vd: Enemy HP bar / Enemy Model)
	private Vector3 targetEffectOffset = Vector3.zero;

	// Attack straight line with arrow head - faded out smoothly
	private IEnumerator FadeAndDestroyGroup(CanvasGroup cg, GameObject root, float duration)
	{
		if (cg == null || root == null) yield break;
		float t = 0f;
		float start = cg.alpha;
		while (t < duration)
		{
			t += Time.deltaTime;
			float a = Mathf.Lerp(start, 0f, t / duration);
			cg.alpha = a;
			yield return null;
		}
		if (root != null) Destroy(root);
	}

	public void ShowAttackLine(RectTransform from, RectTransform to, float lineWidth = 6f, float headLength = 28f, float duration = 0.6f, Color? colorOverride = null)
	{
		if (from == null || to == null) return;
		Canvas canvas = from.GetComponentInParent<Canvas>();
		if (canvas == null) canvas = to.GetComponentInParent<Canvas>();
		if (canvas == null) return;

		RectTransform canvasRect = canvas.transform as RectTransform;
		Camera cam = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;

		Vector2 p1, p2;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, RectTransformUtility.WorldToScreenPoint(cam, from.position), cam, out p1);
		RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, RectTransformUtility.WorldToScreenPoint(cam, to.position), cam, out p2);

		Vector2 dir = p2 - p1;
		float length = dir.magnitude;
		if (length <= Mathf.Epsilon) return;
		Vector2 dirN = dir / length;
		float angle = Mathf.Atan2(dirN.y, dirN.x) * Mathf.Rad2Deg;

		GameObject root = new GameObject("AttackLineRoot", typeof(RectTransform), typeof(CanvasRenderer), typeof(CanvasGroup));
		root.transform.SetParent(canvasRect, false);
		RectTransform rootRt = root.GetComponent<RectTransform>();
		rootRt.anchorMin = new Vector2(0.5f, 0.5f);
		rootRt.anchorMax = new Vector2(0.5f, 0.5f);
		rootRt.pivot = new Vector2(0.5f, 0.5f);
		rootRt.anchoredPosition = Vector2.zero;
		rootRt.sizeDelta = canvasRect.sizeDelta;
		CanvasGroup cg = root.GetComponent<CanvasGroup>();
		cg.alpha = 1f;

		Color col = colorOverride.HasValue ? colorOverride.Value : attackLineColor;

		// Body
		float bodyLen = Mathf.Max(0f, length - headLength);
		Vector2 bodyCenter = p1 + dirN * (bodyLen * 0.5f);
		GameObject bodyObj = new GameObject("LineBody", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
		bodyObj.transform.SetParent(rootRt, false);
		RectTransform bodyRt = bodyObj.GetComponent<RectTransform>();
		bodyRt.anchorMin = new Vector2(0.5f, 0.5f);
		bodyRt.anchorMax = new Vector2(0.5f, 0.5f);
		bodyRt.pivot = new Vector2(0.5f, 0.5f);
		bodyRt.anchoredPosition = bodyCenter;
		bodyRt.sizeDelta = new Vector2(bodyLen, lineWidth);
		bodyRt.localRotation = Quaternion.Euler(0f, 0f, angle);
		Image bodyImg = bodyObj.GetComponent<Image>();
		bodyImg.color = col;

		// Head (rectangle, thicker)
		Vector2 headCenter = p1 + dirN * (bodyLen + headLength * 0.5f);
		GameObject headObj = new GameObject("LineHead", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
		headObj.transform.SetParent(rootRt, false);
		RectTransform headRt = headObj.GetComponent<RectTransform>();
		headRt.anchorMin = new Vector2(0.5f, 0.5f);
		headRt.anchorMax = new Vector2(0.5f, 0.5f);
		headRt.pivot = new Vector2(0.5f, 0.5f);
		headRt.anchoredPosition = headCenter;
		headRt.sizeDelta = new Vector2(headLength, lineWidth * 2.0f);
		headRt.localRotation = Quaternion.Euler(0f, 0f, angle);
		Image headImg = headObj.GetComponent<Image>();
		headImg.color = col;

		StartCoroutine(FadeAndDestroyGroup(cg, root, duration));
	}

	public void ShowAttackDashedLine(
		RectTransform from,
		RectTransform to,
		float lineWidth = 6f,
		float dashLength = 20f,
		float gapLength = 14f,
		float headLength = 32f,
		float duration = 0.6f,
		Color? colorOverride = null,
		float dashScrollSpeed = 180f)
	{
		if (from == null || to == null) return;
		Canvas canvas = from.GetComponentInParent<Canvas>();
		if (canvas == null) canvas = to.GetComponentInParent<Canvas>();
		if (canvas == null) return;

		RectTransform canvasRect = canvas.transform as RectTransform;
		Camera cam = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;

		Vector2 p1, p2;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, RectTransformUtility.WorldToScreenPoint(cam, from.position), cam, out p1);
		RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, RectTransformUtility.WorldToScreenPoint(cam, to.position), cam, out p2);

		Vector2 dir = p2 - p1;
		float length = dir.magnitude;
		if (length <= Mathf.Epsilon) return;
		Vector2 dirN = dir / length;
		float angle = Mathf.Atan2(dirN.y, dirN.x) * Mathf.Rad2Deg;

		// Root container (covers canvas) to avoid layout issues
		GameObject root = new GameObject("AttackDashedLineRoot", typeof(RectTransform), typeof(CanvasRenderer), typeof(CanvasGroup));
		root.transform.SetParent(canvasRect, false);
		RectTransform rootRt = root.GetComponent<RectTransform>();
		rootRt.anchorMin = new Vector2(0.5f, 0.5f);
		rootRt.anchorMax = new Vector2(0.5f, 0.5f);
		rootRt.pivot = new Vector2(0.5f, 0.5f);
		rootRt.anchoredPosition = Vector2.zero;
		rootRt.sizeDelta = canvasRect.sizeDelta;
		CanvasGroup cg = root.GetComponent<CanvasGroup>();
		cg.alpha = 1f;

		Color col = colorOverride.HasValue ? colorOverride.Value : attackLineColor;

		// Helper child aligned to the line to place dashes along local X
		GameObject lineSpace = new GameObject("LineSpace", typeof(RectTransform));
		lineSpace.transform.SetParent(rootRt, false);
		RectTransform lineRt = lineSpace.GetComponent<RectTransform>();
		lineRt.anchorMin = new Vector2(0.5f, 0.5f);
		lineRt.anchorMax = new Vector2(0.5f, 0.5f);
		lineRt.pivot = new Vector2(0.5f, 0.5f);
		lineRt.anchoredPosition = (p1 + p2) * 0.5f;
		lineRt.sizeDelta = new Vector2(length, lineWidth);
		lineRt.localRotation = Quaternion.Euler(0f, 0f, angle);

		// Compute body length (exclude head)
		float bodyLen = Mathf.Max(0f, length - headLength);
		float halfBody = bodyLen * 0.5f;
		float startX = -halfBody + dashLength * 0.5f;
		float step = dashLength + gapLength;
		List<RectTransform> dashRects = new List<RectTransform>();
		for (float x = startX; x <= halfBody + 0.001f; x += step)
		{
			float remaining = halfBody - x + dashLength * 0.5f;
			float segLen = Mathf.Min(dashLength, remaining + dashLength * 0.5f);
			if (segLen <= 0.01f) break;
			GameObject dash = new GameObject("Dash", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
			dash.transform.SetParent(lineRt, false);
			RectTransform dRt = dash.GetComponent<RectTransform>();
			dRt.anchorMin = new Vector2(0.5f, 0.5f);
			dRt.anchorMax = new Vector2(0.5f, 0.5f);
			dRt.pivot = new Vector2(0.5f, 0.5f);
			dRt.anchoredPosition = new Vector2(x, 0f);
			dRt.sizeDelta = new Vector2(segLen, lineWidth);
			Image dImg = dash.GetComponent<Image>();
			dImg.color = col;
			dashRects.Add(dRt);
		}

		// Arrow head rectangle at end
		GameObject head = new GameObject("Head", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
		head.transform.SetParent(lineRt, false);
		RectTransform hRt = head.GetComponent<RectTransform>();
		hRt.anchorMin = new Vector2(0.5f, 0.5f);
		hRt.anchorMax = new Vector2(0.5f, 0.5f);
		hRt.pivot = new Vector2(0.5f, 0.5f);
		hRt.anchoredPosition = new Vector2(halfBody + headLength * 0.5f, 0f);
		hRt.sizeDelta = new Vector2(headLength, lineWidth * 2.0f);
		Image hImg = head.GetComponent<Image>();
		hImg.color = col;

		// Animate dashes scrolling toward the head like a moving wheel along the line
		float minX = -halfBody + dashLength * 0.5f;
		float maxX =  halfBody - dashLength * 0.5f;
		StartCoroutine(AnimateDashes(dashRects, minX, maxX, dashScrollSpeed, duration));
		
		// Fade out the entire line and destroy at the end
		StartCoroutine(FadeAndDestroyGroup(cg, root, duration));
	}

	// Animate dash segments along local X so the dashes appear to move toward the target
	private IEnumerator AnimateDashes(List<RectTransform> dashRects, float minX, float maxX, float speed, float duration)
	{
		if (dashRects == null || dashRects.Count == 0) yield break;
		float elapsed = 0f;
		while (elapsed < duration)
		{
			elapsed += Time.deltaTime;
			for (int i = 0; i < dashRects.Count; i++)
			{
				RectTransform d = dashRects[i];
				if (d == null) continue;
				Vector2 p = d.anchoredPosition;
				p.x += speed * Time.deltaTime; // move toward the head (positive X)
				if (p.x > maxX)
				{
					p.x = minX; // wrap to start to maintain continuous motion
				}
				d.anchoredPosition = p;
			}
			yield return null;
		}
	}
	
	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}
	
	private void LateUpdate()
	{
		// Cập nhật vị trí effect chọn mục tiêu nếu đang bám theo 1 anchor
		if (activeTargetEffect != null && targetEffectFollow != null)
		{
			activeTargetEffect.transform.position = targetEffectFollow.position + targetEffectOffset + Vector3.up * effectHeightOffset;
		}
	}
	
	// Hiển thị hiệu ứng chịu sát thương
	public void ShowDamageEffect(Vector3 position)
	{
		if (damageEffect != null)
		{
			Vector3 effectPosition = position + Vector3.up * effectHeightOffset;
			ParticleSystem effect = Instantiate(damageEffect, effectPosition, Quaternion.identity);
			
			// Tự động hủy sau khi hoàn thành
			Destroy(effect.gameObject, effect.main.duration);
		}
	}
	
	// Hiển thị hiệu ứng hồi máu
	public void ShowHealEffect(Vector3 position)
	{
		if (healEffect != null)
		{
			Vector3 effectPosition = position + Vector3.up * effectHeightOffset;
			ParticleSystem effect = Instantiate(healEffect, effectPosition, Quaternion.identity);
			
			// Tự động hủy sau khi hoàn thành
			Destroy(effect.gameObject, effect.main.duration);
		}
	}
	
	// Bật hiệu ứng chọn mục tiêu tại một vị trí cụ thể (không bám theo)
	public void ShowTargetSelectEffect(Vector3 position)
	{
		if (targetSelectEffect == null) return;
		HideTargetSelectEffect();
		
		Quaternion rot = Quaternion.Euler(targetEffectRotationEuler);
		activeTargetEffect = Instantiate(targetSelectEffect, position + Vector3.up * effectHeightOffset, rot);
		targetEffectFollow = null;
		targetEffectOffset = Vector3.zero;
		
		// Nếu effect không phải loop thì tự hủy theo thời lượng
		if (!targetEffectLoops)
		{
			Destroy(activeTargetEffect.gameObject, activeTargetEffect.main.duration);
		}
	}
	
	// Bật hiệu ứng chọn mục tiêu và bám theo một Transform (ví dụ: thanh máu Enemy hoặc Enemy Model)
	public void ShowTargetSelectEffect(Transform followTransform, Vector3 offset = default)
	{
		if (targetSelectEffect == null || followTransform == null) return;
		HideTargetSelectEffect();
		
		targetEffectFollow = followTransform;
		targetEffectOffset = offset;
		Quaternion rot = Quaternion.Euler(targetEffectRotationEuler);
		activeTargetEffect = Instantiate(targetSelectEffect, followTransform.position + offset + Vector3.up * effectHeightOffset, rot);
		
		// Nếu effect không loop, vẫn đảm bảo tự hủy sau duration
		if (!targetEffectLoops)
		{
			Destroy(activeTargetEffect.gameObject, activeTargetEffect.main.duration);
		}
	}
	
	// Convenience API: Bật hiệu ứng bám theo Enemy HP (ưu tiên anchor, fallback theo tên GameObject "Health_Bar")
	public void ShowTargetSelectOnEnemyHp()
	{
		Transform anchor = enemyHpAnchor;
		if (anchor == null)
		{
			GameObject hpGo = GameObject.Find("Health_Bar");
			if (hpGo != null) anchor = hpGo.transform;
		}
		if (anchor != null)
		{
			ShowTargetSelectEffect(anchor, enemyHpAnchorOffset);
		}
	}
	
	// Convenience API: Bật hiệu ứng bám theo Enemy Model (giống 2 hiệu ứng trước)
	public void ShowTargetSelectOnEnemyModel()
	{
		if (enemyModelAnchor == null)
		{
			// Yêu cầu gán qua Inspector hoặc set từ code
			return;
		}
		ShowTargetSelectEffect(enemyModelAnchor, enemyModelAnchorOffset);
	}
	
	// Tắt hiệu ứng chọn mục tiêu (nếu đang bật)
	public void HideTargetSelectEffect()
	{
		if (activeTargetEffect != null)
		{
			Destroy(activeTargetEffect.gameObject);
			activeTargetEffect = null;
			targetEffectFollow = null;
			targetEffectOffset = Vector3.zero;
		}
	}
	
	// Binding tiện lợi từ code/UI
	public void SetEnemyHpAnchor(Transform anchor, Vector3? offset = null)
	{
		enemyHpAnchor = anchor;
		if (offset.HasValue) enemyHpAnchorOffset = offset.Value;
	}
	
	public void SetEnemyModelAnchor(Transform anchor, Vector3? offset = null)
	{
		enemyModelAnchor = anchor;
		if (offset.HasValue) enemyModelAnchorOffset = offset.Value;
	}
}
