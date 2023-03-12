public class GameGoal
{
	private int mGoalID;

	private bool mGoalCompleted;

	private int mGoalRank;

	private string mGoalDescription;

	private string mGoalTitle;

	private int mGoalRequiredBouncedFlowers;

	private int mGoalRequiredConnectHit;

	private float mGoalRequiredTimeLength;

	private int mGoalRequiredPoints;

	private int mGoalRequiredStars;

	private int mGoalRequiredCombo;

	private bool mGoalRequiredPointsWithoutMiss;

	public int GoalID
	{
		get
		{
			return mGoalID;
		}
		set
		{
			mGoalID = value;
		}
	}

	public bool GoalCompleted
	{
		get
		{
			return mGoalCompleted;
		}
		set
		{
			mGoalCompleted = value;
		}
	}

	public int GoalRank
	{
		get
		{
			return mGoalRank;
		}
		set
		{
			mGoalRank = value;
		}
	}

	public string GoalDescription
	{
		get
		{
			return mGoalDescription;
		}
		set
		{
			mGoalDescription = value;
		}
	}

	public string GoalTitle
	{
		get
		{
			return mGoalTitle;
		}
		set
		{
			mGoalTitle = value;
		}
	}

	public int GoalRequiredBouncedFlowers
	{
		get
		{
			return mGoalRequiredBouncedFlowers;
		}
		set
		{
			mGoalRequiredBouncedFlowers = value;
		}
	}

	public int GoalRequiredConnectHit
	{
		get
		{
			return mGoalRequiredConnectHit;
		}
		set
		{
			mGoalRequiredConnectHit = value;
		}
	}

	public float GoalRequiredTimeLength
	{
		get
		{
			return mGoalRequiredTimeLength;
		}
		set
		{
			mGoalRequiredTimeLength = value;
		}
	}

	public int GoalRequiredPoints
	{
		get
		{
			return mGoalRequiredPoints;
		}
		set
		{
			mGoalRequiredPoints = value;
		}
	}

	public int GoalRequiredStars
	{
		get
		{
			return mGoalRequiredStars;
		}
		set
		{
			mGoalRequiredStars = value;
		}
	}

	public int GoalRequiredCombo
	{
		get
		{
			return mGoalRequiredCombo;
		}
		set
		{
			mGoalRequiredCombo = value;
		}
	}

	public bool GoalRequiredPointsWithoutMiss
	{
		get
		{
			return mGoalRequiredPointsWithoutMiss;
		}
		set
		{
			mGoalRequiredPointsWithoutMiss = value;
		}
	}
}
