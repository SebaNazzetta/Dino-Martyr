using UnityEngine;

public class WorldButtonInput : MonoBehaviour
{
    public static WorldButtonInput Instance;

    [HideInInspector] public int xInput = 0;
    [HideInInspector] public bool jumpPressed = false;
    [HideInInspector] public bool jumpHeld = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        xInput = 0;
        jumpPressed = false;
        jumpHeld = false;

        foreach (Touch touch in Input.touches)
        {
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            Collider2D hit = Physics2D.OverlapPoint(touchPos);

            if (hit != null)
            {
                if (hit.CompareTag("LeftButton"))
                    xInput = -1;
                else if (hit.CompareTag("RightButton"))
                    xInput = 1;
                else if (hit.CompareTag("JumpButton"))
                {
                    jumpHeld = true;

                    if (touch.phase == TouchPhase.Began)
                        jumpPressed = true;
                }
            }
        }
    }
}