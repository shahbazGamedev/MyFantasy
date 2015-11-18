using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour {

	public static GameObject currentWeapon;
	private bool lookingAtWeapon;
	private bool weaponEquipped;
	public int level;
	public static float exp;
	public static float expTillNextLevel;

	public Texture expBar;
	public Texture expBarTextureNoBorder;

	// Stats for player
	public int str;
	public int intel;
	public int vit;
	public int def;
	public int pointsToAdd;



	private bool hasLeveled;

	GUIInterface guiInterface;


	void Awake()
	{
		guiInterface = GetComponent<GUIInterface> ();
	}


	// Use this for initialization
	void Start () {

		GetComponent<GUIInterface> ();
		lookingAtWeapon = false;
		weaponEquipped = false;
		currentWeapon = GameObject.Find ("swordWood");
		transform.position = Respawns.respawnPos;
		level = 1;
		exp = 0;
		expTillNextLevel = 150;

		// Set inital stats of the player starting at 5 each
		str = 5;
		intel = 5;
		vit = 5;
		def = 5;
		hasLeveled = false;
		pointsToAdd = 10;

	}
	
	// Update is called once per frame
	void Update () {

		// Lock the cursor
		Cursor.lockState = CursorLockMode.Locked;

		if(Input.GetKeyDown(KeyCode.K))
		{
			guiInterface.b_statsWindowOpen = !guiInterface.b_statsWindowOpen;
		}

		if(Input.GetKeyDown(KeyCode.O))
		{
			guiInterface.b_optionsWindowOpen = !guiInterface.b_optionsWindowOpen;
		}

		Ray ray = Camera.main.ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0));
		RaycastHit hit;

        //currentWeapon.GetComponent<Animation>().Play();
        if (Physics.Raycast(ray, out hit))
        {

            //Debug.Log ("Hitting : " + hit.transform.name);

            if (hit.transform.tag == "Weapon")
            {
                lookingAtWeapon = true;
                currentWeapon = hit.transform.gameObject;
            }

            else if (hit.transform.gameObject.tag != "Weapon")
            {
                lookingAtWeapon = false;
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (hit.transform.name == "House_01")
                {
                    // Housing area not ready yet, disable it till further notice
                    //Application.LoadLevel("personalHouse");
                }

                if (!weaponEquipped)
                {
                    if (hit.transform.tag == "Weapon")
                    {
                        currentWeapon.gameObject.transform.parent = this.gameObject.transform;
                        currentWeapon.transform.localPosition = new Vector3(-0.33f, 0.006f, 0.42f);
                        currentWeapon.transform.localRotation = new Quaternion(0, 268, 315, 0);
                        weaponEquipped = true;
                    }
                }
            }

            if (weaponEquipped)
            {
                if (Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKeyDown(0))
                {
                    currentWeapon.GetComponent<Animation>().Play("MegaAttack");
                }
            }

            if (exp >= expTillNextLevel)
            {
                level = level + 1;
                Debug.Log(" * Player Level Increased to " + level + " * ");
                expTillNextLevel = expTillNextLevel * 3;
                Debug.Log(" * Next level will be reached at " + expTillNextLevel + " * ");
                hasLeveled = true;
                pointsToAdd = pointsToAdd + 2;
            }

            // If the player should fall, take them back to respawn
            if (transform.position.y <= -20)
            {
                transform.position = Respawns.respawnPos;
            }
        }
	}

	void OnGUI()
	{

		if(lookingAtWeapon)
		{
			GUI.BeginGroup(new Rect(Screen.width / 2 + 100, Screen.height / 2, 250, 250), "");
				GUI.Box(new Rect(0, 0, 250, 250), "");
				GUI.Label(new Rect(20, 20, 200, 100), "Name: " + currentWeapon.GetComponent<WeaponBehaviour>().WeaponName);
				GUI.Label(new Rect(20, 40, 200, 100), "Type: " + currentWeapon.GetComponent<WeaponBehaviour>().WeaponType.ToString().Replace("_", " "));
				GUI.Label(new Rect(20, 60, 200, 100), "Rarity: " + currentWeapon.GetComponent<WeaponBehaviour>().WeaponRarity.ToString().Replace("_", " "));
				GUI.Label(new Rect(20, 80, 200, 100), "Base Damage: " +  currentWeapon.GetComponent<WeaponBehaviour>().BaseDamage);
				GUI.Label(new Rect(20, 100, 200, 100), "Damage Factor: " +  currentWeapon.GetComponent<WeaponBehaviour>().DamageFactor);
				GUI.Label(new Rect(20, 120, 200, 100), "Additional Damage: " +  currentWeapon.GetComponent<WeaponBehaviour>().MinAdditonalDamage + " ~ " + currentWeapon.GetComponent<WeaponBehaviour>().MaxAdditionalDamage);
			GUI.EndGroup();
		}


		GUI.DrawTexture (new Rect (Screen.width - Screen.width, Screen.height - 32, exp * Screen.width / expTillNextLevel, 20), expBar);
		GUI.DrawTexture (new Rect (Screen.width - Screen.width, Screen.height - 32, Screen.width, 20), expBarTextureNoBorder);
		//GUI.Label (new Rect (Screen.width / 2, Screen.height - 32, 300, 20), "Exp: " + exp + " / " + expTillNextLevel + " ( Level " + level + " )", GUI.skin.GetStyle("OutlineText"));

	}
}

