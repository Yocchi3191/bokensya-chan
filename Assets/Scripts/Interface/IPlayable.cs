using UnityEngine.InputSystem;

namespace BokenshaChan
{
	public interface IPlayable
	{
		public void OnMove(InputValue inputValue);
		public void OnInteract(InputValue inputValue);
		public void OnOpenMenu(InputValue inputValue);
	}
}