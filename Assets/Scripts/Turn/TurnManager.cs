using System.Collections;
using UnityEngine;

namespace BokenshaChan
{
	public class TurnManager : MonoBehaviour
	{
		public void StartTurnProcess()
		{
			StartCoroutine(StartTurn());
		}

		IEnumerator StartTurn()
		{
			yield return null;
		}

		public void EndEndTurnProcess()
		{
			StartTurnProcess();
		}

		IEnumerator EndTurn()
		{
			yield return null;
		}

		public void StartActorTurs(Actor actor)
		{
			actor.StartTurn();
		}

	}
}