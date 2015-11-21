using UnityEngine;
using System.Collections;

public class WeaponBehaviour : MonoBehaviour {

	public enum WeaponEnum {
		Wooden_Sword,
		Wooden_Axe, 
		Steel_Sword,
		Steel_Axe, 
		Bow
	}

	public enum WeaponRarityEnum
	{
		Not_Rare,
		Almost_Rare,
		Rare

	}

	public WeaponEnum WeaponType;
	public string WeaponName;
	public WeaponRarityEnum WeaponRarity;
	public float DamageFactor;
	public float BaseDamage;
	public float MinAdditonalDamage;
	public float MaxAdditionalDamage;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
