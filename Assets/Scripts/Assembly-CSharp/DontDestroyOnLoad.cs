using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
	public void Awake()
	{
		Object.DontDestroyOnLoad(base.gameObject);
	}
}
