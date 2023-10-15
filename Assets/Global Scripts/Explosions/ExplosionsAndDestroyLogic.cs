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
        explInObj = obj.GetComponent<ExplosionsInObject>();
        coroutine = SpawnExplosions(explInObj);
        StartCoroutine(coroutine);

    }


    IEnumerator SpawnExplosions(ExplosionsInObject explosions)
    {
        List<GameObject> blowEffectList = new();
        if (explosions.explosionsCount == 1)
        {
            GameObject blowEffect = Instantiate(explosions.explosionParticlePF, explosions.gameObject.transform.position, Quaternion.identity);
            blowEffectList.Add(blowEffect);
            Destroy(explosions.gameObject);
            yield return new WaitForSeconds(explosions.explosionsDelay);            
        }
        else
        {
            for (int count = 0; count < explosions.explosionsCount; count++)
            {
                int randomXPos = Random.Range(-10, 10);
                int randomYPos = Random.Range(-10, 10);
                Vector3 randomPos = new(randomXPos, randomYPos, 0);
                GameObject blowEffect = Instantiate(explosions.explosionParticlePF, explosions.gameObject.transform.position + randomPos, Quaternion.identity);
                blowEffectList.Add(blowEffect);
                yield return new WaitForSeconds(explosions.explosionsDelay);
            }
            Destroy(explosions.gameObject);
        }
        
        foreach (GameObject obj in blowEffectList)
        {
            Destroy(obj);
        }





        //if (explosions.gameObject.CompareTag("Asteroid"))
        //{
        //    Instantiate(explosionParticlePF, transform.position, Quaternion.identity);
        //    Destroy(gameObject);
        //}
        //else
        //{
        //    if (explosionCount == 1)
        //    {
        //        GameObject pS = Instantiate(explosionParticlePF, transform.position, Quaternion.identity);
        //        yield return new WaitForSeconds(2);
        //        Destroy(gameObject);
        //        Destroy(pS, 2);
        //    }

        //    if (explosionCount > 1)
        //    {
        //        float duration = explosionParticlePF.GetComponent<ParticleSystem>().main.duration;
        //        for (int i = 0; i < explosionCount; i++)
        //        {
        //            int randomXPos = Random.Range(-10, 10);
        //            int randomYPos = Random.Range(-10, 10);
        //            Vector3 randomPos = new(randomXPos, randomYPos, 0);
        //            GameObject pS = Instantiate(explosionParticlePF, transform.position + randomPos, Quaternion.identity);
        //            Destroy(pS, 3);
        //            yield return new WaitForSeconds(1);
        //        }
        //        yield return new WaitForSeconds(duration);

        //        Destroy(gameObject);
        //    }
        //}
    }
}
