using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BokenshaChan
{
	public interface ITurnManager
	{
		public void StartTurn();
		public void EndTurn();
	}
}