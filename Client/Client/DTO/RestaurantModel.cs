namespace Client.DTO;

/// <summary> Модель ресторана. </summary>
public class RestaurantModel
{
    /// <summary> Id в БД. </summary>
    public int Id { get; set; }

    /// <summary> Наименование ресторана. </summary>
    public string? Name { get; set; }

    /// <summary> Список столов. </summary>
    public List<TableModel>? Tables { get; set; }
}
