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
            list.Add(new DashboardViewModel { URL = "https://as1.ftcdn.net/v2/jpg/04/29/93/28/1000_F_429932887_ZSdOvhqNb1bhDa7twCKRySQ1hvVtMSsl.jpg" });
            list.Add(new DashboardViewModel { URL = "https://as1.ftcdn.net/v2/jpg/04/24/51/46/1000_F_424514620_iiZHVadYjpe4k3PQg8qhixUXOuAP5MEd.jpg" });
            list.Add(new DashboardViewModel { URL = "https://media.istockphoto.com/vectors/online-school-digital-internet-tutorials-and-courses-online-education-vector-id1218933779" });
            return list;
        }
    }
}
