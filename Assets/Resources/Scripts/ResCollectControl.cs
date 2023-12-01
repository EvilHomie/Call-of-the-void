using UniRx;
using UnityEngine;

public class ResCollectControl : MonoBehaviour
{
    CompositeDisposable _disposable = new();
    CompositeDisposable _disposable2 = new();
    Rigidbody rb;

    LineRenderer lineRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void OnEnable()
    {
        EventBus.TractorBeamActiveStatus
            .Where(status => status == true)
            .Subscribe(_ => GrabLogic()).AddTo(_disposable);
    }

    private void OnDisable()
    {
        _disposable.Clear();
        _disposable2.Clear();
        lineRenderer.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.root.CompareTag("Player"))
        {
            EventBus.CommandOnCollectResource.Execute(gameObject);
        }
    }

    void GrabLogic()
    {
        if (InTractorBeamRange())
        {
            TranslateToPlayer();
            lineRenderer.enabled = true;
        }
    }
    bool InTractorBeamRange()
    {
        float distance = Vector3.Distance(GlobalData.PlayerTransform.position, transform.position);
        if (distance <= PlayerTractorBeam.currentTractorBeam.Value.CurrentCollectDistance) return true;
        else return false;
    }


    void TranslateToPlayer()
    {
        Observable.EveryFixedUpdate()
                .Subscribe(_ =>
                {
                    MoveToPlayer();
                    if (!EventBus.TractorBeamActiveStatus.Value || !InTractorBeamRange())
                    {                        
                        lineRenderer.enabled = false;
                        _disposable2.Clear();
                    }
                }).AddTo(_disposable2);
    }

    void MoveToPlayer()
    {
        Vector3 dir = GlobalData.PlayerTransform.position - transform.position;
        dir = dir.normalized;
        rb.AddForce(dir * PlayerTractorBeam.currentTractorBeam.Value.CurrentPullSpeed, ForceMode.Acceleration);

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, GlobalData.PlayerTransform.position);
    }
}
