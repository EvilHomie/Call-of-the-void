using TMPro;
using UniRx;
using UnityEngine;

public class ErrorManager : MonoBehaviour
{
    CompositeDisposable _disposables = new();
    [SerializeField] GameObject textPrefab;

    void OnEnable()
    {
        EventBus.CommandForShowError.Subscribe(text => CreateErorText(text)).AddTo(_disposables);
    }    

    void OnDisable()
    {
        _disposables.Clear();
    }

    void CreateErorText(string text)
    {
        GameObject errorText = Instantiate(textPrefab, transform);
        errorText.GetComponent<TextMeshProUGUI>().text = text;
        Destroy(errorText, 2);
    }
}
