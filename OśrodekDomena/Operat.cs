using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OśrodekDomena
{
    /// <summary>
    /// Operat.
    /// </summary>
    public class Operat
    {
        public int? Id { get; set; }
        public char? Typ { get; set; }
        public List<int> DokumentyId { get; set; }
        public List<int> PlikiId { get; set; }

        /// <summary>
        /// Identyfikator zasobu.
        /// </summary>
        public string IdZasobu { get; set; }

        /// <summary>
        /// Rozmiar całego operatu na dysku.
        /// </summary>
        public RozmiarPliku Rozmiar => new RozmiarPliku(Pliki.Sum(d => d.Rozmiar.Bajty));
        
        /// <summary>
        /// Pliki operatu.
        /// </summary>
        public IEnumerable<PlikOperatu> Pliki => _pliki;
        List<PlikOperatu> _pliki = new List<PlikOperatu>();
        Dictionary<string, PlikOperatu> _indeksPlików = new Dictionary<string, PlikOperatu>();

        /// <summary>
        /// Folder zawierający pliki operatu.
        /// </summary>
        public string Folder
        {
            get
            {
                if (!Pliki.Any())
                    throw new InvalidOperationException(
                        message: "Nie można ustalić folderu operatu bez dokumentów: " + IdZasobu);
                return Path.GetDirectoryName(Pliki.First().Plik);
            }
        }

        public string Status { get; set; }

        /// <summary>
        /// Dodaj zeskanowany dokument do operatu.
        /// </summary>
        /// <param name="plik"></param>
        public void Dodaj(PlikOperatu plik)
        {
            if (plik == null)
                throw new ArgumentNullException(
                    paramName: "plik",
                    message: "Plik operatu jest null");
            var nazwa = Path.GetFileNameWithoutExtension(plik.Plik).ToLower();
            if (_indeksPlików.ContainsKey(nazwa))
                throw new InvalidOperationException(
                    message: "Operat '" + IdZasobu + "' zawiera już plik: " + nazwa);
            _indeksPlików.Add(nazwa, plik);
            plik.Operat = this;
            _pliki.Add(plik);
        }
    }
}
