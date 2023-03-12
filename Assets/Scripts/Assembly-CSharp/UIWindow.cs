using System.Collections.Generic;
using UnityEngine;

public class UIWindow : IgnoreTimeScale
{
	private static UIWindow mInst;

	private static List<UIPanel> mHistory = new List<UIPanel>();

	private static List<UIPanel> mFading = new List<UIPanel>();

	private static UIPanel mActive;

	public static UIPanel current
	{
		get
		{
			return mActive;
		}
	}

	public static bool NothingFading
	{
		get
		{
			return mFading.Count == 0 && mActive != null && mActive.alpha >= 1f;
		}
	}

	private static void CreateInstance()
	{
		if (mInst == null)
		{
			GameObject gameObject = new GameObject("WindowManager");
			mInst = gameObject.AddComponent<UIWindow>();
			Object.DontDestroyOnLoad(gameObject);
		}
	}

	public static void Add(UIPanel window)
	{
		if (!(mActive == window))
		{
			CreateInstance();
			if (mActive == null)
			{
				mActive = window;
			}
		}
	}

	public static void Show(UIPanel window)
	{
		if (!(mActive == window))
		{
			CreateInstance();
			if (mActive != null)
			{
				mFading.Add(mActive);
				mHistory.Add(mActive);
			}
			if (mHistory.Remove(window))
			{
				mFading.Remove(window);
			}
			else if (window != null)
			{
				window.SetAlphaRecursive(0f, false);
			}
			mActive = window;
			if (mActive != null)
			{
				mActive.gameObject.SetActive(true);
			}
		}
	}

	public static void Overlay(UIPanel window)
	{
		if (!(mActive == window))
		{
			CreateInstance();
			if (mActive != null)
			{
				mHistory.Add(mActive);
			}
			window.SetAlphaRecursive(0f, false);
			mActive = window;
			if (mActive != null)
			{
				mActive.gameObject.SetActive(true);
			}
		}
	}

	public static void GoBack()
	{
		CreateInstance();
		if (mHistory.Count <= 0)
		{
			return;
		}
		if (mActive != null)
		{
			mFading.Add(mActive);
			mActive = null;
		}
		while (mActive == null)
		{
			mActive = mHistory[mHistory.Count - 1];
			mHistory.RemoveAt(mHistory.Count - 1);
			if (mActive != null)
			{
				mActive.SetAlphaRecursive(0f, false);
				mActive.gameObject.SetActive(true);
				mFading.Remove(mActive);
				break;
			}
		}
	}

	public static void Close()
	{
		if (mActive != null)
		{
			CreateInstance();
			mFading.Add(mActive);
			mHistory.Add(mActive);
			mActive = null;
		}
		mHistory.Clear();
	}

	private void Update()
	{
		UpdateRealTimeDelta();
		int num = mFading.Count;
		while (num > 0)
		{
			UIPanel uIPanel = mFading[--num];
			if (uIPanel != null)
			{
				uIPanel.SetAlphaRecursive(Mathf.Clamp01(uIPanel.alpha - base.realTimeDelta * 10f), false);
				if (uIPanel.alpha > 0f)
				{
					continue;
				}
			}
			mFading.RemoveAt(num);
			uIPanel.gameObject.SetActive(false);
		}
		if (mFading.Count == 0 && mActive != null && mActive.alpha < 1f)
		{
			mActive.SetAlphaRecursive(Mathf.Clamp01(mActive.alpha + base.realTimeDelta * 10f), false);
		}
	}
}
