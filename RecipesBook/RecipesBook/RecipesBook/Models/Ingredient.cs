using RecipesBook.Common.Enums;
using SQLite;


namespace RecipesBook.Core.Models
{
    [Table("ingredient")]
    public class Ingredient
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        [MaxLength(250)]
        public string IngredientName { get; set; }
        public int Count { get; set; }
        public Unit IngredientUnit { get; set; }
    }
}
