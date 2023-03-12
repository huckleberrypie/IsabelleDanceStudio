using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	private static SpawnManager _instance;

	public List<GameObject> flowers;

	public GameObject star;

	public GameObject superPose;

	public GameObject bouncePackage;

	public GameObject randomMultiplier;

	public GameObject Notifier;

	public GameObject FloaterBlue;

	public GameObject FloaterGold;

	public GameObject FloaterViolet;

	public GameObject SequinBurst;

	public GameObject StarBurst;

	public GameObject PowerUpBurst;

	public GameObject PoseAnimationEffect;

	public GameObject PoseParticleEffect;

	public GameObject StarHitParticleEffect;

	public GameObject ObjectGlow;

	public static SpawnManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = Object.FindObjectOfType(typeof(SpawnManager)) as SpawnManager;
				if (_instance == null)
				{
					_instance = new GameObject().AddComponent<SpawnManager>();
					_instance.hideFlags = HideFlags.HideAndDontSave;
				}
			}
			return _instance;
		}
	}

	public GameObject AddStartFlower()
	{
		int num = 0;
		List<int> list = new List<int>();
		for (int i = 0; i < Constants.DesginerGlobals.MAX_FLOWERS - 1; i++)
		{
			if (PlayerManager.Instance.isFLowerUnlocked(i))
			{
				list.Add(i);
			}
		}
		if (list.Count > 1)
		{
			num = list[Random.Range(0, list.Count)];
		}
		GameObject gameObject = Object.Instantiate(flowers[num]) as GameObject;
		gameObject.transform.SetParent(GameManager.FallingContainer);
		gameObject.transform.localPosition = Constants.DesginerGlobals.STARTING_FLOWER_POS;
		gameObject.tag = "Flower";
		UISprite uISprite = gameObject.GetComponent("UISprite") as UISprite;
		if (uISprite != null)
		{
			uISprite.spriteName = Constants.Sprites.Game.FLOWERS[num];
			uISprite.depth = Constants.DesginerGlobals.FLOWERSPRITE_DEPTH;
			uISprite.MakePixelPerfect();
		}
		if ((bool)gameObject.GetComponent<Rigidbody>())
		{
			gameObject.GetComponent<Rigidbody>().isKinematic = true;
		}
		return gameObject;
	}

	public GameObject AddFlower(bool badVersion = false)
	{
		int num = 0;
		List<int> list = new List<int>();
		for (int i = 0; i < Constants.DesginerGlobals.MAX_FLOWERS - 1; i++)
		{
			if (PlayerManager.Instance.isFLowerUnlocked(i))
			{
				list.Add(i);
			}
		}
		if (list.Count > 1)
		{
			num = list[Random.Range(0, list.Count)];
		}
		GameObject gameObject = Object.Instantiate(flowers[num]) as GameObject;
		gameObject.transform.SetParent(GameManager.FallingContainer);
		gameObject.transform.localPosition = new Vector3(Random.Range(GameData.Instance.GetLeftMaxPos() + 50f, GameData.Instance.GetRightMaxPos() - 50f), (GameData.Instance.GetScreenTopPos() - GameData.Instance.GetScreenBottomPos()) / 2f + 100f, 0f);
		gameObject.tag = "Flower";
		if (badVersion)
		{
			UISprite uISprite = gameObject.GetComponent("UISprite") as UISprite;
			if (uISprite != null)
			{
				uISprite.spriteName = Constants.Sprites.Game.FLOWERS[Constants.DesginerGlobals.MAX_FLOWERS - 1];
				uISprite.depth = Constants.DesginerGlobals.FLOWERSPRITE_DEPTH;
				uISprite.MakePixelPerfect();
				gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, -Constants.DesginerGlobals.FLOWERSPRITE_DEPTH);
			}
			if (gameObject.GetComponent("FlowerPhysics") as FlowerPhysics != null)
			{
				(gameObject.GetComponent("FlowerPhysics") as FlowerPhysics).BlackStar = true;
			}
			GameObject gameObject2 = Object.Instantiate(ObjectGlow) as GameObject;
			gameObject2.transform.SetParent(GameManager.FallingContainer);
			gameObject2.transform.localPosition = gameObject.transform.localPosition;
			UISprite uISprite2 = gameObject2.GetComponent("UISprite") as UISprite;
			if (uISprite2 != null)
			{
				uISprite2.spriteName = Constants.Sprites.Game.BLACK_STAR_GLOW;
				uISprite2.depth = Constants.DesginerGlobals.FLOWERSPRITE_DEPTH;
				uISprite2.MakePixelPerfect();
			}
			FollowObjectCenter followObjectCenter = gameObject2.AddComponent<FollowObjectCenter>() as FollowObjectCenter;
			if (followObjectCenter != null)
			{
				followObjectCenter.OtherObject = gameObject;
			}
			if (gameObject.GetComponent<Collider>() != null && gameObject.GetComponent("SphereCollider") as SphereCollider != null)
			{
				(gameObject.GetComponent("SphereCollider") as SphereCollider).radius = 0.25f;
			}
			gameObject.AddComponent<DestroyOffScreen>();
			if (!PlayerManager.Instance.SeenBlackPearl)
			{
				gameObject.AddComponent<GameTip>();
				(gameObject.GetComponent("GameTip") as GameTip).SetTip(Enums.GameTip.BLACK_PEARL);
			}
		}
		else
		{
			UISprite uISprite3 = gameObject.GetComponent("UISprite") as UISprite;
			if (uISprite3 != null)
			{
				uISprite3.spriteName = Constants.Sprites.Game.FLOWERS[num];
				uISprite3.depth = Constants.DesginerGlobals.FLOWERSPRITE_DEPTH;
				uISprite3.MakePixelPerfect();
				gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, -Constants.DesginerGlobals.FLOWERSPRITE_DEPTH);
			}
		}
		return gameObject;
	}

	public void AddFlowerBurst(GameObject flower)
	{
		GameObject gameObject = Object.Instantiate(SequinBurst) as GameObject;
		gameObject.transform.SetParent(GameManager.FallingContainer);
		gameObject.transform.localPosition = flower.transform.localPosition;
		UISprite uISprite = gameObject.GetComponent("UISprite") as UISprite;
		if (uISprite != null)
		{
			uISprite.depth = Constants.DesginerGlobals.FLOWERBURST_DEPTH;
		}
		DestroyAfterTime destroyAfterTime = gameObject.AddComponent<DestroyAfterTime>() as DestroyAfterTime;
		if (destroyAfterTime != null)
		{
			destroyAfterTime.lifeCycle = 1f;
		}
	}

	public void AddPowerBurst(Vector3 startPos, GameObject powerObj)
	{
		GameObject gameObject = Object.Instantiate(PowerUpBurst) as GameObject;
		gameObject.transform.SetParent(GameManager.FallingContainer);
		gameObject.transform.localPosition = startPos;
		FollowObjectCenter followObjectCenter = gameObject.AddComponent<FollowObjectCenter>() as FollowObjectCenter;
		if (followObjectCenter != null)
		{
			followObjectCenter.OtherObject = powerObj;
		}
		UISprite uISprite = gameObject.GetComponent("UISprite") as UISprite;
		if (uISprite != null)
		{
			uISprite.depth = Constants.DesginerGlobals.POWERUPBURST_DEPTH;
		}
		DestroyAfterTime destroyAfterTime = gameObject.AddComponent<DestroyAfterTime>() as DestroyAfterTime;
		if (destroyAfterTime != null)
		{
			destroyAfterTime.lifeCycle = 1f;
		}
	}

	public void AddStarBurst(Vector3 startPos, bool blackStar)
	{
		GameObject gameObject = Object.Instantiate(StarBurst) as GameObject;
		gameObject.transform.SetParent(GameManager.FallingContainer);
		gameObject.transform.localPosition = startPos;
		UISprite uISprite = gameObject.GetComponent("UISprite") as UISprite;
		if (uISprite != null)
		{
			uISprite.depth = Constants.DesginerGlobals.STARBURST_DEPTH;
			if (blackStar)
			{
				uISprite.color = new Color(0f, 0f, 0f, 255f);
			}
		}
		DestroyAfterTime destroyAfterTime = gameObject.AddComponent<DestroyAfterTime>() as DestroyAfterTime;
		if (destroyAfterTime != null)
		{
			destroyAfterTime.lifeCycle = 1f;
		}
	}

	public void AddPoseAnimationEffect(string spriteName, UIAtlas atlasType, float opacity, Vector3 position, bool flipped, float fadeOutTime, float destroyTime, bool particle)
	{
		GameObject gameObject = Object.Instantiate((!particle) ? PoseAnimationEffect : PoseParticleEffect) as GameObject;
		gameObject.transform.SetParent(GameManager.FallingContainer);
		gameObject.transform.localPosition = position;
		float gravity = 0f;
		if (flipped)
		{
			gameObject.transform.localScale = new Vector3(-1f, base.transform.localScale.y, base.transform.localScale.z);
		}
		if (!particle)
		{
			UISprite uISprite = gameObject.AddComponent<UISprite>() as UISprite;
			if (uISprite != null)
			{
				uISprite.atlas = atlasType;
				uISprite.depth = Constants.DesginerGlobals.PLAYERSPRITE_DEPTH - 1;
				uISprite.spriteName = spriteName;
				uISprite.MakePixelPerfect();
			}
			AnimationPoseEffect animationPoseEffect = gameObject.AddComponent<AnimationPoseEffect>() as AnimationPoseEffect;
			if (animationPoseEffect != null)
			{
				animationPoseEffect.InitializeEffect(opacity, position, fadeOutTime, destroyTime, gravity);
			}
			DestroyAfterTime destroyAfterTime = gameObject.AddComponent<DestroyAfterTime>() as DestroyAfterTime;
			if (destroyAfterTime != null)
			{
				destroyAfterTime.lifeCycle = destroyTime;
			}
		}
		else
		{
			GetComponent<ParticleEmitter>();
		}
	}

	public void AddFloatingPoints(Vector3 startPos, string text, bool mulitpler = false, bool doubleStars = false)
	{
		GameObject gameObject = null;
		gameObject = (mulitpler ? (Object.Instantiate(FloaterGold) as GameObject) : ((!doubleStars) ? (Object.Instantiate(Notifier) as GameObject) : (Object.Instantiate(FloaterViolet) as GameObject)));
		gameObject.transform.SetParent(GameManager.FallingContainer);
		UILabel uILabel = gameObject.GetComponent("UILabel") as UILabel;
		if (uILabel != null)
		{
			uILabel.text = text;
			uILabel.depth = Constants.DesginerGlobals.FLOATING_TEXT_DEPTH;
			gameObject.transform.localPosition = new Vector3(startPos.x, startPos.y, -100f);
		}
		gameObject.AddComponent<FloatingFadedText>();
	}

	public void AddPowerNotifier(Vector3 pos, string notifierText)
	{
		AddFloatingPoints(pos, notifierText);
	}

	public GameObject AddPoseCollider(int moveIndex, Vector3 finalPos, Vector2 force)
	{
		if (DanceSessionManager.Instance.EndingGameSlowly)
		{
			return null;
		}
		GameObject gameObject = Object.Instantiate(GameManager.PoseCollider, Vector3.zero, Quaternion.identity) as GameObject;
		gameObject.transform.localScale = new Vector3(0.45f, 0.45f, 0.45f);
		BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>() as BoxCollider;
		if (DanceSessionManager.CollectedPoseUp)
		{
			boxCollider.size = new Vector3(2f, 2f, 2f);
		}
		if (DanceSessionManager.Instance.IsSuperPoseMode())
		{
			boxCollider.size = new Vector3(100f, 100f, 100f);
		}
		Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>() as Rigidbody;
		rigidbody.isKinematic = true;
		gameObject.transform.parent = GameManager.FallingContainer.transform;
		gameObject.transform.localPosition = finalPos;
		ApplyForceOnCollision applyForceOnCollision = gameObject.AddComponent<ApplyForceOnCollision>() as ApplyForceOnCollision;
		if (applyForceOnCollision != null)
		{
			applyForceOnCollision.SetForce(force);
		}
		RecordPoint recordPoint = gameObject.AddComponent<RecordPoint>() as RecordPoint;
		if (recordPoint != null)
		{
			recordPoint.FlowerPoint = Constants.DesginerGlobals.HIT_FLOWER_POINT;
		}
		DestroyAfterTime destroyAfterTime = gameObject.AddComponent<DestroyAfterTime>() as DestroyAfterTime;
		if (destroyAfterTime != null)
		{
			destroyAfterTime.lifeCycle = GameData.PoseColliderLifeTime;
		}
		return gameObject;
	}

	public GameObject AddEventBoxCollider(Vector3 pos)
	{
		if (DanceSessionManager.Instance.EndingGameSlowly)
		{
			if (DanceSessionManager.Instance.GameOverDelay < 0f)
			{
				StopAllCoroutines();
				GameManager.Instance.EndGameMode();
			}
			return null;
		}
		GameObject gameObject = Object.Instantiate(GameManager.EventCollider, Vector3.zero, Quaternion.identity) as GameObject;
		gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
		gameObject.tag = "ClickCollider";
		BoxCollider boxCollider = gameObject.GetComponent("BoxCollider") as BoxCollider;
		if ((bool)boxCollider)
		{
			boxCollider.isTrigger = false;
		}
		Rigidbody rigidbody = gameObject.GetComponent("Rigidbody") as Rigidbody;
		if ((bool)rigidbody)
		{
			rigidbody.isKinematic = true;
		}
		gameObject.transform.parent = GameManager.FallingContainer.transform;
		gameObject.transform.localPosition = pos;
		TriggerPlayerEvent triggerPlayerEvent = gameObject.GetComponent("TriggerPlayerEvent") as TriggerPlayerEvent;
		if (triggerPlayerEvent != null)
		{
			triggerPlayerEvent.setProperties(pos);
		}
		DestroyAfterTime destroyAfterTime = gameObject.GetComponent("DestroyAfterTime") as DestroyAfterTime;
		if (destroyAfterTime != null)
		{
			destroyAfterTime.lifeCycle = Constants.DesginerGlobals.CLICK_COLLIDER_LIFE;
		}
		return gameObject;
	}

	public void AddStarsAtPos(Vector3 screenPos, int numOfStars)
	{
		for (int i = 0; i < GameData.StarPositionOffsets.Count; i++)
		{
			GameObject gameObject = Object.Instantiate(star) as GameObject;
			gameObject.transform.SetParent(GameManager.FallingContainer);
			gameObject.transform.localPosition = GameData.StarPositionOffsets[i];
			Collectible collectible = gameObject.GetComponent("Collectible") as Collectible;
			if (collectible == null)
			{
				collectible = gameObject.AddComponent<Collectible>() as Collectible;
			}
			collectible.SetTimeSpan(GameData.StarLifeTime);
			UISprite uISprite = gameObject.GetComponent("UISprite") as UISprite;
			if (uISprite != null)
			{
				int num = Random.Range(0, Constants.Sprites.Game.STARS.Length);
				uISprite.spriteName = Constants.Sprites.Game.STARS[num];
				uISprite.depth = Constants.DesginerGlobals.STARSPRITE_DEPTH;
			}
			if (!PlayerManager.Instance.SeenGems && i == 0)
			{
				gameObject.AddComponent<GameTip>();
				(gameObject.GetComponent("GameTip") as GameTip).SetTip(Enums.GameTip.SEQUINS);
			}
		}
	}

	public void AddStarParticleSystem(Vector3 pos)
	{
		GameObject gameObject = Object.Instantiate(StarHitParticleEffect) as GameObject;
		gameObject.transform.SetParent(GameManager.FallingContainer);
		gameObject.transform.localPosition = pos;
		if ((bool)(gameObject.GetComponent("StarParticleSystem") as StarParticleSystem))
		{
			(gameObject.GetComponent("StarParticleSystem") as StarParticleSystem).InitializeEffect(pos, 0.5f, 0.5f);
		}
	}

	public void AddRandomItem()
	{
		List<int> list = new List<int>();
		list.Add(Constants.PowerUps.FLOWER_BOUNCE_ID);
		list.Add(Constants.PowerUps.RANDOM_MULITPLIER_ID);
		if (!DanceSessionManager.CollectedDoubleStar)
		{
			list.Add(Constants.PowerUps.DOUBLE_STARS_ID);
		}
		if (list.Count <= 0)
		{
			return;
		}
		int index = Random.Range(0, list.Count);
		switch (list[index])
		{
		case 2:
			AddRandomMulitplier();
			break;
		case 3:
		{
			GameObject gameObject3 = Object.Instantiate(this.bouncePackage) as GameObject;
			gameObject3.transform.SetParent(GameManager.FallingContainer);
			(gameObject3.AddComponent<RandomItemPath>() as RandomItemPath).StartPath();
			BouncePackage bouncePackage2 = gameObject3.gameObject.GetComponent("BouncePackage") as BouncePackage;
			bouncePackage2.HiddenItem = Constants.PowerUps.FLOWER_BOUNCE_ID;
			UISprite uISprite3 = gameObject3.gameObject.GetComponent("UISprite") as UISprite;
			if (uISprite3 != null)
			{
				uISprite3.spriteName = Constants.Sprites.Game.BOUNCE_POWERUP;
				uISprite3.depth = Constants.DesginerGlobals.POWERUP_DEPTH;
				uISprite3.MakePixelPerfect();
			}
			GameObject gameObject4 = Object.Instantiate(ObjectGlow) as GameObject;
			gameObject4.transform.SetParent(GameManager.FallingContainer);
			gameObject4.transform.localPosition = gameObject3.transform.localPosition;
			UISprite uISprite4 = gameObject4.GetComponent("UISprite") as UISprite;
			if (uISprite4 != null)
			{
				uISprite4.spriteName = Constants.Sprites.Game.BOUNCE_POWERUP_GLOW;
				uISprite4.depth = Constants.DesginerGlobals.POWERUP_DEPTH;
				uISprite4.MakePixelPerfect();
			}
			FollowObjectCenter followObjectCenter2 = gameObject4.AddComponent<FollowObjectCenter>() as FollowObjectCenter;
			if (followObjectCenter2 != null)
			{
				followObjectCenter2.OtherObject = gameObject3;
			}
			if (!PlayerManager.Instance.SeenBounceUp)
			{
				gameObject3.AddComponent<GameTip>();
				(gameObject3.GetComponent("GameTip") as GameTip).SetTip(Enums.GameTip.BOUNCE_UP);
			}
			break;
		}
		case 4:
		{
			GameObject gameObject5 = Object.Instantiate(this.bouncePackage) as GameObject;
			gameObject5.transform.SetParent(GameManager.FallingContainer);
			(gameObject5.AddComponent<RandomItemPath>() as RandomItemPath).StartPath();
			BouncePackage bouncePackage3 = gameObject5.gameObject.GetComponent("BouncePackage") as BouncePackage;
			bouncePackage3.HiddenItem = Constants.PowerUps.POSE_UP_ID;
			UISprite uISprite5 = gameObject5.gameObject.GetComponent("UISprite") as UISprite;
			if (uISprite5 != null)
			{
				uISprite5.spriteName = Constants.Sprites.Game.POSEUP_POWERUP;
				uISprite5.depth = Constants.DesginerGlobals.POWERUP_DEPTH;
				uISprite5.MakePixelPerfect();
			}
			GameObject gameObject6 = Object.Instantiate(ObjectGlow) as GameObject;
			gameObject6.transform.SetParent(GameManager.FallingContainer);
			gameObject6.transform.localPosition = gameObject5.transform.localPosition;
			UISprite uISprite6 = gameObject6.GetComponent("UISprite") as UISprite;
			if (uISprite6 != null)
			{
				uISprite6.spriteName = Constants.Sprites.Game.BOUNCE_POWERUP_GLOW;
				uISprite6.depth = Constants.DesginerGlobals.POWERUP_DEPTH;
				uISprite6.MakePixelPerfect();
			}
			FollowObjectCenter followObjectCenter3 = gameObject6.AddComponent<FollowObjectCenter>() as FollowObjectCenter;
			if (followObjectCenter3 != null)
			{
				followObjectCenter3.OtherObject = gameObject5;
			}
			if (!PlayerManager.Instance.SeenBounceUp)
			{
				gameObject5.AddComponent<GameTip>();
				(gameObject5.GetComponent("GameTip") as GameTip).SetTip(Enums.GameTip.BOUNCE_UP);
			}
			break;
		}
		case 5:
		{
			GameObject gameObject7 = Object.Instantiate(this.bouncePackage) as GameObject;
			gameObject7.transform.SetParent(GameManager.FallingContainer);
			(gameObject7.AddComponent<RandomItemPath>() as RandomItemPath).StartPath();
			BouncePackage bouncePackage4 = gameObject7.gameObject.GetComponent("BouncePackage") as BouncePackage;
			bouncePackage4.HiddenItem = Constants.PowerUps.DOUBLE_STARS_ID;
			UISprite uISprite7 = gameObject7.gameObject.GetComponent("UISprite") as UISprite;
			if (uISprite7 != null)
			{
				uISprite7.spriteName = Constants.Sprites.Game.DOUBLESTAR_POWERUP;
				uISprite7.depth = Constants.DesginerGlobals.POWERUP_DEPTH;
				uISprite7.MakePixelPerfect();
			}
			GameObject gameObject8 = Object.Instantiate(ObjectGlow) as GameObject;
			gameObject8.transform.SetParent(GameManager.FallingContainer);
			gameObject8.transform.localPosition = gameObject7.transform.localPosition;
			UISprite uISprite8 = gameObject8.GetComponent("UISprite") as UISprite;
			if (uISprite8 != null)
			{
				uISprite8.spriteName = Constants.Sprites.Game.X2_POWERUP_GLOW;
				uISprite8.depth = Constants.DesginerGlobals.POWERUP_DEPTH;
				uISprite8.MakePixelPerfect();
			}
			FollowObjectCenter followObjectCenter4 = gameObject8.AddComponent<FollowObjectCenter>() as FollowObjectCenter;
			if (followObjectCenter4 != null)
			{
				followObjectCenter4.OtherObject = gameObject7;
			}
			if (!PlayerManager.Instance.SeenDouble)
			{
				gameObject7.AddComponent<GameTip>();
				(gameObject7.GetComponent("GameTip") as GameTip).SetTip(Enums.GameTip.DOUBLE);
			}
			break;
		}
		default:
		{
			GameObject gameObject = Object.Instantiate(this.bouncePackage) as GameObject;
			gameObject.transform.SetParent(GameManager.FallingContainer);
			(gameObject.AddComponent<RandomItemPath>() as RandomItemPath).StartPath();
			BouncePackage bouncePackage = gameObject.gameObject.GetComponent("BouncePackage") as BouncePackage;
			bouncePackage.HiddenItem = Constants.PowerUps.FLOWER_BOUNCE_ID;
			UISprite uISprite = gameObject.gameObject.GetComponent("UISprite") as UISprite;
			if (uISprite != null)
			{
				uISprite.spriteName = Constants.Sprites.Game.BOUNCE_POWERUP;
				uISprite.depth = Constants.DesginerGlobals.POWERUP_DEPTH;
				uISprite.MakePixelPerfect();
			}
			GameObject gameObject2 = Object.Instantiate(ObjectGlow) as GameObject;
			gameObject2.transform.SetParent(GameManager.FallingContainer);
			gameObject2.transform.localPosition = gameObject.transform.localPosition;
			UISprite uISprite2 = gameObject2.GetComponent("UISprite") as UISprite;
			if (uISprite2 != null)
			{
				uISprite2.spriteName = Constants.Sprites.Game.BOUNCE_POWERUP_GLOW;
				uISprite2.depth = Constants.DesginerGlobals.POWERUP_DEPTH;
				uISprite2.MakePixelPerfect();
			}
			FollowObjectCenter followObjectCenter = gameObject2.AddComponent<FollowObjectCenter>() as FollowObjectCenter;
			if (followObjectCenter != null)
			{
				followObjectCenter.OtherObject = gameObject;
			}
			if (!PlayerManager.Instance.SeenBounceUp)
			{
				gameObject.AddComponent<GameTip>();
				(gameObject.GetComponent("GameTip") as GameTip).SetTip(Enums.GameTip.BOUNCE_UP);
			}
			break;
		}
		}
	}

	public void AddRandomMulitplier()
	{
		List<int> list = new List<int>();
		list.Add(Constants.PowerUps.PowerUpMulitpliers.TWO_TIMES);
		list.Add(Constants.PowerUps.PowerUpMulitpliers.THREE_TIMES);
		list.Add(Constants.PowerUps.PowerUpMulitpliers.FIVE_TIMES);
		int multiplierType = Random.Range(0, list.Count);
		GameObject gameObject = Object.Instantiate(randomMultiplier) as GameObject;
		gameObject.transform.SetParent(GameManager.FallingContainer);
		UISprite uISprite = gameObject.gameObject.GetComponent("UISprite") as UISprite;
		if (uISprite != null)
		{
			uISprite.spriteName = Constants.Sprites.Game.RANDOM_MULTIPLIER_POWERUP;
			uISprite.depth = Constants.DesginerGlobals.POWERUP_DEPTH;
			uISprite.MakePixelPerfect();
		}
		RandomMultiplierController randomMultiplierController = gameObject.GetComponent("RandomMultiplierController") as RandomMultiplierController;
		if (randomMultiplierController != null)
		{
			randomMultiplierController.MultiplierType = multiplierType;
		}
		gameObject.AddComponent<DestroyOffScreen>();
		(gameObject.AddComponent<RandomItemPath>() as RandomItemPath).StartPath();
		GameObject gameObject2 = Object.Instantiate(ObjectGlow) as GameObject;
		gameObject2.transform.SetParent(GameManager.FallingContainer);
		gameObject2.transform.localPosition = gameObject.transform.localPosition;
		UISprite uISprite2 = gameObject2.GetComponent("UISprite") as UISprite;
		if (uISprite2 != null)
		{
			uISprite2.spriteName = Constants.Sprites.Game.RANDOM_MULITPLIER_GLOW;
			uISprite2.depth = Constants.DesginerGlobals.POWERUP_DEPTH;
			uISprite2.MakePixelPerfect();
		}
		FollowObjectCenter followObjectCenter = gameObject2.AddComponent<FollowObjectCenter>() as FollowObjectCenter;
		if (followObjectCenter != null)
		{
			followObjectCenter.OtherObject = gameObject;
		}
		if (!PlayerManager.Instance.SeenMultiplier)
		{
			gameObject.AddComponent<GameTip>();
			(gameObject.GetComponent("GameTip") as GameTip).SetTip(Enums.GameTip.MULITPLER);
		}
	}
}
