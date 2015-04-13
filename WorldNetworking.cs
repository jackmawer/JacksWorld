using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class WorldNetworking : MonoBehaviour {
	public string playerPrefab;
	public bool testing = true;
	public string version = "Alpha 6";
	[HideInInspector]
	public string versionWarning;


	// Use this for initialization
	void Start()
	{
		PhotonNetwork.ConnectUsingSettings(version);
	}
	
	void OnGUI()
	{
		//GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
		GUILayout.Label(getCurrentDebugLabel());
	}
	
	public string getCurrentDebugLabel () 
	{
		if (Debug.isDebugBuild) { versionWarning = "[Development Build, Do not distribute!]"; } else { versionWarning = "[Testing Copy, Do not distribute!]"; }
		string currentDebugLabel = "Jack's World\nBy Jack Mawer\nVersion "+version+" "+versionWarning+"\n\n"+getDateTime ("")+"\nServer Status: "+PhotonNetwork.connectionStateDetailed.ToString();
		return currentDebugLabel;
	}

	void OnJoinedLobby ()
	{
		RoomOptions roomOptions = new RoomOptions() { isVisible = true, maxPlayers = 500 };
		PhotonNetwork.JoinOrCreateRoom("Default", roomOptions, TypedLobby.Default);
	}

	void OnPhotonJoinRoomFailed (object[] reasonForFailure)
	{
		//Example: void OnPhotonJoinRoomFailed(object[] codeAndMsg) { // codeAndMsg[0] is int ErrorCode. codeAndMsg[1] is string debug msg. } 
		Debug.Log ("Failed to join room with code " + reasonForFailure[0] + " message " + reasonForFailure[1] + ". Retrying.");
		OnJoinedLobby ();//Try again.
	}

	void OnJoinedRoom() 
	{
		SpawnPlayer ();
	}
	
	private void SpawnPlayer()
	{
		Debug.Log ("SpawnPlayer");

		GameObject MyPlayer = PhotonNetwork.Instantiate(playerPrefab, new Vector3(2530f, 8f, 2510f), Quaternion.identity, 0);

		PhotonView myPhotonView;
		myPhotonView = MyPlayer.GetComponent<PhotonView>();
		if (myPhotonView.isMine) {
			RigidbodyFirstPersonController playerControl = MyPlayer.GetComponent<RigidbodyFirstPersonController> ();
			playerControl.enabled = true;
			GameObject playerCamera = MyPlayer.transform.Find("Camera").gameObject;
			playerCamera.active = true;
		}
	}

	// Update is called once per frame
	void Update () {
	
	}

	public string getDateTime(string dateOrTime) 
	{
		string currentDateFormatted;
		string currentTimeFormatted;
		string dateOrTimeResponse;

		currentDateFormatted = System.DateTime.Now.ToString("dd/MM/yyyy");
		currentTimeFormatted = System.DateTime.Now.ToString("HH:mm:ss");
		if (dateOrTime == "time")
		{
			dateOrTimeResponse = currentTimeFormatted;
		} else if (dateOrTime == "date") 
		{
			dateOrTimeResponse = currentDateFormatted;
		} else
		{
			//Debug.LogWarning("getDateTime(string dateOrTime): Expected either 'date' or 'time' but got '"+dateOrTime+"', assuming you want both.");
			dateOrTimeResponse = currentDateFormatted+" "+currentTimeFormatted;
		}
		return dateOrTimeResponse;
	}
}
