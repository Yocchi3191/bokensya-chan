using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BokenshaChan
{
	public class TurnManager
	{
		public void StartTurnProcess(MonoBehaviour mono)
		{
			mono.StartCoroutine(StartTurn());
		}

		IEnumerator StartTurn()
		{
			yield return null;
		}

		public void EndEndTurnProcess(MonoBehaviour mono)
		{
			StartTurnProcess(mono);
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