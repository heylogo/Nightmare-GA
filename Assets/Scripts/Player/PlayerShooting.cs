using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public static float range = 10f;


    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    // The proportion of the timeBetweenBullets that the effects will display for.
    float effectsDisplayTime = 0.2f;


    void Awake ()
    {
        // Create a layer mask for the Shootable layer.
        shootableMask = LayerMask.GetMask ("Shootable");

        // Set up the references.
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
    }


    void Update ()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        // if time to shoot
		if(timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            // ... shoot the gun.
            AutoShoot ();
        }

        // If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
        if(timer >= timeBetweenBullets * effectsDisplayTime)
        {
            // ... disable the effects.
            DisableEffects ();
        }
    }

    public void DisableEffects ()
    {
        // Disable the line renderer and the light.
        gunLine.enabled = false;
        gunLight.enabled = false;
    }

    void AutoShoot ()
    {
        // Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        // Perform the raycast against gameobjects on the shootable layer and if it hits something...
        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
        {
            // Try and find an EnemyHealth script on the gameobject hit.
            EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();

            // If the EnemyHealth component exist...
            if(enemyHealth != null)
            {
                // Reset the timer.
                timer = 0f;

                // Play the gun shot audioclip.
                gunAudio.Play ();

                // Enable the light.
                gunLight.enabled = true;

                // Stop the particles from playing if they were, then start the particles.
                gunParticles.Stop ();
                gunParticles.Play ();

                // Enable the line renderer and set it's first position to be the end of the gun.
                gunLine.enabled = true;
                gunLine.SetPosition (0, transform.position);

                // ... the enemy should take damage.
                enemyHealth.TakeDamage (damagePerShot, shootHit.point);

                // Set the second position of the line renderer to the point the raycast hit.
                gunLine.SetPosition (1, shootHit.point);
            }
        }
    }
}