using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BokenshaChan
{
	public class PlayerInputHandler : MonoBehaviour
	{
		private List<IInputHandler> _inputHandlers;

		private void Awake()
		{
			_inputHandlers = new List<IInputHandler>
		{
			gameObject.GetComponent<MoveInputHandler>(),
			gameObject.GetComponent<InteractInputHandler>()
            // 必要に応じて他のハンドラーを追加
        };
		}

		private void OnEnable()
		{
			foreach (var handler in _inputHandlers)
			{
				handler.Enable();
			}
		}

		private void OnDisable()
		{
			foreach (var handler in _inputHandlers)
			{
				handler.Disable();
			}
		}
	}
}