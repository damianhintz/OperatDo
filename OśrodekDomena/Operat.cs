using System;
using System.Collections.Generic;
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

        /// <summary>
        /// Identyfikator zasobu.
        /// </summary>
        public string IdZasobu { get; set; }

        /// <summary>
        /// Dokumenty wprowadzone do bazy danych.
        /// </summary>
        public IEnumerable<DokumentOperatu> DokumentyDb => Dokumenty.Where(d => d.Id.HasValue);

        /// <summary>
        /// Dokumenty operatu.
        /// </summary>
        public IEnumerable<DokumentOperatu> Dokumenty => _dokumenty;
        List<DokumentOperatu> _dokumenty = new List<DokumentOperatu>();

        /// <summary>
        /// Dodaj zeskanowany dokument do operatu.
        /// </summary>
        /// <param name="dokument"></param>
        public void Dodaj(DokumentOperatu dokument)
        {
            if (dokument == null)
                throw new ArgumentNullException(
                    paramName: "dokument",
                    message: "Dokument jest null");
            _dokumenty.Add(dokument);
        }
    }
}
