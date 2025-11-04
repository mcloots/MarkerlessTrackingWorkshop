using UnityEngine;

public class ButtonScaleAndRotateController : MonoBehaviour
{
    [Header("Scaling")]
    [SerializeField]
    private float scaleStep = 0.1f;

    [SerializeField]
    private float minimumScale = 0.2f;

    [SerializeField]
    private float maximumScale = 5f;

    [Header("Spin (table-style)")]
    [SerializeField]
    private float rotationStep = 15f; // graden per klik

    [SerializeField]
    private bool useBoundsCenter = true; // pivot uit renderer-bounds

    [SerializeField]
    private bool lockPivotHeight = true; // pivot-hoogte = huidige Y

    private Vector3 _initialScale;
    private float _currentScaleMultiplier = 1f;
    private Renderer[] _renderers;

    private void Awake()
    {
        _initialScale = transform.localScale;
        _renderers = GetComponentsInChildren<Renderer>(includeInactive: true);
    }

    // --- SCALE ---
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

    // --- SPIN rond wereld-Y, op de plek ---
    public void RotateLeft() => Spin(-rotationStep);

    public void RotateRight() => Spin(+rotationStep);

    private void Spin(float deltaDeg)
    {
        Vector3 pivot = GetSpinPivot();
        // Wereld-verticale as (als een tafel op de vloer)
        Vector3 axis = Vector3.up;
        transform.RotateAround(pivot, axis, deltaDeg);
    }

    private Vector3 GetSpinPivot()
    {
        // Als je pivot van het object al netjes in het midden zit, kun je gewoon return transform.position;
        if (!useBoundsCenter || _renderers == null || _renderers.Length == 0)
            return transform.position;

        Bounds b = new Bounds(_renderers[0].bounds.center, Vector3.zero);
        for (int i = 1; i < _renderers.Length; i++)
            b.Encapsulate(_renderers[i].bounds);

        // Draai rond het horizontale midden; hoogte vastzetten voorkomt “op/af springen”
        if (lockPivotHeight)
            b.center = new Vector3(b.center.x, transform.position.y, b.center.z);
        return b.center;
    }
}
