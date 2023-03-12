using UnityEngine;

public class FadeOut : MonoBehaviour
{
	private void Start()
	{
		TweenAlpha.Begin(base.transform.gameObject, Constants.DesginerGlobals.LOADING_SCREEN_FADEOUT_TIME, 0f);
		if ((bool)(base.transform.GetComponent("UISprite") as UISprite) && (base.transform.GetComponent("UISprite") as UISprite).atlas.name.CompareTo("SharedUIReferenceAtlas") == 0)
		{
			TweenColor.Begin(base.transform.gameObject, Constants.DesginerGlobals.LOADING_SCREEN_FADEOUT_TIME, new Color(Constants.DesginerGlobals.PREMULITPLY_COLOR_FADE, Constants.DesginerGlobals.PREMULITPLY_COLOR_FADE, Constants.DesginerGlobals.PREMULITPLY_COLOR_FADE, 0.1f));
		}
	}

	public void StartFadeOut(float time)
	{
		TweenAlpha.Begin(base.transform.gameObject, time, 0f);
		if ((bool)(base.transform.GetComponent("UISprite") as UISprite) && (base.transform.GetComponent("UISprite") as UISprite).atlas.name.CompareTo("SharedUIReferenceAtlas") == 0)
		{
			TweenColor.Begin(base.transform.gameObject, time, new Color(Constants.DesginerGlobals.PREMULITPLY_COLOR_FADE, Constants.DesginerGlobals.PREMULITPLY_COLOR_FADE, Constants.DesginerGlobals.PREMULITPLY_COLOR_FADE, 0.1f));
		}
	}
}
