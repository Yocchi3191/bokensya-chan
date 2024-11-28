using UnityEngine;
using UnityEngine.InputSystem;

namespace BokenshaChan
{
	public class PlayerInputHandler : MonoBehaviour
	{
		public IMovable _player;
		[SerializeField] private InputActionReference _holdMove;

		void Awake()
		{
			_player = GetComponent<Player>();

			if (_holdMove == null)
			{
				Debug.Log("ホールドアクションが登録できてないよ");
				return;
			}
			_holdMove.action.performed += OnMove;
			_holdMove.action.Enable();
		}
		public void OnMove(InputAction.CallbackContext context)
		{
			// Player null なら早期リターン
			if (_player == null) return;

			Vector2 input = context.ReadValue<Vector2>();
			int x = (int)input.x;
			int y = (int)input.y;

			// 入力があれば移動関数を実行
			if (x != 0 || y != 0) _player.Move(x, y);
			Debug.Log($"ボタン状態: {context.phase}");
		}

		// public void OnInteract(InputAction.CallbackContext context){
		// 	if(!context.performed) return;

		// 	player.Interact();
		// }
	}
}