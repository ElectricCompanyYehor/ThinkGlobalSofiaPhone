using System;
using System.Collections.Generic;
using System.ComponentModel;
using ThinkGlobalSofia.Models;
using ThinkGlobalSofia.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ThinkGlobalSofia.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}