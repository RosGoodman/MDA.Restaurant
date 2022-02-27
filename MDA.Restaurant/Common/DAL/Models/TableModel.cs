
using Common.Enums;

namespace Common.DAL.Models;

/// <summary> Модель опимсывающая стол ресторана. </summary>
public class TableModel
{
    public int Id { get; set; }
    public State State { get; private set; }
    public int SeatsCount { get; }

    public TableModel(int id)
    {
        Id = id;
        State = State.Free;
        SeatsCount = new Random().Next(2, 5);
    }

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
