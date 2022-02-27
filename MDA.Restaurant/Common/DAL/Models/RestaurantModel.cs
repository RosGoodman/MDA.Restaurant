
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    public List<TableModel> Tables { get; set; }
}
