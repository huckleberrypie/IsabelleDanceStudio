using UnityEngine;

public class BackgroundScale : MonoBehaviour
{
	public void ScaleBackground()
	{
		float x = base.transform.localScale.x;
		MonoBehaviour.print("currentScaleX " + x + " Screen.width " + Screen.width);
		float num = ((!(x <= (float)Screen.width)) ? ((float)Screen.width / x) : 0f);
		base.transform.localScale = new Vector3(base.transform.localScale.x * num, base.transform.localScale.y * num, 1f);
		MonoBehaviour.print("scaling by " + num);
	}
}
