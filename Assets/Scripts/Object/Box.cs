using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace BokenshaChan
{
	public class Box : Prop, IInteract
	{
		int interactCount = 0;
		string interactMessage;

		public bool CanInteract() { return true; }
		public void Interact()
		{
			if (interactCount < 1)
			{
				interactMessage = "箱を開けた！！";
			}
			else if (interactCount < 5)
			{
				interactMessage = "もう何も入っていない";
			}
			else if (interactCount < 7)
			{
				interactMessage = "何も入っていないっていってんだろ";
			}
			else
			{
				interactMessage = null;
			}

			Debug.Log(interactMessage);
		}
	}
}