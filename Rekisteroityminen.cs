//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hyvinvointisovellus
{
    using System;
    using System.Collections.Generic;
    
    public partial class Rekisteroityminen
    {
        public int RekisteroitymisID { get; set; }
        public Nullable<System.DateTime> Alkupvm { get; set; }
        public string Kayttajatunnus { get; set; }
        public string Salasana { get; set; }
        public Nullable<int> KayttajaID { get; set; }
    
        public virtual Kayttajat Kayttajat { get; set; }
        public virtual Kayttajat Kayttajat1 { get; set; }
    }
}