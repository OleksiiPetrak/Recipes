using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(RecipesBook.Core.CustomRendered.NestedScroll),
    typeof(RecipesBook.Droid.CustomRenderer.NestedScrollDroid))]
namespace RecipesBook.Droid.CustomRenderer
{
    public class NestedScrollDroid : ListViewRenderer
    {
        public NestedScrollDroid(Android.Content.Context context) : base(context)
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                var listview = this.Control as Android.Widget.ListView;
                listview.NestedScrollingEnabled = true;
            }
        }
    }
}