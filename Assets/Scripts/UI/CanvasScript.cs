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
            nextCanvas.enabled = true; 
            GetComponent<Canvas>().enabled = false;

        }
    }

    public virtual void ShowPreviousCanvas()
    {
        if (previousCanvas != null)
        {
            previousCanvas.enabled = true;
            GetComponent<Canvas>().enabled = false;
        }
    }
}
