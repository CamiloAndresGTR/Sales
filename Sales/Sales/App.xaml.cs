﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Sales.Views;

namespace Sales
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new ProductsPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
