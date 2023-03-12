public class PirouetteAnimation : UISpriteAnimation
{
	private bool mStarted;

	private void Start()
	{
		base.framesPerSecond = 15;
		base.namePrefix = "McGee_Marshmellow_slingshot0";
		base.loop = false;
		mStarted = true;
	}

	public bool FinishedPlaying()
	{
		if (mStarted && !base.isPlaying)
		{
			return true;
		}
		return false;
	}
}
