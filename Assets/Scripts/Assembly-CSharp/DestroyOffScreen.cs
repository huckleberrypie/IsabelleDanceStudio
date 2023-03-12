using UnityEngine;

public class DestroyOffScreen : MonoBehaviour
{
	private void Update()
	{
		if (base.transform.localPosition.x < GameData.Instance.GetLeftMaxPos() - 100f)
		{
			Object.Destroy(base.transform.gameObject);
		}
		else if (base.transform.localPosition.x > GameData.Instance.GetRightMaxPos() + 100f)
		{
			Object.Destroy(base.transform.gameObject);
		}
		else if (base.transform.localPosition.y < GameData.Instance.GetScreenBottomPos())
		{
			Object.Destroy(base.transform.gameObject);
		}
	}
}
