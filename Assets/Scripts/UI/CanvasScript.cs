using UnityEngine;

[RequireComponent(typeof(Canvas))]
public abstract class CanvasScript : MonoBehaviour
{
    public Canvas nextCanvas;
    public Canvas previousCanvas;

    public virtual void ShowNextCanvas()
    {
        if (nextCanvas != null)
        {
            nextCanvas.gameObject.SetActive(true); 
            gameObject.SetActive(false);
        }
    }

    public virtual void ShowPreviousCanvas()
    {
        if (previousCanvas != null)
        {
            previousCanvas.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
