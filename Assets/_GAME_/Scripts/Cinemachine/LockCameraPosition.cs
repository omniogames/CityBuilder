using UnityEngine;
using Cinemachine;
using Sirenix.OdinInspector;

/// <summary>
/// An add-on module for Cinemachine Virtual Camera that locks the camera's X,Y,Z co-ordinate
/// </summary>
[ExecuteInEditMode] [SaveDuringPlay] [AddComponentMenu("")] // Hide in menu
public class LockCameraPosition : CinemachineExtension
{
    [Tooltip("Lock the camera's X position to this value")] [FoldoutGroup("Lock Position")]
    public float m_XPosition = 0;
    [Tooltip("Lock the camera's Y position to this value")] [FoldoutGroup("Lock Position")]
    public float m_YPosition = 0;
    [Tooltip("Lock the camera's Z position to this value")] [FoldoutGroup("Lock Position")]
    public float m_ZPosition = 0;

    [SerializeField] [FoldoutGroup("Lock Axis's")] private bool lockX;
    [SerializeField] [FoldoutGroup("Lock Axis's")] private bool lockY;
    [SerializeField] [FoldoutGroup("Lock Axis's")] private bool lockZ;
 
    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            if (lockX)
            {
                var pos = state.RawPosition;
                pos.x = m_XPosition;
                state.RawPosition = pos;
            }
            if (lockY)
            {
                var pos = state.RawPosition;
                pos.y = m_YPosition;
                state.RawPosition = pos;
            }
            if (lockZ)
            {
                var pos = state.RawPosition;
                pos.z = m_ZPosition;
                state.RawPosition = pos;
            }
        }
    }
}