using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    Rigidbody asteroidRB;
    IDadamageable parameters;
    ResourcesInObject resInObj;

    [Header("Speed")]
    [SerializeField] float moveSpeed;
    [SerializeField] float minMoveSpeed;
    [SerializeField] float maxMoveSpeed;

    [Header("Rotation")]
    [SerializeField] float tumbleSpeed;
    [SerializeField] float minTumbleSpeed;
    [SerializeField] float maxTumbleSpeed;

    [Header("Size and mass")]
    [SerializeField] float size;
    [SerializeField] float minsize;
    [SerializeField] float maxsize;
    [SerializeField] float massMod;

    [Header("HpMod")]
    [SerializeField] int hpMod;

    [Header("")]
    [SerializeField] float radiusAroundPlayer;    

    void Awake()
    {
        asteroidRB = GetComponent<Rigidbody>();
        parameters = GetComponent<IDadamageable>();
        resInObj = GetComponent<ResourcesInObject>();        
    }
    private void Start()
    {
        SetSize();
        SetMassAndHp();
        SetResCount();
        RandomRotator();
        MoveToGameZone();
    }

    void SetSize()
    {
        size = Random.Range(minsize, maxsize);
        transform.localScale = new Vector3(size, 1, size);
    }

    void SetMassAndHp()
    {
        asteroidRB.mass = (int)(massMod * size * size);
        parameters.SetMaxHpParameters(asteroidRB.mass * hpMod, 0, 0);
    }

    void SetResCount()
    {
        foreach (Resource res in resInObj.resourcesInObj)
        {
            res.amount = (int)size;
        }
    }
    void RandomRotator()
    {
        tumbleSpeed = Random.Range(minTumbleSpeed, maxTumbleSpeed);
        asteroidRB.AddTorque(Vector3.up * tumbleSpeed, ForceMode.VelocityChange);
    }

    void MoveToGameZone()
    {
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
        asteroidRB.AddForce(DirectionCoordonates() * moveSpeed, ForceMode.VelocityChange);
    }

    Vector3 DirectionCoordonates()
    {
        Vector3 randomPointPos = new Vector3(Random.insideUnitCircle.x, 0, Random.insideUnitCircle.y) * radiusAroundPlayer + GlobalData.PlayerTransform.position;
        Vector3 direction = randomPointPos - transform.position;
        return direction.normalized;
    }  
}
