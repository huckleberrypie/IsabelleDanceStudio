using UnityEngine;

public class DestroyOnCollisionExit : MonoBehaviour
{
	private void OnCollisionExit(Collision collision)
	{
		Object.Destroy(base.transform.gameObject);
	}
}
