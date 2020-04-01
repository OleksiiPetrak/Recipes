using Xamarin.Forms;

namespace RecipesBook.UI.Components
{
    public partial class MenuOptionView : StackLayout
	{
        public MenuOptionView()
        {
            InitializeComponent();
        }

        public string Text
        {
            set
            {
                Label.Text = value;
            }
        }
    }
}