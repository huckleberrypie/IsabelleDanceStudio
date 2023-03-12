using UnityEngine;

public class ConstantRotation : MonoBehaviour
{
	public Vector3 rotationSpeed = Vector3.zero;

	private void Update()
	{
		base.transform.Rotate(rotationSpeed * Time.deltaTime);
	}
}
