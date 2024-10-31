using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	[SerializeField] float moveSpeed = 5f;
	[SerializeField] Grid grid; // Gridコンポーネントへの参照を追加

	private PlayerInput playerInput;
	private InputAction moveAction;
	private bool isMoving;
	private Vector2 input;

	private void Awake()
	{
		playerInput = GetComponent<PlayerInput>();
		moveAction = playerInput.actions["Move"];

		// Gridコンポーネントが設定されていない場合は自動で取得
		if (grid == null)
		{
			grid = FindObjectOfType<Grid>();
		}

		// 開始時にタイルの中心に移動
		SnapToGridCenter();
	}

	/// <summary>
	/// GameObjectがアクティブになったとき発動
	/// </summary>
	private void OnEnable()
	{
		moveAction.Enable();
	}

	/// <summary>
	/// GameObjectが非アクティブになったとき発動
	/// </summary>
	private void OnDisable()
	{
		moveAction.Disable();
	}

	// 最も近いタイルの中心にスナップする
	private void SnapToGridCenter()
	{
		Vector3Int cellPosition = grid.WorldToCell(transform.position);
		transform.position = grid.GetCellCenterWorld(cellPosition);
	}

	private void Update()
	{
		if (!isMoving)
		{
			input = moveAction.ReadValue<Vector2>();
			Vector2 discreteInput = GetDiscreteInput(input);

			if (discreteInput != Vector2.zero)
			{
				// 現在位置のグリッドセル座標を取得
				Vector3Int currentCell = grid.WorldToCell(transform.position);

				// 移動先のセル座標を計算
				Vector3Int targetCell = currentCell + new Vector3Int(
					(int)discreteInput.x,
					(int)discreteInput.y,
					0
				);

				// セル座標からワールド座標（セルの中心位置）を取得
				Vector3 targetPos = grid.GetCellCenterWorld(targetCell);

				StartCoroutine(Move(targetPos));
			}
		}
	}

	private Vector2 GetDiscreteInput(Vector2 rawInput)
	{
		if (rawInput == Vector2.zero) return Vector2.zero;

		if (Mathf.Abs(rawInput.x) > Mathf.Abs(rawInput.y))
		{
			return new Vector2(Mathf.Sign(rawInput.x), 0);
		}
		else
		{
			return new Vector2(0, Mathf.Sign(rawInput.y));
		}
	}

	private IEnumerator Move(Vector3 targetPos)
	{
		isMoving = true;

		while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
		{
			transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
			yield return null;
		}

		transform.position = targetPos;
		isMoving = false;
	}

	// デバッグ用：グリッドとの関係を可視化
	private void OnDrawGizmosSelected()
	{
		if (!Application.isPlaying && grid != null)
		{
			// 現在位置のセル
			Vector3Int currentCell = grid.WorldToCell(transform.position);
			Vector3 cellCenter = grid.GetCellCenterWorld(currentCell);

			// 現在のセルを赤で表示
			Gizmos.color = Color.red;
			Vector3 cellSize = grid.cellSize;
			Gizmos.DrawWireCube(cellCenter, cellSize);

			// 移動可能な4方向のセルを黄色で表示
			Gizmos.color = Color.yellow;
			Vector3Int[] directions = new Vector3Int[]
			{
				Vector3Int.up,
				Vector3Int.right,
				Vector3Int.down,
				Vector3Int.left
			};

			foreach (Vector3Int dir in directions)
			{
				Vector3Int targetCell = currentCell + dir;
				Vector3 targetCenter = grid.GetCellCenterWorld(targetCell);
				Gizmos.DrawWireCube(targetCenter, cellSize);
			}

			// 現在位置からセルの中心までの線を描画
			Gizmos.color = Color.blue;
			Gizmos.DrawLine(transform.position, cellCenter);
		}
	}
}