#pragma strict

var Game_Object : GameObject;

function OnTriggerEnter(Trigger : Collider)

{

	if (Trigger.gameObject.name =="Player")
		
		Game_Object.SetActive(true);
			Debug.Log("Player Has Turned Object On");
}