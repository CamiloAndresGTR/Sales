using Sales.Common.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Sales.Services;
using Xamarin.Forms;

namespace Sales.ViewModels
{
    public class ProductsViewModel : BaseViewModel
    {
        private ApiService apiservice;
        private ObservableCollection<Products> products;
        public ObservableCollection<Products> Products 
        {
            get { return this.products; }
            set { this.SetValue(ref this.products, value); }
        }
         public ProductsViewModel()
        {
            this.apiservice = new ApiService();
            this.LoadProducts();
        }
        private async void LoadProducts()
        {
            var response = await this.apiservice.GetList<Products>("https://salesapi20210106205330.azurewebsites.net", "/api", "/Products");
            if(!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }
            var list = (List<Products>)response.Result;
            this.Products = new ObservableCollection<Products>(list);
        }
        
        
    }
}
