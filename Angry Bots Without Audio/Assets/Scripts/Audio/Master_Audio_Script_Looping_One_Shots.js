


var MinPitchVariation = 0.9f;
var MaxPitchVariation = 1.1f;

var MinWaitTime : float	= 1.0;
var MaxWaitTime : float = 2.0;

var MinVolume : float	= 0.5;
var MaxVolume : float	= 1.0;

private var		timer : float	= 0.0;
private var		nextInterval : float	= 1.0;
private var 	audioSource : AudioSource	= null;

var audioClips : AudioClip[];

function Start () //Use the start function to initialise as it gets called when the game starts
{

	GetComponent.<AudioSource>().pitch = Random.Range(MinPitchVariation, MaxPitchVariation);//Random Pitch

	nextInterval = Random.Range(MinWaitTime, MaxWaitTime);//Initialise nextInterval to a random interval
	audioSource = GetComponent.<AudioSource>();//Get the AudioSource Component

	//It's good practice to check things and throw errors

	if(audioSource == null)
		Debug.Log ("There is no audio source");

	if(audioClips.Length == 0)	
		Debug.LogError ("You haven't set any audio clips");
}

function Update () {

	timer += Time.deltaTime;//delta time is time since last frame

	if(timer >= nextInterval && !audioSource.IsPlaying)

	{
		audioSource.clip		= audioClips [Random.Range(0, audioClips.Length -1)]; // NOTE the Length -1 OTHERWISE you can go out of Range
		audioSource.volume  	= Random.Range(MinVolume, MaxVolume); //Set Random Volume
		audioSource.Play();											  //Play the Clip

		nextInterval = Random.Range(MinWaitTime, MaxWaitTime); //Pick the time we wait until the next clip
		timer = 0.0;										   //Zero the Timer


		}

}