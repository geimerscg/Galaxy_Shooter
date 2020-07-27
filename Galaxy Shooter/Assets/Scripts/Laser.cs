using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8.5f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y >= 8f)
        {
            //check if this object has a parent
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            //destroy it as well
            Destroy(this.gameObject);
        }
    }
}
