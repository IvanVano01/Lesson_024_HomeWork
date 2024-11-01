using UnityEngine;

public class InputHandler
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";
    private int _leftMouseButton = 0;
    private int _rightMouseButton = 1;

    public bool IsClickLeftMouseButton { get; private set; }
    public bool IsClickRightMouseButton { get; private set; }

    public Vector3 InputKeyBoard { get; private set; }

    public Vector3 MousePosition { get; private set; }

    public void Update()
    {
        InputKeyBoard = new Vector3(-Input.GetAxisRaw(Vertical), 0f, Input.GetAxisRaw(Horizontal));

        IsClickLeftMouseButton = Input.GetMouseButtonDown(_leftMouseButton);
        IsClickRightMouseButton = Input.GetMouseButtonDown(_rightMouseButton);

        MousePosition = Input.mousePosition;        
    }
}
