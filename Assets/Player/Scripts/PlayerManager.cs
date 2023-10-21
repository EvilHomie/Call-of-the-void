using UnityEngine;

public class PlayerManager : MonoBehaviour
{   
    private void Start()
    {        
        EventBus.ComandOnSetPlayerParameters.Execute(gameObject);
    }

    private void OnDestroy()
    {
        EventBus.ComandOnPlayerDie.Execute();
    }

}
