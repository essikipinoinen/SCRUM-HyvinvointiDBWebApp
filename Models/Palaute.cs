//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hyvinvointisovellus.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Palaute
    {
        public int PalauteID { get; set; }
        public string Palaute1 { get; set; }
        public Nullable<int> KayttajaID { get; set; }
    
        public virtual Kayttajat Kayttajat { get; set; }
    }
}
