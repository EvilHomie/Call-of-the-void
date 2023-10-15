using UnityEngine;

public class TractorBeamManager : MonoBehaviour
{
    AudioSource aS;
    [SerializeField] AudioClip resPickUpSound;

    private void Start()
    {
        aS = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        EventBus.onCollectResource += PlayPickUpSound;
    }

    private void OnDisable()
    {
        EventBus.onCollectResource -= PlayPickUpSound;
    }

    private void PlayPickUpSound(GameObject @object)
    {
        aS.PlayOneShot(resPickUpSound);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            aS.Play();
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            aS.Stop();
        }
    }
}
