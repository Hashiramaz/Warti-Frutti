using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Text statusText;
	// Use this for initialization
	void Start () {
		
	}
	
    public void ChangeStatus(string status)
    {
        statusText.text = status;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
