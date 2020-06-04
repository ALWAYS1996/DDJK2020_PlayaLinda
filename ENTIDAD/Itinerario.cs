using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ENTIDAD
{
   public class Itinerario{

        [DisplayFormat(NullDisplayText = "Sin Respuesta")]
        [Display(Name = "Código")]
        [Key]
        public int idItinerario { get; set; }


        
        [DisplayFormat(NullDisplayText = "Sin Respuesta")]
        [Display(Name = "Desayuno")]
        public string dia { get; set; }
        [DisplayFormat(NullDisplayText = "Sin Respuesta")]
        [Display(Name = "Desayuno")]
        public string desayuno { get; set; }

        [DisplayFormat(NullDisplayText = "Sin Respuesta")]
        [Display(Name = "Imagen")]
        public string imgUrlDesayuno { get; set; }

        [DisplayFormat(NullDisplayText = "Sin Respuesta")]
        [Display(Name = "Almuerzo")]
      
        public string imgUrlAlmuerzo { get; set; }
        public string almuerzo { get; set; }

        [DisplayFormat(NullDisplayText = "Sin Respuesta")]
        [Display(Name = "Cena")]
        public string cena { get; set; }
        [DisplayFormat(NullDisplayText = "Sin Respuesta")]
        [Display(Name = "Imagen")]
        public string imgUrlCena { get; set; }

        public Itinerario(int idItinerario, string dia, string desayuno, string imgUrlDesayuno, string imgUrlAlmuerzo, string almuerzo, string cena, string imgUrlCena)
        {
            this.idItinerario = idItinerario;
            this.dia = dia;
            this.desayuno = desayuno;
            this.imgUrlDesayuno = imgUrlDesayuno;
            this.imgUrlAlmuerzo = imgUrlAlmuerzo;
            this.almuerzo = almuerzo;
            this.cena = cena;
            this.imgUrlCena = imgUrlCena;
        }

        public Itinerario(string dia, string desayuno, string imgUrlDesayuno, string imgUrlAlmuerzo, string almuerzo, string cena, string imgUrlCena)
        {
            this.dia = dia;
            this.desayuno = desayuno;
            this.imgUrlDesayuno = imgUrlDesayuno;
            this.imgUrlAlmuerzo = imgUrlAlmuerzo;
            this.almuerzo = almuerzo;
            this.cena = cena;
            this.imgUrlCena = imgUrlCena;
        }

        public Itinerario()
        {
        }
    }
}
