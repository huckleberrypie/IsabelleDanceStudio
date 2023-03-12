using System.Collections;
using UnityEngine;

[AddComponentMenu("Utilities/HUDFPS")]
public class HUDFPS : MonoBehaviour
{
	public Rect startRect = new Rect(10f, 10f, 75f, 50f);

	public bool updateColor = true;

	public bool allowDrag = true;

	public float frequency = 0.5f;

	public int nbDecimal = 1;

	private float accum;

	private int frames;

	private Color color = Color.white;

	private string sFPS = string.Empty;

	private GUIStyle style;

	private void Start()
	{
		startRect.y = Screen.height - 35;
		StartCoroutine(FPS());
	}

	private void Update()
	{
		accum += Time.timeScale / Time.deltaTime;
		frames++;
	}

	private IEnumerator FPS()
	{
		while (true)
		{
			float fps = accum / (float)frames;
			sFPS = fps.ToString("f" + Mathf.Clamp(nbDecimal, 0, 10));
			color = ((fps >= 29f) ? Color.green : ((!(fps > 10f)) ? Color.red : Color.yellow));
			accum = 0f;
			frames = 0;
			yield return new WaitForSeconds(frequency);
		}
	}

	private void OnGUI()
	{
		if (style == null)
		{
			style = new GUIStyle(GUI.skin.label);
			style.normal.textColor = Color.white;
			style.alignment = TextAnchor.MiddleCenter;
		}
		GUI.color = ((!updateColor) ? Color.white : color);
		startRect = GUI.Window(0, startRect, DoMyWindow, string.Empty);
	}

	private void DoMyWindow(int windowID)
	{
		GUI.Label(new Rect(0f, 0f, startRect.width, startRect.height), sFPS + " FPS", style);
		if (allowDrag)
		{
			GUI.DragWindow(new Rect(0f, 0f, Screen.width, Screen.height));
		}
	}
}
