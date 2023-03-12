using UnityEngine;

public class AtlasManagerInfo
{
	public static bool runSwitcher = true;

	public static int SDResolutionCutOff = 640;

	public static int HDResolutionCutOff = 1280;

	public static int SHDResolutionStart = 1280;

	public static bool IsEnabled
	{
		get
		{
			return runSwitcher;
		}
	}

	public static bool IsSD
	{
		get
		{
			return Screen.width < SDResolutionCutOff;
		}
	}

	public static bool IsHD
	{
		get
		{
			return Screen.width > SDResolutionCutOff && Screen.width <= HDResolutionCutOff;
		}
	}

	public static bool IsSHD
	{
		get
		{
			return Screen.width > SHDResolutionStart;
		}
	}

	public static float ResolutionMultiplier
	{
		get
		{
			if (IsSD)
			{
				return 1f;
			}
			if (IsHD)
			{
				return 2f;
			}
			return 4f;
		}
	}
}
