using System.ComponentModel;
using ThinkGlobalSofia.ViewModels;
using Xamarin.Forms;

namespace ThinkGlobalSofia.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}