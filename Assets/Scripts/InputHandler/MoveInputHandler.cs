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
	}
}