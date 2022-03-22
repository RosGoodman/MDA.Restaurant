
namespace Client;

/// <summary> Модель опимсывающая стол ресторана. </summary>
public class TableModel
{
    #region properies

    /// <summary> Id стола. </summary>
    public int Id { get; set; }

    /// <summary> Состояние стола. </summary>
    public State State { get; set; }

    /// <summary> Кол-во мест. </summary>
    public int SeatsCount { get; set; }

    /// <summary> Навигационный ключ. </summary>
    public int RestaurantModelId { get; set; }

    #endregion

    /// <summary> Назначить состояние стола. </summary>
    /// <param name="state"> Состояние стола. </param>
    /// <returns> Состояние стола измениелось/не изменилость (true/false). </returns>
    public bool SetState(State state)
    {
        if (state == State) return false;

        State = state;
        return true;
    }
}
