using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    [SerializeField]GameObject player;
    
    void Update()
    {
        transform.position = player.transform.position;
    }
}
