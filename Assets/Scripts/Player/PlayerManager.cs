using UnityEngine;

public class PlayerManager : MonoBehaviour
{   
    private void Start()
    {        
        EventBus.onSetPlayerParameters?.Invoke(gameObject);
    }

    private void OnDestroy()
    {
        EventBus.onPlayerDie?.Invoke();
    }

}
