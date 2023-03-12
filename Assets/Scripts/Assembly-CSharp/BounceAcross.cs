using UnityEngine;

public class BounceAcross : MonoBehaviour
{
	private float mSpeedX = 0.005f;

	private void Start()
	{
	}

	private void Update()
	{
		base.transform.Translate(new Vector3(mSpeedX, 0f, 0f));
	}

	public void setSpeed(Vector2 speed)
	{
		mSpeedX = speed.x;
	}
}
