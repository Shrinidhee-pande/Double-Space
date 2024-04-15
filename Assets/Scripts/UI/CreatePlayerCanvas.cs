using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreatePlayerCanvas : CanvasScript
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Image ship;
    [SerializeField] private Button left;
    [SerializeField] private Button right;
    [SerializeField] private Button create;
    [SerializeField] private Sprite[] shipSprites;

    private int currentIndex;

    void Start()
    {
        inputField.onEndEdit.AddListener(PlayerData.Instance.SetPlayerName);
        inputField.text = PlayerData.Instance.GetPlayerName();

        left.onClick.AddListener(ShowPreviousSpaceship);
        right.onClick.AddListener(ShowNextSpaceship);
        create.onClick.AddListener(ShowNextCanvas);

        currentIndex = 0;
        ship.sprite = shipSprites[currentIndex];
    }

    private void ShowNextSpaceship()
    {
        currentIndex++;
        if (currentIndex == shipSprites.Length)
        {
            currentIndex = 0;
        }
        ship.sprite = shipSprites[currentIndex];
    }

    private void ShowPreviousSpaceship()
    {
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = shipSprites.Length - 1;
        }
        ship.sprite = shipSprites[currentIndex];
    }

    public override void ShowNextCanvas()
    {
        PlayerData.Instance.SetShip(ship.sprite);
        base.ShowNextCanvas();
    }
}
