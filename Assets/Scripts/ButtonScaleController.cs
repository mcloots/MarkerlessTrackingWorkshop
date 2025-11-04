using UnityEngine;

public class ButtonScaleController : MonoBehaviour
{
    [Header("Scaling")]
    [SerializeField]
    private float scaleStep = 0.1f;

    [SerializeField]
    private float minimumScale = 0.2f;

    [SerializeField]
    private float maximumScale = 5f;

    private Vector3 _initialScale;
    private float _currentScaleMultiplier = 1f;

    private void Awake()
    {
        _initialScale = transform.localScale;
    }

    public void ScaleUp()
    {
        _currentScaleMultiplier = Mathf.Clamp(
            _currentScaleMultiplier + scaleStep,
            minimumScale,
            maximumScale
        );
        transform.localScale = _initialScale * _currentScaleMultiplier;
    }

    public void ScaleDown()
    {
        _currentScaleMultiplier = Mathf.Clamp(
            _currentScaleMultiplier - scaleStep,
            minimumScale,
            maximumScale
        );
        transform.localScale = _initialScale * _currentScaleMultiplier;
    }
}
