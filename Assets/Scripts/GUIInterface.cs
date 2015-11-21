using UnityEngine;
using System.Collections;

public class GUIInterface : MonoBehaviour {

	// Bools to turn interface on and off
	[HideInInspector]
	public bool b_statsWindowOpen;
	[HideInInspector]
	public bool b_optionsWindowOpen;

	// Window Rect Positions
	public Rect statWindowRect = new Rect(Screen.width / 2 - 175, Screen.height / 2 - 175, 350, 350);	// Stat Window Rect
	public Rect openOptionsRect = new Rect(Screen.width / 2 - 175, Screen.height / 2 - 175, 350, 350);	// Options Window Rect

	// GUI Skins
	public GUISkin NecroSkin;


	// Reference PlayerScript
	PlayerScript playerScript;

	void Awake()
	{
		// Tell this script that the gameobject is this
		playerScript = GetComponent<PlayerScript> ();
	}

	// Use this for initialization
	void Start () {
	
	}

	void OnGUI()
	{

		// Set default GUI skin
		GUI.skin = NecroSkin;

		// Open options window
		if(b_optionsWindowOpen)
		{
			// Draw options screen
			openOptionsRect = GUI.Window(1, openOptionsRect, OpenOptionsWindow, "");
		}


		// Open stats window
		if(b_statsWindowOpen)
		{
			// Draw Stats Window
			statWindowRect = GUILayout.Window(0, statWindowRect, OpenStatWindow, "", GUILayout.Width(250), GUILayout.Height(400));
		}

	}

	// Open stats window
	void OpenStatWindow(int windowID)
	{
		// Unlock the cursor
		Cursor.lockState = CursorLockMode.None;
		
		
		// Wrap it up GUI
		GUILayout.BeginVertical();
			GUILayout.Space (45);
		GUILayout.Label ("STATS", GUI.skin.GetStyle("StatWindow"), GUILayout.Width (150), GUILayout.Height(32));
			GUILayout.Space(20);
		GUILayout.Label("Points: " + playerScript.pointsToAdd, GUI.skin.GetStyle("StatWindow"), GUILayout.Width(150), GUILayout.Height(32));
			GUILayout.Space(20);
		DrawStatGUI("STR: ", ref playerScript.str);
			GUILayout.Space(15);
		DrawStatGUI("INT: ", ref playerScript.intel);
			GUILayout.Space(15);
		DrawStatGUI("VIT: ", ref playerScript.vit);
			GUILayout.Space(15);
		DrawStatGUI("DEF: ", ref playerScript.def);
			GUILayout.Space (5);
		GUILayout.EndVertical();
		GUI.DragWindow();
	}

	// Draw Stats GUI
	void DrawStatGUI(string name, ref int stat)
	{
		GUILayout.BeginHorizontal ();
			GUILayout.Label (name + stat, GUILayout.Width(100));
		GUILayout.FlexibleSpace ();	
		if(GUILayout.Button("+", GUILayout.Width(50)))
		{
			if(playerScript.pointsToAdd > 0)
			{
				IncrementStat(ref stat);
			}
		}
		if(GUILayout.Button("-", GUILayout.Width(50)))
		{
			if(playerScript.pointsToAdd > 0)
			{
				DecrementStat(ref stat);
			}
		}
		GUILayout.EndHorizontal ();
	}


	private void IncrementStat(ref int stat)
	{
		stat++;
		playerScript.pointsToAdd--;
	}
	
	private void DecrementStat(ref int stat)
	{
		if(stat != 5)
		{
			stat--;
			playerScript.pointsToAdd++;
		}
	}


	void OpenOptionsWindow(int windowID)
	{
		// Unlock cursor 
		Cursor.lockState = CursorLockMode.None;
		
		// Left
		
		GUI.Label (new Rect (175 - 92, 68, 184, 32), "O P T I O N S");
		// 68 + 32 (Height of image) + 10 (Offset)
		GUI.Label(new Rect(20, 110, 184, 32), "B a c k g r o u n d V o l u m e:");
		MusicManager.backgroundMusicLevel = GUI.HorizontalSlider (new Rect (200, 116, 100, 20), MusicManager.backgroundMusicLevel, 0, 1);
		GUI.Label (new Rect (20, 142, 184, 32), "G r a p h i c s  L e v e l:");
		GUI.Label(new Rect(20, 192, 50, 17), "H I G H", GUI.skin.GetStyle("GraphicsText"));

		GUI.DragWindow();
	}

}
