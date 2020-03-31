using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    public static LevelController instance;
    public EnumsGame.Level_Type type;

    [SerializeField]
    private LayerMask playerLayer;
    public LayerMask PlayerLayer { get { return playerLayer; } }

    void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        pauseGame(true);
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void pauseGame(bool pause) {
        if (pause)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

}
