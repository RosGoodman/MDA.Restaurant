
using System.ComponentModel.DataAnnotations;

namespace Common.DAL.Models;

/// <summary> Модель ресторана. </summary>
public class Restaurant
{
    /// <summary> Id в БД. </summary>
    [Key]
    public int Id { get; set; }
    /// <summary> Список столов. </summary>
    [Required]
    public List<TableModel> Tables { get; private set; }

    /// <summary> Конструктор класса. </summary>
    public Restaurant()
    {
        for (ushort i = 1; i <= 10; i++)
            Tables.Add(new TableModel(i));
    }
}
