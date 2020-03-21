using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace RecipesBook.Core.Models
{
    [Table("Recipe")]
    public class Recipe
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public Guid Id { get; set; }

        [MaxLength(250), Unique]
        public string Title { get; set; }
        public string RecipeImage { get; set; }
        public int CookingTime { get; set; }
        public Common.Enums.Category Category { get; set; }
        public string CookingSteps { get; set; }
        [OneToMany]
        public IEnumerable<Ingredient> Ingredients { get; set; }
    }
}
