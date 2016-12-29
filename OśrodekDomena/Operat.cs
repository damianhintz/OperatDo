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
        /// <param name="dokument"></param>
        public void Dodaj(PlikOperatu dokument)
        {
            if (dokument == null)
                throw new ArgumentNullException(
                    paramName: "dokument",
                    message: "Dokument jest null");
            dokument.Operat = this;
            _pliki.Add(dokument);
        }
    }
}
