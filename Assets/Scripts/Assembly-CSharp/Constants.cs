using System.Runtime.InteropServices;
using UnityEngine;

public static class Constants
{
	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 1)]
	public struct DesginerGlobals
	{
		public static int MAX_FLOWERS = 6;

		public static int PINK_FLOWER_ID = 0;

		public static int PURPLE_FLOWER_ID = 1;

		public static int RED_FLOWER_ID = 2;

		public static int VIOLET_FLOWER_ID = 3;

		public static int YELLOW_FLOWER_ID = 4;

		public static int SEQUIN_WIDTH = 30;

		public static int FLOATING_TEXT_DEPTH = 14;

		public static int PLAYERSPRITE_DEPTH = 3;

		public static int FLOWERSPRITE_DEPTH = 5;

		public static int FLOWERBURST_DEPTH = 4;

		public static int POWERUP_DEPTH = 5;

		public static int POWERUPBURST_DEPTH = 4;

		public static int STARSPRITE_DEPTH = 1;

		public static int STARBURST_DEPTH = 0;

		public static int GAME_TRANSITION_DEPTH = -1;

		public static int NUM_STARS_FOR_SUPER_POSE = 10;

		public static int HIT_FLOWER_POINT = 1;

		public static int HIT_STAR_POINT = 5;

		public static int GOALS_RANK_1 = 1;

		public static int GOALS_RANK_2 = 2;

		public static int GOALS_RANK_3 = 3;

		public static float CLICK_COLLIDER_LIFE = 0.2f;

		public static float LOW_KICK_SCREEN_AREA = 0.25f;

		public static float MID_KICK_SCREEN_AREA = 0.4f;

		public static float HIGH_KICK_SCREEN_AREA = 0.55f;

		public static float LOW_HAND_SCREEN_AREA = 0.65f;

		public static float MID_HAND_SCREEN_AREA = 0.88f;

		public static float HIGH_HAND_SCREEN_AREA = 1f;

		public static float MIN_PLAYER_POS_Y = -20f;

		public static float MAX_PLAYER_POS_Y = 10f;

		public static float PLAYER_START_X = 100f;

		public static Vector3 STARTING_FLOWER_POS = new Vector3(0f, -120f, 0f);

		public static float FLOWER_BOUNCE_POS_Y = -126f;

		public static float ITEM_MIN_Y = -80f;

		public static float ITEM_MAX_Y = 80f;

		public static float ITEM_MIN_X = -160f;

		public static float ITEM_MAX_X = 160f;

		public static float ITEM_MIN_SPEED = 0.4f;

		public static float ITEM_MAX_SPEED = 0.6f;

		public static float FLOWER_GRAVITY_ACCELARATION_INTERVAL = -0.008f;

		public static float FLOWER_DIRECTIONAL_FORCE = 0.25f;

		public static float LOADING_SCREEN_FADEIN_TIME = 0.5f;

		public static float LOADING_SCREEN_FADEOUT_TIME = 0.5f;

		public static float SPLASH_SCREEN_TIME = 2f;

		public static float IN_GAME_FADEIN_TIME = 0.2f;

		public static float IN_GAME_FADEOUT_TIME = 0.3f;

		public static float FLOWER_FADEOUT_TIME = 0.5f;

		public static float MAX_SUPERPOSE_ANIM_SCALE = 8f;

		public static float MAX_WINK_TIME = 2f;

		public static float WINK_FRAME_TIME = 0.05f;

		public static float END_GAME_DELAY = 4f;

		public static float END_GAME_TOUCH_DELAY = 2f;

		public static float PREMULITPLY_COLOR_FADE = 0.5f;
	}

	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 1)]
	public struct PlayerMoves
	{
		public static int PLAYER_NUM_MOVES = 10;

		public static int PLAYER_MOVE_LOW_KICK = 0;

		public static int PLAYER_MOVE_MID_KICK = 1;

		public static int PLAYER_MOVE_HIGH_KICK = 2;

		public static int PLAYER_MOVE_LOW_HAND = 3;

		public static int PLAYER_MOVE_MID_HAND = 4;

		public static int PLAYER_MOVE_HIGH_HAND = 5;

		public static int PLAYER_MOVE_HIGH_SIDE_HAND = 6;

		public static int PLAYER_MOVE_SUPER_POSE = 7;

		public static int PLAYER_MOVE_IDLE_LAND = 8;

		public static int PLAYER_MOVE_IDLE_LOOP = 9;

		public static int PLAYER_WINK_INDEX = 10;

		public static string LOW_KICK_PREFIX = "Isabelle010";

		public static int LOW_KICK_FPS = 15;

		public static string LOW_KICK_ATTACHMENTNAME = "Isabelle05_bottom_left_&_Centre_leg";

		public static Vector3 LOW_KICK_ATTACHMENTPOS = new Vector3(24f, -8f, 0f);

		public static UIWidget.Pivot LOW_KICK_PIVOT = UIWidget.Pivot.TopRight;

		public static float LOW_KICK_START_ROT = 15f;

		public static float LOW_KICK_END_ROT = -15f;

		public static Vector2 LOW_KICK_HITPONT_OFFSET = new Vector2(-44f, -107f);

		public static Vector2 LOW_KICK_FORCE = new Vector2(50.5f, 100.5f);

		public static string MID_KICK_PREFIX = "Isabelle020";

		public static int MID_KICK_FPS = 15;

		public static string MID_KICK_ATTACHMENTNAME = "Isabelle01_Centre_leg";

		public static Vector3 MID_KICK_ATTACHMENTPOS = new Vector3(19f, 0f);

		public static UIWidget.Pivot MID_KICK_PIVOT = UIWidget.Pivot.Right;

		public static float MID_KICK_START_ROT = 30f;

		public static float MID_KICK_END_ROT = 0f;

		public static Vector2 MID_KICK_HITPONT_OFFSET = new Vector2(-56f, -72f);

		public static Vector2 MID_KICK_FORCE = new Vector2(50.5f, 100.5f);

		public static string HIGH_KICK_PREFIX = "Isabelle040";

		public static int HIGH_KICK_FPS = 15;

		public static string HIGH_KICK_ATTACHMENTNAME = "Isabelle04_left_leg";

		public static Vector3 HIGH_KICK_ATTACHMENTPOS = new Vector3(27f, 8f);

		public static UIWidget.Pivot HIGH_KICK_PIVOT = UIWidget.Pivot.Right;

		public static float HIGH_KICK_START_ROT = 0f;

		public static float HIGH_KICK_END_ROT = -15f;

		public static Vector2 HIGH_KICK_HITPONT_OFFSET = new Vector2(-79f, -14f);

		public static Vector2 HIGH_KICK_FORCE = new Vector2(50.5f, 100f);

		public static string LOW_HAND_PREFIX = "Isabelle030";

		public static int LOW_HAND_FPS = 15;

		public static string LOW_HAND_ATTACHMENTNAME = "Isabelle01_Centre_arm";

		public static Vector3 LOW_HAND_ATTACHMENTPOS = new Vector3(2f, 48f);

		public static UIWidget.Pivot LOW_HAND_PIVOT = UIWidget.Pivot.BottomRight;

		public static float LOW_HAND_START_ROT = 15f;

		public static float LOW_HAND_END_ROT = -15f;

		public static Vector2 LOW_HAND_HITPONT_OFFSET = new Vector2(-84f, 50f);

		public static Vector2 LOW_HAND_FORCE = new Vector2(50.5f, 100f);

		public static string MID_HAND_PREFIX = "Isabelle050";

		public static int MID_HAND_FPS = 15;

		public static string MID_HAND_ATTACHMENTNAME = "Isabelle03_top_left_arm";

		public static Vector3 MID_HAND_ATTACHMENTPOS = new Vector3(8f, 48f);

		public static UIWidget.Pivot MID_HAND_PIVOT = UIWidget.Pivot.BottomRight;

		public static float MID_HAND_START_ROT = 15f;

		public static float MID_HAND_END_ROT = 0f;

		public static Vector2 MID_HAND_HITPONT_OFFSET = new Vector2(-69f, 56f);

		public static Vector2 MID_HAND_FORCE = new Vector2(50.5f, 100f);

		public static string HIGH_HAND_PREFIX = "Isabelle070";

		public static int HIGH_HAND_FPS = 15;

		public static string HIGH_HAND_ATTACHMENTNAME = "Isabelle02_up_arm";

		public static Vector3 HIGH_HAND_ATTACHMENTPOS = new Vector3(21f, 50f);

		public static UIWidget.Pivot HIGH_HAND_PIVOT = UIWidget.Pivot.BottomRight;

		public static float HIGH_HAND_START_ROT = 0f;

		public static float HIGH_HAND_END_ROT = 0f;

		public static Vector2 HIGH_HAND_HITPONT_OFFSET = new Vector2(21f, 117f);

		public static Vector2 HIGH_HAND_FORCE = new Vector2(50.5f, 100f);

		public static string HIGH_SIDE_HAND_PREFIX = "Isabelle060";

		public static int HIGH_SIDE_HAND_FPS = 15;

		public static string HIGH_SIDE_HAND_ATTACHMENTNAME = "Isabelle02_up_arm";

		public static Vector3 HIGH_SIDE_HAND_ATTACHMENTPOS = new Vector3(21f, 50f);

		public static UIWidget.Pivot HIGH_SIDE_HAND_PIVOT = UIWidget.Pivot.BottomRight;

		public static float HIGH_SIDE_HAND_START_ROT = 0f;

		public static float HIGH_SIDE_HAND_END_ROT = 0f;

		public static Vector2 HIGH_SIDE_HAND_HITPONT_OFFSET = new Vector2(-21f, 117f);

		public static Vector2 HIGH_SIDE_HAND_FORCE = new Vector2(50.5f, 100f);

		public static string SUPER_POSE_PREFIX = "Isabelle08_Superpose00";

		public static int SUPER_POSE_FPS = 4;

		public static string SUPER_POSE_ATTACHMENTNAME = string.Empty;

		public static Vector3 SUPER_POSE_ATTACHMENTPOS = Vector3.zero;

		public static UIWidget.Pivot SUPER_POSE_PIVOT = UIWidget.Pivot.Center;

		public static float SUPER_POSE_START_ROT = 0f;

		public static float SUPER_POSE_END_ROT = 0f;

		public static Vector2 SUPER_POSE_HITPONT_OFFSET = new Vector2(21f, 120f);

		public static Vector2 SUPER_POSE_FORCE = new Vector2(1000.5f, 2000f);

		public static string IDLE_POSE_PREFIX = "Isabelle_pose_Default0";

		public static int IDLE_POSE_FPS = 10;

		public static string IDLE_POSE_ATTACHMENTNAME = string.Empty;

		public static Vector3 IDLE_POSE_ATTACHMENTPOS = Vector3.zero;

		public static UIWidget.Pivot IDLE_POSE_PIVOT = UIWidget.Pivot.Center;

		public static float IDLE_POSE_START_ROT = 0f;

		public static float IDLE_POSE_END_ROT = 0f;

		public static Vector2 IDLE_POSE_HITPONT_OFFSET = Vector2.zero;

		public static Vector2 IDLE_POSE_FORCE = new Vector2(0.5f, 20f);

		public static string IDLE_LOOP_PREFIX = "Isabelle_pose_idle0";

		public static int IDLE_LOOP_FPS = 10;

		public static string IDLE_LOOP_ATTACHMENTNAME = string.Empty;

		public static Vector3 IDLE_LOOP_ATTACHMENTPOS = Vector3.zero;

		public static UIWidget.Pivot IDLE_LOOP_PIVOT = UIWidget.Pivot.Center;

		public static float IDLE_LOOP_START_ROT = 0f;

		public static float IDLE_LOOP_END_ROT = 0f;

		public static Vector2 IDLE_LOOP_HITPONT_OFFSET = Vector2.zero;

		public static Vector2 IDLE_LOOP_FORCE = new Vector2(0.5f, 20f);
	}

	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 1)]
	public struct PowerUps
	{
		[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 1)]
		public struct PowerUpMulitpliers
		{
			public static int TWO_TIMES;

			public static int THREE_TIMES = 1;

			public static int FIVE_TIMES = 2;
		}

		public static int SUPER_POSE_ID = 1;

		public static int RANDOM_MULITPLIER_ID = 2;

		public static int FLOWER_BOUNCE_ID = 3;

		public static int POSE_UP_ID = 4;

		public static int DOUBLE_STARS_ID = 5;
	}

	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 1)]
	public struct SaveLoad
	{
		public static string PLAYER_DATA_PREFIX = "Player::";

		public static string BEST_SCORE_KEY = "BSKey";

		public static string GEM_INTRO_KEY = "GemTutKey";

		public static string BLACK_PEARL_TUT_KEY = "BlackPearlTutKey";

		public static string SUPERPOSE_TUT_KEY = "SuperPoseTutKey";

		public static string BOUNCEUP_TUT_KEY = "BounceUpTutKey";

		public static string DOUBLE_TUT_KEY = "DoubleTutKey";

		public static string MULTIPLIER_TUT_KEY = "MultiplierTutKey";
	}

	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 1)]
	public struct Sprites
	{
		[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 1)]
		public struct Game
		{
			public static string BOUNCE_POWERUP = "bounce_powerup";

			public static string POSEUP_POWERUP = "poseup_powerup";

			public static string RANDOM_MULTIPLIER_POWERUP = "random_multiplier";

			public static string SUPERPOSE_POWERUP = "superpose_powerup";

			public static string DOUBLESTAR_POWERUP = "x2_stars_powerup";

			public static string[] POWERUP = new string[5] { BOUNCE_POWERUP, POSEUP_POWERUP, RANDOM_MULTIPLIER_POWERUP, SUPERPOSE_POWERUP, DOUBLESTAR_POWERUP };

			public static string PINK_FLOWER = "sequin_pink";

			public static string PURPLE_FLOWER = "sequin_purple";

			public static string RED_FLOWER = "sequin_red";

			public static string VIOLET_FLOWER = "sequin_yellow";

			public static string YELLOW_FLOWER = "sequin_violet";

			public static string[] STARS = new string[5] { PINK_FLOWER, PURPLE_FLOWER, RED_FLOWER, VIOLET_FLOWER, YELLOW_FLOWER };

			public static string PINK_STAR = "super_pose_barstar_item_pink";

			public static string WHITE_STAR = "super_pose_barstar_item_white";

			public static string RED_STAR = "super_pose_barstar_item_red";

			public static string BLUE_STAR = "super_pose_barstar_item_blue";

			public static string YELLOW_STAR = "super_pose_barstar_item_yellow";

			public static string BLACK_STAR = "black_pearl";

			public static string[] FLOWERS = new string[6] { PINK_STAR, WHITE_STAR, RED_STAR, BLUE_STAR, YELLOW_STAR, BLACK_STAR };

			public static string BLACK_STAR_GLOW = "black_pearl_glow";

			public static string BOUNCE_POWERUP_GLOW = "bounce_powerup_glow";

			public static string RANDOM_MULITPLIER_GLOW = "random_multiplier_glow";

			public static string X2_POWERUP_GLOW = "x2_stars_powerup_glow";

			public static string SUPER_POSE_TIP_GLOW = "superpose_bar_tip_glow";

			public static string IN_GAME_BACKGROUND1 = "bg_ingame";

			public static string IN_GAME_BACKGROUND2 = "AG_SM_Dance_Studio";

			public static string IN_GAME_BACKGROUND3 = "AG_BS_BG_Stage";

			public static string IN_GAME_BACKGROUND4 = "AG_SM_Audition";

			public static string IN_GAME_BACKGROUND5 = "AG_BS_BG_Stage2";

			public static string LOADING_BACKGROUND = "bg";

			public static string MAIN_BACKGROUND = "bg";

			public static string[] IN_GAME_BACKGROUNDS = new string[5] { IN_GAME_BACKGROUND1, IN_GAME_BACKGROUND2, IN_GAME_BACKGROUND3, IN_GAME_BACKGROUND4, IN_GAME_BACKGROUND5 };

			public static string STAR_PARTICLE_BLUE = "star_particle_blue";

			public static string STAR_PARTICLE_GREEN = "star_particle_green";

			public static string STAR_PARTICLE_INDIGO = "star_particle_indigo";

			public static string STAR_PARTICLE_ORANGE = "star_particle_orange";

			public static string STAR_PARTICLE_RED = "star_particle_red";

			public static string STAR_PARTICLE_VIOLET = "star_particle_violet";

			public static string STAR_PARTICLE_WHITE = "star_particle_white";

			public static string STAR_PARTICLE_YELLOW = "star_particle_yellow";

			public static string[] STAR_PARTICLES = new string[8] { STAR_PARTICLE_BLUE, STAR_PARTICLE_GREEN, STAR_PARTICLE_INDIGO, STAR_PARTICLE_ORANGE, STAR_PARTICLE_RED, STAR_PARTICLE_VIOLET, STAR_PARTICLE_WHITE, STAR_PARTICLE_YELLOW };
		}
	}
}
