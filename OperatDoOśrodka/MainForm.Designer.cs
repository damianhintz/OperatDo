namespace OperatDoOśrodka
{
    partial class MainForm
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Wymagana metoda obsługi projektanta — nie należy modyfikować 
        /// zawartość tej metody z edytorem kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.plikMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ośrodekMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ośrodekOperatyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ośrodekPlikiMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.wczytajOperatyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.zakończStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.widokMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zaznaczWszystkoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.zaznaczOdczytaneMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zaznaczNieodczytaneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.operatyBezDokumentówMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.odwróćZaznaczenieMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.pokażZaznaczenieMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pomocMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.czytajToMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.operatView = new System.Windows.Forms.ListView();
            this.operatHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.idHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.typHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dokumentyHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.plikiHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rozmiarHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.folderHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.operatMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pokażOperatMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.wczytajOperatMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.odczytajDokumentyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.zapiszOperatMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.usuńOperatMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel = new System.Windows.Forms.Panel();
            this.status = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menu.SuspendLayout();
            this.operatMenu.SuspendLayout();
            this.panel.SuspendLayout();
            this.status.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.BackColor = System.Drawing.SystemColors.Menu;
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.plikMenuItem,
            this.widokMenuItem,
            this.pomocMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menu.Size = new System.Drawing.Size(1210, 24);
            this.menu.TabIndex = 0;
            this.menu.Text = "menuStrip1";
            // 
            // plikMenuItem
            // 
            this.plikMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ośrodekMenuItem,
            this.toolStripSeparator2,
            this.wczytajOperatyMenuItem,
            this.toolStripSeparator1,
            this.zakończStripMenuItem});
            this.plikMenuItem.Name = "plikMenuItem";
            this.plikMenuItem.Size = new System.Drawing.Size(34, 20);
            this.plikMenuItem.Text = "Plik";
            // 
            // ośrodekMenuItem
            // 
            this.ośrodekMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ośrodekOperatyMenuItem,
            this.ośrodekPlikiMenuItem});
            this.ośrodekMenuItem.Name = "ośrodekMenuItem";
            this.ośrodekMenuItem.Size = new System.Drawing.Size(206, 22);
            this.ośrodekMenuItem.Text = "Ośrodek";
            // 
            // ośrodekOperatyMenuItem
            // 
            this.ośrodekOperatyMenuItem.Name = "ośrodekOperatyMenuItem";
            this.ośrodekOperatyMenuItem.Size = new System.Drawing.Size(155, 22);
            this.ośrodekOperatyMenuItem.Tag = "Osrodek.json";
            this.ośrodekOperatyMenuItem.Text = "Ośrodek operaty";
            this.ośrodekOperatyMenuItem.ToolTipText = "Osrodek.json";
            this.ośrodekOperatyMenuItem.Click += new System.EventHandler(this.czytajToMenuItem_Click);
            // 
            // ośrodekPlikiMenuItem
            // 
            this.ośrodekPlikiMenuItem.Name = "ośrodekPlikiMenuItem";
            this.ośrodekPlikiMenuItem.Size = new System.Drawing.Size(155, 22);
            this.ośrodekPlikiMenuItem.Tag = "OsrodekPliki.json";
            this.ośrodekPlikiMenuItem.Text = "Ośrodek pliki";
            this.ośrodekPlikiMenuItem.ToolTipText = "OsrodekPliki.json";
            this.ośrodekPlikiMenuItem.Click += new System.EventHandler(this.czytajToMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(203, 6);
            // 
            // wczytajOperatyMenuItem
            // 
            this.wczytajOperatyMenuItem.Name = "wczytajOperatyMenuItem";
            this.wczytajOperatyMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.wczytajOperatyMenuItem.Size = new System.Drawing.Size(206, 22);
            this.wczytajOperatyMenuItem.Text = "Wczytaj operaty...";
            this.wczytajOperatyMenuItem.ToolTipText = "Wczytaj zeskanowane operaty z dysku";
            this.wczytajOperatyMenuItem.Click += new System.EventHandler(this.wczytajOperatyMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(203, 6);
            // 
            // zakończStripMenuItem
            // 
            this.zakończStripMenuItem.Name = "zakończStripMenuItem";
            this.zakończStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.zakończStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.zakończStripMenuItem.Text = "Zakończ";
            this.zakończStripMenuItem.Click += new System.EventHandler(this.zakończStripMenuItem_Click);
            // 
            // widokMenuItem
            // 
            this.widokMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zaznaczWszystkoMenuItem,
            this.toolStripSeparator4,
            this.zaznaczOdczytaneMenuItem,
            this.zaznaczNieodczytaneToolStripMenuItem,
            this.toolStripSeparator8,
            this.operatyBezDokumentówMenuItem,
            this.toolStripSeparator3,
            this.odwróćZaznaczenieMenuItem,
            this.toolStripSeparator5,
            this.pokażZaznaczenieMenuItem});
            this.widokMenuItem.Name = "widokMenuItem";
            this.widokMenuItem.Size = new System.Drawing.Size(48, 20);
            this.widokMenuItem.Text = "Widok";
            // 
            // zaznaczWszystkoMenuItem
            // 
            this.zaznaczWszystkoMenuItem.Name = "zaznaczWszystkoMenuItem";
            this.zaznaczWszystkoMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.zaznaczWszystkoMenuItem.Size = new System.Drawing.Size(211, 22);
            this.zaznaczWszystkoMenuItem.Text = "Zaznacz wszystko";
            this.zaznaczWszystkoMenuItem.Click += new System.EventHandler(this.zaznaczWszystkoMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(208, 6);
            // 
            // zaznaczOdczytaneMenuItem
            // 
            this.zaznaczOdczytaneMenuItem.Name = "zaznaczOdczytaneMenuItem";
            this.zaznaczOdczytaneMenuItem.Size = new System.Drawing.Size(211, 22);
            this.zaznaczOdczytaneMenuItem.Text = "Zaznacz odczytane";
            this.zaznaczOdczytaneMenuItem.ToolTipText = "Zaznacz operaty w bazie danych Ośrodka";
            this.zaznaczOdczytaneMenuItem.Click += new System.EventHandler(this.zaznaczOdczytaneMenuItem_Click);
            // 
            // zaznaczNieodczytaneToolStripMenuItem
            // 
            this.zaznaczNieodczytaneToolStripMenuItem.Name = "zaznaczNieodczytaneToolStripMenuItem";
            this.zaznaczNieodczytaneToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.zaznaczNieodczytaneToolStripMenuItem.Text = "Zaznacz nieodczytane";
            this.zaznaczNieodczytaneToolStripMenuItem.ToolTipText = "Zaznacz operaty nieznalezione w bazie danych Ośrodka";
            this.zaznaczNieodczytaneToolStripMenuItem.Click += new System.EventHandler(this.zaznaczNieodczytaneToolStripMenuItem_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(208, 6);
            // 
            // operatyBezDokumentówMenuItem
            // 
            this.operatyBezDokumentówMenuItem.Name = "operatyBezDokumentówMenuItem";
            this.operatyBezDokumentówMenuItem.Size = new System.Drawing.Size(211, 22);
            this.operatyBezDokumentówMenuItem.Text = "Operaty bez dokumentów";
            this.operatyBezDokumentówMenuItem.ToolTipText = "Operaty bez dokumentów w bazie danych Ośrodka";
            this.operatyBezDokumentówMenuItem.Click += new System.EventHandler(this.zaznaczBezDokumentówMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(208, 6);
            // 
            // odwróćZaznaczenieMenuItem
            // 
            this.odwróćZaznaczenieMenuItem.Name = "odwróćZaznaczenieMenuItem";
            this.odwróćZaznaczenieMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.odwróćZaznaczenieMenuItem.Size = new System.Drawing.Size(211, 22);
            this.odwróćZaznaczenieMenuItem.Text = "Odwróć zaznaczenie";
            this.odwróćZaznaczenieMenuItem.Click += new System.EventHandler(this.odwróćZaznaczenieMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(208, 6);
            // 
            // pokażZaznaczenieMenuItem
            // 
            this.pokażZaznaczenieMenuItem.Name = "pokażZaznaczenieMenuItem";
            this.pokażZaznaczenieMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pokażZaznaczenieMenuItem.Size = new System.Drawing.Size(211, 22);
            this.pokażZaznaczenieMenuItem.Text = "Pokaż zaznaczenie";
            this.pokażZaznaczenieMenuItem.Click += new System.EventHandler(this.pokażZaznaczenieMenuItem_Click);
            // 
            // pomocMenuItem
            // 
            this.pomocMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.czytajToMenuItem});
            this.pomocMenuItem.Name = "pomocMenuItem";
            this.pomocMenuItem.Size = new System.Drawing.Size(50, 20);
            this.pomocMenuItem.Text = "Pomoc";
            // 
            // czytajToMenuItem
            // 
            this.czytajToMenuItem.Name = "czytajToMenuItem";
            this.czytajToMenuItem.Size = new System.Drawing.Size(118, 22);
            this.czytajToMenuItem.Tag = "README.md";
            this.czytajToMenuItem.Text = "Czytaj to";
            this.czytajToMenuItem.ToolTipText = "README.md";
            this.czytajToMenuItem.Click += new System.EventHandler(this.czytajToMenuItem_Click);
            // 
            // operatView
            // 
            this.operatView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.operatHeader,
            this.idHeader,
            this.typHeader,
            this.dokumentyHeader,
            this.plikiHeader,
            this.rozmiarHeader,
            this.folderHeader});
            this.operatView.ContextMenuStrip = this.operatMenu;
            this.operatView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.operatView.FullRowSelect = true;
            this.operatView.HideSelection = false;
            this.operatView.Location = new System.Drawing.Point(0, 0);
            this.operatView.Name = "operatView";
            this.operatView.Size = new System.Drawing.Size(1210, 413);
            this.operatView.TabIndex = 1;
            this.operatView.UseCompatibleStateImageBehavior = false;
            this.operatView.View = System.Windows.Forms.View.Details;
            this.operatView.VirtualMode = true;
            this.operatView.SelectedIndexChanged += new System.EventHandler(this.operatView_SelectedIndexChanged);
            // 
            // operatHeader
            // 
            this.operatHeader.Text = "Operat";
            this.operatHeader.Width = 200;
            // 
            // idHeader
            // 
            this.idHeader.Text = "Id";
            // 
            // typHeader
            // 
            this.typHeader.Text = "Typ";
            this.typHeader.Width = 50;
            // 
            // dokumentyHeader
            // 
            this.dokumentyHeader.Text = "Dokumenty";
            this.dokumentyHeader.Width = 80;
            // 
            // plikiHeader
            // 
            this.plikiHeader.Text = "Pliki";
            this.plikiHeader.Width = 100;
            // 
            // rozmiarHeader
            // 
            this.rozmiarHeader.Text = "Rozmiar [MB]";
            this.rozmiarHeader.Width = 100;
            // 
            // folderHeader
            // 
            this.folderHeader.Text = "Folder";
            this.folderHeader.Width = 600;
            // 
            // operatMenu
            // 
            this.operatMenu.BackColor = System.Drawing.SystemColors.Menu;
            this.operatMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pokażOperatMenuItem,
            this.toolStripSeparator7,
            this.wczytajOperatMenuItem,
            this.odczytajDokumentyMenuItem,
            this.toolStripSeparator9,
            this.zapiszOperatMenuItem,
            this.toolStripSeparator6,
            this.usuńOperatMenuItem});
            this.operatMenu.Name = "operatMenu";
            this.operatMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.operatMenu.Size = new System.Drawing.Size(222, 154);
            // 
            // pokażOperatMenuItem
            // 
            this.pokażOperatMenuItem.Name = "pokażOperatMenuItem";
            this.pokażOperatMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.V)));
            this.pokażOperatMenuItem.Size = new System.Drawing.Size(230, 22);
            this.pokażOperatMenuItem.Text = "Pokaż folder";
            this.pokażOperatMenuItem.ToolTipText = "Pokaż operat na dysku";
            this.pokażOperatMenuItem.Click += new System.EventHandler(this.pokażOperatMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(227, 6);
            // 
            // wczytajOperatMenuItem
            // 
            this.wczytajOperatMenuItem.Name = "wczytajOperatMenuItem";
            this.wczytajOperatMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.R)));
            this.wczytajOperatMenuItem.Size = new System.Drawing.Size(230, 22);
            this.wczytajOperatMenuItem.Text = "Odczytaj operat";
            this.wczytajOperatMenuItem.ToolTipText = "Odczytaj dane operatu z bazy danych Ośrodka";
            this.wczytajOperatMenuItem.Click += new System.EventHandler(this.wczytajOperatMenuItem_Click);
            // 
            // odczytajDokumentyMenuItem
            // 
            this.odczytajDokumentyMenuItem.Name = "odczytajDokumentyMenuItem";
            this.odczytajDokumentyMenuItem.Size = new System.Drawing.Size(221, 22);
            this.odczytajDokumentyMenuItem.Text = "Odczytaj dokumenty operatu";
            this.odczytajDokumentyMenuItem.Click += new System.EventHandler(this.odczytajDokumentyOperatuMenuItem_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(227, 6);
            // 
            // zapiszOperatMenuItem
            // 
            this.zapiszOperatMenuItem.Name = "zapiszOperatMenuItem";
            this.zapiszOperatMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.W)));
            this.zapiszOperatMenuItem.Size = new System.Drawing.Size(230, 22);
            this.zapiszOperatMenuItem.Text = "Zapisz operat";
            this.zapiszOperatMenuItem.ToolTipText = "Zapisz dokumenty i pliki operatu w bazie danych Ośrodka";
            this.zapiszOperatMenuItem.Click += new System.EventHandler(this.zapiszOperatMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(227, 6);
            // 
            // usuńOperatMenuItem
            // 
            this.usuńOperatMenuItem.Name = "usuńOperatMenuItem";
            this.usuńOperatMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.D)));
            this.usuńOperatMenuItem.Size = new System.Drawing.Size(230, 22);
            this.usuńOperatMenuItem.Text = "Usuń operat";
            this.usuńOperatMenuItem.Click += new System.EventHandler(this.usuńOperatMenuItem_Click);
            // 
            // panel
            // 
            this.panel.Controls.Add(this.operatView);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 24);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(1210, 413);
            this.panel.TabIndex = 2;
            // 
            // status
            // 
            this.status.BackColor = System.Drawing.SystemColors.Menu;
            this.status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.status.Location = new System.Drawing.Point(0, 415);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(1210, 22);
            this.status.TabIndex = 3;
            this.status.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(44, 17);
            this.statusLabel.Text = "Gotowe";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1210, 437);
            this.Controls.Add(this.status);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.menu);
            this.MainMenuStrip = this.menu;
            this.Name = "MainForm";
            this.Text = "OperatDoOśrodka v1.0-beta - Importuj zeskanowane operaty do Ośrodka (27 grudnia 2" +
    "016)";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.operatMenu.ResumeLayout(false);
            this.panel.ResumeLayout(false);
            this.status.ResumeLayout(false);
            this.status.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem plikMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zakończStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pomocMenuItem;
        private System.Windows.Forms.ToolStripMenuItem czytajToMenuItem;
        private System.Windows.Forms.ListView operatView;
        private System.Windows.Forms.ColumnHeader operatHeader;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.StatusStrip status;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripMenuItem wczytajOperatyMenuItem;
        private System.Windows.Forms.ContextMenuStrip operatMenu;
        private System.Windows.Forms.ToolStripMenuItem zapiszOperatMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pokażOperatMenuItem;
        private System.Windows.Forms.ColumnHeader folderHeader;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ColumnHeader idHeader;
        private System.Windows.Forms.ToolStripMenuItem ośrodekMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ośrodekOperatyMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ośrodekPlikiMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem wczytajOperatMenuItem;
        private System.Windows.Forms.ColumnHeader typHeader;
        private System.Windows.Forms.ToolStripMenuItem widokMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zaznaczWszystkoMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem zaznaczOdczytaneMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zaznaczNieodczytaneToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem odwróćZaznaczenieMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem pokażZaznaczenieMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem usuńOperatMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ColumnHeader plikiHeader;
        private System.Windows.Forms.ColumnHeader rozmiarHeader;
        private System.Windows.Forms.ColumnHeader dokumentyHeader;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem operatyBezDokumentówMenuItem;
        private System.Windows.Forms.ToolStripMenuItem odczytajDokumentyMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
    }
}

