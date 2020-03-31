using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    [SerializeField]
    private int bombForce = 30;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.layer == 8) {
            int hpPlayer = PlayerController.instance.hitPlayer(bombForce);

            Debug.Log("O player ficou com " + hpPlayer + " de HP.");

            //CRIAR O POOL DEPOIS
            Destroy(gameObject);
            //______________________

        }
    }
}
