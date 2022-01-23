using UnityEngine;

public enum SwipeDirection { Right, Left, Up, Down, None }

public class GameInput : MonoBehaviour
{
    [SerializeField] private bool isSwiping;
    [SerializeField] private float minSwipeDistance;
    [SerializeField] private SwipeDirection swipeDirection = SwipeDirection.None;    

    private Touch initialTouch;

    public bool IsSwiping => isSwiping;
    public SwipeDirection SwipeDirection => swipeDirection;
    public Vector2 Direction => GetDirection();    

    private void Start()
    {
        Input.multiTouchEnabled = true;
    }

    private void Update()
    {
        #if UNITY_ANDROID
        UpdateTouchInput();
        #endif

        #if UNITY_EDITOR
        UpdateMouseInput();
        #endif
    }

    private void UpdateTouchInput()
    {
        if (Input.touchCount <= 0)
            return;

        foreach (var touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                initialTouch = touch;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                var deltaX = touch.position.x - initialTouch.position.x; //greater than 0 is right and less than zero is left
                var deltaY = touch.position.y - initialTouch.position.y; //greater than 0 is up and less than zero is down
                var swipeDistance = Mathf.Abs(deltaX) + Mathf.Abs(deltaY);

                if (swipeDistance > minSwipeDistance && (Mathf.Abs(deltaX) > 0 || Mathf.Abs(deltaY) > 0))
                {
                    isSwiping = true;
                    CalculateSwipeDirection(deltaX, deltaY);
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                initialTouch = new Touch();
                isSwiping = false;
                swipeDirection = SwipeDirection.None;
            }
            else if (touch.phase == TouchPhase.Canceled)
            {
                initialTouch = new Touch();
                isSwiping = false;
                swipeDirection = SwipeDirection.None;
            }
        }
    }

    private void UpdateMouseInput()
    {
        if (Input.GetMouseButton(0))
        {
            isSwiping = true;
            Vector3 mouseWordPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var deltaX = mouseWordPosition.x - transform.position.x; 
            var deltaY = mouseWordPosition.y - transform.position.y;            
            CalculateSwipeDirection(deltaX, deltaY);
        }
        else
        {
            isSwiping = false;
        }
    }

    private void CalculateSwipeDirection(float deltaX, float deltaY)
    {
        bool isHorizontalSwipe = Mathf.Abs(deltaX) > Mathf.Abs(deltaY);

        // horizontal swipe
        if (isHorizontalSwipe)
        {
            //right
            if (deltaX > 0)
                swipeDirection = SwipeDirection.Right;
            //left
            else if (deltaX < 0)
                swipeDirection = SwipeDirection.Left;
        }
        //vertical swipe
        else if (!isHorizontalSwipe)
        {
            //up
            if (deltaY > 0)
                swipeDirection = SwipeDirection.Up;
            //down
            else if (deltaY < 0)
                swipeDirection = SwipeDirection.Down;
        }
        //diagonal swipe
        else
        {
            isSwiping = false;
        }
    }

    private Vector2 GetDirection()
    {
        Vector2 direction;
        switch (swipeDirection)
        {
            case SwipeDirection.Right:
                direction = Vector2.right;
                break;
            case SwipeDirection.Left:
                direction = Vector2.left;
                break;
            case SwipeDirection.Up:
                direction = Vector2.up;
                break;
            case SwipeDirection.Down:
                direction = Vector2.down;
                break;
            default:
                direction = Vector2.zero;
                break;
        }
        return direction;
    }
}
