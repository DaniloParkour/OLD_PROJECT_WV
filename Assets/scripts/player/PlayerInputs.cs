using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour {

    private float leftJet = 0;
    public float RightJet { get { return rightJet; } }

    private float rightJet = 0;
    public float LeftJet { get { return leftJet; } }
    
    private string str_LeftJet = "LeftJet";
    private string str_RightJet = "RightJet";

    private bool enableInputs = true;
    
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if(enableInputs) {
            leftJet = Input.GetAxis(str_LeftJet);
            rightJet = Input.GetAxis(str_RightJet);
        }
    }

    void OnEnable() {
        EventsManager.OnWinLevel += disableInputs;
    }


    void OnDisable() {
        EventsManager.OnWinLevel -= disableInputs;
    }

    private void disableInputs() {
        enableInputs = false;
    }

}
