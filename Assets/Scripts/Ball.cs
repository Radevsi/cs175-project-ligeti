using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float velocity = 60f;
    private Rigidbody rigidbodyComponent;
    private ParticleSystem fireball;


    // GameObject fire;
    GameObject[] fires;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();

        // Turn off FireBall
        Transform ballTransform = transform.Find("FireBall");
        fireball = ballTransform.gameObject.GetComponent<ParticleSystem>();
        var fireballEmission = fireball.emission;
        fireballEmission.enabled = false; 

        // Turn off particle emission in every Fire GameObject
        fires = GameObject.FindGameObjectsWithTag("Fire");
        if (fires.Length != 0)
        {
            foreach (GameObject fire in fires)
            {
                ParticleSystem fireParticleSystem = fire.GetComponent<ParticleSystem>();
                var fireParticleSystemEmission = fireParticleSystem.emission;
                fireParticleSystemEmission.enabled = false;                
            }
        }        
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Call once every Physic update
    void FixedUpdate()
    {

        // get arrow key functionality 
        float horizontalMvmt = Input.GetAxis("Horizontal");
        float verticalMvmt = Input.GetAxis("Vertical");

        Vector3 mvmt = new Vector3 (horizontalMvmt, 0f, verticalMvmt);

        if (Input.GetKeyDown(KeyCode.B))
        {
            rigidbodyComponent.velocity = new Vector3 (0f, 0f, 0f);
        }
        else
        {
            // add a force
            // rigidbodyComponent.velocity = mvmt;
            rigidbodyComponent.AddForce(mvmt * velocity);

        }
    }

    private float clickDepth = 1f;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == 4) // Water layer
        {
            var fireballEmission = fireball.emission;
            fireballEmission.enabled = false;     
        }

        if (other.gameObject.layer == 7) // FireTrigger layer
        {

            // Enable "clicking" functionality
            other.transform.position += new Vector3 (0f, -clickDepth, 0f);

            // Now that we have the Fire Trigger Game Object, 
            // get an object reference to the parent and traverse
            // down the parent's other branch.
            Transform groundTransform = other.gameObject.transform.parent;
            Transform thisFireTransform = groundTransform.Find("Ground/Grass/Log/Fire");
            GameObject thisFireGameObject = thisFireTransform.gameObject;

            // Enable fire
            ParticleSystem fireParticleSystem = thisFireGameObject.GetComponent<ParticleSystem>();
            var fireParticleSystemEmission = fireParticleSystem.emission;
            fireParticleSystemEmission.enabled = true;  
        }
        else if (other.gameObject.layer == 8) // Log layer
        {
            ParticleSystem fireParticleSystem = other.transform.Find("Fire").GetComponent<ParticleSystem>();
            if (fireParticleSystem.emission.enabled)
            {
                var fireballEmission = fireball.emission;
                fireballEmission.enabled = true;     
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            other.transform.position += new Vector3 (0f, clickDepth, 0f);
        }
    }
}
