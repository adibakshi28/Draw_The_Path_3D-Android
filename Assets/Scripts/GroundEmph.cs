using UnityEngine;
using System.Collections;

public class GroundEmph : MonoBehaviour {
	public Color colorStart = Color.red;
	public Color colorEnd = Color.green;
	public float duration = 1.0F;
	public float scrollSpeed = 0.5F;
	public Renderer rend;
	void Start() {
		rend = GetComponent<Renderer>();
	}
	void Update() {
		float lerp = Mathf.PingPong(Time.time, duration) / duration;
		rend.material.color = Color.Lerp(colorStart, colorEnd, lerp);
		float offset = Time.time * scrollSpeed;
		rend.material.mainTextureOffset = new Vector2(0,offset);
	}
}