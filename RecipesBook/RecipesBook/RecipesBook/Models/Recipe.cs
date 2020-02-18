using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace RecipesBook.Core.Models
{
    [Table("recipe")]
    public class Recipe
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }

        [MaxLength(250), Unique]
        public string Title { get; set; }
        public string Image { get; set; }
        public List<Ingredient> MyProperty { get; set; }
        public string CookingSteps { get; set; }
    }
}
