/*
Entornos virtuales
Creador: Julio Morales: juliocesar.mr@protonmail.com
*/

using Photon.Pun;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
public class ExperimentoSetup : MonoBehaviour
{

	public static ExperimentoSetup GS;
	public Transform[] spawnPoints;
	[SerializeField] private Text playerName;
	[SerializeField] private Text characterName;
	private void OnEnable()
	{
		if (ExperimentoSetup.GS == null)
		{
			ExperimentoSetup.GS = this;
		}
	}
	void Start()
	{
		//PhotonNetwork.AutomaticallySyncScene = true;//activamos de nuevo la sincronización de escenas.
		Debug.Log("Entró a game setup");
		playerName.text = PhotonNetwork.LocalPlayer.NickName.ToString();
		characterName.text = "Pirata " + SelectCharacter.SC.characterSelected.ToString();
		CreatePlayer();
		Debug.Log("Se seleccionó " + SelectCharacter.SC.characterSelected.ToString());
	}
	private void CreatePlayer()
	{
		PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonNetworkPlayer"), new Vector3(-1.3f, 6.4f, 9.8f), Quaternion.identity);

	}
}
