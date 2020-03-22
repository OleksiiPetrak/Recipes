using MvvmCross.Commands;
using MvvmCross.Navigation;
using RecipesBook.Common.Enums;
using RecipesBook.Common.Extensions;
using RecipesBook.Core.Interfaces;
using RecipesBook.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipesBook.Core.ViewModels
{
    public class IngredientViewModel : BaseViewModel<List<Ingredient>,List<Ingredient>>
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IIngredientService _ingredientService;
        private List<Ingredient> _ingredients;

        public IngredientViewModel(IMvxNavigationService navigationService,
            IIngredientService ingredientService)
        {
            _navigationService = navigationService;
            _ingredientService = ingredientService;
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
            get => Enum.GetNames(typeof(Unit)).Select(c=>c.SplitCamelCase()).ToList();
        }

        public string SelectedUnit
        {
            get => _selectedUnit;
            set
            {
                _selectedUnit = value;
                RaisePropertyChanged(() => SelectedUnit);
            }
        }

        public string IngredientName
        {
            get => _ingredientName;
            set
            {
                _ingredientName = value;
                RaisePropertyChanged(() => IngredientName);
            }
        }

        public int IngredientAmount
        {
            get => _ingredientAmount;
            set
            {
                _ingredientAmount = value;
                RaisePropertyChanged(() => IngredientAmount);
            }
        }

        public string SaveIngredientButtonText
        {
            get => _saveIngredientButtonText;
            set
            {
                _saveIngredientButtonText = value;
                RaisePropertyChanged(() => SaveIngredientButtonText);
            }
        }

        public IMvxCommand SaveIngredientCommand { get; private set; }

        public async Task SaveIngredient()
        {
            try
            {
                if (IngredientAmount > 0 && IngredientName != null && SelectedUnit != null)
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
            catch(Exception ex)
            {
                throw;
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
