using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
	private static LanguageManager instance;

	public string language = "en";

	public string defaultLanguage = "en";

	private string resourceFile = "Localization/Generated Assets/Language";

	private string xmlHeader = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<root>";

	private Dictionary<string, string> textDataBase = new Dictionary<string, string>();

	private Dictionary<string, LocalizedObject> localizedObjectDataBase = new Dictionary<string, LocalizedObject>();

	private bool initialized;

	private List<string> availableLanguages = new List<string>();

	private List<CultureInfo> availableLanguagesCultureInfo = new List<CultureInfo>();

	public static LanguageManager Instance
	{
		get
		{
			if (instance == null)
			{
				GameObject gameObject = new GameObject();
				instance = gameObject.AddComponent<LanguageManager>();
				gameObject.name = "LanguageManager";
			}
			return instance;
		}
	}

	public List<string> AvailableLanguages
	{
		get
		{
			return availableLanguages;
		}
	}

	public List<CultureInfo> AvailableLanguagesCultureInfo
	{
		get
		{
			return availableLanguagesCultureInfo;
		}
	}

	public bool IsInitialized
	{
		get
		{
			return initialized;
		}
	}

	[method: MethodImpl(32)]
	public event ChangeLanguageEventHandler OnChangeLanguage;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			Object.DontDestroyOnLoad(base.gameObject);
		}
		if (PlayerPrefs.HasKey("cws_defaultLanguage"))
		{
			defaultLanguage = PlayerPrefs.GetString("cws_defaultLanguage");
		}
		GetAvailableLanguages();
		Debug.Log("LanguageManager.cs: Waking up");
		foreach (string availableLanguage in availableLanguages)
		{
			if (availableLanguage == defaultLanguage)
			{
				ChangeLanguage(availableLanguage);
				break;
			}
		}
		if (!initialized && availableLanguages.Count > 0)
		{
			ChangeLanguage(availableLanguages[0]);
		}
		else if (!initialized)
		{
			Debug.LogError("LanguageManager.cs: No language is available! Use Window->Smart Localization tool to create a language");
		}
	}

	private void OnDestroy()
	{
		this.OnChangeLanguage = null;
	}

	private void LoadResources()
	{
		initialized = false;
		textDataBase.Clear();
		localizedObjectDataBase.Clear();
		TextAsset textAsset = ((language != null) ? (Resources.Load(resourceFile + "." + language) as TextAsset) : (Resources.Load(resourceFile) as TextAsset));
		if (!textAsset && defaultLanguage != language)
		{
			Debug.LogError("ERROR: Language file:" + language + " could not be found! - reverting to default language:" + defaultLanguage);
			ChangeLanguage(defaultLanguage);
			return;
		}
		if (!textAsset)
		{
			Debug.LogError("ERROR: Language file:" + language + " could not be found!");
			return;
		}
		int length = "</xsd:schema>".Length;
		string text = textAsset.text;
		int num = text.IndexOf("</xsd:schema>");
		num += length;
		string text2 = text.Substring(num);
		text2 = xmlHeader + text2;
		XmlReader xmlReader = XmlReader.Create(new StringReader(text2));
		ReadElements(xmlReader);
		xmlReader.Close();
		initialized = true;
	}

	private void ReadElements(XmlReader reader)
	{
		while (reader.Read())
		{
			XmlNodeType nodeType = reader.NodeType;
			if (nodeType == XmlNodeType.Element && reader.Name == "data")
			{
				ReadData(reader);
			}
		}
	}

	private void ReadData(XmlReader reader)
	{
		string key = "ERROR";
		string text = "ERROR";
		if (reader.HasAttributes)
		{
			while (reader.MoveToNextAttribute())
			{
				if (reader.Name == "name")
				{
					key = reader.Value;
				}
			}
		}
		reader.MoveToElement();
		if (reader.ReadToDescendant("value"))
		{
			do
			{
				text = reader.ReadString();
			}
			while (reader.ReadToNextSibling("value"));
		}
		textDataBase.Add(key, text);
		LocalizedObject localizedObject = new LocalizedObject();
		localizedObject.ObjectType = LocalizedObject.GetLocalizedObjectType(key);
		localizedObject.TextValue = text;
		localizedObjectDataBase.Add(LocalizedObject.GetCleanKey(key, localizedObject.ObjectType), localizedObject);
	}

	private void GetAvailableLanguages()
	{
		availableLanguages.Clear();
		availableLanguagesCultureInfo.Clear();
		Object[] array = Resources.LoadAll("Localization/Generated Assets", typeof(TextAsset));
		string text = "Language.";
		Object[] array2 = array;
		foreach (Object @object in array2)
		{
			if (@object.name != "Language" && @object.name.StartsWith(text))
			{
				string item = @object.name.Substring(text.Length);
				availableLanguages.Add(item);
				availableLanguagesCultureInfo.Add(CultureInfo.GetCultureInfo(item));
			}
		}
	}

	public string GetTextValue(string key)
	{
		LocalizedObject localizedObject = GetLocalizedObject(key);
		if (localizedObject != null)
		{
			return localizedObject.TextValue;
		}
		return null;
	}

	public AudioClip GetAudioClip(string key)
	{
		LocalizedObject localizedObject = GetLocalizedObject(key);
		if (localizedObject != null)
		{
			return Resources.Load("Localization/" + language + "/Audio Files/" + key) as AudioClip;
		}
		return null;
	}

	public GameObject GetPrefab(string key)
	{
		LocalizedObject localizedObject = GetLocalizedObject(key);
		if (localizedObject != null)
		{
			return Resources.Load("Localization/" + language + "/Prefabs/" + key) as GameObject;
		}
		return null;
	}

	public Texture GetTexture(string key)
	{
		LocalizedObject localizedObject = GetLocalizedObject(key);
		if (localizedObject != null)
		{
			return Resources.Load("Localization/" + language + "/Textures/" + key) as Texture;
		}
		return null;
	}

	private LocalizedObject GetLocalizedObject(string key)
	{
		LocalizedObject value;
		localizedObjectDataBase.TryGetValue(key, out value);
		return value;
	}

	public void ChangeLanguage(string language)
	{
		this.language = language;
		LoadResources();
		if (IsInitialized && this.OnChangeLanguage != null)
		{
			this.OnChangeLanguage(this);
		}
	}

	public Dictionary<string, string> GetTextDataBase()
	{
		return textDataBase;
	}

	public Dictionary<string, LocalizedObject> GetLocalizedObjectDataBase()
	{
		return localizedObjectDataBase;
	}

	public void Clear()
	{
		instance = null;
		Object.DestroyImmediate(base.gameObject);
	}

	public void SetDefaultLanguage(string languageName)
	{
		PlayerPrefs.SetString("cws_defaultLanguage", languageName);
	}

	public void SetDefaultLanguage(CultureInfo languageInfo)
	{
		SetDefaultLanguage(languageInfo.Name);
	}

	public bool IsLanguageSupported(string languageName)
	{
		return availableLanguages.Contains(languageName);
	}

	public bool IsLanguageSupported(CultureInfo cultureInfo)
	{
		return IsLanguageSupported(cultureInfo.Name);
	}

	public string GetSystemLanguage()
	{
		if (Application.systemLanguage == SystemLanguage.Unknown)
		{
			Debug.LogWarning("LanguageManager.cs: The system language of this application is Unknown");
			return "Unknown";
		}
		string text = Application.systemLanguage.ToString();
		CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
		CultureInfo[] array = cultures;
		foreach (CultureInfo cultureInfo in array)
		{
			if (cultureInfo.EnglishName == text)
			{
				return cultureInfo.Name;
			}
		}
		Debug.LogError("LanguageManager.cs: A system language of this application is could not be found!");
		return "System Language not found!";
	}

	public CultureInfo GetCultureInfo(string languageName)
	{
		return CultureInfo.GetCultureInfo(languageName);
	}
}
