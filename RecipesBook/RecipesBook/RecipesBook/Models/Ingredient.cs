using RecipesBook.Common.Enums;
using RecipesBook.Common.Extensions;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace RecipesBook.Core.Models
{
    [Table("Ingredient")]
    public class Ingredient
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public Guid Id { get; set; }
        [MaxLength(250)]
        public string IngredientName { get; set; }
        public int Count { get; set; }
        public Unit IngredientUnit { get; set; }
        [Ignore]
        public string UnitName 
        { 
            get 
            {
                return Enum.GetName(typeof(Unit), IngredientUnit).SplitCamelCase(); 
            }
            set 
            {
                IngredientUnit = (Unit) Enum.Parse(typeof(Unit), value);
            } 
        }
        [ForeignKey(typeof(Recipe))]
        public Guid? RecipeId { get; set; }
    }
}
