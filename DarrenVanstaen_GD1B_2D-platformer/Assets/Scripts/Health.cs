using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	
	public float health = 100f;
	public Texture2D healthBarTexture;
	
	// Update is called once per frame
	void Update () {
		if (health < 0) {
			
			if (transform.gameObject.tag == "Player")
				Application.LoadLevel("Scene 1");
			else
				gameObject.SetActive(false);
		}
	}
	
	public void TakeDamage (float amount) {
		health -= amount;
	}
	
	void OnGUI() {
		if (gameObject.tag == "Player") {
			GUI.DrawTexture (new Rect (10, 10, (health/960) * 200, 10), healthBarTexture);
		}
	}
}
