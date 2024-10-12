using System.Numerics;

public class Actor : Object
{
	Vector2 currentPos;
	int currentMapID;

	public void Move(Vector2 actorPos, Vector2 range)
	{
		if (CanMove(actorPos, range))
		{
			Actor.Position += range;
		}
	}

public bool CanMove(Vector2 currentPos, Vector2 range){

}

}
