using System.Collections;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    ResourcesInObject resourcesInObject;

    [SerializeField] GameObject explosionParticlePF;
    [SerializeField] int explosionCount;
    bool objNotDestroy = true;

    private void Awake()
    {
        resourcesInObject = GetComponent<ResourcesInObject>();
    }
    public void OnDestroyEvents()
    {
        if (objNotDestroy)
        {
            objNotDestroy = false;
            StartCoroutine(LaunchExplosions());
            if (!gameObject.CompareTag("Player") && resourcesInObject.resourcesInObj.Count != 0)
                EventBus.spawnResources?.Invoke(resourcesInObject.resourcesInObj, transform.position);            
        }        
    }
    IEnumerator LaunchExplosions()
    {
        if (gameObject.CompareTag("Asteroid"))
        {
            Instantiate(explosionParticlePF, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else 
        {            
            if (explosionCount == 1)
            {
                GameObject pS = Instantiate(explosionParticlePF, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(2);
                Destroy(gameObject);
                Destroy(pS,2);
            }

            if (explosionCount > 1)
            {                
                float duration = explosionParticlePF.GetComponent<ParticleSystem>().main.duration;
                for (int i = 0; i < explosionCount; i++)
                {
                    int randomXPos = Random.Range(-10, 10);
                    int randomYPos = Random.Range(-10, 10);
                    Vector3 randomPos = new(randomXPos, randomYPos, 0);
                    GameObject pS = Instantiate(explosionParticlePF, transform.position + randomPos, Quaternion.identity);
                    Destroy(pS, 3);
                    yield return new WaitForSeconds(1);
                }
                yield return new WaitForSeconds(duration);

                Destroy(gameObject);
            }
        }        
    }
}
