using UnityEngine;
using UnityEngine.InputSystem;

namespace BokenshaChan
{
	public class PlayerInputHandler : MonoBehaviour
	{
		public IMovable _player;
		[SerializeField] private InputActionReference _holdMove;

		private Vector2 _currentMoveInput;
		[SerializeField] private bool _isMoving;
		[SerializeField] private float _moveCheckInterval = 0.1f; // 移動チェックの間隔
		void Awake()
		{
			_player = GetComponent<Player>();

			if (_holdMove == null)
			{
				Debug.Log("ホールドアクションが登録できてないよ");
				return;
			}
			// 入力開始時のイベント
			_holdMove.action.started += OnMoveStarted;
			// 入力中のイベント
			_holdMove.action.performed += OnMovePerformed;
			// 入力終了時のイベント
			_holdMove.action.canceled += OnMoveCanceled;

			_holdMove.action.Enable();
		}
		private void OnMoveStarted(InputAction.CallbackContext context)
		{
			_isMoving = true;
			_currentMoveInput = context.ReadValue<Vector2>();
			StartCoroutine(ContinuousMovementCoroutine());
		}

		private void OnMovePerformed(InputAction.CallbackContext context)
		{
			_currentMoveInput = context.ReadValue<Vector2>();
		}

		private void OnMoveCanceled(InputAction.CallbackContext context)
		{
			_isMoving = false;
			_currentMoveInput = Vector2.zero;
		}

		private System.Collections.IEnumerator ContinuousMovementCoroutine()
		{
			while (_isMoving)
			{
				int x = Mathf.RoundToInt(_currentMoveInput.x);
				int y = Mathf.RoundToInt(_currentMoveInput.y);

				if (x != 0 || y != 0)
				{
					_player.Move(x, y);
				}

				yield return new WaitForSeconds(_moveCheckInterval);
			}
		}

		private void OnDisable()
		{
			if (_holdMove != null && _holdMove.action != null)
			{
				_holdMove.action.started -= OnMoveStarted;
				_holdMove.action.performed -= OnMovePerformed;
				_holdMove.action.canceled -= OnMoveCanceled;
			}
		}

		// public void OnInteract(InputAction.CallbackContext context){
		// 	if(!context.performed) return;

		// 	player.Interact();
		// }
	}
}