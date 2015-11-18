using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	public AudioClip[] audioClips;

	public static float backgroundMusicLevel = 0.5f;
	public float someOtherMusicLevel;

	private AudioSource audio;

	// Use this for initialization
	void Start () {

		audio = Camera.main.GetComponent<AudioSource>();

		// Set default music on start up.
		audio.clip = audioClips [0];

		audio.Play ();

	}
	
	// Update is called once per frame
	void Update () {

		audio.volume = backgroundMusicLevel;
	}
}
