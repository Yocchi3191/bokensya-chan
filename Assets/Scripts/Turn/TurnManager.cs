using System.Collections;
using UnityEngine;

namespace BokenshaChan
{
	public class TurnManager : MonoBehaviour
	{
		Player player;
		void StartTurnProcess()
		{
			StartCoroutine(StartTurn());

		}

		IEnumerator StartTurn()
		{
			yield return null;
		}

		IEnumerator EndStartTurn()
		{
			yield return null;
		}

	}
}