using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaBallLaunch : MonoBehaviour
{
    private float speed = .2f;
    Vector3 randomDirection;
    // Start is called before the first frame update
    void Start()
    {
        randomDirection = new Vector3(Random.Range(0, 2), 0, 1);
    }

    private void Update()
    {
        Launch();
    }

    private void Launch()
    {        
        transform.position += Time.deltaTime * speed * randomDirection;
    }
}
