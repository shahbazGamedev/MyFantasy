using UnityEngine;
using System.Collections;

public class EnemyHealthBar : MonoBehaviour {

	public Texture enemyHealthBar;
	public Texture enemyHealthBarNoBorder;
	Color guiColor = new Color(0, 1, 0);

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI()
	{


		// If the enemy is being attacked, draw the GUI texture 
		if (EnemyBehaviour.beingAttacked) {
			// Begin by setting the color of the health bar to green
			// Draw the texture
			float xOffset = enemyHealthBar.width / 2;
			Debug.Log ("Enemy Health : " + EnemyBehaviour.percentHealthLeft);

			GUI.DrawTexture (new Rect (Screen.width / 2 - enemyHealthBar.width + xOffset, 25, enemyHealthBar.width, 20), enemyHealthBar);
			GUI.DrawTexture (new Rect (Screen.width / 2 - enemyHealthBar.width + xOffset, 25, enemyHealthBarNoBorder.width * EnemyBehaviour.percentHealthLeft / 100, 20), enemyHealthBarNoBorder);
			GUI.color = Color.red;
			GUI.Label (new Rect (Screen.width / 2, 0, 100, 25), EnemyBehaviour.percentHealthLeft.ToString ("F0") + "%");
			//GUI.Label(new Rect(Screen.width / 2 + 50, 0, 100, 25), EnemyBehaviour.EnemyRef.name);

			// Set the color of the text back to white
			GUI.color = Color.white;
		} else {

		}
	}
}
