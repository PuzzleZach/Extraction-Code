using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Helicopter : MonoBehaviour
{
    public float speed = 10f;

    private MeshRenderer playerGunSkin;
    private SkinnedMeshRenderer playerSkin;
    // We want to drop from EvacTransform to 1.25

    private bool evac = false;

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerBase = GameObject.FindGameObjectWithTag("PlayerSkin");
        playerSkin = playerBase.GetComponent<SkinnedMeshRenderer>();
        GameObject playerGun = GameObject.FindGameObjectWithTag("Gun");
        playerGunSkin = playerGun.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > 1.25 && !evac)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.deltaTime, transform.position.z);
        }
        else if (transform.position.y < 20)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
        }

        if (transform.position.y >= 19)
        {
            GameManager manager = GameObject.Find("GameManager").GetComponent<GameManager>();
            manager.SetWinScene();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player")
        {
            // "Hide" the player.
            playerGunSkin.enabled = false;
            playerSkin.enabled = false;
            evac = true;
        }
    }

}
