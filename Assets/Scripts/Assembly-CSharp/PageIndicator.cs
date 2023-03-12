using UnityEngine;

public class PageIndicator : MonoBehaviour
{
	private const string ATLAS = "MENUONLYUI";

	private const string DOT = "dot";

	private const string DOT_EMPTY = "dot_empty";

	public GameObject[] pages;

	private UISprite[] sprites;

	private void Awake()
	{
		sprites = new UISprite[pages.Length];
		for (int i = 0; i < pages.Length; i++)
		{
			sprites[i] = pages[i].GetComponent<UISprite>();
			if (AtlasManagerInfo.IsSHD)
			{
				sprites[i].atlas = (Resources.Load("SHD/SHDMENUONLYUI") as GameObject).GetComponent<UIAtlas>();
			}
			else if (AtlasManagerInfo.IsHD)
			{
				sprites[i].atlas = (Resources.Load("HD/HDMENUONLYUI") as GameObject).GetComponent<UIAtlas>();
			}
			else
			{
				sprites[i].atlas = (Resources.Load("SD/SDMENUONLYUI") as GameObject).GetComponent<UIAtlas>();
			}
		}
	}

	public void UpdatePage(int index)
	{
		for (int i = 0; i < sprites.Length; i++)
		{
			if (i == index)
			{
				sprites[i].spriteName = "dot";
			}
			else
			{
				sprites[i].spriteName = "dot_empty";
			}
		}
	}
}
