
var GunStart : AudioClip;
var GunFire : AudioClip;
var GunEnd : AudioClip;

var MinPitchVariation = 0.9f;
var MaxPitchVariation = 1.1f;

var MinVolumeVariation = 0.4f;
var MaxVolumeVariation = 0.6f;

function Start () 

	{

		//Randomly select a sound from the Array to play
		GetComponent.<AudioSource>().clip = GunStart[Random.Range (0,SFX.Length -1)];

	

		//Play SFX
		GetComponent.<AudioSource>().Play();

		//Else Don't Play
		else;

	

}
