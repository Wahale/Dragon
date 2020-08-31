using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private ManagerGame _managerGame;
    private Rigidbody _rigidbody;
    private float forceUp = 1.1f, forceToSides = 1.2f;
    public float scores,forceUpToSide;
    public bool VFX = true;

    private void Start()
    {
        _managerGame = GameObject.Find("ManagerGame").GetComponent<ManagerGame>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;
    }

    private void Update()
    {
        if (_managerGame.start)
        {
            _rigidbody.isKinematic = false;
            if (Input.GetKeyDown(KeyCode.W))
            {
                _rigidbody.AddForce(Vector3.forward * forceUp, ForceMode.Impulse);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                _rigidbody.AddForce(Vector3.forward * forceUpToSide, ForceMode.Impulse);
                _rigidbody.AddForce(Vector3.right * forceToSides, ForceMode.Impulse);
                transform.localScale = new Vector3(.15f, .15f, .15f);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                _rigidbody.AddForce(Vector3.forward * forceUpToSide, ForceMode.Impulse);
                _rigidbody.AddForce(Vector3.left * forceToSides, ForceMode.Impulse);
                transform.localScale = new Vector3(.15f, .15f, -.15f);
            }
        }
        _managerGame._scoreText.text = "Score :" + scores;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            StartCoroutine(DesMet("VFX/Red"));
            _managerGame.finishScore.text = "Score :" + scores;
            if (!Camera.main.GetComponent<CameraShake>())
            {
                Camera.main.gameObject.AddComponent<CameraShake>();
            }
            else
            {
                Destroy(Camera.main.GetComponent<CameraShake>());
                Camera.main.gameObject.AddComponent<CameraShake>();
            }
            gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
            AudioClip clip =Resources.Load("Sounds/Blow")as AudioClip;
            gameObject.GetComponent<AudioSource>().clip = clip;
            gameObject.GetComponent<AudioSource>().Play();
            Cursor.visible = true;
            VFX = false;
        }

        if (collision.gameObject.CompareTag("Teleport"))
            gameObject.transform.position = new Vector3(6.18f, -0.51f, 5.47f);
        if (collision.gameObject.CompareTag("TeleportTwo"))
            gameObject.transform.position = new Vector3(6.18f, -0.51f, 0.13f);
        if (collision.gameObject.CompareTag("TeleportThree"))
            gameObject.transform.position = new Vector3(2.16f, -0.51f, 0.38f);
        if (collision.gameObject.CompareTag("TeleportFour"))
            gameObject.transform.position = new Vector3(2.16f, -0.51f, 5.47f);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            scores++;
            _rigidbody.mass += .04f;
            gameObject.GetComponent<AudioSource>().Play();
            _managerGame.NewPosCoin(_managerGame.Coin);
            StartCoroutine(DesMet("VFX/Yellow"));
        }
    }


    private IEnumerator DesMet(string path)
    {
        if (VFX)
        {
            GameObject vfx = Instantiate(Resources.Load(path)) as GameObject;
            vfx.transform.position = gameObject.transform.position;
            vfx.name = "VFX";
        }
            yield return new WaitForSeconds(.5f);
            Destroy(GameObject.Find("VFX").gameObject);
            if (path == "VFX/Red")
                Destroy(gameObject);
        }
    
}


