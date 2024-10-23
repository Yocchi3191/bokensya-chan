using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
	private GameManager gameManager;

	private void Start()
	{
		gameManager = FindObjectOfType<GameManager>();
	}

	public void OnMove(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			Vector2 moveInput = context.ReadValue<Vector2>();
			gameManager.StartMoving(moveInput); // 移動開始
		}

		if (context.canceled)
		{
			gameManager.StopMoving(); // 移動停止
		}
	}
}

