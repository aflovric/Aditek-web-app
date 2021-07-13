using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aditek.Models
{
    public enum JobTypes
    {
        [Display(Name = "Sve")]
        sve,
        [Display(Name = "Betoniranje")]
        betoniranje,
        [Display(Name = "Fasade")]
        fasade,
        [Display(Name = "Građevinski radovi")]
        građevinskiRadovi,
        [Display(Name = "Izolacija krova")]
        izolacijaKrova,
        [Display(Name = "Keramičarski radovi")]
        keramičarskiRadovi,
        [Display(Name = "Manji vodovodni radovi popravci")]
        manjiVodovodniRadoviPopravci,
        [Display(Name = "Manji zidarski radovi")]
        manjiZidarskiRadovi,
        [Display(Name = "Održavanje objekta")]
        održavanjeObjekta,
        [Display(Name = "Rušenja")]
        rušenja,
        [Display(Name = "Rušenja drveća")]
        rušenjaDrveća,
        [Display(Name = "Sanacija balkona terase")]
        sanacijaBalkonaTerase,
        [Display(Name = "Sanacija vlage")]
        sanacijaVlage,
        [Display(Name = "Specijalizirani građevinski radovi")]
        specijaliziraniGrađevinskiRadovi,
        [Display(Name = "Ugradnja prozora i vrata")]
        ugradnjaProzoraIVrata
    }
}