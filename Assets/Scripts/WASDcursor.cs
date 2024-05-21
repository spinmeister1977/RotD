using UnityEngine;
using UnityEngine.UI;

public class CursorWASDController : MonoBehaviour
{
    public RectTransform cursorTransform;
    public float speed = 100f;

    private void Update()
    {
        float x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float y = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        cursorTransform.anchoredPosition += new Vector2(x, y);

        if (Input.GetKeyDown(KeyCode.J))
        {
            ClickAtPosition();
        }
    }

    private void ClickAtPosition()
    {
        Debug.Log("WASD Cursor clicked at: " + cursorTransform.anchoredPosition);
    }
}
