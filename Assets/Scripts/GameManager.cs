using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
	public TextMeshProUGUI mapText;
	private Map map;
	private Player player;

	[SerializeField] private float moveCooldown = 0.2f;  // 最初の移動と次の移動の間隔
	[SerializeField] private float moveRepeatRate = 0.05f; // 連続移動の間隔
	[SerializeField] private float timeSinceLastMove = 0f; // 最後に移動してからの時間
	private bool isMoving = false; // プレイヤーが動き続けているか
	private Vector2 moveDirection; // 移動の方向

	private void Start()
	{
		InitializeGame();
	}

	private void InitializeGame()
	{
		map = new Map(mapText);

		// プレイヤーをランダムな空いている場所に配置
		Vector2Int randomStartPosition = map.GetRandomEmptyPosition();
		player = new Player(randomStartPosition);

		map.DisplayMap(player.GetPosition());
	}

	private void Update()
	{
		// プレイヤーの連続移動の処理
		if (isMoving)
		{
			timeSinceLastMove += Time.deltaTime;

			// 指定された間隔が経過したら移動を行う
			if (timeSinceLastMove >= moveCooldown)
			{
				player.Move(moveDirection, map);
				map.DisplayMap(player.GetPosition());

				// 次の移動からは、連続移動の間隔に切り替える
				timeSinceLastMove = 0f;
				moveCooldown = moveRepeatRate;
			}
		}
	}

	// プレイヤーの移動処理を開始する
	public void StartMoving(Vector2 direction)
	{
		moveDirection = direction;
		isMoving = true;
		timeSinceLastMove = 0f; // タイマーをリセット
		moveCooldown = 0.2f;    // 最初の移動の遅延をリセット

		// 一度目の移動
		player.Move(direction, map);
		map.DisplayMap(player.GetPosition());
	}

	// プレイヤーの移動を停止する
	public void StopMoving()
	{
		isMoving = false;
	}
}
