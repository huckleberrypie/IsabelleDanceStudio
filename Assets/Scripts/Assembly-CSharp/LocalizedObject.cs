using System;
using UnityEngine;

[Serializable]
public class LocalizedObject
{
	[SerializeField]
	private LocalizedObjectType objectType;

	[SerializeField]
	private string textValue;

	[SerializeField]
	private GameObject thisGameObject;

	[SerializeField]
	private AudioClip thisAudioClip;

	[SerializeField]
	private Texture thisTexture;

	public static string keyTypeIdentifier = "[type=";

	public LocalizedObjectType ObjectType
	{
		get
		{
			return objectType;
		}
		set
		{
			objectType = value;
		}
	}

	public string TextValue
	{
		get
		{
			return textValue;
		}
		set
		{
			textValue = value;
		}
	}

	public GameObject ThisGameObject
	{
		get
		{
			return thisGameObject;
		}
		set
		{
			thisGameObject = value;
		}
	}

	public AudioClip ThisAudioClip
	{
		get
		{
			return thisAudioClip;
		}
		set
		{
			thisAudioClip = value;
		}
	}

	public Texture ThisTexture
	{
		get
		{
			return thisTexture;
		}
		set
		{
			thisTexture = value;
		}
	}

	public static LocalizedObjectType GetLocalizedObjectType(string key)
	{
		if (key.StartsWith(keyTypeIdentifier))
		{
			if (key.StartsWith(keyTypeIdentifier + "AUDIO]"))
			{
				return LocalizedObjectType.AUDIO;
			}
			if (key.StartsWith(keyTypeIdentifier + "GAME_OBJECT]"))
			{
				return LocalizedObjectType.GAME_OBJECT;
			}
			if (key.StartsWith(keyTypeIdentifier + "TEXTURE]"))
			{
				return LocalizedObjectType.TEXTURE;
			}
			Debug.LogError("LocalizedObject.cs: ERROR IN SYNTAX of key:" + key + ", setting object type to STRING");
			return LocalizedObjectType.STRING;
		}
		return LocalizedObjectType.STRING;
	}

	public static string GetCleanKey(string key, LocalizedObjectType objectType)
	{
		int length = (keyTypeIdentifier + objectType.ToString() + ">").Length;
		switch (objectType)
		{
		case LocalizedObjectType.STRING:
			return key;
		case LocalizedObjectType.GAME_OBJECT:
		case LocalizedObjectType.AUDIO:
		case LocalizedObjectType.TEXTURE:
			return key.Substring(length);
		default:
			Debug.LogError("LocalizedObject.GetCleanKey(key) error!, object type is unknown! objectType:" + (int)objectType);
			return key;
		}
	}

	public static string GetCleanKey(string key)
	{
		LocalizedObjectType localizedObjectType = GetLocalizedObjectType(key);
		return GetCleanKey(key, localizedObjectType);
	}

	public string GetFullKey(string parsedKey)
	{
		if (objectType == LocalizedObjectType.STRING)
		{
			return parsedKey;
		}
		return keyTypeIdentifier + objectType.ToString() + "]" + parsedKey;
	}

	public static string GetFullKey(string parsedKey, LocalizedObjectType objectType)
	{
		if (objectType == LocalizedObjectType.STRING)
		{
			return parsedKey;
		}
		return keyTypeIdentifier + objectType.ToString() + "]" + parsedKey;
	}
}
