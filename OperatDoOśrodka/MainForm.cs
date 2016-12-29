using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using OśrodekDomena;
using OśrodekPliki;
using OśrodekFirebird;
using System.Diagnostics;
using System.IO;
using OperatDoOśrodka.Domena;
using System.Threading;

namespace OperatDoOśrodka
{
    /// <summary>
    /// Główny formularz aplikacji.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Zwróć zaznaczone operaty.
        /// </summary>
        IEnumerable<OperatViewModel> ZaznaczoneOperaty
        {
            get
            {
                var operaty = new List<OperatViewModel>();
                foreach (int i in operatView.SelectedIndices)
                {
                    var item = operatView.Items[i] as OperatViewModel;
                    operaty.Add(item);
                }
                return operaty;
            }
        }

        RepozytoriumOperatów _operaty = new RepozytoriumOperatów();

        public MainForm()
        {
            InitializeComponent();
            operatView.RetrieveVirtualItem += OperatView_RetrieveVirtualItem;
        }

        private void OperatView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            var operat = _operaty[e.ItemIndex];
            var item = new OperatViewModel(operat);
            e.Item = item;
        }

        private void zakończStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void wczytajOperatyMenuItem_Click(object sender, EventArgs e)
        {
            var folderDialog = new FolderBrowserDialog();
            folderDialog.SelectedPath = @"s:\2903_skanowanie Bartoszyce\Skanowanie.1 Etap\";
            var result = folderDialog.ShowDialog(this);
            if (result != DialogResult.OK) return;
            statusLabel.Text = "Wczytywanie zeskanowanych operatów: " + folderDialog.SelectedPath;
            Application.DoEvents();
            var reader = new OperatReader(_operaty);
            reader.Wczytaj(folderDialog.SelectedPath);
            operatView.VirtualListSize = _operaty.Count();
            MessageBox.Show(owner: this,
                text: "Wczytane operaty: " + _operaty.Count() +
                "\nWczytane pliki: " + reader.Pliki.Count(),
                caption: "Zeskanowane operaty",
                buttons: MessageBoxButtons.OK,
                icon: MessageBoxIcon.Information);
            statusLabel.Text = "Gotowe";
        }

        private void czytajToMenuItem_Click(object sender, EventArgs e)
        {
            var item = sender as ToolStripItem;
            var fileName = item.Tag as string;
            Process.Start(fileName);
        }

        private void zaznaczWszystkoMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < operatView.VirtualListSize; i++)
                operatView.SelectedIndices.Add(i);
        }

        private void odwróćZaznaczenieMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < operatView.VirtualListSize; i++)
            {
                var item = operatView.Items[i];
                item.Selected = !item.Selected;
            }
        }

        private void zaznaczOdczytaneMenuItem_Click(object sender, EventArgs e)
        {
            operatView.SelectedIndices.Clear(); //Odznacz wszystko
            for (int i = 0; i < operatView.VirtualListSize; i++)
            {
                var item = operatView.Items[i];
                var operat = item.Tag as Operat;
                item.Selected = operat.Id.HasValue;
            }
        }

        private void zaznaczNieodczytaneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            operatView.SelectedIndices.Clear(); //Odznacz wszystko
            for (int i = 0; i < operatView.VirtualListSize; i++)
            {
                var item = operatView.Items[i];
                var operat = item.Tag as Operat;
                item.Selected = !operat.Id.HasValue;
            }
        }

        private void zaznaczBezDokumentówMenuItem_Click(object sender, EventArgs e)
        {
            operatView.SelectedIndices.Clear(); //Odznacz wszystko
            for (int i = 0; i < operatView.VirtualListSize; i++)
            {
                var item = operatView.Items[i];
                var operat = item.Tag as Operat;
                item.Selected = operat.DokumentyId != null && operat.DokumentyId.Count == 0;
            }
        }

        private void zaznaczZapisaneMenuItem_Click(object sender, EventArgs e)
        {
            operatView.SelectedIndices.Clear(); //Odznacz wszystko
            for (int i = 0; i < operatView.VirtualListSize; i++)
            {
                var item = operatView.Items[i];
                var operat = item.Tag as Operat;
                item.Selected = operat.DokumentyId != null && operat.DokumentyId.Count == operat.Pliki.Count();
            }
        }

        private void pokażZaznaczenieMenuItem_Click(object sender, EventArgs e)
        {
            var records = new List<string>();
            foreach (var operat in ZaznaczoneOperaty)
            {
                records.Add(
                    string.Format("{0}: {1}: {2}: {3}",
                    operat.IdZasobu, operat.Id, operat.Dokumenty, operat.Status));
            }
            PokażPlik(records);
        }

        void PokażPlik(IEnumerable<string> records)
        {
            var fileName = Path.GetTempFileName();
            fileName = Path.ChangeExtension(fileName, ".txt");
            File.WriteAllLines(fileName, records);
            Process.Start(fileName);
        }

        private void operatView_SelectedIndexChanged(object sender, EventArgs e)
        {
            var operaty = ZaznaczoneOperaty;
            var pliki = operaty.Sum(op => op.Operat.Pliki.Count());
            var rozmiar = operaty.Sum(op => op.Operat.Rozmiar.MegaBajty);
            statusLabel.Text = "Zaznaczone operaty: " + operaty.Count() +
                ", " + pliki + " pliki (" + rozmiar + " MB)";
        }

        private void pokażOperatMenuItem_Click(object sender, EventArgs e)
        {
            var operaty = ZaznaczoneOperaty;
            if (!operaty.Any()) return; //Brak operatów do pokazania
            if (operaty.Count() > 1) return; //Za dużo operatów do pokazania
            var jedynyOperat = operaty.Single();
            Process.Start(jedynyOperat.Folder);
        }

        private void wczytajOperatMenuItem_Click(object sender, EventArgs e)
        {
            OdczytajOperaty();
        }

        void OdczytajOperaty()
        {
            var operaty = ZaznaczoneOperaty;
            if (!operaty.Any()) return; //Brak operatów do wczytania
            var writer = new OperatWriter();
            var operatConfig = OśrodekConfig.Wczytaj("Osrodek.json");
            var plikConfig = OśrodekConfig.Wczytaj("OsrodekPliki.json");
            writer.OperatDb = new OśrodekOperatDb(operatConfig);
            writer.PlikDb = new OśrodekPlikiDb(plikConfig);
            var result = MessageBox.Show(owner: this,
                text: "Wczytywane operaty: " + operaty.Count() +
                "\nOśrodek operaty: " + operatConfig.Path +
                "\nOśrodek pliki: " + plikConfig.Path,
                caption: "Odczytać operaty?",
                buttons: MessageBoxButtons.YesNo,
                icon: MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return; //Odczytywanie anulowane
            var wczytaneOperaty = new List<OperatViewModel>();
            var niewczytaneOperaty = new List<OperatViewModel>();
            foreach (var operat in operaty)
            {
                statusLabel.Text = "Odczytywanie operatu: " + operat.IdZasobu;
                var wczytany = writer.WczytajOperat(operat.Operat);
                if (wczytany) wczytaneOperaty.Add(operat);
                else niewczytaneOperaty.Add(operat);
                Application.DoEvents();
            }
            foreach (var operat in wczytaneOperaty)
            {
                operat.Operat.Status = "Odczytany z bazy danych Ośrodka";
                operat.Odśwież();
            }
            foreach (var operat in niewczytaneOperaty)
            {
                operat.Operat.Status = "Brak w bazie danych Ośrodka";
                operat.Odśwież();
            }
            writer.Dispose();
            var icon = MessageBoxIcon.Information;
            if (niewczytaneOperaty.Count > 0) icon = MessageBoxIcon.Error;
            MessageBox.Show(owner: this,
                text: "Wczytane operaty: " + wczytaneOperaty.Count + "\n" +
                "Niewczytane operaty: " + niewczytaneOperaty.Count,
                caption: "Wczytywanie zakończone",
                buttons: MessageBoxButtons.OK,
                icon: icon);
            statusLabel.Text = "Gotowe";
        }

        private void usuńOperatMenuItem_Click(object sender, EventArgs e)
        {
            var operaty = ZaznaczoneOperaty;
            if (!operaty.Any()) return; //Brak operatów do usunięcia
            var result = MessageBox.Show(owner: this,
                text: "Usuwane operaty: " + operaty.Count(),
                caption: "Usunąć operaty?",
                buttons: MessageBoxButtons.YesNo,
                icon: MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return; //Usuwanie anulowane
            operatView.SelectedIndices.Clear();
            foreach (var operat in operaty) _operaty.Usuń(operat.Operat);
            operatView.VirtualListSize = _operaty.Count();
            var icon = MessageBoxIcon.Information;
            MessageBox.Show(owner: this,
                text: "Usunięte operaty: " + operaty.Count(),
                caption: "Usuwanie zakończone",
                buttons: MessageBoxButtons.OK,
                icon: icon);
            statusLabel.Text = "Gotowe";
        }

        private void zapiszOperatMenuItem_Click(object sender, EventArgs e)
        {
            var operaty = ZaznaczoneOperaty;
            if (!operaty.Any()) return; //Brak operatów do zapisania
            var operatyGdzieDokumenty = operaty.Where(op => op.Operat.DokumentyId.Count > 0);
            if (operatyGdzieDokumenty.Any())
            {
                MessageBox.Show(owner: this,
                text: "Wybrane operaty: " + operaty.Count() +
                "\nNieprawidłowe operaty: " + operatyGdzieDokumenty.Count() +
                "\nWybrano operaty, które posiadają dokumenty w bazie danych Ośrodka!",
                caption: "Przerwane zapisywanie operatów",
                buttons: MessageBoxButtons.OK,
                icon: MessageBoxIcon.Warning);
                return; //Przerwano bo wybrano operaty z dokumentami
            }
            var writer = new OperatWriter();
            var operatConfig = OśrodekConfig.Wczytaj("Osrodek.json");
            var plikConfig = OśrodekConfig.Wczytaj("OsrodekPliki.json");
            writer.OperatDb = new OśrodekOperatDb(operatConfig);
            writer.PlikDb = new OśrodekPlikiDb(plikConfig);
            var pliki = operaty.Sum(op => op.Operat.Pliki.Count());
            var rozmiar = operaty.Sum(op => op.Operat.Rozmiar.MegaBajty);
            var result = MessageBox.Show(owner: this,
                text: "Zapisywane operaty: " + operaty.Count() +
                "\nLiczba plików: " + pliki +
                "\nRozmiar plików [MB]: " + rozmiar +
                "\nOśrodek operaty: " + operatConfig.Path +
                "\nOśrodek pliki: " + plikConfig.Path,
                caption: "Zapisać operaty?",
                buttons: MessageBoxButtons.YesNo,
                icon: MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return; //Zapisywanie anulowane
            var zapisaneOperaty = new List<OperatViewModel>();
            var niezapisaneOperaty = new List<OperatViewModel>();
            var index = 0;
            var count = operaty.Count();
            foreach (var operat in operaty)
            {
                index++;
                statusLabel.Text = "Zapisywanie operatu [" + index + "/" + count + "]: " + operat.IdZasobu;
                Application.DoEvents();
                //Thread.Sleep(500);
                var countDok = operat.Operat.Pliki.Count();
                try
                {
                    var filesOk = writer.ZapiszOperat(operat.Operat);
                    var numerDok = 1;
                    var countFalse = 0;
                    foreach (var ok in filesOk)
                    {
                        statusLabel.Text = "Zapisywanie operatu [" + index + "/" + count + "]: " 
                            + operat.IdZasobu + ": " + numerDok + "/" + countDok;
                        Application.DoEvents();
                        numerDok++;
                        if (!ok) countFalse++;
                    }
                    if (countFalse == 0)
                    {
                        zapisaneOperaty.Add(operat);
                    }
                    else
                    {
                        niezapisaneOperaty.Add(operat);
                    }
                }
                catch (Exception ex)
                {
                    operat.Operat.Status = "Niezapisany: " + ex.Message;
                    niezapisaneOperaty.Add(operat);
                }
                Application.DoEvents();
            }
            foreach (var operat in zapisaneOperaty)
            {
                operat.Operat.Status = "Zapisany do Ośrodka";
                operat.Operat.DokumentyId = new List<int>(operat.Operat.Pliki.Select(p => p.Id.Value));
                operat.Operat.PlikiId = new List<int>(operat.Operat.Pliki.Select(p => p.PlikId.Value));
                operat.Odśwież();
            }
            foreach (var operat in niezapisaneOperaty)
            {
                operat.Odśwież();
            }
            writer.Dispose();
            var icon = MessageBoxIcon.Information;
            if (niezapisaneOperaty.Count > 0) icon = MessageBoxIcon.Error;
            MessageBox.Show(owner: this,
                text: "Zapisane operaty: " + zapisaneOperaty.Count + "\n" +
                "Niezapisane operaty: " + niezapisaneOperaty.Count,
                caption: "Zapisywanie zakończone",
                buttons: MessageBoxButtons.OK,
                icon: icon);
            statusLabel.Text = "Gotowe";
        }

        private void odczytajDokumentyOperatuMenuItem_Click(object sender, EventArgs e)
        {
            var operaty = ZaznaczoneOperaty;
            if (!operaty.Any()) return; //Brak operatów do wczytania
            var operatyBrakDokumentu = operaty.Where(op => op.Operat.Id.HasValue == false);
            if (operatyBrakDokumentu.Any())
            {
                MessageBox.Show(owner: this,
                text: "Wybrane operaty: " + operaty.Count() +
                "\nNieprawidłowe operaty: " + operatyBrakDokumentu.Count() +
                "\nWybrano operaty, których nie znaleziono w bazie danych Ośrodka!",
                caption: "Przerwane odczytywanie dokumentów operatów",
                buttons: MessageBoxButtons.OK,
                icon: MessageBoxIcon.Warning);
                return; //Wybrano operaty dla których nie ustalono czy posiadają dokumenty
            }
            var writer = new OperatWriter();
            var operatConfig = OśrodekConfig.Wczytaj("Osrodek.json");
            var plikConfig = OśrodekConfig.Wczytaj("OsrodekPliki.json");
            writer.OperatDb = new OśrodekOperatDb(operatConfig);
            writer.PlikDb = new OśrodekPlikiDb(plikConfig);
            var result = MessageBox.Show(owner: this,
                text: "Wybrane operaty: " + operaty.Count() +
                "\nOśrodek operaty: " + operatConfig.Path +
                "\nOśrodek pliki: " + plikConfig.Path,
                caption: "Odczytać dokumenty operatów?",
                buttons: MessageBoxButtons.YesNo,
                icon: MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return; //Odczytywanie anulowane
            statusLabel.Text = "Odczytywanie dokumentów operatów...";
            Application.DoEvents();
            writer.WczytajDokumenty(operaty.Select(op => op.Operat));
            var wczytaneOperaty = new List<OperatViewModel>();
            var niewczytaneOperaty = new List<OperatViewModel>();
            var dokumenty = 0;
            foreach (var operat in operaty)
            {
                if (operat.Operat.DokumentyId != null)
                {
                    if (operat.Operat.DokumentyId.Count > 0)
                        operat.Operat.Status = "Odczytany z bazy danych Ośrodka z dokumentami";
                    else
                        operat.Operat.Status = "Odczytany z bazy danych Ośrodka bez dokumentów";
                    operat.Odśwież();
                    wczytaneOperaty.Add(operat);
                    dokumenty += operat.Operat.DokumentyId.Count;
                }
                else niewczytaneOperaty.Add(operat);
            }
            writer.Dispose();
            var icon = MessageBoxIcon.Information;
            if (dokumenty > 0) icon = MessageBoxIcon.Warning;
            MessageBox.Show(owner: this,
                text: "Sprawdzone operaty: " + wczytaneOperaty.Count +
                "\nOdczytane dokumenty operatów: " + dokumenty +
                "\nNieodczytane operaty: " + niewczytaneOperaty.Count,
                caption: "Odczytywanie dokumentów zakończone",
                buttons: MessageBoxButtons.OK,
                icon: icon);
            statusLabel.Text = "Gotowe";
        }

        private void usuńDokumentyMenuItem_Click(object sender, EventArgs e)
        {
            var operaty = ZaznaczoneOperaty;
            if (!operaty.Any()) return; //Brak operatów
            //Zgodna liczba dokumentów i plików na dysku
            var writer = new OperatWriter();
            var operatConfig = OśrodekConfig.Wczytaj("Osrodek.json");
            var plikConfig = OśrodekConfig.Wczytaj("OsrodekPliki.json");
            writer.OperatDb = new OśrodekOperatDb(operatConfig);
            writer.PlikDb = new OśrodekPlikiDb(plikConfig);
            var result = MessageBox.Show(owner: this,
                text: "Wybrane operaty: " + operaty.Count() +
                "\nOśrodek operaty: " + operatConfig.Path +
                "\nOśrodek pliki: " + plikConfig.Path,
                caption: "Usunąć dokumenty operatów?",
                buttons: MessageBoxButtons.YesNo,
                icon: MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return; //Usuwanie anulowane
            writer.Dispose();
            var dokumenty = new List<string>();
            var pliki = new List<string>();
            foreach (var operatView in operaty)
            {
                var operat = operatView.Operat;
                if (!operat.Id.HasValue)
                {
                    dokumenty.Add("-- brak operatu " + operat.IdZasobu);
                    continue;
                }
                dokumenty.Add("-- " + operat.IdZasobu);
                if (operat.DokumentyId == null) continue;
                var operatId = operat.Id.Value;
                var operatTyp = operat.Typ.Value;
                for (int i = 0; i < operat.DokumentyId.Count; i++)
                {
                    var dokumentId = operat.DokumentyId[i];
                    var plikId = operat.PlikiId[i];
                    var deleteDokument = string.Format(
                        "delete from operdok where uid={0} and id_ope={1} and typ='{2}';",
                        dokumentId, operatId, operatTyp);
                    dokumenty.Add(deleteDokument);
                    var deletePlik = string.Format(
                        "delete from fbdok where uid={0};",
                        plikId);
                    pliki.Add(deletePlik);
                }
            }
            PokażPlik(dokumenty);
        }

        private void usuńPlikiMenuItem_Click(object sender, EventArgs e)
        {
            var operaty = ZaznaczoneOperaty;
            if (!operaty.Any()) return; //Brak operatów
            //Zgodna liczba dokumentów i plików na dysku
            var writer = new OperatWriter();
            var operatConfig = OśrodekConfig.Wczytaj("Osrodek.json");
            var plikConfig = OśrodekConfig.Wczytaj("OsrodekPliki.json");
            writer.OperatDb = new OśrodekOperatDb(operatConfig);
            writer.PlikDb = new OśrodekPlikiDb(plikConfig);
            var result = MessageBox.Show(owner: this,
                text: "Wybrane operaty: " + operaty.Count() +
                "\nOśrodek operaty: " + operatConfig.Path +
                "\nOśrodek pliki: " + plikConfig.Path,
                caption: "Usunąć pliki operatów?",
                buttons: MessageBoxButtons.YesNo,
                icon: MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return; //Usuwanie anulowane
            writer.Dispose();
            var dokumenty = new List<string>();
            var pliki = new List<string>();
            foreach (var operatView in operaty)
            {
                var operat = operatView.Operat;
                if (!operat.Id.HasValue)
                {
                    dokumenty.Add("-- brak operatu " + operat.IdZasobu);
                    continue;
                }
                if (operat.DokumentyId == null) continue;
                pliki.Add("-- " + operat.IdZasobu);
                var operatId = operat.Id.Value;
                var operatTyp = operat.Typ.Value;
                for (int i = 0; i < operat.DokumentyId.Count; i++)
                {
                    var dokumentId = operat.DokumentyId[i];
                    var plikId = operat.PlikiId[i];
                    var deleteDokument = string.Format(
                        "delete from operdok where uid={0} and id_ope={1} and typ='{2}';",
                        dokumentId, operatId, operatTyp);
                    dokumenty.Add(deleteDokument);
                    var deletePlik = string.Format(
                        "delete from fbdok where uid={0};",
                        plikId);
                    pliki.Add(deletePlik);
                }
            }
            PokażPlik(pliki);
        }
    }
}
