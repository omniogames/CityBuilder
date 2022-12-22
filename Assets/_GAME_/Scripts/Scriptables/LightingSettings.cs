using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "Settings/Lighting Settings", fileName = "LightingSettings")]
public class LightingSettings : ScriptableObject
{

    [FoldoutGroup("Environment")]
    public Material SkyboxMaterial;
    [FoldoutGroup("Environment")]
    public Light SunSource;
    
    [Header("Environment Lighting")]
    [FoldoutGroup("Environment"), LabelText("Source"), Indent(2)]
    public AmbientMode LightingSource;
    [FoldoutGroup("Environment"), ShowIf("LightingSource", AmbientMode.Skybox), 
     Range(0f, 8f), LabelText("Intensity Multiplier"), Indent(2)]
    public float LightingIntensityMultiplier = 1f;
    [FoldoutGroup("Environment"), ShowIf("LightingSource", AmbientMode.Trilight), 
     ColorUsageAttribute(true, true), Indent(2)]
    public Color SkyColor;
    [FoldoutGroup("Environment"), ShowIf("LightingSource", AmbientMode.Trilight),
     ColorUsageAttribute(true, true), Indent(2)]
    public Color EquatorColor;
    [FoldoutGroup("Environment"), ShowIf("LightingSource", AmbientMode.Trilight),
     ColorUsageAttribute(true, true), Indent(2)]
    public Color GroundColor;
    [FoldoutGroup("Environment"), ShowIf("LightingSource", AmbientMode.Flat),
     ColorUsageAttribute(true, true), Indent(2)]
    public Color AmbientColor;
    
    [Header("Environment Reflections")]
    [FoldoutGroup("Environment"), LabelText("Source"), Indent(2)]
    public DefaultReflectionMode ReflectionSource;
    [FoldoutGroup("Environment"), ShowIf("ReflectionSource", DefaultReflectionMode.Skybox),
    ValueDropdown("GetResolutionValues"), Indent(2)]
    public int Resolution = 16;
    [FoldoutGroup("Environment"), ShowIf("ReflectionSource", DefaultReflectionMode.Custom), Indent(2)]
    public Cubemap Cubemap;
    [FoldoutGroup("Environment"), Range(0f, 1f), LabelText("Intensity Multiplier"), Indent(2)]
    public float ReflectionIntensityMultiplier = 1f;
    [FoldoutGroup("Environment"), Range(1, 5), Indent(2)]
    public int Bounces = 1;
    
    [FoldoutGroup("Other Settings")]
    public bool Fog;
    [FoldoutGroup("Other Settings"), ShowIf("Fog"), Indent(2)]
    public Color FogColor;
    [FoldoutGroup("Other Settings"), ShowIf("Fog"), Indent(2)]
    public FogMode Mode = FogMode.Linear;
    [FoldoutGroup("Other Settings"), ShowIf("IsFogLinear"), Indent(2)]
    public float Start;
    [FoldoutGroup("Other Settings"), ShowIf("IsFogLinear"), Indent(2)]
    public float End;
    [FoldoutGroup("Other Settings"), ShowIf("IsFogExponential"), Indent(2)]
    public float Density;

#if UNITY_EDITOR
    private IEnumerable GetResolutionValues()
    {
        return new [] {16, 32, 64, 128, 256, 512, 1024, 2048};
    }

    private bool IsFogExponential => Fog && Mode == FogMode.Exponential || Mode == FogMode.ExponentialSquared;
    private bool IsFogLinear => Fog && Mode == FogMode.Linear;
#endif

}
