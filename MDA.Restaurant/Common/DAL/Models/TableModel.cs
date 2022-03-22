
using Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.DAL.Models;

/// <summary> Модель опимсывающая стол ресторана. </summary>
public class TableModel
{
    #region properies

    /// <summary> Id стола. </summary>
    [Key]
    public int Id { get; set; }

    /// <summary> Состояние стола. </summary>
    [Required]
    public State State { get; set; }

    /// <summary> Кол-во мест. </summary>
    [Required]
    public int SeatsCount { get; set; }

    /// <summary> Навигационный ключ. </summary>
    [ForeignKey("RestaurantModel")]
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
