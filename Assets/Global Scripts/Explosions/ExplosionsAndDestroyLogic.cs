using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionsAndDestroyLogic : MonoBehaviour
{
    ExplosionsInObject explInObj;
    private IEnumerator coroutine;
    
    private void OnEnable()
    {
        EventBus.onObjDie += LaunchExplosions;
    }

    private void OnDisable()
    {
        EventBus.onObjDie -= LaunchExplosions;
    }

    private void LaunchExplosions(GameObject obj)
    {        
        coroutine = SpawnExplosions(obj);
        StartCoroutine(coroutine);
    }


    IEnumerator SpawnExplosions(GameObject obj)
    {
        explInObj = obj.GetComponent<ExplosionsInObject>();
        List<GameObject> activeBlowEffects = new();

        if (explInObj.explosionsCount == 1)
        {
            InstantiateExplosion(Vector3.zero, obj);
            Destroy(obj);
            yield return new WaitForSeconds(explInObj.explosionsDelay);            
        }
        else if (explInObj.explosionsCount > 1)
        {
            for (int count = 0; count < explInObj.explosionsCount; count++)
            {
                int randomXPos = Random.Range(-10, 10);
                int randomYPos = Random.Range(-10, 10);
                Vector3 randomPos = new(randomXPos, randomYPos, 0);
                InstantiateExplosion(randomPos, obj);
                yield return new WaitForSeconds(explInObj.explosionsDelay);
            }
            Destroy(explInObj.gameObject);
        }
        
        foreach (GameObject blowEfect in activeBlowEffects)
        {
            Destroy(blowEfect);
        }
        activeBlowEffects.Clear();

        void InstantiateExplosion(Vector3 position, GameObject obj)
        {
            GameObject blowEffect = Instantiate(explInObj.explosionParticlePF, obj.transform.position + position, explInObj.explosionParticlePF.transform.rotation);
            blowEffect.transform.localScale = obj.transform.localScale;
            activeBlowEffects.Add(blowEffect);
        }
    }    
}
