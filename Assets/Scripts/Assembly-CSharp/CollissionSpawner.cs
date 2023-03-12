using UnityEngine;

public class CollissionSpawner : MonoBehaviour
{
	private static CollissionSpawner _instance;

	public GameObject collideObj;

	public static CollissionSpawner Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = Object.FindObjectOfType(typeof(CollissionSpawner)) as CollissionSpawner;
				if (_instance == null)
				{
					_instance = new GameObject().AddComponent<CollissionSpawner>();
					_instance.hideFlags = HideFlags.HideAndDontSave;
				}
			}
			return _instance;
		}
	}

	public static void CreateAt(float xPos, float yPos)
	{
		Vector3 localPosition = new Vector3(xPos, yPos, 0f);
		GameObject gameObject = Object.Instantiate(Instance.collideObj, Vector3.zero, Quaternion.identity) as GameObject;
		gameObject.transform.parent = GameManager.FallingContainer.transform;
		gameObject.transform.localPosition = localPosition;
	}

	public void InitAt(float xPos, float yPos)
	{
	}
}
