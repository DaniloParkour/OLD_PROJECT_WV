using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoviments : MonoBehaviour {

    [SerializeField]
    private float jetForce = 1;

    private Rigidbody2D rgdb;

    private Transform dir_01;
    private Transform dir_02;
    private Transform dir_03;
    private Vector2 naveDirection;
    private Vector2 naveFowardDirection;

    private Transform t_leftJet;
    private Transform t_rightJet;

    private PlayerController controller;
    
    // Start is called before the first frame update
    void Start() {
        rgdb = GetComponent<Rigidbody2D>();
        dir_01 = transform.Find("naveDirection").Find("01");
        dir_02 = transform.Find("naveDirection").Find("02");
        dir_03 = transform.Find("naveDirection").Find("03");

        t_leftJet = transform.Find("jets").Find("leftJet");
        t_rightJet = transform.Find("jets").Find("rightJet");

        controller = PlayerController.instance;
    }

    // Update is called once per frame
    void Update() {
        naveDirection = (dir_02.position - dir_01.position).normalized;
        naveFowardDirection = (dir_03.position - dir_01.position).normalized;

        //Debug.DrawRay(t_leftJet.position, naveDirection);
        //Debug.DrawRay(t_rightJet.position, naveDirection);
    }

    void FixedUpdate() {
        if (controller.Inputs.LeftJet > 0) {
            rgdb.AddForceAtPosition(jetForce * naveDirection, t_leftJet.position);            
        }
        if (controller.Inputs.RightJet > 0) {
            rgdb.AddForceAtPosition(jetForce * naveDirection, t_rightJet.position);
        }
        if(Mathf.Abs(rgdb.velocity.x) > 0.1f) {
            rgdb.velocity = Vector2.Lerp(rgdb.velocity, Vector2.up * rgdb.velocity, 0.01f);
        }
        
        if(rgdb.rotation > 80) {
            rgdb.rotation = Mathf.Lerp(rgdb.rotation, 80, 0.01f);
            rgdb.angularVelocity = 0;
        } else if (rgdb.rotation < -80) {
            rgdb.rotation = Mathf.Lerp(rgdb.rotation, -80, 0.01f);
            rgdb.angularVelocity = 0;
        }

        if(rgdb.rotation > controller.AngleTurnOffJet && !controller.OnEndLevelZone) {
            rgdb.velocity += naveFowardDirection * 0.015f;
        }
            

    }
    
}
