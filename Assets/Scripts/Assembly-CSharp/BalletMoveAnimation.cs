public class BalletMoveAnimation : UISpriteAnimation
{
	private void Start()
	{
		base.framesPerSecond = 15;
		base.loop = false;
	}

	public void SetAnimationByPrefix(string prefix)
	{
		base.namePrefix = prefix;
	}

	public bool FinishedPlaying()
	{
		if (base.namePrefix.CompareTo(string.Empty) == 0)
		{
			return true;
		}
		if (!base.isPlaying)
		{
			return true;
		}
		return false;
	}
}
