using System;
 
namespace Cecchetti.Mattia._5H.SecondaWeb.Models
{
    public class Prenotazione
    {
        public int PrenotazioneId { get; set; } 
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public bool? Partecipazione { get; set; }  //variabile nullbool, può essere anche null
        public DateTime DataPrenotazione { get; set; }=DateTime.Now;
    }
}