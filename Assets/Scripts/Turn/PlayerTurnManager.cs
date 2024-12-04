using System.Collections;
using UnityEngine;

namespace BokenshaChan
{
	public class PlayerTurnManager : TurnManager
	{
		public override void ProcessTurn(Actor actor)
		{

		}
		private override IEnumerator StartTurn(Actor actor)
		{
			
		}
		private override IEnumerator EndTurn(Actor actor)
		{
			yield return null;
		}
		private IEnumerator WaitInput(){
			yield return 
		}
	}
}