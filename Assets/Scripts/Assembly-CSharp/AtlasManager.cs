using System.Collections.Generic;
using UnityEngine;

public class AtlasManager : MonoBehaviour
{
	private static AtlasManager _instance;

	public List<UIAtlas> referenceAtlases;

	public List<string> SDAtlasNames;

	public List<string> HDAtlasNames;

	public List<string> SHDAtlasNames;

	public List<UIFont> referenceFonts;

	public List<string> SDFontNames;

	public List<string> HDFontNames;

	public List<string> SHDFontNames;

	public static AtlasManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = Object.FindObjectOfType(typeof(AtlasManager)) as AtlasManager;
				if (_instance == null)
				{
					_instance = new GameObject().AddComponent<AtlasManager>();
					_instance.hideFlags = HideFlags.HideAndDontSave;
				}
			}
			return _instance;
		}
	}

	public static void LoadAtlas(Enums.AtlasOrder p_atlas)
	{
		if (!(Instance.referenceAtlases[(int)p_atlas].replacement != null))
		{
			UIAtlas uIAtlas = null;
			MonoBehaviour.print("About to replaced atlas " + Instance.SHDAtlasNames[(int)p_atlas]);
			uIAtlas = (AtlasManagerInfo.IsSHD ? (Resources.Load("SHD/" + Instance.SHDAtlasNames[(int)p_atlas]) as GameObject).GetComponent<UIAtlas>() : ((!AtlasManagerInfo.IsHD) ? (Resources.Load("SD/" + Instance.SDAtlasNames[(int)p_atlas]) as GameObject).GetComponent<UIAtlas>() : (Resources.Load("HD/" + Instance.HDAtlasNames[(int)p_atlas]) as GameObject).GetComponent<UIAtlas>()));
			MonoBehaviour.print("Just replaced atlas " + uIAtlas.name);
			Instance.referenceAtlases[(int)p_atlas].replacement = uIAtlas;
		}
	}

	public static void UnloadAtlas(Enums.AtlasOrder p_atlas)
	{
		Instance.referenceAtlases[(int)p_atlas].replacement = null;
		Instance.referenceAtlases[(int)p_atlas].spriteMaterial = null;
	}

	public static void UnloadFont(Enums.FontOrder p_font)
	{
		Instance.referenceFonts[(int)p_font].replacement = null;
	}

	public static void LoadFont(Enums.FontOrder p_font)
	{
		if (!(Instance.referenceFonts[(int)p_font].replacement != null))
		{
			UIFont uIFont = null;
			MonoBehaviour.print("About to replaced font " + Instance.SHDFontNames[(int)p_font]);
			uIFont = (AtlasManagerInfo.IsSHD ? (Resources.Load("SHD/Fonts/" + Instance.SHDFontNames[(int)p_font]) as GameObject).GetComponent<UIFont>() : ((!AtlasManagerInfo.IsHD) ? (Resources.Load("SD/Fonts/" + Instance.SDFontNames[(int)p_font]) as GameObject).GetComponent<UIFont>() : (Resources.Load("HD/Fonts/" + Instance.HDFontNames[(int)p_font]) as GameObject).GetComponent<UIFont>()));
			MonoBehaviour.print("Just replaced font " + uIFont.name);
			Instance.referenceFonts[(int)p_font].replacement = uIFont;
		}
	}
}
