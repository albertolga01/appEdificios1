using System;
using System.Collections.Generic;
using System.Text;

namespace AppEdificiosP.ViewModels
{
    public class DashboardViewModel
    {
        public string URL { get; set; }
    }
    public class DashboardModel
    {
        public List<DashboardViewModel> GetBannerList()
        {
            List<DashboardViewModel> list = new List<DashboardViewModel>();
            list.Add(new DashboardViewModel { URL = "https://app.petromargas.com/banners/Banner-APPPetromarGAS-01.png" });
            list.Add(new DashboardViewModel { URL = "https://app.petromargas.com/banners/Banner-APPPetromarGAS-02.png" });
            list.Add(new DashboardViewModel { URL = "https://app.petromargas.com/banners/Banner-APPPetromarGAS-03.png" });
          
            return list;
        }
    }
}
