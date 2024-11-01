using UnityEngine;

public class BootStrap : MonoBehaviour
{
    [SerializeField] private LayerMask _groundlayerMask;
    [SerializeField] private Transform _targetAgentPositionPrefab;
    [SerializeField] private Character _character;

    private InputHandler _inputHandler;
    private ScreenClickHandler _screenClickHandler;    

    private void Awake()
    {
        _inputHandler = new InputHandler();
        _screenClickHandler = new ScreenClickHandler(_targetAgentPositionPrefab, _inputHandler, _groundlayerMask);
        _character.Initialize(_inputHandler,_screenClickHandler);
    }
}
