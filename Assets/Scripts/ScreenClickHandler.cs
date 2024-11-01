using UnityEngine;

public class ScreenClickHandler 
{
    private Transform _targetAgentPositionPrefab;
    private Vector3 _targetAgentPosition = Vector3.zero;
    private InputHandler _inputHandler;
    private LayerMask _groundlayerMask;
    private float _rayDistanceMax = 500f;

    public ScreenClickHandler(Transform targetAgentPositionPrefab, InputHandler inputHandler, LayerMask groundlayerMask)
    {
        _targetAgentPositionPrefab = targetAgentPositionPrefab;
        _inputHandler = inputHandler;
        _groundlayerMask = groundlayerMask;
    }

    public void Update()
    {
        ProcessScreenClickForGroundWalk();
    }

    private void ProcessScreenClickForGroundWalk()
    {
        if (_inputHandler.IsClickLeftMouseButton)
        {
            Ray ray = Camera.main.ScreenPointToRay(_inputHandler.MousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, _rayDistanceMax, _groundlayerMask))
            {
                _targetAgentPosition = hitInfo.point;
                _targetAgentPositionPrefab.position = hitInfo.point;
            }
        }
    }

    public Vector3 TargetAgentPosition => _targetAgentPosition;
}
