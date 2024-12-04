using UnityEngine;

namespace BokenshaChan
{
	public class Main : MonoBehaviour
	{
		TurnManager turnManager;
		private void Awake()
		{
			turnManager = gameObject.GetComponent<TurnManager>();
		}
	}
}