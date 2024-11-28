using UnityEngine;
using UnityEngine.InputSystem;

namespace BokenshaChan
{
	public class InteractInputHandler : MonoBehaviour, IInputHandler
	{
		[SerializeField] private InputActionReference _interactAction;
		private IInteract _interact;
		private void Awake()
		{
			_interact = GetComponent<IInteract>();
		}

		public void Enable()
		{
			if (_interactAction == null) return;

			_interactAction.action.performed += OnInteract;
			_interactAction.action.Enable();
		}

		public void Disable()
		{
			if (_interactAction == null) return;

			_interactAction.action.performed -= OnInteract;
			_interactAction.action.Disable();
		}

		private void OnEnable() => Enable();
		private void OnDisable() => Disable();
		private void OnInteract(InputAction.CallbackContext context)
		{
			/// 未定
			/// IInteractableを取得して、
			/// _interact.Interact(_interactable)
			/// ってしたい
		}
	}
}