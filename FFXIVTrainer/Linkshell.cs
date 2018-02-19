using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVTrainer
{
	public static class Linkshell
	{
		private static Dictionary<string, List<Action>> actions = new Dictionary<string, List<Action>>();
		public static Dictionary<string, List<Action>> Actions
		{
			get => actions;
		}

		public static void Emit(string eventName)
		{
			// set the event name to uppercase
			var upper = eventName.ToUpper();
			// check for the action list
			if (!actions.ContainsKey(upper))
				return;

			// loop through the actions list
			foreach (var action in actions[upper])
				action();
		}

		/// <summary>
		/// Register an action on an event name
		/// </summary>
		/// <param name="eventName"></param>
		/// <param name="action"></param>
		public static void Register(string eventName, Action action)
		{
			// get the uppercase event name
			var upper = eventName.ToUpper();

			// check for an existing action list
			if (actions.ContainsKey(upper))
				actions[upper].Add(action);
			else
			{
				// create a new list for the actions
				var list = new List<Action>();
				list.Add(action);
				// add the list
				actions.Add(eventName.ToUpper(), list);
			}
		}
	}
}
