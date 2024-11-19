using System.Collections;

namespace BokenshaChan
{
	public class Player : Actor
	{
		public void Interact()
		{
			StartCoroutine(InteractCoroutine());
		}
		private IEnumerator InteractCoroutine()
		{
			if (IsActing) yield break;

			
		}
	}
}