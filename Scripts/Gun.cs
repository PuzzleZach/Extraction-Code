using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int rounds = 30;
    public float cooldown = 0.01f; // Our firing rate
    public float timeSinceFired = 1f;
    public float reloadSpeed = 0.5f;
    public GameObject bullet;
    public GameObject projectileSpawn;
    public TMP_Text bulletText;
    public ParticleSystem bulletEject;
    public AudioClip gunReloadStart;
    public AudioClip gunReloadEnd;
    public List<AudioClip> gunFire;
    public AudioSource audioSource;

    private bool isReloading = false;
    // Start is called before the first frame update
    void Start()
    {
        bulletText.text = rounds.ToString();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && rounds > 0 && timeSinceFired > cooldown)
        {
            ShootBullet();
            timeSinceFired = 0f;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!isReloading) StartCoroutine(ReloadGun());
        }
        timeSinceFired += Time.deltaTime;
    }

    void ShootBullet()
    {
        // Expel a round, update UI.
        bulletEject.Play();
        // Get a random gun fire sound
        audioSource.clip = gunFire[Random.Range(0, gunFire.Count)];
        audioSource.volume = 0.5f;
        rounds--;
        bulletText.text = rounds.ToString();
        Instantiate(bullet, projectileSpawn.transform.position, transform.rotation);
        audioSource.Play();
    }

    IEnumerator ReloadGun()
    {
        audioSource.volume = 1.0f;
        audioSource.clip = gunReloadStart;
        audioSource.Play();
        isReloading = true;
        yield return new WaitForSeconds(reloadSpeed);
        while (audioSource.isPlaying)
        {
            // Wait for reload sfx to finish.
            yield return new WaitForSeconds(0.1f);
        }
        audioSource.clip = gunReloadEnd;
        audioSource.Play();
        rounds = 30;
        bulletText.text = rounds.ToString();
        isReloading = false;
    }
    
}
