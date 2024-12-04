using System.Collections;
using UnityEngine;

namespace BokenshaChan
{
	public abstract class TurnManager : MonoBehaviour
	{
		public abstract void ProcessTurn(Actor actor);
		protected abstract IEnumerator StartTurn(Actor actor);
		protected abstract IEnumerator EndTurn(Actor actor);
	}
}