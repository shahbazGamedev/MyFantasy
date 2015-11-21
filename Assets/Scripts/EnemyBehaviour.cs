using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

    // Enum to determine the type of enemy
	public enum EnemyEnum {
		Spider,
		Golem, 
		Normal, 
		Boss
	}

    // Use this to try and stabalise the enemy and prevent it
    // from looking up at the character
    public enum Stabalise
    {
        Up,
        Down,
        Right

    }

    public Stabalise stable = new Stabalise();
	public EnemyEnum EnemyType = new EnemyEnum();

    public float HealthValue;
    public float AttackRate;
    public float expGenerated;
    public static float percentHealthLeft;
    

    public GameObject[] NodePoints;
    public static GameObject EnemyRef;
    private Transform player;
    private GameObject playertwo;
    GameObject go;

    
	public static bool beingAttacked;
	
	
	int index;

	// Use this for initialization
	void Start () {
			index = Random.Range (0, NodePoints.Length);

			switch(EnemyType)
			{
				case EnemyEnum.Spider:
					GetComponent<Animation> ().Play ("walk");
				break;

				case EnemyEnum.Golem:
					//GetComponent<Animator>().SetFloat("speed", 1);
				break;

				default:
			
				break;
			}

			beingAttacked = false;

			player = GameObject.FindGameObjectWithTag ("Player").transform;
			playertwo = GameObject.FindGameObjectWithTag ("Player");

	}
	
	// Update is called once per frame
	void Update () {

		// If the enemy is not being attacked, carry on walk cycle
		if(!beingAttacked)
		{
			MoveEnemy ();
		}


	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Weapon")
		{
                Debug.Log(EnemyType + " has been hit by player");

                //Decrease enemy health
                HealthValue = HealthValue - PlayerScript.currentWeapon.GetComponent<WeaponBehaviour>().BaseDamage *
                                            PlayerScript.currentWeapon.GetComponent<WeaponBehaviour>().DamageFactor +
                                            Random.Range(PlayerScript.currentWeapon.GetComponent<WeaponBehaviour>().MinAdditonalDamage,
                                             PlayerScript.currentWeapon.GetComponent<WeaponBehaviour>().MaxAdditionalDamage);


                percentHealthLeft = (HealthValue / 100) * 100;

                if (HealthValue <= 0)
                {
                    HealthValue = 0;
                    percentHealthLeft = 0;
                    beingAttacked = false;
                    PlayerScript.exp = PlayerScript.exp + expGenerated;
                    Debug.Log("Player exp is " + PlayerScript.exp);
                    Destroy(this.gameObject);
                    Debug.Log("Is the enemy being attacked? " + beingAttacked);
                }


                // Play the hit animation
                switch (EnemyType)
                {
                    case EnemyEnum.Spider:
                        GetComponent<Animation>().Play("hit1");
                        break;

                    case EnemyEnum.Golem:
                        //GetComponent<Animator>().SetFloat("speed", 1);
                        break;

                    default:

                        break;
                }


                // Oh no, enemy is being attacked. Begin attacking invoke
                InvokeRepeating("AttackPlayer", 1, 1);






                // I'm now being attacked...
                beingAttacked = true;

                // Tell the player script which enemy we are
                EnemyRef = this.gameObject;



                Debug.Log(percentHealthLeft);

		}
	}

	void AttackPlayer()
	{
		// Check if the player is within distance of attack
		float distance = Vector3.Distance (player.transform.position, transform.position);

		// Look at the player while they beat the enemy. Prevent the enemy from looking upwards.
		Vector3 point = player.position;
		point.y = 0;

        switch(stable)
        {
                case Stabalise.Up:
                transform.LookAt(point, Vector3.up);
                break;

                case Stabalise.Down:
                transform.LookAt(point, Vector3.down);
                break;

                case Stabalise.Right:
                transform.LookAt(point, Vector3.down);
                break;
        }
        

		//transform.rotation = new Quaternion (0, GameObject.FindGameObjectWithTag ("Player").transform.rotation.y, GameObject.FindGameObjectWithTag ("Player").transform.rotation.z, 1);

		// Play attacking animation
		switch(EnemyType)
		{
			case EnemyEnum.Spider:
				GetComponent<Animation> ().Play ("attack1");
			break;
			
			case EnemyEnum.Golem:
				//GetComponent<Animator>().SetBool("attack", true);
			break;
			
			default:
			
			break;
		}


		// If the player's distance is greater than 5, stop this attacking invoke
		if (distance > 5){

			// Cancel the Invoke
			CancelInvoke("AttackPlayer");

			// Tell the Developer...
			Debug.Log("Cancelling attack on player");

			// Enemy no longer being attacked
			beingAttacked = false;

			switch(EnemyType)
			{
				case EnemyEnum.Spider:
					GetComponent<Animation> ().Play ("walk");
				break;
				
				case EnemyEnum.Golem:
					//GetComponent<Animator>().SetBool("attack", false);
				break;
				
				default:
				
				break;
			}

		}
	}

	void MoveEnemy()
	{
		// Begin by selecting random node
		go = NodePoints [index];

		// Enemy, look at Node
		//transform.LookAt (go.transform.position);

		// Play the walking animation


		//Debug.Log ("Unity has selected Node Number: " + index);

		//Debug.Log ("Spider is at " + transform.position + " Node is at " + go.transform.position);

		// If we've reached node position...
		if(transform.position == go.transform.position)
		{
			//Debug.Log("Spider hit position");
			// Play the idle animation
			switch(EnemyType)
			{
				case EnemyEnum.Spider:
					GetComponent<Animation> ().Play ("walk");
				break;
				
				case EnemyEnum.Golem:
					//GetComponent<Animator>().SetFloat("speed", 1);
				break;
				
				default:
				
				break;
			}

			// Select new node position
			index = Random.Range(0, NodePoints.Length);
		}

		// Move the target to node position
		transform.position = Vector3.MoveTowards (transform.position, go.transform.position, 0.5f * Time.deltaTime);

	}
}
