using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class MapMove : MonoBehaviour
{
	public TextMeshProUGUI mapText; // TextMeshProUGUIをアタッチする
	string[,] map = new[,]
	{
		{"#","#","#","#","#","#","#","#","#","#","#","#"},
		{"#","0","0","0","0","0","0","0","0","0","0","#"},
		{"#","0","0","0","0","0","0","0","0","0","0","#"},
		{"#","0","0","0","0","0","0","0","0","0","0","#"},
		{"#","0","0","0","0","0","0","0","0","0","0","#"},
		{"#","0","0","0","0","0","0","0","0","0","0","#"},
		{"#","0","0","0","0","0","0","0","0","0","0","#"},
		{"#","0","0","0","0","0","0","0","0","0","0","#"},
		{"#","0","0","0","0","0","0","0","0","0","0","#"},
		{"#","0","0","0","0","0","0","0","0","0","0","#"},
		{"#","0","0","0","0","0","0","0","0","0","0","#"},
		{"#","#","#","#","#","#","#","#","#","#","#","#"}
	};

	string player = "@"; // プレイヤーの表示キャラクター
	Vector2Int playerPosition; // プレイヤーの位置
	private Vector2 moveInput;

	private float moveCooldown = 0.2f;  // 最初の連続移動を開始するまでの遅延
	private float moveRepeatRate = 0.1f; // 連続移動の間隔
	private float timeSinceLastMove = 0f; // 最後の移動からの経過時間
	private bool isMoving = false; // キーが押され続けているかどうか

	private void Start()
	{
		// ランダムな壁以外の位置にプレイヤーを配置
		SetRandomPlayerPosition();
		DisplayMap(); // マップを表示
	}

	// ランダムにプレイヤーを壁以外の位置に配置
	private void SetRandomPlayerPosition()
	{
		System.Random rand = new System.Random();
		int x, y;

		do
		{
			x = rand.Next(1, map.GetLength(1) - 1); // マップの壁を避ける範囲でランダムに選ぶ
			y = rand.Next(1, map.GetLength(0) - 1);
		}
		while (map[y, x] == "#"); // 壁ではない場所になるまで繰り返す

		playerPosition = new Vector2Int(x, y);
		map[playerPosition.y, playerPosition.x] = player;
	}

	// マップをTextMeshProUGUIに表示する
	private void DisplayMap()
	{
		string mapString = "";
		for (int y = 0; y < map.GetLength(0); y++)
		{
			for (int x = 0; x < map.GetLength(1); x++)
			{
				string currentTile = map[y, x];

				// プレイヤーの場所
				if (currentTile == player)
				{
					mapString += "<color=#00FF00>" + player + "</color>"; // プレイヤーを緑色に
				}
				// 壁の場所
				else if (currentTile == "#")
				{
					mapString += "<color=#FF0000>#</color>"; // 壁を赤色に
				}
				// その他の場所（床など）
				else
				{
					mapString += "<color=#FFFFFF>0</color>"; // 床を白色に
				}
			}
			mapString += "\n"; // 次の行に移動
		}
		mapText.text = mapString; // TextMeshProUGUIにマップを表示
	}

	// プレイヤーの移動処理
	public void OnMove(InputAction.CallbackContext context)
	{
		if (context.performed) // キーが押された瞬間に1マス移動
		{
			moveInput = context.ReadValue<Vector2>();
			isMoving = true; // キーが押され続けている状態
			timeSinceLastMove = 0f; // タイマーリセット
			MovePlayer(); // まず1マス移動
		}

		if (context.canceled) // キーが離されたら移動を止める
		{
			isMoving = false;
		}
	}

	private void Update()
	{
		// キーが押され続けている場合
		if (isMoving)
		{
			timeSinceLastMove += Time.deltaTime;

			// 最初の連続移動の遅延後、連続移動を開始
			if (timeSinceLastMove >= moveCooldown)
			{
				MovePlayer(); // 連続移動
				timeSinceLastMove = 0f; // 移動後にタイマーをリセット
				moveCooldown = moveRepeatRate; // 次からは連続移動の間隔に切り替え
			}
		}
	}

	// プレイヤーを移動させる処理
	private void MovePlayer()
	{
		// 新しい位置を計算
		int newX = playerPosition.x + (int)moveInput.x;
		int newY = playerPosition.y - (int)moveInput.y; // Y軸は逆向き

		// 新しい位置が壁でないかチェック
		if (map[newY, newX] != "#")
		{
			// 元の位置をクリア
			map[playerPosition.y, playerPosition.x] = "0";

			// 新しい位置にプレイヤーを移動
			playerPosition = new Vector2Int(newX, newY);
			map[playerPosition.y, playerPosition.x] = player;

			// マップを再描画
			DisplayMap();
		}
	}
}
