﻿using System;
using System.Collections.Generic;
using System.Text;
using Sales.ViewModels;

namespace Sales.Infrastructure
{
    public class InstanceLocator
    {
        public MainViewModel Main { get; set; }
        public InstanceLocator()
        {
            this.Main = new MainViewModel(); 
        }
    }
}
