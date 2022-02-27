
using System.ComponentModel.DataAnnotations;

namespace Common.DAL.Models;

/// <summary> Модель ресторана. </summary>
public class RestaurantModel
{
    /// <summary> Id в БД. </summary>
    [Key]
    public int Id { get; set; }
    /// <summary> Наименование ресторана. </summary>
    [Required]
    public string Name { get; set; }
    /// <summary> Список столов. </summary>
    [Required]
    public List<TableModel> Tables { get; private set; }

    /// <summary> Конструктор класса. </summary>
    public RestaurantModel()
    {
        for (ushort i = 1; i <= 10; i++)
            Tables.Add(new TableModel(i));
    }
}
