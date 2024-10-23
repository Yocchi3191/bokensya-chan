using UnityEngine;
using TMPro;

public class Map
{
	private string[,] map;
	private TextMeshProUGUI mapText;

	public Map(TextMeshProUGUI mapText)
	{
		this.mapText = mapText;
		GenerateMap();
	}

	public void GenerateMap()
	{
		map = new[,]
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
	}

	public void DisplayMap(Vector2Int playerPosition)
	{
		string mapString = "";
		for (int y = 0; y < map.GetLength(0); y++)
		{
			for (int x = 0; x < map.GetLength(1); x++)
			{
				if (x == playerPosition.x && y == playerPosition.y)
				{
					mapString += "<color=#00FF00>@</color>"; // プレイヤーを緑色に表示
				}
				else if (map[y, x] == "#")
				{
					mapString += "<color=#FF0000>#</color>"; // 壁を赤色に表示
				}
				else
				{
					mapString += "<color=#FFFFFF>0</color>"; // 床を白色に表示
				}
			}
			mapString += "\n";
		}
		mapText.text = mapString;
	}


	/// <summary>
	///Map上の壁でないランダムな座標を返す 
	/// </summary>
	/// <returns></returns>
	public Vector2Int GetRandomEmptyPosition()
	{
		System.Random rand = new System.Random();
		int x, y;

		// ランダムに空いている（"0"の）場所を探す
		do
		{
			x = rand.Next(1, map.GetLength(1) - 1); // マップの内側でランダムに選ぶ
			y = rand.Next(1, map.GetLength(0) - 1);
		}
		while (map[y, x] != "0"); // 空いている場所（"0"）になるまで繰り返す

		return new Vector2Int(x, y);
	}

	/// <summary>
	/// Mapの指定した箇所が壁(#)かどうかを返す
	/// </summary>
	/// <param name="position"></param>
	/// <returns></returns>
	public bool IsWall(Vector2Int position)
	{
		return map[position.y, position.x] == "#";
	}
}
