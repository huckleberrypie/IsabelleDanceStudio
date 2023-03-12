using UnityEngine;

public class TraceManager : MonoBehaviour
{
	private static TraceManager _instance;

	public static TraceManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = Object.FindObjectOfType(typeof(TraceManager)) as TraceManager;
				if (_instance == null)
				{
					_instance = new GameObject().AddComponent<TraceManager>();
					_instance.hideFlags = HideFlags.HideAndDontSave;
				}
			}
			return _instance;
		}
	}

	public void TraceOut(string message)
	{
		MonoBehaviour.print("Trace Out: " + message);
	}
}
