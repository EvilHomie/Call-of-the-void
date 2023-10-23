using UnityEngine;

public class PlayerManager : MonoBehaviour
{   
    private void Start()
    {        
        EventBus.CommandOnSetPlayerParameters.Execute(gameObject);
    }

    private void OnDestroy()
    {
        EventBus.CommandOnPlayerDie.Execute();
    }

}
