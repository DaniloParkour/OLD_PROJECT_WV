using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public static PlayerController instance;

    private PlayerInputs inputs;
    public PlayerInputs Inputs { get { return inputs; } }
    private PlayerMoviments moviments;
    public PlayerMoviments Moviments { get { return moviments; } }
    private PlayerData data;
    public PlayerData Data { get { return data; } }
    private PlayerAnimations animations;
    public PlayerAnimations Animations { get { return animations; } }

    private bool onEndLevelZone = false;
    public bool OnEndLevelZone { get { return onEndLevelZone; } }
    private bool landed = false;
    public bool Landed {  get { return landed; } }

    [SerializeField]
    private int hp;
    public int Hp { get { return hp; } }
    [SerializeField]
    private float angleTurnOffJet = -30;
    public float AngleTurnOffJet { get { return angleTurnOffJet; } }
    [SerializeField]
    private int quantSupplements = 1;

    private GameObject[] damageds;

    private Rigidbody2D rgdb;

    private RaycastHit2D testRaycast;

    private bool playerWinLevel = false;
    public bool PlayerWinLevel { get { return playerWinLevel; } }

    private LayerMask lmask_landindArea;
    
    //Strings
    private string str_endLevelZone = "EndLevelZone";

    void Awake() {
        instance = this;
        inputs = GetComponent<PlayerInputs>();
        moviments = GetComponent<PlayerMoviments>();
        animations = GetComponent<PlayerAnimations>();
        data = GetComponent<PlayerData>();
        rgdb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start() {
        damageds = new GameObject[transform.Find("damaged").childCount];
        for (int i = 0; i < damageds.Length; i++) {
            damageds[i] = transform.Find("damaged").Find("0" + (i + 1)).gameObject;
            damageds[i].SetActive(false);
        }

        lmask_landindArea = LayerMask.GetMask("LandindArea");

        Debug.Log("Level Type: " + LevelController.instance.type);
    }

    // Update is called once per frame
    void Update() {
        Debug.DrawLine(animations.jetFireLeft.transform.position, animations.jetFireLeft.transform.position + Vector3.down / 10);
        Debug.DrawLine(animations.jetFireRight.transform.position, animations.jetFireRight.transform.position + Vector3.down / 10);

        //Debug.Log(">>> " + onEndLevelZone + " <> " + LevelController.instance.type.Equals(EnumsGame.Level_Type.GET_SUPPLEMENTS) + " <> " + !landed);

        verifyEndLevel();
     
    }

    public int hitPlayer(int hitForce) {
        hp -= hitForce;
        if (hp <= 0) {
            hp = 0;
            animations.destroyPlayer();
            EventsManager.instance.initPlayerExplodes();
            this.gameObject.SetActive(false);
            Invoke("loadMainMenu", 3);
            return 0;
        }
        animations.playerHitted();

        //MUDAR DEPOIS________________________
        if (hp < 90)
            damageds[0].SetActive(true);
        if (hp < 75)
            damageds[1].SetActive(true);
        if (hp < 50)
            damageds[2].SetActive(true);
        if (hp < 30)
            damageds[3].SetActive(true);
        if (hp < 10)
            damageds[4].SetActive(true);
        //_______________________________________

        //EventsManager.instance.initPlayerHitted();

        return hp;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        
        if (collision.collider.gameObject.layer == 9) {
            ContactPoint2D cp;
            for(int i = 0; i < collision.contactCount; i++) {
                cp = collision.GetContact(i);
                rgdb.AddForce(collision.contacts[0].normal * collision.relativeVelocity.magnitude * 200);
                if (cp.otherCollider.gameObject.layer != 0) {
                    Debug.Log("HP = "+hitPlayer((int)(collision.relativeVelocity.magnitude * 25)));
                    break;
                } else {
                    Debug.Log("Menor Hit = " + hitPlayer((int)(collision.relativeVelocity.magnitude * 10)));
                    break;
                }
            }
        }
        
    }

    private void verifyEndLevel() {

        if (onEndLevelZone && !landed) {
            if (Physics2D.Raycast(animations.jetFireRight.transform.position, Vector2.down, 0.1f, lmask_landindArea) &&
                Physics2D.Raycast(animations.jetFireLeft.transform.position, Vector2.down, 0.1f, lmask_landindArea)) {

                if (LevelController.instance.type.Equals(EnumsGame.Level_Type.GET_SUPPLEMENTS) && rgdb.velocity.Equals(Vector2.zero)) {
                    landed = true;
                    EventsManager.instance.initWinLevel();
                    playerWinLevel = true;
                } else if (LevelController.instance.type.Equals(EnumsGame.Level_Type.DELIVER_SUPPLEMENTS) && quantSupplements > 0) {
                    quantSupplements--;
                    Debug.Log("Criar em prefabe se suprimentos e mandar jogar da nave aqui!");
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag.Equals(str_endLevelZone)) {
            onEndLevelZone = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        if (collider.gameObject.tag.Equals(str_endLevelZone)) {
            onEndLevelZone = false;
        }
    }

    void loadMainMenu()
    {
        SceneManager.LoadScene("MainManu");
    }

}
