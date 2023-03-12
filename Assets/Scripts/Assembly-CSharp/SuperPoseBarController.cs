using UnityEngine;

public class SuperPoseBarController : MonoBehaviour
{
	public GameObject background;

	public GameObject letters;

	public GameObject leftCorner;

	public GameObject S;

	public GameObject U;

	public GameObject P;

	public GameObject E;

	public GameObject R;

	public GameObject middle;

	public GameObject P2;

	public GameObject O;

	public GameObject S2;

	public GameObject E2;

	public GameObject rightCorner;

	public GameObject superPoseReadyObj;

	private void Update()
	{
		background.SetActive(true);
		letters.SetActive(true);
		int starsLeftBeforeSuperPose = DanceSessionManager.Instance.StarsLeftBeforeSuperPose;
		if (starsLeftBeforeSuperPose < 10)
		{
			if (starsLeftBeforeSuperPose < 10)
			{
				leftCorner.SetActive(true);
				S.SetActive(true);
			}
			if (starsLeftBeforeSuperPose < 9)
			{
				U.SetActive(true);
			}
			if (starsLeftBeforeSuperPose < 8)
			{
				P.SetActive(true);
			}
			if (starsLeftBeforeSuperPose < 7)
			{
				E.SetActive(true);
			}
			if (starsLeftBeforeSuperPose < 6)
			{
				R.SetActive(true);
				middle.SetActive(true);
			}
			if (starsLeftBeforeSuperPose < 5)
			{
				P2.SetActive(true);
			}
			if (starsLeftBeforeSuperPose < 4)
			{
				O.SetActive(true);
			}
			if (starsLeftBeforeSuperPose < 3)
			{
				S2.SetActive(true);
			}
			if (starsLeftBeforeSuperPose < 2)
			{
				E2.SetActive(true);
			}
			if (starsLeftBeforeSuperPose < 1)
			{
				rightCorner.SetActive(true);
				superPoseReadyObj.SetActive(true);
			}
		}
	}

	public void ResetBar()
	{
		background.SetActive(true);
		letters.SetActive(false);
		rightCorner.SetActive(false);
		S.SetActive(false);
		U.SetActive(false);
		P.SetActive(false);
		E.SetActive(false);
		R.SetActive(false);
		middle.SetActive(false);
		P2.SetActive(false);
		O.SetActive(false);
		S2.SetActive(false);
		E2.SetActive(false);
		leftCorner.SetActive(false);
		superPoseReadyObj.SetActive(false);
	}

	private void OnEnable()
	{
		ResetBar();
	}
}
