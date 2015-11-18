using UnityEngine;
using System.Collections;

public class FacebookLogin : MonoBehaviour {

	public GameObject userProfileImage;

	// Use this for initialization
	void Start () {
		if (!FB.IsInitialized) {
			Debug.Log("Starting Facebook");
			FB.Init (FBInitCallback, FB.OnHideUnity, null);
		} else {
			FBInitCallback();
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void FBInitCallback()
	{
		if(!FB.IsLoggedIn)
		{
			Debug.Log("*** User is not logged in ***");
			FB.Login("email", AuthCallback);
		}
	}

	void AuthCallback(FBResult result)
	{
		if (FB.IsLoggedIn) {
			Debug.Log ("USER ID IS: " + FB.UserId);
			StartCoroutine(DisplayProfilePic());

		} else {
			Debug.Log("User cancelled Login");
		}
	}

	// Get the profile picture of the user and display it. 
	IEnumerator DisplayProfilePic()
	{
		if (FB.IsLoggedIn) {
			
			WWW url = new WWW ("https" + "://graph.facebook.com/" + FB.UserId + "/picture?type=large");
			
			Texture2D textFb2 = new Texture2D (10, 10, TextureFormat.DXT1, false);
			
			yield return url;
			userProfileImage.GetComponent<Renderer> ().material.mainTexture = textFb2;
			url.LoadImageIntoTexture (textFb2);
		}
	}
}
