using UnityEngine;

public static class Extensions
{
	public static void FlashColor(this GameObject p_object, Color p_color, float p_duration)
	{
	}

	public static void SetParent(this Transform p_transform, Transform p_parent)
	{
		Vector3 localScale = p_transform.localScale;
		Vector3 position = p_transform.position;
		p_transform.parent = p_parent;
		p_transform.localScale = localScale;
		p_transform.position = position;
	}

	public static Vector3 ClampX(this Vector3 p_vector3, float p_min, float p_max)
	{
		return new Vector3(Mathf.Clamp(p_vector3.x, p_min, p_max), p_vector3.y, p_vector3.z);
	}

	public static Vector3 ClampY(this Vector3 p_vector3, float p_min, float p_max)
	{
		return new Vector3(p_vector3.x, Mathf.Clamp(p_vector3.y, p_min, p_max), p_vector3.z);
	}

	public static Vector3 ClampZ(this Vector3 p_vector3, float p_min, float p_max)
	{
		return new Vector3(p_vector3.x, p_vector3.y, Mathf.Clamp(p_vector3.z, p_min, p_max));
	}

	public static Vector3 ClampAll(this Vector3 p_vector3, float p_min, float p_max)
	{
		return new Vector3(Mathf.Clamp(p_vector3.x, p_min, p_max), Mathf.Clamp(p_vector3.y, p_min, p_max), Mathf.Clamp(p_vector3.z, p_min, p_max));
	}

	public static void SetX(this Transform p_transform, float p_x)
	{
		p_transform.position = new Vector3(p_x, p_transform.position.y, p_transform.position.z);
	}

	public static void SetLocalX(this Transform p_transform, float p_x)
	{
		p_transform.localPosition = new Vector3(p_x, p_transform.localPosition.y, p_transform.localPosition.z);
	}

	public static void SetY(this Transform p_transform, float p_y)
	{
		p_transform.position = new Vector3(p_transform.position.x, p_y, p_transform.position.z);
	}

	public static void SetLocalY(this Transform p_transform, float p_y)
	{
		p_transform.localPosition = new Vector3(p_transform.localPosition.x, p_y, p_transform.localPosition.z);
	}

	public static void SetZ(this Transform p_transform, float p_z)
	{
		p_transform.position = new Vector3(p_transform.position.x, p_transform.position.y, p_z);
	}

	public static void SetLocalZ(this Transform p_transform, float p_z)
	{
		p_transform.localPosition = new Vector3(p_transform.localPosition.x, p_transform.localPosition.y, p_z);
	}

	public static void ShiftX(this Transform p_transform, float p_x)
	{
		p_transform.position += new Vector3(p_x, 0f, 0f);
	}

	public static void ShiftLocalX(this Transform p_transform, float p_x)
	{
		p_transform.localPosition += new Vector3(p_x, 0f, 0f);
	}

	public static void ShiftY(this Transform p_transform, float p_y)
	{
		p_transform.position += new Vector3(0f, p_y, 0f);
	}

	public static void ShiftLocalY(this Transform p_transform, float p_y)
	{
		p_transform.localPosition += new Vector3(0f, p_y, 0f);
	}

	public static void ShiftZ(this Transform p_transform, float p_z)
	{
		p_transform.position += new Vector3(0f, 0f, p_z);
	}

	public static void ShiftLocalZ(this Transform p_transform, float p_z)
	{
		p_transform.localPosition += new Vector3(0f, 0f, p_z);
	}

	public static void SetRotationEulerAngles(this Transform p_transform, Vector3 p_euler)
	{
		Quaternion localRotation = p_transform.localRotation;
		localRotation.eulerAngles = p_euler;
		p_transform.localRotation = localRotation;
	}
}
