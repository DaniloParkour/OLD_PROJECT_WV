using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour {

    public Image redPanel;
    
    private GameObject initialPanel;
    private GameObject endLevelPanel;

    private Vector3 playerPos;

    [SerializeField]
    private float shakeDuration = 0.6f;
    [SerializeField]
    private float shakeForce = 1;


    // Start is called before the first frame update
    void Start() {
        playerPos = transform.position - PlayerController.instance.transform.position;
        initialPanel = redPanel.gameObject.transform.parent.Find("initialPanel").gameObject;
        endLevelPanel = redPanel.gameObject.transform.parent.Find("endLevelPanel").gameObject;
    }

    // Update is called once per frame
    void Update() {
        transform.position = PlayerController.instance.transform.position + playerPos;
    }

    void OnEnable() {
        EventsManager.OnPlayerExplodes += cameraShake;
        EventsManager.OnPlayerExplodes += flashRedPanel;
        EventsManager.OnWinLevel += openEndLevelPanel;
    }


    void OnDisable() {
        EventsManager.OnPlayerExplodes -= cameraShake;
        EventsManager.OnPlayerExplodes -= flashRedPanel;
        EventsManager.OnWinLevel -= openEndLevelPanel;
    }

    private void cameraShake() {
        StartCoroutine("shakeNow");
    }

    private void flashRedPanel() {
        redPanel.gameObject.SetActive(true);
        Invoke("disableRedPanel", 0.2f);
    }

    private void disableRedPanel() {
        redPanel.gameObject.SetActive(false);
    }

    private void openEndLevelPanel() {
        endLevelPanel.SetActive(true);
    }

    IEnumerator shakeNow() {
        float timeLeft = shakeDuration;
        Vector3 originalPos = transform.position;
        float randX, randY;

        while (timeLeft > 0) {
            randX = Random.Range(-shakeForce, shakeForce);
            randY = Random.Range(-shakeForce, shakeForce);
            transform.position = new Vector3(originalPos.x + randX, originalPos.y + randY, originalPos.z);
            timeLeft -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        transform.position = originalPos;
    }

}
