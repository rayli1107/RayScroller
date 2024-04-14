using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObjectController : MonoBehaviour
{
    public float endY;
    public float speed;
    private Rigidbody2D _body;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= endY)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        _body.velocity = new Vector3(0, 100 * Time.fixedDeltaTime, 0);
    }
}
