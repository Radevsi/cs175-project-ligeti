using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{

    GameObject player;
    Vector3 cameraOffset;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Ball");
        cameraOffset = new Vector3(-10f, 30f, -60f);
    }

    // Update is called once per frame
    void Update()
    {

        // https://stackoverflow.com/questions/31053933/how-to-let-a-camera-follow-a-rolling-ball-with-photon-in-unity
        transform.position = new Vector3(player.transform.position.x + cameraOffset.x, 
                                        player.transform.position.y + cameraOffset.y, 
                                        player.transform.position.z + cameraOffset.z);

    }
}