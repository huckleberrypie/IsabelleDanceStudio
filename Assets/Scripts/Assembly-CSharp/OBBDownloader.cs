using System.Collections;
using UnityEngine;

public class OBBDownloader : MonoBehaviour
{
	private string nextScene = "Game_JW";

	private void Start()
	{
			// Trimmed and stubbed as we DON'T need an OBB package for this game anyway
			Application.LoadLevel(nextScene);
	}
}