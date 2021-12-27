using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hyvinvointisovellus.ViewModel
{
    public class FiilisModel1
    {
        public int EventID { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> Start { get; set; }
        public Nullable<System.DateTime> End { get; set; }
        public string ThemeColor { get; set; }
        public Nullable<bool> IsFullDay { get; set; }
        public Nullable<int> KayttajaID { get; set; }
        public Nullable<int> HymynaamaID { get; set; }
    }
}