using UnityEngine;
using UnityEngine.UI;

public class ManagerGame : MonoBehaviour
{
    private Player _player;
    private GameObject[] loc;
    public GameObject restartPanel;
    public bool start = false;
    public Text _scoreText,finishScore;
    public GameObject Coin;

    private void Awake()
    {
        Instantiate(Resources.Load("Prefabs/Container"));
        loc = GameObject.FindGameObjectsWithTag("Floor");
    }

    private void Start()
    {
        SpawnAI();
        SetPlayer();
        DontDestroyOnLoad(this.gameObject);
        SpawnCoins();
    }

    private void Update()
    {
        if (!GameObject.Find("Player"))
        {
            restartPanel.SetActive(true);
        }
        if (Coin == null)
            SpawnCoins();
    }

    public GameObject SpawnCoins()
    {
        GameObject spawnPoint = loc[Random.Range(0, loc.Length)];
        GameObject coin = Instantiate(Resources.Load<GameObject>("Prefabs/Coin"));
        coin.transform.position = new Vector3(spawnPoint.transform.position.x, -0.51f, spawnPoint.transform.position.z);
        coin.name = "Coin";
        Coin = coin;
        return Coin;
    }

    public void NewPosCoin(GameObject coin)
    {
        GameObject spawnPoint = loc[Random.Range(0, loc.Length)];
        coin.transform.position = new Vector3(spawnPoint.transform.position.x, -0.51f, spawnPoint.transform.position.z);
    }

    public void OutOfGame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        GameObject player = Instantiate(Resources.Load<GameObject>("Prefabs/Player"));
        player.name = "Player";
        _player.scores = 0;
        restartPanel.SetActive(false);
        start = true;
        Cursor.visible = false;
    }

    public void ButtonStart()
    {
        start = true;
        Destroy(GameObject.Find("Panel"));
        Cursor.visible = false;
    }

    private void SetPlayer()
    {
        GameObject player = Instantiate(Resources.Load<GameObject>("Prefabs/Player"));
        player.name = "Player";
        _player = GameObject.Find("Player").GetComponent<Player>();
        _player.VFX = true;
    }

    private void SpawnAI()
    {
        Instantiate(Resources.Load("Prefabs/AI"));
    }
}

