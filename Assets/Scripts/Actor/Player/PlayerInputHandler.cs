using UnityEngine;
using UnityEngine.InputSystem;

namespace BokenshaChan
{
	public class PlayerInputHandler : MonoBehaviour
	{
		Player player;
		private void Awake()
		{
			player = GetComponent<Player>();
		}
		public void OnMove(InputAction.CallbackContext context)
		{
			// Performed でなければ早期リターン
			if (!context.performed) return;

			Vector2 input = context.ReadValue<Vector2>();
			int x = (int)input.x;
			int y = (int)input.y;

			// 入力があれば移動関数を実行
			if (x != 0 || y != 0) player.Move(x, y);
		}

		// public void OnInteract(InputAction.CallbackContext context){
		// 	if(!context.performed) return;

		// 	player.Interact();
		// }
	}
}