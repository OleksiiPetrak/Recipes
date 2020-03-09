using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.Media;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using RecipesBook.Common.Enums;
using RecipesBook.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;
using RecipesBook.Common.Extensions;

namespace RecipesBook.Core.ViewModels
{
    public class IngredientViewModel : BaseViewModel<List<Ingredient>,List<Ingredient>>
    {
        private readonly IMvxNavigationService _navigationService;
        private List<Ingredient> _ingredients;

        public IngredientViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            SaveIngredientButtonText = "Save ingredient";
            SaveIngredientCommand = new MvxAsyncCommand(SaveIngredient);
        }

        public override void Prepare(List<Ingredient> parameter)
        {
            _ingredients = parameter;
        }

        private string _ingredientName;
        private int _ingredientAmount;
        private string _selectedUnit;
        private string _saveIngredientButtonText;

        public List<string> Units
        {
            get
            {
                return Enum.GetNames(typeof(Unit)).Select(c=>c.SplitCamelCase()).ToList();
            }
        }

        public string SelectedUnit
        {
            get
            {
                return _selectedUnit;
            }
            set
            {
                _selectedUnit = value;
                RaisePropertyChanged(() => SelectedUnit);
            }
        }

        public string IngredientName
        {
            get
            {
                return _ingredientName;
            }
            set
            {
                _ingredientName = value;
                RaisePropertyChanged(() => IngredientName);
            }
        }

        public int IngredientAmount
        {
            get
            {
                return _ingredientAmount;
            }
            set
            {
                _ingredientAmount = value;
                RaisePropertyChanged(() => IngredientAmount);
            }
        }

        public string SaveIngredientButtonText
        {
            get
            {
                return _saveIngredientButtonText;
            }
            set
            {
                _saveIngredientButtonText = value;
                RaisePropertyChanged(() => SaveIngredientButtonText);
            }
        }

        public IMvxCommand SaveIngredientCommand { get; private set; }

        private async Task SaveIngredient()
        {
            if (IngredientAmount >= 0 && IngredientName != null && SelectedUnit != null)
            {
                var ingredient = new Ingredient
                {
                    IngredientName = IngredientName,
                    Count = IngredientAmount,
                    IngredientUnit = ConvertUnitInEnum(SelectedUnit)
                };

                _ingredients.Add(ingredient);

                await _navigationService.Close(this, _ingredients).ConfigureAwait(false);
            }
            else
            {
                SaveIngredientButtonText = "Input correct data";
            }
        }

        private Unit ConvertUnitInEnum(string name)
        {
            var consistentName = name.Replace(" ", "");
            var unit = (Unit) Enum.Parse(typeof(Unit), consistentName);
            return unit;
        }
    }
}
