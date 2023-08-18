using UnityEngine;

public class BGPlayerFollow : MonoBehaviour
{
    [SerializeField]GameObject player;
    
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, 0, player.transform.position.z);
    }
}
