using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 6.0f;
    [SerializeField]
    private float _superSpeed = 10.0f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    [SerializeField]
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private bool _tripleShotActive = false;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private bool _speedBoostActive = false;
    [SerializeField]
    private bool _shieldsActive = false;
    [SerializeField]
    private GameObject _shieldVisual;
    [SerializeField]
    private int _playerScore = 0;
    private UIManager _uIManager;

    // Start is called before the first frame update
    void Start()
    {
        // take the current position = (0,0,0)
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("the spawn manager is null");
        }

        if (_uIManager == null)
        {
            Debug.LogError("the ui manager is null");
        }

    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        FireLaser();
    }


    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //controls player input
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        //multiplys player input*speed*real time
        if (_speedBoostActive == true)
        {
            transform.Translate(direction * _superSpeed * Time.deltaTime);
        }
        else if (_speedBoostActive == false)
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }
        //sets the top and bottom boundries
        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }

        //wraps the player on the left and right
        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            _canFire = Time.time + _fireRate;
            if (_tripleShotActive == true)
            {
                //fire triple_shot prefab
                Instantiate(_tripleShotPrefab, transform.position + new Vector3(0, 0.99f, 0), Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.99f, 0), Quaternion.identity);
            }
        }
    }

    public void Damage()
    {
        //if shields is active, do nothing to life, then deactivate shields
        //return
        if (_shieldsActive == true)
        {
            _shieldsActive = false;
            _shieldVisual.SetActive(false);
            return;
        }



        _lives--;

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();            
            Destroy(this.gameObject);
        }
        _uIManager.UpdateLives(_lives);

    }

    public void TripleShotActivated()
    {
        //triple shot active becomes true
        _tripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    //start powerdown coroutine
    // Ienumerator, wait 5 secs, triple shot becomes false

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _tripleShotActive = false;
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _speedBoostActive = false;
    }

    public void SpeedBoostActivated()
    {
        _speedBoostActive = true;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    public void ShieldsActivated()
    {
        _shieldsActive = true;
        _shieldVisual.SetActive(true);
    }

    public void UpdateScore(int points)
    {
        _playerScore += points;
        _uIManager.UpdateScoreText(_playerScore);
    }



}
