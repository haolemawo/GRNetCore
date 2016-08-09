using System;
using System.Collections.Generic;

namespace GR.Services.Menus.Models
{
    public class MenuListViewModel
    {
        public MenuListViewModel()
        {
            Items = new List<MenuViewModel>();
        }

        public List<MenuViewModel> Items { get; set; }
    }
}
