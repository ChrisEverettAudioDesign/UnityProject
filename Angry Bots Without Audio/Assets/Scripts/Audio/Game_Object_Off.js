#pragma strict

var Game_Object : GameObject;

function OnTriggerEnter(Trigger : Collider)

{

	if (Trigger.gameObject.name =="Player")
	
	Game_Object.SetActive(false);
		Debug.Log("Player Has Turned Off Object");

}