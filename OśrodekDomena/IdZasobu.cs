using System;
using System.Collections.Generic;
using System.Linq;

namespace OśrodekDomena
{
    /// <summary>
    /// Identyfikator zasobu.
    /// </summary>
    public class IdZasobu
    {
        public string Id => string.Format("P.{0}.{1}.{2}", Kod, Rok, Numer);

        //public static readonly char P = 'P';

        /// <summary>
        /// Czterocyfrowy kod.
        /// </summary>
        public string Kod => _kod;
        readonly string _kod;

        public int Rok => _rok;
        readonly int _rok;

        /// <summary>
        /// Numer w roku.
        /// </summary>
        public int Numer => _nr;
        readonly int _nr;

        public IdZasobu(int numer, int rok, string kod = "2801")
        {
            if (numer <= 0)
                throw new ArgumentOutOfRangeException(
                    paramName: "numer",
                    message: "Numer w roku powinien być dodatni: " + numer);
            if (rok < 1900)
                throw new ArgumentOutOfRangeException(
                    paramName: "rok",
                    message: "Rok operatu za stary: " + rok);
            if (rok > DateTime.Now.Year)
                throw new ArgumentOutOfRangeException(
                    paramName: "rok",
                    message: "Operat z przyszłości: " + rok);
            if (string.IsNullOrEmpty(kod) || kod.Length != 4)
                throw new ArgumentException(
                    paramName: "kod",
                    message: "Kod nie jest czterocyfrowy: " + kod);
            _nr = numer;
            _rok = rok;
            _kod = kod;
        }

        public override string ToString()
        {
            return Id;
        }

        /// <summary>
        /// Parsowanie id zasobu w postaci P.KOD.ROK.NR
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static IdZasobu Parse(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(
                    paramName: "id",
                    message: "Id zasobu jest pusty");
            if (!id.StartsWith("P."))
                throw new ArgumentException(
                    paramName: "id",
                    message: "Id zasobu nie zaczyna się od P.: " + id);
            var values = id.Split('.');
            if (values.Length != 4)
                throw new ArgumentException(
                    paramName: "id",
                    message: "Id zasobu zawiera " + values.Length +
                    " składniki, a powinny być dokładnie cztery rozdzielone kropką: " + id);
            var nr = 0;
            if (!int.TryParse(values[3], out nr))
                throw new ArgumentException(
                    paramName: "id",
                    message: "Id zasobu ma nieprawidłowy format (nr w roku nie jest liczbą): " + id);
            var rok = 0;
            if (!int.TryParse(values[2], out rok))
                throw new ArgumentException(
                    paramName: "id",
                    message: "Id zasobu ma nieprawidłowy format (rok nie jest liczbą): " + id);
            var kod = values[1];
            //var p = values[0];
            var idZasobu = new IdZasobu(nr, rok, kod);
            if (!idZasobu.Id.Equals(id))
                throw new InvalidOperationException(
                    message: "Wynik parsowania id zasobu jest błędny: oczekiwano '" +
                    id + "', a otrzymano '" + idZasobu.Id + "'");
            return idZasobu;
        }
    }
}
