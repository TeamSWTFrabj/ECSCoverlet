using System;
using System.Collections.Generic;
using System.Text;

namespace ECS.Redesign
{
	public interface IAlarm
	{
		public void SendAlarm(string alarmText);
	}
}
