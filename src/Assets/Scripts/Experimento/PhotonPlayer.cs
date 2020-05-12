/*
Entornos virtuales
Creador: Julio Morales: juliocesar.mr@protonmail.com
*/


using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class PhotonPlayer : MonoBehaviour
{

	private PhotonView PV;

	void Start()
	{
		CreateAvatar();
	}


	void CreateAvatar()
	{
		PV = GetComponent<PhotonView>();
		int spawnPicker = Random.Range(0, ExperimentoSetup.GS.spawnPoints.Length);
		Debug.Log("Spawn: " + spawnPicker.ToString());
		if (PV.IsMine)
		{
			string personaje = "";
			switch (SelectCharacter.SC.characterSelected)
			{
				case 1:
					personaje = "Character1";
					break;
				case 2:
					personaje = "Character2";
					break;
				case 3:
					personaje = "Character3";
					break;
				case 4:
					personaje = "Character4";
					break;
			}
			PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", personaje),
			ExperimentoSetup.GS.spawnPoints[spawnPicker].position, ExperimentoSetup.GS.spawnPoints[spawnPicker].rotation,
			0);

		}
	}
}
