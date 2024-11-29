using UnityEngine;

public enum CurrencyType
{
    Ticket,
    Gold
}

public class PlayerCurrencyController : MonoBehaviour
{
    #region INSPECTOR VARS
    [Tooltip("The amount of tickets that will initialize the game")]
    [SerializeField][Range(0, 9999)] private int initialTickets = 20;
    [Tooltip("The amount of gold that will initialize the game")]
    [SerializeField][Range(0, 9999)] private int initialGold = 5000;
    #endregion

    #region GETTERS
    public int Tickets { get; private set; }
    public int Gold { get; private set; }
    #endregion

    #region UNITY METHODS
    private void Awake()
    {
        Tickets = initialTickets;
        Gold = initialGold;
    }
    #endregion

    #region PUBLIC METHODS
    /// <summary>Increment player currency type by the value amount</summary>
    /// <param name="type"></param>
    /// <param name="value"></param>
    public void IncrementCurrency(CurrencyType type, int value = 1)
    {
        if (type == CurrencyType.Gold)
        {
            if (Gold >= 9999) return;
            Gold += value;
        }
        else
        {
            if (Tickets >= 9999) return;
            Tickets += value;
        }
    }
    #endregion
}
