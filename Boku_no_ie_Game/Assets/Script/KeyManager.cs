using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class KeyManager {

	// SHOKUINSHITSU
	// KATEIKASHITSU
	// SEIBUTUSHITSU
	// KATEIKASHITSU


	public static void GetRoomKey( string roomName ){
		PlayerPrefs.SetInt (roomName, 1);
	}

	public static bool HaveRoomKey( string roomName ){
		int keyNum = PlayerPrefs.GetInt (roomName, 0);
		if (keyNum == 1) {
			return true;
		}{
			return false;
		}
	}

	public static void clearKey(){
		PlayerPrefs.DeleteAll ();
	}


}
