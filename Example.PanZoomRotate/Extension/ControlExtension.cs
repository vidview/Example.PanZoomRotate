using System;
using Forms = System.Windows.Forms;

namespace Example.PanZoomRotate.Extension
{
	public static class ControlExtension
	{
		public static void Invoke(this Forms.Control me, Action action)
		{
			if (me.InvokeRequired)
			{
				try { me.BeginInvoke((Delegate)action); }
				catch (System.InvalidOperationException) { }
			}
			else
				action();
		}
	}
}
