using UnityEngine;
using UnityEngine.UIElements;
using System;

public enum ItemShopRarity
{
    Common,
    Rare,
    Epic,
    Legendary
}

[System.Serializable]
public struct ItemShop
{
    [Tooltip("The rarity of the item")]
    public ItemShopRarity ItemRarity;
    [Tooltip("The color that the background of item image will be")]
    public Color BackgroundColor;
    [Tooltip("The color that the background of info button will be")]
    public Color InfoColor;
    [Tooltip("The image of the item itself that will be displayed")]
    public Texture2D CarImage;
    [Tooltip("The duration of the offer in DateTime. x = days | y = hours | z = minutes")]
    public Vector3 OfferDuration;
    [Tooltip("The price of the item")]
    public float Price;

    // the current time of the offer of the time
    [HideInInspector] public DateTime CurrentOfferTime;
}

[RequireComponent(typeof(EssentialUIController))]
public class ItemShopController : MonoBehaviour
{
    #region INSPECTOR VARS
    [Header("SHOP ITEMS")]
    [Tooltip("All the items that will be displayed on the shop menu")]
    [SerializeField] private ItemShop[] shopItems;

    [Space(10)][Header("RARITY")]
    [Tooltip("Tells the width of the item rarity container for common items")]
    [SerializeField][Range(100, 210)] private float commonWidth = 160f;
    [Tooltip("Tells the width of the item rarity container for common items")]
    [SerializeField][Range(100, 210)] private float rareWidth = 130f;
    [Tooltip("Tells the width of the item rarity container for common items")]
    [SerializeField][Range(100, 210)] private float epicWidth = 132f;
    [Tooltip("Tells the width of the item rarity container for common items")]
    [SerializeField][Range(100, 210)] private float legendaryWidth = 210f;
    [Tooltip("The position on left of the rarity text if there's no icon in it")]
    [SerializeField] private float noIconLeftPosition = 0f;

    [Space(10)][Header("BUTTONS")]
    [Tooltip("The audio source that will be reproduced when clicking a button")]
    [SerializeField] private AudioSource clickSound;
    #endregion

    #region VARS
    // element names
    private readonly string containerName = "Container";
    private readonly string itemPrefix = "ShopItem_";
    private readonly string imageItemName = "img_item";
    private readonly string rarityContainerName = "RarityContainer";
    private readonly string rarityIconName = "img_RarityIcon";
    private readonly string rarityLabelName = "txt_RarityLabel";
    private readonly string infoContainerName = "InfoContainer";
    private readonly string offerTimeName = "txt_timer";
    private readonly string itemPriceName = "txt_price";
    private readonly string buyButtonName = "BuyButtonContainer";

    // root element
    private VisualElement rootElement;

    // scripts
    private EssentialUIController essentialUIController;
    #endregion

    #region UNITY METHODS
    private void Start()
    {
        essentialUIController = GetComponent<EssentialUIController>();
        rootElement = essentialUIController.RootDocument.rootVisualElement;
        SetAllItems();
    }

    private void Update()
    {
        UpdateOfferTime();
    }
    #endregion

    #region METHODS
    /// <summary>Get all items in the array of ItemShop and set all the UI parameters</summary>
    private void SetAllItems()
    {
        // prevent null condition
        if (shopItems.Length <= 0)
        {
            Debug.LogWarning("No items assigned in ItemShopController.");
            return;
        }

        for (int i = 0; i < shopItems.Length; i++)
        {
            var item = GetShopItemElement(i);
            if (item == null) continue;

            // initialize offer time
            var offerTime = DateTime.Now.Add(new TimeSpan((int)shopItems[i].OfferDuration.x, // days
                                                          (int)shopItems[i].OfferDuration.y, // hours
                                                          (int)shopItems[i].OfferDuration.z, // minutes
                                                          0));
            shopItems[i].CurrentOfferTime = offerTime;

            var rarity = shopItems[i].ItemRarity;

            // getting elements
            var background      = VisualElementHelper.GetVisualElement<VisualElement>(item, containerName);
            var image           = VisualElementHelper.GetVisualElement<VisualElement>(item, imageItemName);
            var rarityContainer = VisualElementHelper.GetVisualElement<VisualElement>(item, rarityContainerName);
            var rarityIcon      = VisualElementHelper.GetVisualElement<VisualElement>(item, rarityIconName);
            var infoContainer   = VisualElementHelper.GetVisualElement<VisualElement>(item, infoContainerName);
            var buyButton       = VisualElementHelper.GetVisualElement<VisualElement>(item, buyButtonName);
            var rarityLabel     = VisualElementHelper.GetVisualElement<Label>(item, rarityLabelName);
            var priceLabel      = VisualElementHelper.GetVisualElement<Label>(item, itemPriceName);

            // setting elements parameters
            background.style.backgroundColor = shopItems[i].BackgroundColor;
            image.style.backgroundImage = shopItems[i].CarImage;
            rarityContainer.style.width = GetRarityWidth(rarity);
            rarityIcon.style.unityBackgroundImageTintColor = GetRarityIconColor(rarity);
            infoContainer.style.backgroundColor = shopItems[i].InfoColor;
            rarityLabel.text = GetRarityName(rarity);
            priceLabel.text = shopItems[i].Price.ToString();

            // reposition rarity label if there's no icon for it
            if (rarity == ItemShopRarity.Common || rarity == ItemShopRarity.Rare)
                rarityLabel.style.left = noIconLeftPosition;

            // assign button click event
            buyButton.RegisterCallback<ClickEvent>(OnClickButton);
        }
    }

    #region RARITY METHODS
    /// <summary>Returns the width value for the container of the passed item rarity</summary>
    /// <param name="rarity"></param>
    /// <returns></returns>
    private float GetRarityWidth(ItemShopRarity rarity)
    {
        float value = rarity switch
        {
            ItemShopRarity.Common => commonWidth,
            ItemShopRarity.Rare => rareWidth,
            ItemShopRarity.Epic => epicWidth,
            ItemShopRarity.Legendary => legendaryWidth,
            _ => 0f
        };

        return value;
    }

    /// <summary>Returns the color for the item icon of the passed item rarity</summary>
    /// <param name="rarity"></param>
    /// <returns></returns>
    private Color GetRarityIconColor(ItemShopRarity rarity)
    {
        var epicHex = "#FFA3FE";
        var legendaryHex = "#EBA216";

        string colorHex = rarity switch
        {
            ItemShopRarity.Epic => epicHex,
            ItemShopRarity.Legendary => legendaryHex,
            _ => ""
        };

        Color finalColor;

        if (ColorUtility.TryParseHtmlString(colorHex, out finalColor))
            return finalColor;

        // return the icon with 0 alpha if there's no color for the icon
        finalColor = Color.white;
        finalColor.a = 0f;
        return finalColor;
    }

    /// <summary>Returns the string of the passed rarity</summary>
    /// <param name="rarity"></param>
    /// <returns></returns>
    private string GetRarityName(ItemShopRarity rarity)
    {
        string name = rarity switch
        {
            ItemShopRarity.Common => "COMMON",
            ItemShopRarity.Rare => "RARE",
            ItemShopRarity.Epic => "EPIC",
            ItemShopRarity.Legendary => "LEGENDARY",
            _ => "NO RARITY"
        };

        return name;
    }
    #endregion

    #region OFFER TIME METHODS
    /// <summary>Update the time offer of all items in the shop</summary>
    private void UpdateOfferTime()
    {
        for (int i = 0; i < shopItems.Length; i++)
        {
            if (shopItems[i].CurrentOfferTime == null) continue;

            var item = GetShopItemElement(i);
            if (item == null) return;

            var timeLeft = shopItems[i].CurrentOfferTime - DateTime.Now;

            var formattedTime = $"{timeLeft.Days}d {timeLeft.Hours}h {timeLeft.Minutes}m";

            // get the time label
            var timeLabel = VisualElementHelper.GetVisualElement<Label>(item, offerTimeName);

            // set time text
            timeLabel.text = formattedTime;
        }
    }
    #endregion

    #region EVENTS
    private void OnClickButton(ClickEvent evt) => clickSound.Play();
    #endregion

    #region AUX METHODS
    /// <summary>Get the item container for the specific item index</summary>
    /// <param name="index"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    private VisualElement GetShopItemElement(int index)
    {
        var item = VisualElementHelper.GetVisualElement<VisualElement>(rootElement, $"{itemPrefix}{index}");

        // throw error if cannot find item shop
        if (item == null)
            throw new Exception($"Cannot find shop item with the index {index}");

        return item;
    }
    #endregion
    #endregion
}
