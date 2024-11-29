using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(EssentialUIController))]
public class CurrencyController : MonoBehaviour
{
    #region INSPECTOR VARS
    [Header("PLAYER CONTROLLER")]
    [SerializeField] private PlayerCurrencyController playerController;

    [Space(10)][Header("IMAGES")]
    [Tooltip("The image of ticket icon")]
    [SerializeField] private Texture2D ticketTexture;
    [Tooltip("The image of gold icon")]
    [SerializeField] private Texture2D goldTexture;
    #endregion

    #region VARS
    // element names
    private readonly string goldContainerName = "GoldContainer";
    private readonly string ticketContainerName = "TicketContainer";
    private readonly string currencyQuantityName = "txt_quantity";
    private readonly string currencyImageName = "img_currencyIcon";
    private readonly string currencyPlusButtonName = "btn_plusIcon";

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

        // elements
        var goldImage   = VisualElementHelper.GetVisualElement<VisualElement>(goldContainer, currencyImageName);
        var goldPlus    = VisualElementHelper.GetVisualElement<VisualElement>(goldContainer, currencyPlusButtonName);
        var ticketImage = VisualElementHelper.GetVisualElement<VisualElement>(ticketContainer, currencyImageName);
        var ticketPlus  = VisualElementHelper.GetVisualElement<VisualElement>(ticketContainer, currencyPlusButtonName);

        // set texts
        goldLabel.text = playerController.Gold.ToString();
        ticketLabel.text = playerController.Tickets.ToString();

        // set textures
        goldImage.style.backgroundImage = goldTexture;
        ticketImage.style.backgroundImage = ticketTexture;

        // set click events
        goldPlus.RegisterCallback<ClickEvent>(OnClickButtonSound);
        goldPlus.RegisterCallback<ClickEvent>(evt => OnClickButtonEffect(CurrencyType.Gold, goldLabel));
        ticketPlus.RegisterCallback<ClickEvent>(OnClickButtonSound);
        ticketPlus.RegisterCallback<ClickEvent>(evt => OnClickButtonEffect(CurrencyType.Ticket, ticketLabel));
    }

    #region EVENTS
    private void OnClickButtonSound(ClickEvent evt) => essentialUIController.CoinSound.Play();

    private void OnClickButtonEffect(CurrencyType currencyType, Label element)
    {
        playerController.IncrementCurrency(currencyType);

        var isGold = currencyType == CurrencyType.Gold;
        var value = isGold ? playerController.Gold : playerController.Tickets;

        element.text = value.ToString();
    }
    #endregion
    #endregion
}
