using UnityEngine;

public class Background : MonoBehaviour
{

	private float length, startPos;
	public GameObject cam;
	public float parallexEffect;

	void Start()
	{
		startPos = transform.position.x;
		length = GetComponent<SpriteRenderer>().bounds.size.x;
	}

	// background moves at a different rate depending on distance and speed of the player
	void Update()
	{
		float temp = (cam.transform.position.x * (1 - parallexEffect));
		float dist = (cam.transform.position.x * parallexEffect);

		transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

		if (temp > startPos + length) startPos += length;
		else if (temp < startPos - length) startPos -= length;
	}

}