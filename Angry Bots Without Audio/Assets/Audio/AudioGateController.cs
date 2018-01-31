using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AudioGateController : MonoBehaviour {

    public AudioMixerSnapshot TransitionToSnapshot;
    public float TransitionTime;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnTriggerEnter(Collider other)
    {

        TransitionToSnapshot.TransitionTo(TransitionTime);
    }
}
