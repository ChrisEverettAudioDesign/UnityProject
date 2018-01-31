

var SFX : AudioClip[];

var MinPitchVariation = 0.9f;
var MaxPitchVariation = 1.1f;

var MinVolumeVariation = 0.4f;
var MaxVolumeVariation = 0.6f;

var ProbabilityOfSFXPlaying = 1f;

function Update(){

	//Press Spacebar to triggerSFX from array GET RID OF THIS! WHEN YOU ARE HAPPY THAT THE CODE IS WORKING!
	if(Input.GetButtonUp("Jump"))

	{

		//Randomly select a sound from the Array to play
		GetComponent.<AudioSource>().clip = SFX[Random.Range (0,SFX.Length -1)];

		//Get Random Pitch
		GetComponent.<AudioSource>().pitch = Random.Range (MinPitchVariation, MaxPitchVariation);

		//Get Random Volume
		GetComponent.<AudioSource>().volume = Random.Range (MinVolumeVariation, MaxVolumeVariation);

		//Chance SFX will Play (Default to 100% chance)
		if (Random.value <=ProbabilityOfSFXPlaying)

		//Play SFX
		GetComponent.<AudioSource>().Play();

		//Else Don't Play
		else;

	}

	}