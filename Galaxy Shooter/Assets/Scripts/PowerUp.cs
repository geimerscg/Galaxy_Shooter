using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField] //0 = triple shot, 1 = speed boost, 2 = shields
    private int _powerUpID;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Random.Range(-9.5f, 9.5f), 6.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        
        if (transform.position.y < -6f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //communicate with player object
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                switch (_powerUpID)
                {
                    case 0:
                        player.TripleShotActivated();
                        break;
                    case 1:
                        player.SpeedBoostActivated();
                        break;
                    case 2:
                        player.ShieldsActivated();
                        break;
                    default:
                        Debug.Log("default");
                        break;
                }
            }
            Destroy(this.gameObject);

        }
    }
}
