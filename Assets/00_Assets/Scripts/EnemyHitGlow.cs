using DG.Tweening;
using UnityEngine;

public class EnemyHitGlow : MonoBehaviour
{
    [SerializeField] private Renderer targetRenderer;
    [SerializeField, Min(0f)] private float glowIntensity = 10f;
    [SerializeField, Min(0.01f)] private float glowDuration = 1f;
    [SerializeField] private Color glowColor = Color.white;

    private Material materialInstance;
    private Tween glowTween;

    private static readonly int EmissionColor =
        Shader.PropertyToID("_EmissionColor");

    private void Awake()
    {
        if (targetRenderer == null)
            targetRenderer = GetComponentInChildren<Renderer>();

        if (targetRenderer == null)
        {
            Debug.LogWarning($"{nameof(EnemyHitGlow)}: No Renderer found.", this);
            return;
        }

        // Creates/gets a material instance for this renderer.
        materialInstance = targetRenderer.material;

        if (!materialInstance.HasProperty(EmissionColor))
        {
            Debug.LogWarning(
                $"{nameof(EnemyHitGlow)}: Shader '{materialInstance.shader.name}' " +
                "does not have an _EmissionColor property.",
                this);
            return;
        }

        materialInstance.EnableKeyword("_EMISSION");
    }

    public void GlowHit()
    {
        if (materialInstance == null ||
            !materialInstance.HasProperty(EmissionColor))
            return;

        glowTween?.Kill();

        Color baseColor = materialInstance.GetColor(EmissionColor);
        Color targetColor = glowColor * glowIntensity;

        glowTween = DOTween.To(
                () => materialInstance.GetColor(EmissionColor),
                value => materialInstance.SetColor(EmissionColor, value),
                targetColor,
                glowDuration / 2f)
            .SetLoops(2, LoopType.Yoyo);
    }

    private void OnDestroy()
    {
        glowTween?.Kill();

        // Clean up the material instance created for this renderer.
        if (materialInstance != null)
            Destroy(materialInstance);
    }
}
