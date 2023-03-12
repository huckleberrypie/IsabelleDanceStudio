using System.Collections.Generic;
using UnityEngine;

public class TitleFlash : MonoBehaviour
{
	public List<UISprite> Stars = new List<UISprite>();

	private List<UISprite> remainingStars = new List<UISprite>();

	private float intervalTime = 0.5f;

	private void OnEnable()
	{
		if (Stars.Count <= 0)
		{
			return;
		}
		foreach (UISprite star in Stars)
		{
			TweenAlpha.Begin(star.gameObject, 0.1f, 0f);
			remainingStars.Add(star);
		}
	}

	private void Update()
	{
		intervalTime -= Time.deltaTime;
		if (intervalTime < 0f)
		{
			intervalTime = 0.15f;
		}
		if (remainingStars.Count > 1 && intervalTime == 0.15f)
		{
			int index = Random.Range(0, remainingStars.Count);
			if (remainingStars[index] != null)
			{
				UISprite uISprite = remainingStars[index];
				if (uISprite.gameObject.GetComponent("FadeInFadeOut") as FadeInFadeOut == null)
				{
					(uISprite.gameObject.AddComponent<FadeInFadeOut>() as FadeInFadeOut).SetIntervals(0.5f, 0.5f);
					(uISprite.gameObject.GetComponent("FadeInFadeOut") as FadeInFadeOut).RemoveScriptAfter = true;
					(uISprite.gameObject.GetComponent("FadeInFadeOut") as FadeInFadeOut).SetTimeSpan(1.1f);
					(uISprite.gameObject.GetComponent("FadeInFadeOut") as FadeInFadeOut).AllowStart = true;
					remainingStars.RemoveAt(index);
				}
			}
		}
		else
		{
			if (remainingStars.Count > 1)
			{
				return;
			}
			remainingStars.Clear();
			foreach (UISprite star in Stars)
			{
				if (star.gameObject.GetComponent("FadeInFadeOut") as FadeInFadeOut != null)
				{
					return;
				}
			}
			foreach (UISprite star2 in Stars)
			{
				remainingStars.Add(star2);
			}
		}
	}
}
