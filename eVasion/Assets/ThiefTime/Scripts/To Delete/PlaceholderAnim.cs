using UnityEngine;
using System.Collections;

public class PlaceholderAnim : MonoBehaviour {

    public Animator animatorGuard;
	// Use this for initialization
	void Start () {
        animatorGuard = GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {

        animatorGuard.SetBool("Idle", true);
        animatorGuard.SetBool("Walking", true);
        animatorGuard.SetBool("Detect", true);
        animatorGuard.SetBool("Dash", true);

	}
}
