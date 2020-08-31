using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    private ManagerGame _managerGame;
    private NavMeshAgent agent;

    private void Start()
    {
        _managerGame = GameObject.Find("ManagerGame").GetComponent<ManagerGame>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        GameObject coin = GameObject.Find("Coin");
        if (_managerGame.start)
        {
            if (coin != null)
            {
                agent.SetDestination(coin.transform.position);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
            _managerGame.NewPosCoin(_managerGame.Coin);


    }
}
