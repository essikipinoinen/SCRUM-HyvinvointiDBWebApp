using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hyvinvointisovellus.ViewModel
{
    public class Rekisterointi
    {
        [Required(ErrorMessage = "Kirjoita etunimesi!", AllowEmptyStrings = false)]
        [Display(Name = "Etunimi")]
        public string Etunimi { get; set; }

        [Required(ErrorMessage = "Kirjoita sukunimesi!", AllowEmptyStrings = false)]
        [Display(Name = "Sukunimi")]
        public string Sukunimi { get; set; }

        [Required(ErrorMessage = "Kirjoita osoitteesi!", AllowEmptyStrings = false)]
        [Display(Name = "Osoite")]
        public string Osoite { get; set; }

        [Required(ErrorMessage = "Kirjoita postinumerosi!", AllowEmptyStrings = false)]
        [Display(Name = "Postinumero")]
        public string Postinumero { get; set; }

        [Required(ErrorMessage = "Kirjoita käyttäjätunnuksesi!", AllowEmptyStrings = false)]
        [Display(Name = "Käyttäjätunnus")]
        public string Kayttajatunnus { get; set; }

        [Required(ErrorMessage = "Kirjoita salasanasi!", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string Salasana { get; set; }
        [Compare("Salasana", ErrorMessage = "Salasanat eivät täsmää!")]
        [DataType(DataType.Password)]
        public string VahvistaSalasana { get; set; }

    }
}