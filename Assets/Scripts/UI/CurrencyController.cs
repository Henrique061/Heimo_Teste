using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(EssentialUIController))]
public class CurrencyController : MonoBehaviour
{
    #region INSPECTOR VARS
    [Header("IMAGES")]
    [Tooltip("The image of ticket icon")]
    [SerializeField] private Texture2D ticketTexture;
    [Tooltip("The image of gold icon")]
    [SerializeField] private Texture2D goldTexture;

    [Space(10)][Header("INITIAL QUANTITY")]
    [Tooltip("The quantity of tickets that player will initialize")]
    [SerializeField][Range(0, 9999)] private int initialTickets;
    [Tooltip("The quantity of gold that player will initialize")]
    [SerializeField][Range(0, 9999)] private int initialGold;
    #endregion

    #region VARS
    // element names
    private readonly string goldContainerName = "GoldContainer";
    private readonly string ticketContainerName = "TicketContainer";
    private readonly string currencyQuantityName = "txt_quantity";
    private readonly string currencyImageName = "img_currencyIcon";

    // references
    private EssentialUIController essentialUIController;
    private VisualElement rootElement;
    #endregion

    #region UNITY METHODS
    private void Start()
    {
        essentialUIController = GetComponent<EssentialUIController>();
        rootElement = essentialUIController.RootDocument.rootVisualElement;

        InitializeCurrency();
    }
    #endregion

    #region METHODS
    /// <summary>Initializes all currency parameters of visual elements</summary>
    private void InitializeCurrency()
    {
        // containers
        var goldContainer = VisualElementHelper.GetVisualElement<VisualElement>(rootElement, goldContainerName);
        var ticketContainer = VisualElementHelper.GetVisualElement<VisualElement>(rootElement, ticketContainerName);

        // labels
        var goldLabel = VisualElementHelper.GetVisualElement<Label>(goldContainer, currencyQuantityName);
        var ticketLabel = VisualElementHelper.GetVisualElement<Label>(ticketContainer, currencyQuantityName);

        // visuals
        var goldImage = VisualElementHelper.GetVisualElement<VisualElement>(goldContainer, currencyImageName);
        var ticketImage = VisualElementHelper.GetVisualElement<VisualElement>(ticketContainer, currencyImageName);

        // set texts
        goldLabel.text = initialGold.ToString();
        ticketLabel.text = initialTickets.ToString();

        // set textures
        goldImage.style.backgroundImage = goldTexture;
        ticketImage.style.backgroundImage = ticketTexture;
    }
    #endregion
}
