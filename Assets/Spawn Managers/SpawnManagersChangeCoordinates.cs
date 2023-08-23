using UnityEngine;

public class SpawnManagersChangeCoordinates : MonoBehaviour
{
    [SerializeField]GameObject player;


    //private void OnEnable()
    //{
    //    PlayerControl.playerVelocity += ChangeSpawnCentreWhenPlayerToFast;
    //}

    //private void OnDisable()
    //{
    //    PlayerControl.playerVelocity -= ChangeSpawnCentreWhenPlayerToFast;
    //}

    void Update()
    {
        transform.position = player.transform.position;
    }

    //void ChangeSpawnCentreWhenPlayerToFast(Vector3 playerVelocity)
    //{
    //    transform.position = player.transform.position + playerVelocity*2;
    //}
}
