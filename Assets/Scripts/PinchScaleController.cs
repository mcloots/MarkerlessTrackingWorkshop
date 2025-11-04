using UnityEngine;

/// <summary>
/// Allows scaling a model uniformly using a two finger pinch gesture.
/// Attach this behaviour to the root object you would like to scale.
/// </summary>
public class PinchScaleController : MonoBehaviour
{
    [Header("Scaling")]
    [SerializeField, Tooltip("Minimum scale multiplier relative to the initial scale.")]
    private float minimumScaleMultiplier = 0.5f;

    [SerializeField, Tooltip("Maximum scale multiplier relative to the initial scale.")]
    private float maximumScaleMultiplier = 2.5f;

    [SerializeField, Tooltip("Sensitivity applied to the pinch distance delta.")]
    private float pinchSensitivity = 0.005f;

    private Vector3 _initialScale;
    private float _currentScaleMultiplier = 1f;

    private void Awake()
    {
        _initialScale = transform.localScale;
        _currentScaleMultiplier = 1f;
    }

    private void Update()
    {
        if (Input.touchCount < 2)
        {
            return;
        }

        Touch firstTouch = Input.GetTouch(0);
        Touch secondTouch = Input.GetTouch(1);

        Vector2 firstTouchPreviousPosition = firstTouch.position - firstTouch.deltaPosition;
        Vector2 secondTouchPreviousPosition = secondTouch.position - secondTouch.deltaPosition;

        float previousDistance = (firstTouchPreviousPosition - secondTouchPreviousPosition).magnitude;
        float currentDistance = (firstTouch.position - secondTouch.position).magnitude;
        float distanceDelta = currentDistance - previousDistance;

        AdjustScale(distanceDelta * pinchSensitivity);
    }

    private void AdjustScale(float delta)
    {
        if (Mathf.Approximately(delta, 0f))
        {
            return;
        }

        _currentScaleMultiplier = Mathf.Clamp(
            _currentScaleMultiplier + delta,
            minimumScaleMultiplier,
            maximumScaleMultiplier);

        transform.localScale = _initialScale * _currentScaleMultiplier;
    }
}
