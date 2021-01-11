using Sales.Common.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Sales.Services;
using Xamarin.Forms;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace Sales.ViewModels
{
    public class ProductsViewModel : BaseViewModel
    {
        private ApiService apiservice;
        private bool isRefreshing;
        private ObservableCollection<Products> products;
        public ObservableCollection<Products> Products 
        {
            get { return this.products; }
            set { this.SetValue(ref this.products, value); }
        }
        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }
        public ProductsViewModel()
        {
            this.apiservice = new ApiService();
            this.LoadProducts();
        }
        private async void LoadProducts()
        {
            this.IsRefreshing = true;
            var response = await this.apiservice.GetList<Products>("https://salesapi20210106205330.azurewebsites.net", "/api", "/Products");
            if(!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }
            var list = (List<Products>)response.Result;
            this.Products = new ObservableCollection<Products>(list);
            this.IsRefreshing = false;
        }
        
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadProducts);
            }
        }
        
    }
}
