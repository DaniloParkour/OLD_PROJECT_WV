using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour {

    private PlayerController controller;

    private Rigidbody2D rgdb;

    //POR ENQUANTO SEM ANIMAÇÕES - MODIFICAR DEPOIS PARA AS ANIMAÇÕES
    public GameObject jetFireLeft;
    public GameObject jetFireRight;
    public GameObject backJetFire;
    //_________________________________________________________________

    private Animator anim_hitPlayer;
    private string str_hitPlayer = "hitPlayer";

    private Animator anim_playerExplodes;
    private string str_playerExplodes = "playerExplodes";

    // Start is called before the first frame update
    void Start() {

        //POR ENQUANTO SEM ANIMAÇÕES - MODIFICAR DEPOIS PARA AS ANIMAÇÕES
        jetFireLeft = transform.Find("jets").Find("leftJet").Find("jetFire").gameObject;
        jetFireRight = transform.Find("jets").Find("rightJet").Find("jetFire").gameObject;

        backJetFire = transform.Find("jets").Find("backJetFire").gameObject;

        jetFireLeft.SetActive(false);
        jetFireRight.SetActive(false);
        //___________________________________________________________________

        controller = PlayerController.instance;

        rgdb = GetComponent<Rigidbody2D>();

        anim_hitPlayer = GetComponent<Animator>();
        anim_playerExplodes = transform.Find("playerExplosion").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {

        //POR ENQUANTO SEM ANIMAÇÕES - MODIFICAR DEPOIS PARA AS ANIMAÇÕES
        if (controller.Inputs.LeftJet > 0.01f) {
            jetFireLeft.SetActive(true);
        } else {
            jetFireLeft.SetActive(false);
        }
        if (controller.Inputs.RightJet > 0) {
            jetFireRight.SetActive(true);
        } else {
            jetFireRight.SetActive(false);
        }
        //_________________________________________________________________

    }

    void FixedUpdate() {
        if (rgdb.rotation > controller.AngleTurnOffJet && !controller.OnEndLevelZone &&  !backJetFire.activeSelf) {
            backJetFire.SetActive(true);
        } else if ((rgdb.rotation <= controller.AngleTurnOffJet || controller.OnEndLevelZone) && backJetFire.activeSelf) {
            backJetFire.SetActive(false);
        }
    }

    public void playerHitted() {

        anim_hitPlayer.gameObject.SetActive(true);
        anim_hitPlayer.Play(str_hitPlayer);

        //POR ENQUANTO SEM ANIMAÇÕES - MODIFICAR DEPOIS PARA AS ANIMAÇÕES
        //SpriteRenderer sr = GetComponent<SpriteRenderer>();
        //sr.color = new Color(controller.Hp/100f, controller.Hp/100f, controller.Hp/100f);
        //________________________________________________________________

    }

    public void destroyPlayer() {
        anim_playerExplodes.gameObject.SetActive(true);
        anim_playerExplodes.Play(str_playerExplodes);
        anim_playerExplodes.transform.parent = null;
    }

}
