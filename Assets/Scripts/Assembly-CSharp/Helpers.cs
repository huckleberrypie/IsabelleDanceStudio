using UnityEngine;

public class Helpers : MonoBehaviour
{
	private static Helpers _instance;

	public static Helpers Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = Object.FindObjectOfType(typeof(Helpers)) as Helpers;
				if (_instance == null)
				{
					_instance = new GameObject().AddComponent<Helpers>();
					_instance.hideFlags = HideFlags.HideAndDontSave;
				}
			}
			return _instance;
		}
	}

	public static Color RandomColor()
	{
		return new Color(Random.value, Random.value, Random.value);
	}

	public static void PlayerPrefsSetBool(string p_key, bool p_value)
	{
		PlayerPrefs.SetInt(p_key, p_value ? 1 : 0);
	}

	public static bool PlayerPrefsGetBool(string p_key, bool p_defaultValue)
	{
		return PlayerPrefs.GetInt(p_key, p_defaultValue ? 1 : 0) == 1;
	}
}
