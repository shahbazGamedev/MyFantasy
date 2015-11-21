using UnityEngine;
using System.Collections;

public class Respawns : MonoBehaviour {

	public GameObject[] respawnPoints;
	public static Vector3 respawnPos;

	// Use this for initialization
	void Start () {
	
		// Find and add all gameobjects with the tag of 'Respawn'
		respawnPoints = GameObject.FindGameObjectsWithTag ("Respawn");

		// Set startup respawn position
		respawnPos = respawnPoints [0].transform.position;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
