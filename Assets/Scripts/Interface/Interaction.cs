namespace BokenshaChan
{
	/// <summary>
	/// インタラクトする側用のインタフェース
	/// </summary>
	public interface IInteract
	{
		public void Interact(IInteractable interactableObject);
	}

	/// <summary>
	///	インタラクトされる側用のインタフェース 
	/// </summary>
	public interface IInteractable
	{
		public void ReturnReaction();
	}
}