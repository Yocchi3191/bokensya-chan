using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BokenshaChan
{
	public class TurnManager : MonoBehaviour
	{
		Queue<Actor> actors;
		void StartTurnProcess()
		{
			StartCoroutine(StartTurn());

		}

		IEnumerator StartTurn()
		{
			yield return null;
		}

		void EndEndTurnProcess()
		{
		}

		IEnumerator EndTurn()
		{
			yield return null;
		}
	}
}