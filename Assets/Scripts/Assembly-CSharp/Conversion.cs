using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public static class Conversion
{
	public static string TimeSecToMin(float sec)
	{
		int num = (int)(sec * 100f);
		int num2 = num / 6000;
		int num3 = num % 6000 / 100;
		int num4 = num % 100 / 10;
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(num2 + ":" + num3 + "." + num4);
		if (num2 < 10)
		{
			stringBuilder.Insert(0, "0");
		}
		if (num3 < 10)
		{
			stringBuilder.Insert(3, "0");
		}
		return stringBuilder.ToString();
	}

	public static string TimeSecToMinNoMilisec(float sec)
	{
		int num = (int)(sec * 100f);
		int num2 = num / 6000;
		int num3 = num % 6000 / 100;
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(num2 + ":" + num3);
		if (num3 < 10)
		{
			if (num2 < 10)
			{
				stringBuilder.Insert(2, "0");
			}
			else
			{
				stringBuilder.Insert(3, "0");
			}
		}
		return stringBuilder.ToString();
	}

	public static string AddCommas(int num)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(num.ToString());
		int length = stringBuilder.Length;
		if (length > 3)
		{
			int num2 = Mathf.FloorToInt(length / 3);
			for (int i = 0; i < num2; i++)
			{
				int num3 = length - i - 3 * (i + 1) + i;
				if (num3 > 0)
				{
					stringBuilder.Insert(num3, ",");
				}
			}
		}
		return stringBuilder.ToString();
	}

	public static string AddCommas(float num)
	{
		return AddCommas((double)num);
	}

	public static string AddCommas(double num)
	{
		string[] array = num.ToString().Split('.');
		if (array.Length > 1)
		{
			string text = array[1];
			array[1] = text.Remove(2);
		}
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(array[0]);
		int length = stringBuilder.Length;
		if (length > 3)
		{
			int num2 = Mathf.FloorToInt(length / 3);
			for (int i = 0; i < num2; i++)
			{
				int num3 = length - i - 3 * (i + 1) + i;
				if (num3 > 0)
				{
					stringBuilder.Insert(num3, ",");
				}
			}
			if (array.Length > 1)
			{
				stringBuilder.Insert(stringBuilder.Length, "." + array[1]);
			}
		}
		return stringBuilder.ToString();
	}

	public static string FirstCapLetter(string text)
	{
		return text.Substring(0, 1).ToUpper() + text.Substring(1).ToLower();
	}

	public static void ShuffleFast<T>(IList<T> list)
	{
		int num = list.Count;
		while (num > 1)
		{
			num--;
			int index = Random.Range(0, num + 1);
			T value = list[index];
			list[index] = list[num];
			list[num] = value;
		}
	}

	public static void ShuffleMoreRandom<T>(IList<T> list)
	{
		RNGCryptoServiceProvider rNGCryptoServiceProvider = new RNGCryptoServiceProvider();
		int num = list.Count;
		while (num > 1)
		{
			byte[] array = new byte[1];
			do
			{
				rNGCryptoServiceProvider.GetBytes(array);
			}
			while (array[0] >= num * (255 / num));
			int index = (int)array[0] % num;
			num--;
			T value = list[index];
			list[index] = list[num];
			list[num] = value;
		}
	}
}
