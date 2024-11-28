using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BokenshaChan
{
	public class MoveInputHandler : MonoBehaviour, IInputHandler
	{
		[SerializeField] private InputActionReference _moveAction;
		private IMovable _movable;
		private Vector2 _currentMoveInput;
		private bool _isMoving;
		[SerializeField] private float _moveCheckInterval = 0.1f;

		private void Awake()
		{
			// このスクリプトがアタッチされているオブジェクトについている
			// IMovableを実装しているコンポーネントを取得する。
			// ex.) Player : IMovable なら Playerコンポーネントを取得
			_movable = GetComponent<IMovable>();
		}
		public void Enable()
		{
			if (_moveAction == null) return;

			_moveAction.action.started += OnMoveStarted;
			_moveAction.action.performed += OnMovePerformed;
			_moveAction.action.canceled += OnMoveCanceled;
			_moveAction.action.Enable();
		}
		public void Disable()
		{
			if (_moveAction == null) return;

			_moveAction.action.started -= OnMoveStarted;
			_moveAction.action.performed -= OnMovePerformed;
			_moveAction.action.canceled -= OnMoveCanceled;
			_moveAction.action.Disable();
		}
		private void OnEnable() => Enable();
		private void OnDisable() => Disable();

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
		private IEnumerator ContinuousMovementCoroutine()
		{
			while (_isMoving)
			{
				int x = Mathf.RoundToInt(_currentMoveInput.x);
				int y = Mathf.RoundToInt(_currentMoveInput.y);

				if ((x != 0 && y == 0) || (x == 0 && y != 0))
				{
					_movable.Move(x, y);
				}

				yield return new WaitForSeconds(_moveCheckInterval);
			}
		}


	}
}