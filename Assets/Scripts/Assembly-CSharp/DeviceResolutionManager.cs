using UnityEngine;

public class DeviceResolutionManager
{
	public static bool runSwitcher = true;

	public static int SDResolutionCutOff = 640;

	public static int HDResolutionCutOff = 1200;

	public static int SHDResolutionStart = 1200;

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

	public static float DeviceMultiplier
	{
		get
		{
			if (IsSD)
			{
				return 1f;
			}
			if (IsHD)
			{
				return 0.5f;
			}
			return 0.25f;
		}
	}
}
