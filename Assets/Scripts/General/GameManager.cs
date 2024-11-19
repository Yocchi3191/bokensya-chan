using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class GameManager : MonoBehaviour
{
	[SerializeField] Tilemap field;
	public static GameManager _instance;
	public static GameManager Instance { get { return _instance; } }
	private void Awake()
	{
		if (_instance == null)
		{
			_instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void Start()
	{
		SetRandomStartPosition();
	}
	private void SetRandomStartPosition()
	{
		// タイルが存在する全ての位置を取得
		List<Vector3Int> availablePositions = new List<Vector3Int>();
		BoundsInt bounds = field.cellBounds;

		for (int x = bounds.min.x; x < bounds.max.x; x++)
		{
			for (int y = bounds.min.y; y < bounds.max.y; y++)
			{
				Vector3Int tilePosition = new Vector3Int(x, y, 0);
				if (field.HasTile(tilePosition))
				{
					availablePositions.Add(tilePosition);
				}
			}
		}

		if (availablePositions.Count > 0)
		{
			// ランダムな位置を選択
			int randomIndex = Random.Range(0, availablePositions.Count);
			Vector3Int randomCell = availablePositions[randomIndex];

			// 選択した位置にプレイヤーを配置
			Vector3 worldPosition = field.GetCellCenterWorld(randomCell);
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			if (player != null)
			{
				player.transform.position = worldPosition;
			}

			Debug.Log($"ランダムな開始位置に配置: {randomCell}");
		}
		else
		{
			Debug.LogWarning("利用可能なタイルが見つかりません！");
		}
	}

	// 任意の位置にプレイヤーを再配置するパブリックメソッド
	public void Respawn()
	{
		SetRandomStartPosition();
	}
}