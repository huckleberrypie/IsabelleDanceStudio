using System.Collections.Generic;
using UnityEngine;

public class GoalsManager : MonoBehaviour
{
	private static GoalsManager _instance;

	private List<GameGoal> mGoalsLibrary = new List<GameGoal>();

	private List<int> mCompletedGoals = new List<int>();

	private List<int> mCurrentGoals = new List<int>();

	public static GoalsManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = Object.FindObjectOfType(typeof(GoalsManager)) as GoalsManager;
				if (_instance == null)
				{
					_instance = new GameObject().AddComponent<GoalsManager>();
					_instance.hideFlags = HideFlags.HideAndDontSave;
				}
			}
			return _instance;
		}
	}

	private void Update()
	{
		if (mCurrentGoals.Count <= 0)
		{
			return;
		}
		for (int i = 0; i < mCurrentGoals.Count; i++)
		{
			if (!GetGoalByID(mCurrentGoals[i]).GoalCompleted && GoalRequirementsMet(mCurrentGoals[i]))
			{
				GetGoalByID(mCurrentGoals[i]).GoalCompleted = true;
			}
		}
	}

	public void AddGoalToLibrary(GameGoal goal)
	{
		if (goal != null)
		{
			mGoalsLibrary.Add(goal);
		}
	}

	public void AddGoalToCompleteList(int goalID)
	{
		GetGoalByID(goalID).GoalCompleted = true;
		mCompletedGoals.Add(goalID);
	}

	public void PrepareCurrentGoals()
	{
		mCurrentGoals.Clear();
		int num = -1;
		int num2 = -1;
		int num3 = -1;
		for (int i = 0; i < mGoalsLibrary.Count; i++)
		{
			if (mGoalsLibrary[i].GoalRank == Constants.DesginerGlobals.GOALS_RANK_1)
			{
				if (num > mGoalsLibrary[i].GoalID || num == -1)
				{
					num = mGoalsLibrary[i].GoalID;
				}
			}
			else if (mGoalsLibrary[i].GoalRank == Constants.DesginerGlobals.GOALS_RANK_2)
			{
				if (num2 > mGoalsLibrary[i].GoalID || num2 == -1)
				{
					num2 = mGoalsLibrary[i].GoalID;
				}
			}
			else if (mGoalsLibrary[i].GoalRank == Constants.DesginerGlobals.GOALS_RANK_3 && (num3 > mGoalsLibrary[i].GoalID || num3 == -1))
			{
				num3 = mGoalsLibrary[i].GoalID;
			}
		}
		if (num != -1)
		{
			mCurrentGoals.Add(num);
		}
		if (num2 != -1)
		{
			mCurrentGoals.Add(num2);
		}
		if (num3 != -1)
		{
			mCurrentGoals.Add(num3);
		}
	}

	public GameGoal GetGoalByID(int goalID)
	{
		for (int i = 0; i < mGoalsLibrary.Count; i++)
		{
			if (mGoalsLibrary[i].GoalID == goalID)
			{
				return mGoalsLibrary[i];
			}
		}
		return null;
	}

	public bool GoalRequirementsMet(int goalID)
	{
		GameGoal goalByID = GetGoalByID(goalID);
		bool result = true;
		if (goalByID != null)
		{
			if (goalByID.GoalRequiredBouncedFlowers > 0 && goalByID.GoalRequiredBouncedFlowers > Scoreboard.Instance.FlowersHit)
			{
				result = false;
			}
			if (goalByID.GoalRequiredConnectHit > 0 && goalByID.GoalRequiredConnectHit > Scoreboard.Instance.FlowersHit)
			{
				result = false;
			}
			if (goalByID.GoalRequiredTimeLength > 0f && goalByID.GoalRequiredTimeLength > Scoreboard.Instance.CurrentGameTime)
			{
				result = false;
			}
			if (goalByID.GoalRequiredStars > 0 && goalByID.GoalRequiredStars > Scoreboard.Instance.CollectedStars)
			{
				result = false;
			}
			if (goalByID.GoalRequiredPoints > 0 && goalByID.GoalRequiredPoints > Scoreboard.Instance.CollectedPoints)
			{
				result = false;
			}
			if (goalByID.GoalRequiredPointsWithoutMiss && goalByID.GoalRequiredPoints > 0 && goalByID.GoalRequiredPoints > Scoreboard.Instance.CollectedPoints)
			{
				result = false;
			}
		}
		return result;
	}
}
