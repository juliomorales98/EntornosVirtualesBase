/*
Entornos virtuales
Creador: Julio Morales: juliocesar.mr@protonmail.com

Manejamos las validaciones de la escena, así como poner el glow naranja localmente y cambiar las propiedades de los avatares cuando se seleccionan
*/


using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectCharacter : MonoBehaviourPunCallbacks
{

	public static SelectCharacter SC;
	public Text playerSelected;
	public Text info;

	public int characterSelected;

	[SerializeField] private GameObject startButton;

	[SerializeField] private GameObject[] avatarsGlow;

	private GameObject currentSelected;

	void Start()
	{

		//Creamos instancia de script
		if (SelectCharacter.SC == null)
		{
			SC = this;
		}
		else if (SelectCharacter.SC != this)
		{
			Destroy(SelectCharacter.SC);
			SelectCharacter.SC = this;
		}

		characterSelected = -1;

		playerSelected.text = "";
		info.text = "Selecciona un personaje.";

		if (PhotonNetwork.IsMasterClient)
		{
			startButton.SetActive(true);

		}
		else
		{
			startButton.SetActive(false);
		}

		//Quitamos el glow de todos los avatares
		InicializarGlow();

		currentSelected = null;
	}

	void Update()
	{
		if (Input.GetMouseButtonUp(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, 100.0f))
			{
				if (!hit.transform)
					return;

				GameObject hitGo = hit.collider.gameObject;

				/*
				Para la parte del selector se realizan los siguientes pasos:
				1. Se valida si se ha dado click en un avatar de pirata.
				2. Se le pide el ownership al objeto para poder cambiar los valores a través de la red.
				3. Se valida si ya se ha seleccionado un pirata y si ese seleccionado es el mismo al que se le dió click.Si esto se cumple no realiza nada más.
				4. Si el punto tres no se cumple, valida que el pirata no haya sido seleccionado. Si es así, primero quita el actual seleccionado (en caso de tener uno) y le asigna
				el nuevo.Ilumina la plataforma también.				
				*/

				if (hit.collider.name == "Player1_Selector")
				{
					hitGo.GetComponent<PhotonView>().RequestOwnership();
					if (HaveSelected() && hitGo.GetComponent<IsSelected>().GetOwner() == PhotonNetwork.NickName)
					{
						Debug.Log("Ya es mío");
					}
					else if (hitGo.GetComponent<IsSelected>().SetOwnership(PhotonNetwork.NickName))
					{
						playerSelected.text = "Personaje 1";
						characterSelected = 1;
						if (currentSelected != null)
							currentSelected.GetComponent<IsSelected>().RemoveOwnership();
						currentSelected = hitGo;
						IluminatePlatform(0);

					}

				}
				else if (hit.collider.name == "Player2_Selector")
				{
					hitGo.GetComponent<PhotonView>().RequestOwnership();
					if (HaveSelected() && hitGo.GetComponent<IsSelected>().GetOwner() == PhotonNetwork.NickName)
					{
						Debug.Log("Ya es mío");
					}
					else if (hitGo.GetComponent<IsSelected>().SetOwnership(PhotonNetwork.NickName))
					{
						playerSelected.text = "Personaje 2";
						characterSelected = 2;
						if (currentSelected != null)
							currentSelected.GetComponent<IsSelected>().RemoveOwnership();
						currentSelected = hitGo;
						IluminatePlatform(1);

					}

				}
				else if (hit.collider.name == "Player3_Selector")
				{
					hitGo.GetComponent<PhotonView>().RequestOwnership();
					if (HaveSelected() && hitGo.GetComponent<IsSelected>().GetOwner() == PhotonNetwork.NickName)
					{
						Debug.Log("Ya es mío");
					}
					else if (hitGo.GetComponent<IsSelected>().SetOwnership(PhotonNetwork.NickName))
					{
						playerSelected.text = "Personaje 3";
						characterSelected = 3;
						if (currentSelected != null)
							currentSelected.GetComponent<IsSelected>().RemoveOwnership();
						currentSelected = hitGo;
						IluminatePlatform(2);

					}

				}
				else if (hit.collider.name == "Player4_Selector")
				{
					hitGo.GetComponent<PhotonView>().RequestOwnership();
					if (HaveSelected() && hitGo.GetComponent<IsSelected>().GetOwner() == PhotonNetwork.NickName)
					{
						Debug.Log("Ya es mío");
					}
					else if (hitGo.GetComponent<IsSelected>().SetOwnership(PhotonNetwork.NickName))
					{
						playerSelected.text = "Personaje 4";
						characterSelected = 4;
						if (currentSelected != null)
							currentSelected.GetComponent<IsSelected>().RemoveOwnership();
						currentSelected = hitGo;
						IluminatePlatform(3);

					}
				}

			}
		}


	}

	private bool HaveSelected()
	{
		if (currentSelected == null)
		{
			return false;
		}

		return true;
	}

	private void IluminatePlatform(int op)
	{
		//Quitamos el glow naranja de todos los piratas y activamos el del indíce de argumento
		InicializarGlow();
		avatarsGlow[op].GetComponent<Renderer>().enabled = true;

	}

	private void InicializarGlow()
	{
		foreach (GameObject go in avatarsGlow)
		{
			go.GetComponent<Renderer>().enabled = false;
		}
	}

	public void StartExperiment()
	{
		if (playerSelected.text == "")
		{   //Si el host no ha seleccionado algún pirata
			NotificationManager.Instance.SetNewNotification("No has seleccionado ningún personaje");
			return;
		}

		//---------------------------------Validación de si todos los jugadores han seleccionado un pirata.-------------\\
		GameObject[] characters = GameObject.FindGameObjectsWithTag("CharacterToSelect");
		int playersSelected = 0;
		foreach (GameObject go in characters)
		{
			if (go.transform.GetComponent<IsSelected>().GetIsSelected())
			{
				playersSelected += 1;
			}

		}

		if (PhotonNetwork.PlayerList.Length > playersSelected)
		{
			NotificationManager.Instance.SetNewNotification("No todos los jugadores han seleccionado a un personaje.");
			Debug.Log(PhotonNetwork.PlayerList.Length + " < " + playersSelected);
			return;
		}
		//------------------------------------------------------------------------------------------------------------------\\


		PhotonNetwork.LoadLevel(2);
	}

	void OnGUI()
	{
		//Dependiendo del pirata seleccionado, la descripción que aparece en pantalla de este cambiará.
		if (characterSelected != 0)
		{
			if (characterSelected == 1)
			{
				info.text = "Descripción";

			}
			else if (characterSelected == 2)
			{
				info.text = "Descripción";

			}
			else if (characterSelected == 3)
			{
				info.text = "Descripción";

			}
			else if (characterSelected == 4)
			{
				info.text = "Descripción";

			}
		}
	}


}
