using System;
using System.Collections.Generic;
using System.Text;

namespace ECS.Redesign
{
	interface IAlarm
	{
		public void SendAlarm(string alarmText);
	}
}
