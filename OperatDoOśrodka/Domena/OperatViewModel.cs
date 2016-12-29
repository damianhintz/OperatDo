using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OśrodekDomena;
using System.Windows.Forms;

namespace OperatDoOśrodka.Domena
{
    /// <summary>
    /// Widok operatu.
    /// </summary>
    class OperatViewModel : ListViewItem
    {
        public Operat Operat => _operat;
        public string Id => Operat.Id.HasValue ? Operat.Id.Value.ToString() : "Brak";
        public string Typ => Operat.Typ.HasValue ? Operat.Typ.Value.ToString() : "Brak";
        public string Dokumenty => Operat.DokumentyId != null ? Operat.DokumentyId.Count.ToString() : "Brak";
        public string IdZasobu => Operat.IdZasobu;
        public string Pliki => Operat.Pliki.Count().ToString();
        public string Rozmiar => Operat.Rozmiar.MegaBajty.ToString();
        public string Folder => Operat.Folder;
        public string Status => Operat.Status;
        Operat _operat;

        public OperatViewModel(Operat operat)
            : base(operat.IdZasobu)
        {
            _operat = operat;
            AddItems();
            Tag = operat;
        }

        void AddItems()
        {
            SubItems.Add(Id);
            SubItems.Add(Typ);
            SubItems.Add(Dokumenty);
            SubItems.Add(Pliki);
            SubItems.Add(Rozmiar);
            SubItems.Add(Folder);
            SubItems.Add(Status);
        }

        public void Odśwież()
        {
            SubItems.Clear();
            AddItems();
        }
    }
}
