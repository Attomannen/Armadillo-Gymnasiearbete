using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.InputSystem;
public class ClickExample : MonoBehaviour
{
	public Button yourButton;
	GameObject player;
	GameObject player2;
	void Start()
	{
		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		player = GameObject.Find("Player(Clone)");
		player2 = GameObject.Find("Player2(Clone)");


		player.GetComponent<PlayerInput>().ActivateInput();
		player2.GetComponent<PlayerInput>().ActivateInput();

		Debug.Log("You have clicked the button!");

		Destroy(gameObject);
	}
}