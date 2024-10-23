using UnityEngine;

public class Player
{
	private Vector2Int position;

	public Player(Vector2Int startPosition)
	{
		position = startPosition;
	}

	public Vector2Int GetPosition()
	{
		return position;
	}

	public void Move(Vector2 direction, Map map)
	{
		Vector2Int newPosition = position + new Vector2Int((int)direction.x, -(int)direction.y);

		if (!map.IsWall(newPosition))
		{
			position = newPosition;
		}
	}
}
