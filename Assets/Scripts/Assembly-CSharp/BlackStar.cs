using UnityEngine;

public class BlackStar : MonoBehaviour
{
	private void Update()
	{
		FlowerPhysics flowerPhysics = base.transform.gameObject.GetComponent("FlowerPhysics") as FlowerPhysics;
		if (flowerPhysics != null)
		{
			MonoBehaviour.print("found flowerphysics");
			flowerPhysics.BlackStar = true;
		}
		else
		{
			MonoBehaviour.print("tag = " + base.transform.tag);
			MonoBehaviour.print("name = " + base.transform.name);
			MonoBehaviour.print("fS = " + flowerPhysics);
		}
	}
}
