using UnityEngine;
using System.Collections;

public class EnemyBoardWatcher : MonoBehaviour
{
	[Header("Check Settings")]
	[SerializeField] private float checkInterval = 0.1f; // Giảm để responsive hơn
	[SerializeField] private string enemyZoneBaseName = "Enemy_Zone"; // Enemy_Zone, Enemy_Zone1..Enemy_Zone7
	[SerializeField] private int zoneCount = 8;
	[SerializeField] private bool autoStart = true;

	[Header("Effect Settings")]
	[SerializeField] private bool followEnemyModel = true; // true: bám model (giống 2 hiệu ứng trước), false: bám HP bar
	[SerializeField] private Vector3 extraOffset = Vector3.zero;

	private Transform[] enemyZones;
	private bool? lastEmptyState;
	private Coroutine checkRoutine;
	private bool lastIsYourTurn;
	private TurnSystem turnSystem;

	private void Start()
	{
		CacheZones();
		turnSystem = FindObjectOfType<TurnSystem>();
		lastIsYourTurn = TurnSystem.isYourTurn;
		HandleTurnGate(lastIsYourTurn);
	}

	private void Update()
	{
		bool nowIsYourTurn = TurnSystem.isYourTurn;
		if (nowIsYourTurn != lastIsYourTurn)
		{
			HandleTurnGate(nowIsYourTurn);
			lastIsYourTurn = nowIsYourTurn;
		}
	}

	private void HandleTurnGate(bool isPlayerTurn)
	{
		if (isPlayerTurn)
		{
			// Vào lượt của bạn: có thể bỏ qua lượt đầu để không check bàn AI
			lastEmptyState = null; // force re-evaluate
			if (turnSystem == null) turnSystem = FindObjectOfType<TurnSystem>();
			int playerTurns = turnSystem != null ? turnSystem.playerTurnCount : 0;
			bool shouldSkipFirstTurn = playerTurns <= 1;
			bool gameClockStarted = turnSystem != null && turnSystem.timerStart;
			if (!shouldSkipFirstTurn && gameClockStarted)
			{
				StartWatching();
			}
			else
			{
				StopWatching();
				HideEffectIfAny();
			}
		}
		else
		{
			// Lượt AI: dừng kiểm tra và tắt hiệu ứng
			StopWatching();
			HideEffectIfAny();
		}
	}

	public void StartWatching()
	{
		if (checkRoutine == null)
		{
			checkRoutine = StartCoroutine(WatchLoop());
		}
		
		// Force check ngay lập tức khi bắt đầu watching
		ForceCheckNow();
	}

	public void StopWatching()
	{
		if (checkRoutine != null)
		{
			StopCoroutine(checkRoutine);
			checkRoutine = null;
		}
	}

	private void CacheZones()
	{
		enemyZones = new Transform[zoneCount];
		for (int i = 0; i < zoneCount; i++)
		{
			string name = i == 0 ? enemyZoneBaseName : enemyZoneBaseName + i;
			GameObject go = GameObject.Find(name);
			enemyZones[i] = go != null ? go.transform : null;
		}
	}

	private IEnumerator WatchLoop()
	{
		var wait = new WaitForSeconds(checkInterval);
		while (true)
		{
			bool isEmpty = IsEnemyBoardEmpty();
			if (lastEmptyState == null || lastEmptyState.Value != isEmpty)
			{
				ToggleTargetEffect(isEmpty);
				lastEmptyState = isEmpty;
			}
			yield return wait;
		}
	}

	private bool IsEnemyBoardEmpty()
	{
		// If zones not cached (e.g. scene reloaded), try cache again
		if (enemyZones == null || enemyZones.Length == 0)
		{
			CacheZones();
		}
		for (int i = 0; i < zoneCount; i++)
		{
			Transform z = enemyZones[i];
			if (z == null)
			{
				// attempt lazy resolve
				string name = i == 0 ? enemyZoneBaseName : enemyZoneBaseName + i;
				GameObject go = GameObject.Find(name);
				enemyZones[i] = go != null ? go.transform : null;
				z = enemyZones[i];
			}
			if (z != null && z.childCount > 0) return false;
		}
		return true;
	}

	private void ToggleTargetEffect(bool show)
	{
		if (SimpleParticleManager.Instance == null)
			return;

		if (show)
		{
			if (followEnemyModel)
			{
				SimpleParticleManager.Instance.ShowTargetSelectOnEnemyModel();
			}
			else
			{
				SimpleParticleManager.Instance.ShowTargetSelectOnEnemyHp();
			}
		}
		else
		{
			HideEffectIfAny();
		}
	}

	private void HideEffectIfAny()
	{
		if (SimpleParticleManager.Instance != null)
		{
			SimpleParticleManager.Instance.HideTargetSelectEffect();
		}
	}
	
	// Force check ngay lập tức để hiển thị effect
	public void ForceCheckNow()
	{
		bool isEmpty = IsEnemyBoardEmpty();
		ToggleTargetEffect(isEmpty);
		lastEmptyState = isEmpty;
	}
	
	// Public method để bật effect chỉ mục tiêu trực tiếp (dùng cho spell)
	public void ShowTargetEffectDirectly()
	{
		if (SimpleParticleManager.Instance != null)
		{
			if (followEnemyModel)
			{
				SimpleParticleManager.Instance.ShowTargetSelectOnEnemyModel();
			}
			else
			{
				SimpleParticleManager.Instance.ShowTargetSelectOnEnemyHp();
			}
		}
	}
	
	// Public method để tắt effect chỉ mục tiêu (dùng khi spell bị phá hủy)
	public void HideTargetEffectDirectly()
	{
		HideEffectIfAny();
	}
}


