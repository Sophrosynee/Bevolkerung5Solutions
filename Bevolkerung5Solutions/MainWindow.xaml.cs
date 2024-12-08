using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

//_1 Bevíz László
//_2 Erdős Barnabás
//_3 Bobák Kornél
//_4 Dombóvári Máté
//_5 Marcalek Máté


namespace Bevolkerung5Solutions;
public partial class MainWindow : Window
{
    List<Allampolgar> lakossag = [];
    const int feladatSzam = 45;
    System.Globalization.CultureInfo german = System.Globalization.CultureInfo.GetCultureInfo("de-DE");
    Random rnd = new Random();

    #region Logika
    public MainWindow()
    {
        InitializeComponent();
        AdatokFeltoltese();
        DataContext = lakossag;
        MegoldasTeljes.Loaded += (s, e) => DataGridMagassagBeallitas();
    }

    private void DataGridMagassagBeallitas()
    {
        MegoldasTeljes.Height = MegoldasTeljes.ColumnHeaderHeight + MegoldasTeljes.RowHeight * 15;
    }

    private void AdatokFeltoltese()
    {
        using (StreamReader sr = new StreamReader(path: @"..\..\..\src\bevolkerung.txt", encoding: Encoding.UTF8))
        {
            _ = sr.ReadLine();
            while (!sr.EndOfStream)
                lakossag.Add(new Allampolgar(sr.ReadLine()));
        }
    }

    private void ComboFeltoltese(object sender, RoutedEventArgs e)
    {
        ComboBox combo = sender as ComboBox;
        combo.Items.Add("-");
        for (int i = 1; i <= feladatSzam; i++)
        {
            combo.Items.Add($"{combo.Tag}/{i}");
        }
    }

    private void Nullazas()
    {
        MegoldasMondatos.Content = string.Empty;
        MegoldasTeljes.ItemsSource = null;
        MegoldasLista.Items.Clear();
    }

    private void FeladatokCombo_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        Nullazas();
        var combo = (ComboBox)sender;

        var methodName = $"Feladat{combo.SelectedIndex}_{combo.Tag}";
        var method = GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
        method?.Invoke(this, null);
    }
    #endregion

    #region Bevíz
    private void Feladat1_1()
    {
        MegoldasMondatos.Content = $"Legmagasabb nettó éves jövedelem: {lakossag.OrderByDescending(l => l.NettoJovedelem).First().NettoJovedelem.ToString("C", german)}";
    }
    private void Feladat2_1()
    {
        MegoldasMondatos.Content = $"Állampolgárok átlagos nettó éves jövedelme: {lakossag.Average(a => a.NettoJovedelem).ToString("C", german)}"; 
    }
    private void Feladat3_1()
    {
        foreach (IGrouping<string, Allampolgar> tartomany in lakossag.GroupBy(l => l.Tartomany))
            MegoldasLista.Items.Add($"{tartomany.Key} - {tartomany.Count()} fő");
    }
    private void Feladat4_1()
    {
        MegoldasTeljes.ItemsSource = lakossag.Where(x => x.Nemzetiseg == "angolai");
    }
    private void Feladat5_1()
    {
        int legfiatalabbKor = lakossag.OrderBy(x => x.Kor).First().Kor;
        MegoldasTeljes.ItemsSource = lakossag.Where(x => x.Kor == legfiatalabbKor);
    }
    private void Feladat6_1()
    {
        foreach (Allampolgar dohanyzo in lakossag.Where(a => !a.Dohanyzik))
            MegoldasLista.Items.Add($"{dohanyzo.Id} - {dohanyzo.HaviNettoJovedelem.ToString("C", german)}");
    }
    private void Feladat7_1()
    {
        MegoldasTeljes.ItemsSource = lakossag.Where(x => x.Tartomany == "Bajorország" && x.NettoJovedelem > 30000)
                                             .OrderBy(o => o.IskolaiVegzettseg);
    }
    private void Feladat8_1()
    {
        foreach (Allampolgar ferfi in lakossag.Where(a => a.Nem == "férfi"))
            MegoldasLista.Items.Add(ferfi.ToString(true));
    }
    private void Feladat9_1()
    {
        foreach (Allampolgar bajorNo in lakossag.Where(a => a.Tartomany == "Bajorország" && a.Nem == "nő"))
            MegoldasLista.Items.Add(bajorNo.ToString(false));
    }
    private void Feladat10_1()
    {
        MegoldasTeljes.ItemsSource = lakossag.Where(x => !x.Dohanyzik).OrderByDescending(j => j.NettoJovedelem).Take(10);
    }
    private void Feladat11_1()
    {
        MegoldasTeljes.ItemsSource = lakossag.OrderByDescending(x => x.Kor).Take(5);
    }
    private void Feladat12_1()
    {
        foreach (IGrouping<string, Allampolgar> nemetNepcsoport in lakossag.Where(x => x.Nemzetiseg == "német").GroupBy(n => n.Nepcsoport))
        {
            MegoldasLista.Items.Add(nemetNepcsoport.Key);
            foreach (Allampolgar polgar in nemetNepcsoport)
                MegoldasLista.Items.Add($"\t{(polgar.AktivSzavazo ? "aktív szavazó" : "nem aktív szavazó")}\t {polgar.PolitikaiNezet}");
        }
    }
    private void Feladat13_1()
    {
        MegoldasMondatos.Content = $"Férfi évi átlagos sörfogyasztás: {lakossag.Where(x => x.Nem == "férfi" && x.SorFogyasztasEventeSzoveges != "NA").Average(a => a.SorFogyasztasEvente):.00} l";
    }
    private void Feladat14_1()
    {
        MegoldasTeljes.ItemsSource = lakossag.OrderBy(x => x.IskolaiVegzettseg);
    }
    private void Feladat15_1()
    {
        foreach (Allampolgar gazdag in lakossag.OrderByDescending(x => x.NettoJovedelem).Take(3))
            MegoldasLista.Items.Add($"{gazdag.ToString(false)}");

        MegoldasLista.Items.Add("");

        foreach (Allampolgar szegeny in lakossag.OrderBy(x => x.NettoJovedelem).Take(3))
            MegoldasLista.Items.Add($"{szegeny.ToString(false)}");
    }
    private void Feladat16_1()
    {
        MegoldasMondatos.Content = $"Az állampogárok {Math.Round((decimal)lakossag.Count(x => x.AktivSzavazo) / lakossag.Count() * 100, 2)}%-a aktív szavazó";
    }
    private void Feladat17_1()
    {
        MegoldasTeljes.ItemsSource = lakossag.Where(x => x.AktivSzavazo).OrderBy(t => t.Tartomany);
    }
    private void Feladat18_1()
    {
        MegoldasMondatos.Content = $"Az állampolgárok átlagos életkora {lakossag.Average(x => x.Kor):.00} év";
    }
    private void Feladat19_1()
    {
        var legmagasabb = lakossag
            .GroupBy(t => t.Tartomany)
            .OrderByDescending(o => o.Average(a => a.NettoJovedelem))
            .ThenByDescending(o => o.Count())
            .First();

        MegoldasMondatos.Content = $"Legmagasabb átlagos éves nettó jövedelem: {legmagasabb.Key} {legmagasabb.ToList().Average(x => x.NettoJovedelem).ToString("C", german)} ({legmagasabb.Count()} fő)";
    }
    private void Feladat20_1()
    {
        List<Allampolgar> sorban = lakossag.OrderBy(x => x.Suly).ToList();
        double median;
        if (lakossag.Count % 2 == 0)
            median = (double)(sorban[lakossag.Count / 2].Suly + sorban[lakossag.Count / 2 + 1].Suly) / 2;
        else median = sorban[lakossag.Count / 2 + 1].Suly;

        MegoldasLista.Items.Add($"Az állampolgárok testsúlyának átlaga {lakossag.Average(x => x.Suly):.00} kg");
        MegoldasLista.Items.Add($"Az állampolgárok testsúlyának mediánja {median:.00} kg");
    }
    private void Feladat21_1()
    {
        double aktivSor = lakossag.Where(x => x.AktivSzavazo && x.SorFogyasztasEventeSzoveges != "NA").Average(a => a.SorFogyasztasEvente);
        double inaktivSor = lakossag.Where(x => !x.AktivSzavazo && x.SorFogyasztasEventeSzoveges != "NA").Average(a => a.SorFogyasztasEvente);

        MegoldasLista.Items.Add($"Aktív szavazók átlagos sörfogyasztása: {aktivSor:.00} l");
        MegoldasLista.Items.Add($"Nem szavazók átlagos sörfogyasztása: {inaktivSor:.00} l");

        if (aktivSor > inaktivSor)
            MegoldasLista.Items.Add("Az aktív szavazók fogyasztanak több sört");
        else if (aktivSor < inaktivSor)
            MegoldasLista.Items.Add("Az inaktív szavazók fogyasztanak több sört");
        else MegoldasLista.Items.Add("Az aktív és inaktív szavazók ugyanannyi sört fogyasztanak");
    }
    private void Feladat22_1()
    {
        MegoldasLista.Items.Add($"Férfiak átlagmagassága: {lakossag.Where(x => x.Nem == "férfi").Average(m => m.Magassag):.00} cm");
        MegoldasLista.Items.Add($"Nők átlagmagassága: {lakossag.Where(x => x.Nem == "nő").Average(m => m.Magassag):.00} cm");
    }
    private void Feladat23_1()
    {
        IGrouping<string, Allampolgar> legtobbNepcsoport = lakossag
            .Where(x => x.Nemzetiseg == "német")
            .GroupBy(n => n.Nepcsoport)
            .OrderByDescending(o => o.Count())
            .ThenByDescending(o => o.Average(k => k.Kor))
            .First();

        MegoldasMondatos.Content = $"A legtöbb német állampolgár a(z) {legtobbNepcsoport.Key} népcsoportba tartozik ({legtobbNepcsoport.Count()} fő)";
    }
    private void Feladat24_1()
    {
        double dohanyzo = lakossag.Where(x => x.Dohanyzik).Average(a => a.NettoJovedelem);
        double nemDohanyzo = lakossag.Where(x => !x.Dohanyzik).Average(a => a.NettoJovedelem);

        MegoldasLista.Items.Add($"Dohányzók átlagos nettó éves jövedelme: {dohanyzo.ToString("C", german)}");
        MegoldasLista.Items.Add($"Nem dohányzók átlagos nettó éves jövedelme: {nemDohanyzo.ToString("C", german)}");

        if (dohanyzo > nemDohanyzo)
            MegoldasLista.Items.Add("A dohányzóknak több az átlagos nettó éves jövedelme");
        else if (dohanyzo < nemDohanyzo)
            MegoldasLista.Items.Add("A nem dohányzóknak több az átlagos nettó éves jövedelme");
        else MegoldasLista.Items.Add("A dohányzó és nem dohányzók átlagos nettó éves jövedelme azonos.");
    }
    private void Feladat25_1()
    {
        double atlagKrumpli = lakossag.Where(x => x.KrumpliFogyasztasEventeSzoveges != "NA").Average(a => a.KrumpliFogyasztasEvente);

        MegoldasMondatos.Content = $"Az állampolgárok átlagos krumplifogyasztása {atlagKrumpli:.00} kg";
        MegoldasTeljes.ItemsSource = lakossag
            .Where(x => x.KrumpliFogyasztasEvente > atlagKrumpli)
            .OrderByDescending(k => k.KrumpliFogyasztasEvente)
            .Take(15);
    }
    private void Feladat26_1()
    {
        foreach (IGrouping<string, Allampolgar> tartomany in lakossag.GroupBy(t => t.Tartomany))
            MegoldasLista.Items.Add($"{tartomany.Key} - {tartomany.Average(k => k.Kor):.00} átlagév");
    }
    private void Feladat27_1()
    {
        MegoldasLista.Items.Add($"50 évnél idősebbek száma: {lakossag.Count(k => k.Kor > 50)} fő\n");
        foreach (Allampolgar otvenes in lakossag.Where(k => k.Kor > 50))
            MegoldasLista.Items.Add($"{otvenes.ToString(true)}");
    }
    private void Feladat28_1()
    {
        List<Allampolgar> dohanyzoNok = lakossag.Where(x => x.Nem == "nő" && x.Dohanyzik).ToList();

        if (dohanyzoNok.Count > 0)
        {
            MegoldasLista.Items.Add($"Maximális jövedelem: {dohanyzoNok.OrderByDescending(n => n.NettoJovedelem).First().NettoJovedelem.ToString("C", german)}\n");

            foreach (Allampolgar dohanyzoNo in lakossag.Where(x => x.Nem == "nő" && x.Dohanyzik))
                MegoldasLista.Items.Add(dohanyzoNo.ToString(false));
        }
        else MegoldasLista.Items.Add("Nincs dohányzó nő");
    }
    private void Feladat29_1()
    {
        foreach (IGrouping<string, Allampolgar> tartomany in lakossag.GroupBy(t => t.Tartomany))
        {
            Allampolgar legnagyobbAlkesz = tartomany
                .Where(a => a.SorFogyasztasEventeSzoveges != "NA")
                .OrderByDescending(s => s.SorFogyasztasEvente).First();

            MegoldasLista.Items.Add($"{tartomany.Key} - {legnagyobbAlkesz.Id} ({legnagyobbAlkesz.SorFogyasztasEvente}l sör)");
        }
    }
    private void Feladat30_1()
    {
        MegoldasLista.Items.Add(lakossag.Where(n => n.Nem == "nő").OrderByDescending(k => k.Kor).First().ToString(true));
        MegoldasLista.Items.Add(lakossag.Where(n => n.Nem == "férfi").OrderByDescending(k => k.Kor).First().ToString(true));
    }
    private void Feladat31_1()
    {
        foreach (string nemzetiseg in lakossag.Select(x => x.Nemzetiseg).Distinct().OrderDescending())
            MegoldasLista.Items.Add(nemzetiseg);
    }
    private void Feladat32_1()
    {
        foreach (IGrouping<string, Allampolgar> tartomany in lakossag.GroupBy(t => t.Tartomany).OrderBy(x => x.Count()))
            MegoldasLista.Items.Add(tartomany.Key);
    }
    private void Feladat33_1()
    {
        foreach (Allampolgar topJovedelmu in lakossag.OrderByDescending(j => j.NettoJovedelem).Take(3))
            MegoldasLista.Items.Add($"{topJovedelmu.Id}\t{topJovedelmu.NettoJovedelem.ToString("C", german)}");
    }
    private void Feladat34_1()
    {
        MegoldasMondatos.Content = $"55 kg feletti krumplifogyasztók átlagos súlya: {lakossag
            .Where(k => k.KrumpliFogyasztasEvente > 55 && k.Nem == "férfi")
            .Average(s => s.Suly):.00} kg";
    }
    private void Feladat35_1()
    {
        foreach (IGrouping<string, Allampolgar> tartomany in lakossag.GroupBy(t => t.Tartomany))
            MegoldasLista.Items.Add($"{tartomany.Key} - {tartomany.OrderBy(k => k.Kor).First().Kor} éves");
    }
    private void Feladat36_1()
    {
        foreach (string nemzetiseg in lakossag.Select(x => x.Nemzetiseg).Distinct())
            foreach (string tartomany in lakossag.Where(x => x.Nemzetiseg == nemzetiseg).Select(x => x.Tartomany).Distinct())
                MegoldasLista.Items.Add($"{nemzetiseg} - {tartomany}");
    }
    private void Feladat37_1()
    {
        double atlag = lakossag.Average(x => x.NettoJovedelem);

        MegoldasLista.Items.Add($"Átlagos jövedelem: {atlag.ToString("C", german)}");
        MegoldasLista.Items.Add($"Átlagon felül keresők: {lakossag.Count(x => x.NettoJovedelem > atlag)} db\n");

        foreach (Allampolgar atlagFolotti in lakossag.Where(x => x.NettoJovedelem > atlag))
            MegoldasLista.Items.Add(atlagFolotti.ToString(false));
    }
    private void Feladat38_1()
    {
        MegoldasLista.Items.Add($"Nők száma: {lakossag.Count(x => x.Nem == "nő")}");
        MegoldasLista.Items.Add($"Férfiak száma: {lakossag.Count(x => x.Nem == "férfi")}");
    }
    private void Feladat39_1()
    {
        foreach (IGrouping<string, Allampolgar> tartomany in lakossag.GroupBy(t => t.Tartomany).OrderBy(o => o.Max(m => m.NettoJovedelem)))
            MegoldasLista.Items.Add($"{tartomany.Key} - {tartomany.Max(m => m.NettoJovedelem).ToString("C", german)}");
    }
    private void Feladat40_1()
    {
        double nemetekHaviJovedelme = lakossag.Where(x => x.Nemzetiseg == "német").Sum(s => s.HaviNettoJovedelem);
        double nemNemetekHaviJovedelme = lakossag.Where(x => x.Nemzetiseg != "német").Sum(s => s.HaviNettoJovedelem);

        double abszKulonbseg = Math.Abs(nemetekHaviJovedelme - nemNemetekHaviJovedelme);
        double atlag = (nemetekHaviJovedelme + nemNemetekHaviJovedelme) / 2;

        double szazalekosKulonbseg = abszKulonbseg / atlag * 100;

        MegoldasLista.Items.Add($"Németek havi jövedelme: {nemetekHaviJovedelme.ToString("C", german)}");
        MegoldasLista.Items.Add($"Nem németek havi jövedelme: {nemNemetekHaviJovedelme.ToString("C", german)}\n");
        MegoldasLista.Items.Add($"Százalékos különbség: {szazalekosKulonbseg:.00}%");
    }

    private void Feladat41_1()
    {
        if (lakossag.Any(n => n.Nemzetiseg == "török" && n.AktivSzavazo))
            MegoldasTeljes.ItemsSource = lakossag.Where(n => n.Nemzetiseg == "török" && n.AktivSzavazo).OrderBy(a => Guid.NewGuid()).Take(rnd.Next(1, 11));
        else
            MegoldasMondatos.Content = "Nincs török aktív szavazó";
    }

    private void Feladat42_1()
    {
        double altagosSorfogyasztas = lakossag
            .Where(x => x.SorFogyasztasEventeSzoveges != "NA")
            .Average(a => a.SorFogyasztasEvente);

        List<Allampolgar> otAtlagFelettiSorfogyaszto = lakossag
            .Where(x => x.SorFogyasztasEvente > altagosSorfogyasztas)
            .OrderBy(a => Guid.NewGuid())
            .Take(5).ToList();

        MegoldasLista.Items.Add($"Átlagos sörfogyasztás: {altagosSorfogyasztas:.00} l\n");
        MegoldasLista.Items.Add("5 véletlenszerű állampolgár, akik az átlagosnál több sört fogyasztanak:");
        foreach (Allampolgar polgar in otAtlagFelettiSorfogyaszto)
            MegoldasLista.Items.Add($"{polgar.ToString(true)}");
    }

    private void Feladat43_1()
    {
        double atlagosJovedelem = lakossag.Average(x => x.NettoJovedelem);
        var nagyobbTartomanyok = lakossag
            .GroupBy(t => t.Tartomany)
            .Where(t => t.Min(m => m.NettoJovedelem) > atlagosJovedelem)
            .OrderBy(a => Guid.NewGuid())
            .Take(2);

        MegoldasLista.Items.Add($"Átlagos nettó jövedelem: {atlagosJovedelem.ToString("C", german)}\n");
        foreach (IGrouping<string, Allampolgar> tartomany in nagyobbTartomanyok)
            MegoldasLista.Items.Add($"{tartomany.Key} - legkisebb nettó jövedelem: {tartomany.Min(m => m.NettoJovedelem).ToString("C", german)}");
    }

    private void Feladat44_1()
    {
        MegoldasTeljes.ItemsSource = lakossag.Where(x => x.IskolaiVegzettseg == "").OrderBy(a => Guid.NewGuid()).Take(3);
    }

    private void Feladat45_1()
    {
        MegoldasTeljes.ItemsSource = lakossag.Where(x => x.Nem == "nő" && x.Nepcsoport != "bajor" && x.IskolaiVegzettseg == "Universität").Take(5);

        double elsoFizetese = lakossag.Where(x => x.Nem == "nő" && x.Nepcsoport != "bajor" && x.IskolaiVegzettseg == "Universität").First().NettoJovedelem;

        foreach (Allampolgar polgar in lakossag.Where(x => x.Nem == "nő" && x.NettoJovedelem > elsoFizetese).OrderBy(a => Guid.NewGuid()).Take(3))
            MegoldasLista.Items.Add($"{polgar.ToString(true)}");
    }

    #endregion

    #region Erdős
    private void Feladat1_2()
    {
        MegoldasMondatos.Content = $"Legmagasabb netto jövedelem: {lakossag.Max(x => x.NettoJovedelem)}";
    }

    private void Feladat2_2()
    {
        MegoldasMondatos.Content = $"Átlag éves netto jövedelem : {lakossag.Average(l => l.NettoJovedelem):.00}";
    }

    private void Feladat3_2()
    {
        var nemzetisegek = lakossag.GroupBy(l => l.Nemzetiseg).Select(g => new { Nemzetiseg = g.Key, db = g.Count() });
        foreach (var nemzetiseg in nemzetisegek)
        {
            MegoldasLista.Items.Add($"{nemzetiseg.Nemzetiseg} {nemzetiseg.db} db");
        }
    }

    private void Feladat4_2()
    {
        MegoldasTeljes.ItemsSource = lakossag.Where(l => l.Nemzetiseg.Equals("angolai", StringComparison.OrdinalIgnoreCase));
    }

    private void Feladat5_2()
    {
        MegoldasTeljes.ItemsSource = lakossag.Where(l => l.SzuletesiEv == lakossag.Min(x => x.SzuletesiEv));
    }

    private void Feladat6_2()
    {
        var nemDohanyzik = lakossag.Where(a => !a.Dohanyzik);
        foreach (var lakos in nemDohanyzik)
        {
            MegoldasLista.Items.Add($"{lakos.Id}-{lakos.HaviNettoJovedelem}");
        }
    }

    private void Feladat7_2()
    {
        var bajorLakosok = lakossag.Where(a => a.Tartomany.Equals("bajorország", StringComparison.OrdinalIgnoreCase) && a.NettoJovedelem > 30_000).OrderBy(l => l.IskolaiVegzettseg);
        MegoldasTeljes.ItemsSource = bajorLakosok;
    }

    private void Feladat8_2()
    {
        var ferfiak = lakossag.Where(l => l.Nem.Equals("férfi", StringComparison.OrdinalIgnoreCase));
        foreach (var lakos in ferfiak)
        {
            MegoldasLista.Items.Add(lakos.ToString(true));
        };
    }

    private void Feladat9_2()
    {
        var bajorNok = lakossag.Where(l => l.Nem.Equals("nő", StringComparison.OrdinalIgnoreCase) && l.Tartomany.Equals("bajorország", StringComparison.OrdinalIgnoreCase));
        foreach (var no in bajorNok)
        {
            MegoldasLista.Items.Add(no.ToString(false));
        }
    }

    private void Feladat10_2()
    {
        var legmagasbbJovedelem = lakossag.Where(l => !l.Dohanyzik).OrderBy(l => l.NettoJovedelem).Take(10);
        MegoldasTeljes.ItemsSource = legmagasbbJovedelem;
    }

    private void Feladat11_2()
    {
        var legidosebb = lakossag.OrderByDescending(l => l.Kor).Take(5);
        MegoldasTeljes.ItemsSource = legidosebb;
    }

    private void Feladat12_2()
    {
        var nemetAllampolgarok = lakossag
       .Where(l => l.Nemzetiseg.Equals("német", StringComparison.OrdinalIgnoreCase))
       .GroupBy(l => l.Nepcsoport);

        foreach (var csoport in nemetAllampolgarok)
        {
            MegoldasLista.Items.Add(csoport.Key);
            foreach (var allampolgar in csoport)
            {
                var aktivSzavazo = allampolgar.AktivSzavazo ? "aktív szavazó" : "nem aktív szavazó";
                MegoldasLista.Items.Add($"  - {aktivSzavazo}, {allampolgar.PolitikaiNezet}");
            }
        }
    }

    private void Feladat13_2()
    {
        var atlagsorFogyasztas = lakossag
            .Where(l => l.Nem.Equals("férfi", StringComparison.OrdinalIgnoreCase) && int.TryParse(l.SorFogyasztasEventeSzoveges, out _))
            .Average(l => int.Parse(l.SorFogyasztasEventeSzoveges));
        MegoldasMondatos.Content = $"Átlagos sör fogyasztás: {atlagsorFogyasztas:.00}";
    }

    private void Feladat14_2()
    {
        var iskolaiVegzettseg = lakossag.GroupBy(l => l.IskolaiVegzettseg).OrderBy(i => i.Key);
        foreach (var csoport in iskolaiVegzettseg)
        {
            MegoldasLista.Items.Add(csoport.Key ?? "N/A");
            foreach (var allampolgar in csoport)
            {
                MegoldasLista.Items.Add($"  - {allampolgar.Id}");
            }
        }
    }

    private void Feladat15_2()
    {
        var legmagasabbak = lakossag.OrderByDescending(l => l.NettoJovedelem).Take(3);
        var legalacsonyabbak = lakossag.OrderBy(l => l.NettoJovedelem).Take(3);
        foreach (var allampolgar in legmagasabbak)
        {
            MegoldasLista.Items.Add(allampolgar.ToString(false));
        }
        foreach (var allampolgar in legalacsonyabbak)
        {
            MegoldasLista.Items.Add(allampolgar.ToString(false));
        }
    }

    private void Feladat16_2()
    {
        MegoldasMondatos.Content = $"Az állampolágrok {lakossag.Count(l => l.AktivSzavazo) / (double)lakossag.Count * 100:.00}% aktív szavazó";
    }

    private void Feladat17_2()
    {
        var szavazok = lakossag.Where(l => l.AktivSzavazo).OrderBy(l => l.Tartomany);
        MegoldasTeljes.ItemsSource = szavazok;
    }

    private void Feladat18_2()
    {
        MegoldasMondatos.Content = $"Az állampolágrok átlagos életkora: {lakossag.Average(l => l.Kor):.00}";
    }

    private void Feladat19_2()
    {
        var tartomanyok = lakossag.GroupBy(l => l.Tartomany)
            .Select(g => new
            {
                Tartomany = g.Key,
                AtlagJovedelem = g.Average(l => l.NettoJovedelem),
                LakosokSzama = g.Count()
            })
            .OrderByDescending(t => t.AtlagJovedelem)
            .ThenByDescending(t => t.LakosokSzama)
            .First();
        MegoldasMondatos.Content = $"{tartomanyok.Tartomany} - {tartomanyok.AtlagJovedelem:.00} - {tartomanyok.LakosokSzama} db";
    }

    private void Feladat20_2()
    {
        var sulyok = lakossag.Select(l => l.Suly).OrderBy(s => s).ToList();
        var atlagSuly = lakossag.Average(l => l.Suly);
        var medianSuly = sulyok.Count % 2 == 0
                            ? sulyok.Skip(sulyok.Count / 2 - 1).Take(2).Average()
                            : sulyok.Skip(sulyok.Count / 2).First();

        MegoldasMondatos.Content = $"Átlagos súly: {atlagSuly:.00} - Medián súly: {medianSuly}";
    }

    private void Feladat21_2()
    {
        var sorAktiv = lakossag.Where(l => l.AktivSzavazo && int.TryParse(l.SorFogyasztasEventeSzoveges, out _)).Average(l => int.Parse(l.SorFogyasztasEventeSzoveges));
        var sorNemAktiv = lakossag.Where(l => !l.AktivSzavazo && int.TryParse(l.SorFogyasztasEventeSzoveges, out _)).Average(l => int.Parse(l.SorFogyasztasEventeSzoveges));
        MegoldasLista.Items.Add($"Nem aktív szavazók átlagos sör fogyasztása: {sorNemAktiv:.00} - Aktív szavazók átlagos sör fogyasztása: {sorAktiv:.00}");
        MegoldasLista.Items.Add(sorAktiv < sorNemAktiv ? "Nem aktív szavazók fogyasztanak több sört" : "Aktív szavazók fogyasztanak több sört");
    }

    private void Feladat22_2()
    {
        var atlagMagassagFerfiak = lakossag.Where(l => l.Nem.Equals("férfi", StringComparison.OrdinalIgnoreCase)).Average(l => l.Magassag);
        var atlagMagassagNok = lakossag.Where(l => l.Nem.Equals("nő", StringComparison.OrdinalIgnoreCase)).Average(l => l.Magassag);
        MegoldasMondatos.Content = $"Átlagos magasság: Férfiak: {atlagMagassagFerfiak:.00} cm - Nők: {atlagMagassagNok:.00} cm";
    }

    private void Feladat23_2()
    {
        var nepcsoportok = lakossag.GroupBy(l => l.Nepcsoport)
            .Select(g => new
            {
                Nepcsoport = g.Key,
                LakosokSzama = g.Count(x => x.Nepcsoport != null),
                AtlagKor = g.Average(l => l.Kor)
            })
            .OrderByDescending(n => n.LakosokSzama)
            .ThenByDescending(g => g.AtlagKor)
            .First();
        MegoldasMondatos.Content = $"Népcsoport: {nepcsoportok.Nepcsoport ?? "N/A"} Lakosok {nepcsoportok.LakosokSzama}";
    }

    private void Feladat24_2()
    {
        var dohanyzok = lakossag.Where(l => l.Dohanyzik).Average(l => l.NettoJovedelem);
        var nemDohanyzok = lakossag.Where(l => !l.Dohanyzik).Average(l => l.NettoJovedelem);
        MegoldasMondatos.Content = $"Dohányzók átlagos jövedelme: {dohanyzok:.00} - Nem dohányzók átlagos jövedelme: {nemDohanyzok:.00}";
    }

    private void Feladat25_2()
    {
        var atlagKrumplifogyasztas = lakossag.Where(l => int.TryParse(l.KrumpliFogyasztasEventeSzoveges, out _)).Average(l => int.Parse(l.KrumpliFogyasztasEventeSzoveges));
        MegoldasMondatos.Content = $"Átlagos krumpli fogyasztás: {atlagKrumplifogyasztas:.00}";
        var atlagFeletFogyasztok = lakossag.Where(l => int.TryParse(l.KrumpliFogyasztasEventeSzoveges, out int x) && x > atlagKrumplifogyasztas).OrderByDescending(a => int.Parse(a.KrumpliFogyasztasEventeSzoveges)).Take(15);
        MegoldasTeljes.ItemsSource = atlagFeletFogyasztok;
    }

    private void Feladat26_2()
    {
        var atlageletkor = lakossag.GroupBy(k => k.Tartomany);

        foreach (var item in atlageletkor)
        {
            MegoldasLista.Items.Add($"{item.Key} - {item.Average(k => k.Kor):.00} év");
        }
    }

    private void Feladat27_2()
    {
        var idossebb = lakossag.Where(l => l.Kor > 50);
        MegoldasMondatos.Content = $"50 év feletti állampolgárok száma: {idossebb.Count()}";
        foreach (var item in idossebb)
        {
            MegoldasLista.Items.Add(item.ToString(false));
        }
    }

    private void Feladat28_2()
    {
        var dohanyzonok = lakossag.Where(l => l.Dohanyzik);
        MegoldasMondatos.Content = $"Dohányzó nők száma: {dohanyzonok.Count()}";
        foreach (var item in dohanyzonok)
        {
            MegoldasLista.Items.Add(item.ToString(false));
        }
    }

    private void Feladat29_2()
    {
        var legnagyobbSorfogyaszto = lakossag.GroupBy(l => l.Tartomany)
            .Select(g => new
            {
                Tartomany = g.Key,
                LegnagyobbSorfogyaszto = g.OrderByDescending(l => int.TryParse(l.SorFogyasztasEventeSzoveges, out int x) ? x : 0).First()
            });
        foreach (var item in legnagyobbSorfogyaszto)
        {
            MegoldasLista.Items.Add($"{item.Tartomany} - {item.LegnagyobbSorfogyaszto.Id} - {item.LegnagyobbSorfogyaszto.SorFogyasztasEvente}");
        }
    }

    private void Feladat30_2()
    {
        MegoldasLista.Items.Add("Legidősebb férfi");
        MegoldasLista.Items.Add($"{lakossag.Where(l => l.Nem.Equals("férfi", StringComparison.OrdinalIgnoreCase)).MaxBy(k => k.Kor)!.ToString(true)}");
        MegoldasLista.Items.Add("Legidősebb nő");
        MegoldasLista.Items.Add($"{lakossag.Where(l => l.Nem.Equals("nő", StringComparison.OrdinalIgnoreCase)).MaxBy(k => k.Kor)!.ToString(true)}");
    }

    private void Feladat31_2()
    {
        MegoldasLista.ItemsSource = lakossag.Select(k => k.Nemzetiseg).Distinct().OrderDescending();
    }

    private void Feladat32_2()
    {
        MegoldasLista.ItemsSource = lakossag.GroupBy(l => l.Tartomany).OrderBy(l => l.Count()).Select(l => l.Key);
    }

    private void Feladat33_2()
    {
        var legmagassabbJovedelem = lakossag.OrderByDescending(l => l.NettoJovedelem).Take(3);
        foreach (var item in legmagassabbJovedelem)
        {
            MegoldasLista.Items.Add($"{item.Id}- {item.NettoJovedelem}");
        }
    }

    private void Feladat34_2()
    {
        var ferfiKrumpli = lakossag.Where(l => l.Nem.Equals("férfi", StringComparison.OrdinalIgnoreCase) && int.TryParse(l.KrumpliFogyasztasEventeSzoveges, out int x) && x < 55).Average(f => f.Suly);
        MegoldasMondatos.Content = $"Az 55kg feletti férfiak átlagos krumplifogyasztása: {ferfiKrumpli:.00}";
    }

    private void Feladat35_2()
    {
        var minKor = lakossag.GroupBy(l => l.Tartomany).Select(g => new { Tartomany = g.Key, MinKor = g.Min(l => l.Kor) });
        foreach (var item in minKor)
        {
            MegoldasLista.Items.Add($"{item.Tartomany} - {item.MinKor}");
        }
    }

    private void Feladat36_2()
    {
        var nemzetisegTartomany = lakossag.GroupBy(l => new { l.Nemzetiseg, l.Tartomany });
        foreach (var item in nemzetisegTartomany)
        {
            MegoldasLista.Items.Add($"{item.Key.Nemzetiseg} - {item.Key.Tartomany}");
        }
    }

    private void Feladat37_2()
    {
        double atlagJovedelem = lakossag.Average(l => l.NettoJovedelem);
        var atlagJovedelemnelTobb = lakossag.Where(l => l.NettoJovedelem > atlagJovedelem);
        MegoldasMondatos.Content = $"Átlag jövedelemet {atlagJovedelemnelTobb.Count()} lakos haladja meg.";

        foreach (var item in atlagJovedelemnelTobb)
        {
            MegoldasLista.Items.Add(item.ToString(false));
        }
    }

    private void Feladat38_2()
    {
        MegoldasMondatos.Content = $"Férfiak: {lakossag.Count(l => l.Nem.Equals("férfi"))} fő  Nők: {lakossag.Count(l => l.Nem.Equals("nő"))} fő";
    }

    private void Feladat39_2()
    {
        var x = lakossag.GroupBy(l => l.Tartomany).Select(g => new { Tartomany = g.Key, LegMagasabbJovedelem = g.Max(l => l.NettoJovedelem) }).OrderByDescending(l => l.LegMagasabbJovedelem);
        foreach (var item in x)
        {
            MegoldasLista.Items.Add($"{item.Tartomany} - {item.LegMagasabbJovedelem}");
        }
    }

    private void Feladat40_2()
    {
        var nemetek = lakossag.Where(l => l.Nemzetiseg.Equals("német", StringComparison.OrdinalIgnoreCase)).Sum(l => l.HaviNettoJovedelem);
        var nemNemetek = lakossag.Where(l => !l.Nemzetiseg.Equals("német", StringComparison.OrdinalIgnoreCase)).Sum(l => l.HaviNettoJovedelem);
        MegoldasMondatos.Content = $"Németek és nem németek jövedelmének aránya: {nemetek / (double)nemNemetek * 100:.00}%";
    }

    private void Feladat41_2()
    {
        var torokok = lakossag.Where(l => l.Nemzetiseg.Equals("török", StringComparison.OrdinalIgnoreCase)).OrderBy(l => Random.Shared.Next()).Take(10);
        MegoldasTeljes.ItemsSource = torokok;
    }

    private void Feladat42_2()
    {
        var atlagSor = lakossag.Where(l => int.TryParse(l.SorFogyasztasEventeSzoveges, out _)).Average(l => int.Parse(l.SorFogyasztasEventeSzoveges));
        var atlagFolottFogyasztas = lakossag.Where(l => int.TryParse(l.SorFogyasztasEventeSzoveges, out int x) && x > atlagSor).OrderByDescending(l => int.Parse(l.SorFogyasztasEventeSzoveges)).Take(10);
        foreach (var item in atlagFolottFogyasztas)
        {
            MegoldasLista.Items.Add(item.ToString(true));
        }
    }

    private void Feladat43_2()
    {
        double atlagJovedelem = lakossag.Average(l => l.NettoJovedelem);
        var tartomanyok = lakossag
        .GroupBy(l => l.Tartomany)
        .Where(g => g.Min(l => l.NettoJovedelem) > atlagJovedelem)
        .OrderBy(_ => Random.Shared.Next())
        .Take(2)
        .Select(g => new { Tartomany = g.Key, MinJovedelem = g.Min(l => l.NettoJovedelem) });

        foreach (var item in tartomanyok)
        {
            MegoldasLista.Items.Add($"{item.Tartomany} - {item.MinJovedelem}");
        }
    }

    private void Feladat44_2()
    {
        var iskolaiVegzettseg = lakossag.Where(l => l.IskolaiVegzettseg == null).OrderBy(_ => Random.Shared.Next()).Take(3);
        MegoldasTeljes.ItemsSource = iskolaiVegzettseg;
    }

    private void Feladat45_2()
    {
        var egyetemistak = lakossag.Where(l => l.IskolaiVegzettseg == "Universität" && l.Nem.Equals("nő", StringComparison.OrdinalIgnoreCase) && l.Nepcsoport != "bajor").Take(5);
        var legmagasabbJovedelem = egyetemistak.Max(l => l.NettoJovedelem);
        var nemetEgyetemistak = lakossag.Where(l => l.Nemzetiseg.Equals("német", StringComparison.OrdinalIgnoreCase) && l.NettoJovedelem > legmagasabbJovedelem).OrderBy(_ => Random.Shared.Next()).Take(3);
        MegoldasTeljes.ItemsSource = nemetEgyetemistak;
    }

    #endregion

    #region Bobák-nem figyelt az output controlra
    private void Feladat1_3()
    {
        MegoldasLista.Items.Clear();
        var legMagasabbNettoJovedelem = lakossag.OrderByDescending(x => x.NettoJovedelem).First();
        MegoldasLista.Items.Add(legMagasabbNettoJovedelem);
    }

    public void Feladat2_3()
    {
        MegoldasLista.Items.Clear();
        var atlagosNettoEvesJovedelem = Math.Round(lakossag.Average(x => x.NettoJovedelem), 2);
        MegoldasLista.Items.Add(atlagosNettoEvesJovedelem);
    }

    public void Feladat3_3()
    {
        MegoldasLista.Items.Clear();
        //tartományok és ott élő lakosok mennyisegenek csoportositasa
        var tartomanyok = lakossag.GroupBy(x => x.Tartomany).Select(x => new { Tartomany = x.Key, Lakosok = x.Count() });
        foreach (var item in tartomanyok)
        {
            MegoldasLista.Items.Add(item);
        }

    }

    public void Feladat4_3()
    {
        MegoldasLista.Items.Clear();
        var angilaiAllampolgarok = lakossag.Where(x => x.Nemzetiseg == "angolai").ToList();
        foreach (var item in angilaiAllampolgarok)
        {
            MegoldasLista.Items.Add(item);
        }

    }

    public void Feladat5_3()
    {
        MegoldasLista.Items.Clear();
        var legfiatalabb = lakossag.OrderByDescending(x => x.SzuletesiEv).First();
        var legfiatalabbak = lakossag.Where(x => x.SzuletesiEv == legfiatalabb.SzuletesiEv).ToList();
        foreach (var item in legfiatalabbak)
        {
            MegoldasLista.Items.Add(item);
        }
    }

    public void Feladat6_3()
    {
        MegoldasLista.Items.Clear();
        var nemdohanyzo = lakossag.Where(x => x.Dohanyzik == false).ToList();
        foreach (var item in nemdohanyzo)
        {
            MegoldasLista.Items.Add($"{item.Id}, {item.NettoJovedelem}");
        }
    }

    public void Feladat7_3()
    {
        MegoldasLista.Items.Clear();
        var f7 = lakossag.Where(x => x.Tartomany == "Bajorország" && x.NettoJovedelem > 30000).ToList();
        f7 = f7.OrderBy(x => x.IskolaiVegzettseg).ToList();
        foreach (var item in f7)
        {
            MegoldasLista.Items.Add(item);
        }
    }

    public void Feladat8_3()
    {
        MegoldasLista.Items.Clear();
        var ferfiak = lakossag.Where(x => x.Nem == "férfi").ToList();
        foreach (var item in ferfiak)
        {
            MegoldasLista.Items.Add(item.ToString(true));
        }

    }

    public void Feladat9_3()
    {
        MegoldasLista.Items.Clear();
        var bajornok = lakossag.Where(x => x.Tartomany == "Bajorország" && x.Nem == "nő").ToList();
        foreach (var item in bajornok)
        {
            MegoldasLista.Items.Add(item.ToString(false));
        }

    }

    public void Feladat10_3()
    {
        MegoldasLista.Items.Clear();
        var nemdohanyzok = lakossag.Where(x => x.Dohanyzik == false).ToList();
        nemdohanyzok = nemdohanyzok.OrderBy(x => x.NettoJovedelem).Take(10).ToList();
        foreach (var item in nemdohanyzok)
        {
            MegoldasLista.Items.Add(item);
        }

    }

    public void Feladat11_3()
    {
        MegoldasLista.Items.Clear();
        var otlegidosebb = lakossag.OrderByDescending(x => x.GetAge()).Take(5).ToList();
        foreach (var item in otlegidosebb)
        {
            MegoldasLista.Items.Add(item);
        }
    }

    public void Feladat12_3()
    {
        MegoldasLista.Items.Clear();
        var nemetek = lakossag.Where(x => x.Nemzetiseg == "német").GroupBy(x => x.Nepcsoport).ToList();
        foreach (var item in nemetek)
        {
            MegoldasLista.Items.Add(item.Key);
            foreach (var item2 in item)
            {
                MegoldasLista.Items.Add($"{(item2.AktivSzavazo ? "aktív szavazó" : "nem aktív szavazó")} - {item2.PolitikaiNezet}");
            }
        }

    }

    public void Feladat13_3()
    {
        MegoldasLista.Items.Clear();
        var ferfiak = lakossag.Where(x => x.Nem == "férfi").ToList();
        var validSorfogyasztas = ferfiak
            .Select(x => x.SorFogyasztasEventeSzoveges)
            .Where(value => int.TryParse(value, out _))
            .Select(int.Parse);

        if (validSorfogyasztas.Any())
        {
            var sorfogyasztas = validSorfogyasztas.Average();
            MegoldasLista.Items.Add(sorfogyasztas);
        }
    }

    public void Feladat14_3()
    {
        MegoldasLista.Items.Clear();
        var iskolaiVegzettseg = lakossag.GroupBy(x => x.IskolaiVegzettseg).ToList();
        foreach (var item in iskolaiVegzettseg)
        {
            MegoldasLista.Items.Add(item.Key);
            foreach (var item2 in item)
            {
                MegoldasLista.Items.Add(item2);
            }
        }

    }

    public void Feladat15_3()
    {
        MegoldasLista.Items.Clear();
        var legmagasabb = lakossag.OrderByDescending(x => x.NettoJovedelem).Take(3).ToList();
        var legalacsonyabb = lakossag.OrderBy(x => x.NettoJovedelem).Take(3).ToList();
        foreach (var item in legmagasabb)
        {
            MegoldasLista.Items.Add(item.ToString(false));
        }
        foreach (var item in legalacsonyabb)
        {
            MegoldasLista.Items.Add(item.ToString(false));
        }

    }

    public void Feladat16_3()
    {
        MegoldasLista.Items.Clear();
        var aktivSzavazok = lakossag.Count(x => x.AktivSzavazo);
        var osszes = lakossag.Count();
        var szazalek = Math.Round((double)aktivSzavazok / osszes * 100, 2);
        MegoldasLista.Items.Add(szazalek);

    }

    public void Feladat17_3()
    {
        MegoldasLista.Items.Clear();
        var aktivSzavazok = lakossag.Where(x => x.AktivSzavazo).OrderBy(x => x.Tartomany).ToList();
        foreach (var item in aktivSzavazok)
        {
            MegoldasLista.Items.Add(item);
        }

    }

    public void Feladat18_3()
    {
        MegoldasLista.Items.Clear();
        var atlagEletkor = Math.Round(lakossag.Average(x => x.GetAge()), 2);
        MegoldasLista.Items.Add(atlagEletkor);

    }

    public void Feladat19_3()
    {
        MegoldasLista.Items.Clear();
        var tartomanyok = lakossag.GroupBy(x => x.Tartomany).Select(x => new { Tartomany = x.Key, Atlag = x.Average(y => y.NettoJovedelem), Lakosok = x.Count() });
        var legmagasabb = tartomanyok.OrderByDescending(x => x.Atlag).First();
        MegoldasLista.Items.Add(legmagasabb);

    }

    public void Feladat20_3()
    {
        MegoldasLista.Items.Clear();
        var atlagSuly = Math.Round(lakossag.Average(x => x.Suly), 2);
        var sulyok = lakossag.Select(x => x.Suly).OrderBy(x => x).ToList();
        double median;
        if (sulyok.Count % 2 == 0)
        {
            median = (sulyok[sulyok.Count / 2] + sulyok[sulyok.Count / 2 - 1]) / 2;
        }
        else
        {
            median = sulyok[sulyok.Count / 2];
        }
        MegoldasLista.Items.Add($"Átlagos súly: {atlagSuly}, Medián súly: {median}");

    }

    public void Feladat21_3()
    {

        MegoldasLista.Items.Clear();
        var aktivSzavazok = lakossag.Where(x => x.AktivSzavazo).ToList();
        var nemAktivSzavazok = lakossag.Where(x => !x.AktivSzavazo).ToList();
        var aktivSor = aktivSzavazok.Select(x => x.SorFogyasztasEventeSzoveges).Where(value => int.TryParse(value, out _)).Select(int.Parse).Average();
        var nemAktivSor = nemAktivSzavazok.Select(x => x.SorFogyasztasEventeSzoveges).Where(value => int.TryParse(value, out _)).Select(int.Parse).Average();
        MegoldasLista.Items.Add($"Aktív szavazók átlagos sörfogyasztása: {aktivSor}");
        MegoldasLista.Items.Add($"Nem aktív szavazók átlagos sörfogyasztása: {nemAktivSor}");
        if (aktivSor > nemAktivSor)
        {
            MegoldasLista.Items.Add("Az aktív szavazók fogyasztanak több sört évente.");
        }
        else
        {
            MegoldasLista.Items.Add("A nem aktív szavazók fogyasztanak több sört évente.");
        }

    }

    public void Feladat22_3()
    {
        MegoldasLista.Items.Clear();
        var ferfiak = lakossag.Where(x => x.Nem == "férfi").ToList();
        var nok = lakossag.Where(x => x.Nem == "nő").ToList();
        var ferfiakMagassaga = ferfiak.Average(x => x.Magassag);
        var nokMagassaga = nok.Average(x => x.Magassag);
        MegoldasLista.Items.Add($"Férfiak átlagos magassága: {ferfiakMagassaga}");
        MegoldasLista.Items.Add($"Nők átlagos magassága: {nokMagassaga}");

    }

    public void Feladat23_3()
    {

        //olyanbol van a legtobb ahol nincsen megadva nepcsoport
        MegoldasLista.Items.Clear();
        var nepcsoportok = lakossag.GroupBy(x => x.Nepcsoport).Select(x => new { Nepcsoport = x.Key, Lakosok = x.Count() });
        var legtobb = nepcsoportok.OrderByDescending(x => x.Lakosok).First();
        if (nepcsoportok.Count(x => x.Lakosok == legtobb.Lakosok) > 1)
        {
            var atlagEletkor = lakossag.Where(x => x.Nepcsoport == legtobb.Nepcsoport).Average(x => x.GetAge());
            var legidosebb = nepcsoportok.Where(x => x.Lakosok == legtobb.Lakosok).OrderByDescending(x => lakossag.Where(y => y.Nepcsoport == x.Nepcsoport).Average(y => y.GetAge())).First();
            MegoldasLista.Items.Add(legidosebb);
        }
        else
        {
            MegoldasLista.Items.Add(legtobb);
        }

    }

    public void Feladat24_3()
    {

        MegoldasLista.Items.Clear();
        var dohanyzok = lakossag.Where(x => x.Dohanyzik).ToList();
        var nemdohanyzok = lakossag.Where(x => !x.Dohanyzik).ToList();
        var dohanyzoAtlag = dohanyzok.Average(x => x.NettoJovedelem);
        var nemdohanyzoAtlag = nemdohanyzok.Average(x => x.NettoJovedelem);
        MegoldasLista.Items.Add($"Dohányzók átlagos nettó éves jövedelme: {dohanyzoAtlag}");
        MegoldasLista.Items.Add($"Nem dohányzók átlagos nettó éves jövedelme: {nemdohanyzoAtlag}");

        if (dohanyzoAtlag > nemdohanyzoAtlag)
        {
            MegoldasLista.Items.Add("A dohányzók átlagos nettó éves jövedelme magasabb.");
        }
        else
        {
            MegoldasLista.Items.Add("A nem dohányzók átlagos nettó éves jövedelme magasabb.");
        }
    }

    public void Feladat25_3()
    {

        MegoldasLista.Items.Clear();
        var validKrumpliFogyasztas = lakossag
            .Select(x => x.KrumpliFogyasztasEventeSzoveges)
            .Where(value => int.TryParse(value, out _))
            .Select(int.Parse);

        if (validKrumpliFogyasztas.Any())
        {
            var atlag = validKrumpliFogyasztas.Average();
            var atlagFelettiek = lakossag.Where(x => int.TryParse(x.KrumpliFogyasztasEventeSzoveges, out int krumpli) && krumpli > atlag).Take(15).ToList();
            MegoldasLista.Items.Add($"Átlagos krumplifogyasztás: {atlag}");
            foreach (var item in atlagFelettiek)
            {
                MegoldasLista.Items.Add(item);
            }

        }

    }

    public void Feladat26_3()
    {
        MegoldasLista.Items.Clear();
        var tartomanyok = lakossag.GroupBy(x => x.Tartomany).Select(x => new { Tartomany = x.Key, Atlag = x.Average(y => y.GetAge()) });
        foreach (var item in tartomanyok)
        {
            MegoldasLista.Items.Add(item);
        }

    }

    public void Feladat27_3()
    {
        MegoldasLista.Items.Clear();
        var otvenEvesek = lakossag.Where(x => x.GetAge() >= 50).ToList();
        foreach (var item in otvenEvesek)
        {
            MegoldasLista.Items.Add($"{item.Id} {item.Nem} {item.SzuletesiEv} {item.Suly} {item.Magassag}");
        }
        MegoldasLista.Items.Add($"Összesen: {otvenEvesek}");

    }

    public void Feladat28_3()
    {
        MegoldasLista.Items.Clear();
        var dohanyzoNok = lakossag.Where(x => x.Dohanyzik && x.Nem == "nő").ToList();
        foreach (var item in dohanyzoNok)
        {
            MegoldasLista.Items.Add(item.ToString(false));
        }
        var maxNettoJovedelem = dohanyzoNok.Max(x => x.NettoJovedelem);
        MegoldasLista.Items.Add($"Maximális nettó éves jövedelem: {maxNettoJovedelem}");

    }

    public void Feladat29_3()
    {
        MegoldasLista.Items.Clear();
        var tartomanyok = lakossag.GroupBy(x => x.Tartomany).ToList();
        foreach (var item in tartomanyok)
        {
            var legnagyobbSorfogyaszto = item.OrderByDescending(x => int.TryParse(x.SorFogyasztasEventeSzoveges, out int sor) ? sor : 0).First();
            MegoldasLista.Items.Add($"{item.Key} {legnagyobbSorfogyaszto.Id} {legnagyobbSorfogyaszto} ");
        }
    }

    public void Feladat30_3()
    {
        MegoldasLista.Items.Clear();
        var legidosebbNok = lakossag.Where(x => x.Nem == "nő").OrderByDescending(x => x.GetAge()).First();
        var legidosebbFerfiak = lakossag.Where(x => x.Nem == "férfi").OrderByDescending(x => x.GetAge()).First();
        MegoldasLista.Items.Add(legidosebbNok.ToString(true));
        MegoldasLista.Items.Add(legidosebbFerfiak.ToString(true));

    }

    public void Feladat31_3()
    {
        MegoldasLista.Items.Clear();
        var nemzetisegek = lakossag.Select(x => x.Nemzetiseg).Distinct().OrderByDescending(x => x).ToList();
        foreach (var item in nemzetisegek)
        {
            MegoldasLista.Items.Add(item);
        }

    }

    public void Feladat32_3()
    {
        MegoldasLista.Items.Clear();
        var tartomanyok = lakossag.GroupBy(x => x.Tartomany).Select(x => new { Tartomany = x.Key, Lakosok = x.Count() }).OrderBy(x => x.Lakosok).ToList();
        foreach (var item in tartomanyok)
        {
            MegoldasLista.Items.Add(item.Tartomany);
        }

    }

    public void Feladat33_3()
    {
        MegoldasLista.Items.Clear();
        var legmagasabbJovedelmuek = lakossag.OrderByDescending(x => x.NettoJovedelem).Take(3).ToList();
        foreach (var item in legmagasabbJovedelmuek)
        {
            MegoldasLista.Items.Add($"{item.Id} {item.NettoJovedelem}");
        }

    }

    public void Feladat34_3()
    {

        MegoldasLista.Items.Clear();
        var ferfiak = lakossag.Where(x => x.Nem == "férfi").ToList();
        var validKrumpliFogyasztas = ferfiak
            .Select(x => x.KrumpliFogyasztasEventeSzoveges)
            .Where(value => int.TryParse(value, out _))
            .Select(int.Parse);

        if (validKrumpliFogyasztas.Any())
        {
            var atlagsuly = ferfiak.Where(x => int.TryParse(x.KrumpliFogyasztasEventeSzoveges, out int krumpli) && krumpli > 55).Average(x => x.Suly);
            MegoldasLista.Items.Add(atlagsuly);

        }
    }

    public void Feladat35_3()
    {
        MegoldasLista.Items.Clear();
        var tartomanyok = lakossag.GroupBy(x => x.Tartomany).Select(x => new { Tartomany = x.Key, Min = x.Min(y => y.GetAge()) }).ToList();
        foreach (var item in tartomanyok)
        {
            MegoldasLista.Items.Add(item);
        }

    }

    public void Feladat36_3()
    {
        MegoldasLista.Items.Clear();
        var nemzetisegTartomany = lakossag.Select(x => new { x.Nemzetiseg, x.Tartomany }).Distinct().ToList();
        foreach (var item in nemzetisegTartomany)
        {
            MegoldasLista.Items.Add(item);
        }

    }

    public void Feladat37_3()
    {
        MegoldasLista.Items.Clear();
        var atlag = lakossag.Average(x => x.NettoJovedelem);
        var atlagFelettiek = lakossag.Where(x => x.NettoJovedelem > atlag).ToList();
        foreach (var item in atlagFelettiek)
        {
            MegoldasLista.Items.Add(item.ToString(false));
        }

    }

    public void Feladat38_3()
    {
        MegoldasLista.Items.Clear();
        var nokszama = lakossag.Count(x => x.Nem == "nő");
        var ferfiszama = lakossag.Count(x => x.Nem == "férfi");
        MegoldasLista.Items.Add($"Nők száma: {nokszama}");
        MegoldasLista.Items.Add($"Férfiak száma: {ferfiszama}");

    }

    public void Feladat39_3()
    {
        MegoldasLista.Items.Clear();
        var tartomanyok = lakossag.GroupBy(x => x.Tartomany).Select(x => new { Tartomany = x.Key, Max = x.Max(y => y.NettoJovedelem) }).OrderByDescending(x => x.Max).ToList();
        foreach (var item in tartomanyok)
        {
            MegoldasLista.Items.Add(item);
        }

    }

    public void Feladat40_3()
    {
        MegoldasLista.Items.Clear();
        var nemetekhavijovedelme = lakossag.Where(x => x.Nemzetiseg == "német").Sum(x => x.NettoJovedelem);
        var nemnemetekhavijovedelme = lakossag.Where(x => x.Nemzetiseg != "német").Sum(x => x.NettoJovedelem);
        MegoldasLista.Items.Add($"Németek összes nettó jövedelme: {nemetekhavijovedelme}");
        MegoldasLista.Items.Add($"Nem németek összes nettó jövedelme: {nemnemetekhavijovedelme}");

    }

    public void Feladat41_3()
    {
        MegoldasLista.Items.Clear();
        var rnd = new Random();
        var turkactvoter = lakossag.Where(x => x.Nemzetiseg == "török" && x.AktivSzavazo)
            .OrderBy(x => rnd.Next())
            .Take(10)
            .ToList();

        //var turkactvoter = lakossag.Where(x => x.Nemzetiseg == "török" && x.AktivSzavazo)
        //    .OrderBy(x => Guid.NewGuid())
        //    .Take(10)
        //    .ToList();

        foreach (var item in turkactvoter)
        {
            MegoldasLista.Items.Add(item);
        }

    }

    public void Feladat42_3()
    {
        MegoldasLista.Items.Clear();
        var atlagsorfogyasztas = lakossag.Select(x => x.SorFogyasztasEventeSzoveges).Where(value => int.TryParse(value, out _)).Select(int.Parse).Average();
        var atlagfolottivok = lakossag.Where(x => int.TryParse(x.SorFogyasztasEventeSzoveges, out int sor) && sor > atlagsorfogyasztas).ToList();
        //var randottostringtrue = atlagfolottivok.OrderBy(x => Guid.NewGuid()).Take(5).ToList();
        var rnd = new Random();
        var randottostringtrue = atlagfolottivok.OrderBy(x => rnd.Next()).Take(5).ToList();
        foreach (var item in randottostringtrue)
        {
            MegoldasLista.Items.Add(item.ToString(true));
        }

    }

    public void Feladat43_3()
    {
        MegoldasLista.Items.Clear();
        var atlag = lakossag.Average(x => x.NettoJovedelem);
        var tartomanyok = lakossag.GroupBy(x => x.Tartomany).Select(x => new { Tartomany = x.Key, Min = x.Min(y => y.NettoJovedelem) }).Where(x => x.Min > atlag).OrderBy(x => Guid.NewGuid()).Take(2).ToList();
        foreach (var item in tartomanyok)
        {
            MegoldasLista.Items.Add(item);
        }
        MegoldasLista.Items.Add($"Átlag: {atlag}");

    }

    public void Feladat44_3()
    {
        var rnd = new Random();
        MegoldasLista.Items.Clear();
        var nemvegzettsulit = lakossag.Where(x => x.IskolaiVegzettseg == null).OrderBy(x => rnd.Next()).Take(3).ToList();
        foreach (var item in nemvegzettsulit)
        {
            MegoldasLista.Items.Add(item);
        }
    }

    public void Feladat45_3()
    {
        MegoldasLista.Items.Clear();
        var universitatnoknembajor = lakossag.Where(x => x.IskolaiVegzettseg == "Universität" && x.Nem == "nő" && x.Tartomany != "Bajorország").ToList().Take(5);
        foreach (var item in universitatnoknembajor)
        {
            MegoldasLista.Items.Add(item);
        }
    }

    #endregion

    #region Dombóvári
    private void Feladat1_4()
    {
        MegoldasMondatos.Content = lakossag.MaxBy(lakossag => lakossag.NettoJovedelem).NettoJovedelem;
    }
    private void Feladat2_4()
    {
        MegoldasMondatos.Content = lakossag.Average(lakossag => lakossag.NettoJovedelem);
    }
    private void Feladat3_4()
    {
        var csoportositott = lakossag.GroupBy(lakossag => lakossag.Tartomany);
        foreach (var csoport in csoportositott)
        {
            MegoldasLista.Items.Add($"{csoport.Key} - {csoport.Count()}");
        }
    }
    private void Feladat4_4()
    {
        MegoldasTeljes.ItemsSource = lakossag.Where(lakossag => lakossag.Nemzetiseg == "angolai");
    }
    private void Feladat5_4()
    {
        MegoldasTeljes.ItemsSource = lakossag.FindAll(v => v.Kor == lakossag.MinBy(lakossag => lakossag.Kor).Kor);
    }
    private void Feladat6_4()
    {
        var f6 = lakossag.Where(x => !x.Dohanyzik);
        foreach (var item in f6)
        {
            MegoldasLista.Items.Add($"{item.Id} - {item.HaviNettoJovedelem}");

        }
    }
    private void Feladat7_4()
    {
        var f7 = lakossag.Where(x => x.Tartomany == "Bajorország" && x.NettoJovedelem > 30000).OrderBy(x => x.IskolaiVegzettseg);
        MegoldasTeljes.ItemsSource = f7;
    }
    private void Feladat8_4()
    {
        foreach (var item in lakossag)
        {
            MegoldasLista.Items.Add($"{item.ToString(true)}");
        }
    }
    private void Feladat9_4()
    {
        var f9 = lakossag.Where(x => x.Tartomany == "Bajorország" && x.Nem == "nő");
        foreach (var item in f9)
        {
            MegoldasLista.Items.Add($"{item.ToString(false)}");
        }
    }
    private void Feladat10_4()
    {
        var f10 = lakossag.Where(x => !x.Dohanyzik).OrderByDescending(x => x.NettoJovedelem).Take(10);
        MegoldasTeljes.ItemsSource = f10;
    }
    private void Feladat11_4()
    {
        var f11 = lakossag.OrderByDescending(x => x.Kor).Take(5);
        MegoldasTeljes.ItemsSource = f11;
    }
    private void Feladat12_4()
    {
        var groupedByNepcsoport = lakossag
            .Where(a => a.Nemzetiseg == "német")
            .GroupBy(a => a.Nepcsoport);

        foreach (var group in groupedByNepcsoport)
        {
            MegoldasLista.Items.Add($"- Népcsoport: {group.Key}");
            foreach (var allampolgar in group)
            {
                string aktivSzavazoStatus = allampolgar.AktivSzavazo ? "aktív szavazó" : "nem aktív szavazó";
                MegoldasLista.Items.Add($"{aktivSzavazoStatus}, Politikai nézet: {allampolgar.PolitikaiNezet}");
            }
        }

    }
    private void Feladat13_4()
    {
        var f13 = lakossag.Where(x => x.Nem == "férfi").Average(x => int.Parse(x.SorFogyasztasEventeSzoveges));
        MegoldasMondatos.Content = f13;
    }
    private void Feladat14_4()
    {
        var f14 = lakossag.GroupBy(x => x.IskolaiVegzettseg).OrderBy(y => y.Key);
        foreach (var group in f14)
        {
            MegoldasLista.Items.Add($"- {group.Key ?? "NA"}");
            foreach (var item in group)
            {
                MegoldasLista.Items.Add($"{item.Id}");
            }
        }
    }

    private void Feladat15_4()
    {
        var legmagasabb = lakossag.OrderByDescending(x => x.NettoJovedelem).Take(3);
        var legalacsonyabb = lakossag.OrderBy(x => x.NettoJovedelem).Take(3);
        MegoldasLista.Items.Add("Legmagasabb:");
        foreach (var item in legmagasabb)
        {
            MegoldasLista.Items.Add($"- {item.ToString(false)}");
        }
        MegoldasLista.Items.Add("Legalacsonyabb:");
        foreach (var item in legalacsonyabb)
        {
            MegoldasLista.Items.Add($"- {item.ToString(false)}");
        }
    }
    private void Feladat16_4()
    {
        var f16 = lakossag.Count(x => x.AktivSzavazo) / (double)lakossag.Count() * 100;
        MegoldasMondatos.Content = $"{f16:0.00}% aktiv szavazo";
    }
    private void Feladat17_4()
    {
        var f17 = lakossag.Where(x => x.AktivSzavazo).OrderByDescending(y => y.Tartomany);
        MegoldasTeljes.ItemsSource = f17;
    }
    private void Feladat18_4()
    {
        var f18 = lakossag.Average(x => x.Kor);
        MegoldasMondatos.Content = $"Átlagos életkor {f18:.00}";
    }
    private void Feladat19_4()
    {
        var tartomanyCsoportok = lakossag
          .GroupBy(a => a.Tartomany)
          .Select(g => new
          {
              Tartomany = g.Key,
              AtlagosNettoJovedelem = g.Average(a => a.NettoJovedelem),
              LakosokSzama = g.Count()
          })
          .OrderByDescending(g => g.AtlagosNettoJovedelem)
          .ThenByDescending(g => g.LakosokSzama)
          .FirstOrDefault();

        MegoldasMondatos.Content = $"{tartomanyCsoportok.Tartomany} - {tartomanyCsoportok.AtlagosNettoJovedelem} - {tartomanyCsoportok.LakosokSzama}";
    }
    private void Feladat20_4()
    {
        var f20 = lakossag.OrderBy(x => lakossag.Average(x => x.Suly)).Skip(lakossag.Count() / 2).First();
        MegoldasMondatos.Content = $"{f20.Suly} médián súly, {lakossag.Average(x => x.Suly)} átlag súly";

    }

    private void Feladat21_4()
    {
        // Számítsa ki, hogy az aktív szavazók vagy a nem szavazók fogyasztanak-e több sört évente átlagosan. Jelenítse meg a két értéket, és a döntést.

        var aktivSzavazokAtlagosSorFogyasztasa = lakossag
          .Where(a => a.AktivSzavazo && int.TryParse(a.SorFogyasztasEventeSzoveges, out _)).Average(y => int.Parse(y.SorFogyasztasEventeSzoveges));

        var nemSzavazokAtlagosSorFogyasztasa = lakossag
          .Where(a => !a.AktivSzavazo && int.TryParse(a.SorFogyasztasEventeSzoveges, out _)).Average(y => int.Parse(y.SorFogyasztasEventeSzoveges));

        MegoldasLista.Items.Add($"Aktív szavazók átlagos sörfogyasztása: {aktivSzavazokAtlagosSorFogyasztasa:.00} liter/év");
        MegoldasLista.Items.Add($"Nem szavazók átlagos sörfogyasztása: {nemSzavazokAtlagosSorFogyasztasa:.00} liter/év");

        if (aktivSzavazokAtlagosSorFogyasztasa > nemSzavazokAtlagosSorFogyasztasa)
        {
            MegoldasLista.Items.Add("Az aktív szavazók fogyasztanak több sört évente átlagosan.");
        }
        else if (aktivSzavazokAtlagosSorFogyasztasa < nemSzavazokAtlagosSorFogyasztasa)
        {
            MegoldasLista.Items.Add("A nem szavazók fogyasztanak több sört évente átlagosan.");
        }
        else
        {
            MegoldasLista.Items.Add("Az aktív szavazók és a nem szavazók egyformán fogyasztanak sört évente átlagosan.");
        }

    }
    private void Feladat22_4()
    {
        var ferfiakAtlagosMagassaga = lakossag
            .Where(a => a.Nem == "férfi")
            .Average(a => a.Magassag);

        var nokAtlagosMagassaga = lakossag
            .Where(a => a.Nem == "nő")
            .Average(a => a.Magassag);

        MegoldasLista.Items.Add($"Férfiak átlagos magassága: {ferfiakAtlagosMagassaga:.00} cm");
        MegoldasLista.Items.Add($"Nők átlagos magassága: {nokAtlagosMagassaga:.00} cm");

    }

    private void Feladat23_4()
    {
        var nepCsoportok = lakossag
        .GroupBy(a => a.Nepcsoport)
        .Select(g => new
        {
            Nepcsoport = g.Key,
            LakosokSzama = g.Count(x => x.Nepcsoport != null),
            AtlagosKor = g.Average(a => DateTime.Now.Year - a.SzuletesiEv)
        })
        .OrderByDescending(g => g.LakosokSzama)
        .ThenByDescending(g => g.AtlagosKor)
        .FirstOrDefault();

        MegoldasMondatos.Content = $"Népcsoport: {nepCsoportok.Nepcsoport ?? "N/A"} Lakosok száma: {nepCsoportok.LakosokSzama} Átlagos életkor: {nepCsoportok.AtlagosKor}";
    }
    private void Feladat24_4()
    {
        var dohanyzokAtlagosJovedeleme = lakossag
            .Where(a => a.Dohanyzik)
            .Average(a => a.NettoJovedelem);

        var nemDohanyzokAtlagosJovedeleme = lakossag
            .Where(a => !a.Dohanyzik)
            .Average(a => a.NettoJovedelem);

        MegoldasLista.Items.Add($"Dohányzók átlagos nettó éves jövedelme: {dohanyzokAtlagosJovedeleme:.00} Ft");
        MegoldasLista.Items.Add($"Nem dohányzók átlagos nettó éves jövedelme: {nemDohanyzokAtlagosJovedeleme:.00} Ft");

        if (dohanyzokAtlagosJovedeleme > nemDohanyzokAtlagosJovedeleme)
        {
            MegoldasLista.Items.Add("A dohányzók átlagos nettó éves jövedelme magasabb.");
        }
        else if (dohanyzokAtlagosJovedeleme < nemDohanyzokAtlagosJovedeleme)
        {
            MegoldasLista.Items.Add("A nem dohányzók átlagos nettó éves jövedelme magasabb.");
        }
        else
        {
            MegoldasLista.Items.Add("A dohányzók és a nem dohányzók átlagos nettó éves jövedelme egyforma.");
        }

    }

    private void Feladat25_4()
    {

        var f25atlag = lakossag.Where(x => int.TryParse(x.KrumpliFogyasztasEventeSzoveges, out _)).Average(y => int.Parse(y.KrumpliFogyasztasEventeSzoveges));
        var f25 = lakossag.Where(x => int.TryParse(x.KrumpliFogyasztasEventeSzoveges, out _) && int.Parse(x.KrumpliFogyasztasEventeSzoveges) > f25atlag).OrderByDescending(y => y.KrumpliFogyasztasEvente).Take(15);

        foreach (var item in f25)
        {
            MegoldasLista.Items.Add(item.ToString(false));

        }
    }
    private void Feladat26_4()
    {
        var f26 = lakossag.GroupBy(x => x.Tartomany)
        .Select(g => new
        {
            Tartomany = g.Key,
            AtlagosKor = g.Average(a => DateTime.Now.Year - a.SzuletesiEv)
        });

        foreach (var tartomany in f26)
        {
            MegoldasLista.Items.Add($"Tartomány: {tartomany.Tartomany}, Átlagos életkor: {tartomany.AtlagosKor:F2} év");
        }

    }

    private void Feladat27_4()
    {
        var f27 = lakossag.Where(x => x.Kor > 50);
        MegoldasMondatos.Content = $"50 évesnél idősebbek darabszáma {f27.Count()}";
        foreach (var item in f27)
        {
            MegoldasLista.Items.Add(item.ToString(false));
        }
    }

    private void Feladat28_4()
    {

        var dohanyzoNok = lakossag
           .Where(a => a.Dohanyzik && a.Nem == "nő")
           .ToList();

        foreach (var no in dohanyzoNok)
        {
            MegoldasLista.Items.Add(no.ToString(false));
        }

        var maxJovedelem = dohanyzoNok.Max(a => a.NettoJovedelem);
        MegoldasMondatos.Content = $"A dohányzó nők maximális nettó éves jövedelme: {maxJovedelem}";

    }

    private void Feladat29_4()
    {

        var legnagyobbSorFogyasztok = lakossag
            .GroupBy(a => a.Tartomany)
            .Select(g => new
            {
                Tartomany = g.Key,
                LegnagyobbSorFogyaszto = g.OrderByDescending(a => int.TryParse(a.SorFogyasztasEventeSzoveges, out int x) ? x : 0).FirstOrDefault()
            });

        foreach (var tartomany in legnagyobbSorFogyasztok)
        {
            if (tartomany.LegnagyobbSorFogyaszto != null)
            {
                MegoldasLista.Items.Add($"Tartomány: {tartomany.Tartomany}, Lakos Id: {tartomany.LegnagyobbSorFogyaszto.Id}, Sörfogyasztás: {tartomany.LegnagyobbSorFogyaszto.SorFogyasztasEvente} liter/év");
            }
        }

    }

    private void Feladat30_4()
    {
        var legidosebbNo = lakossag
            .Where(a => a.Nem == "nő")
            .OrderBy(a => a.SzuletesiEv)
            .FirstOrDefault();

        var legidosebbFerfi = lakossag
            .Where(a => a.Nem == "férfi")
            .OrderBy(a => a.SzuletesiEv)
            .FirstOrDefault();

        MegoldasLista.Items.Add($"Legidősebb nő: {legidosebbNo.ToString(true)}");
        MegoldasLista.Items.Add($"Legidősebb férfi: {legidosebbFerfi.ToString(true)}");

    }

    private void Feladat31_4()
    {
        var kulonbozoNemzetisegek = lakossag
           .Select(a => a.Nemzetiseg)
           .Distinct()
           .OrderByDescending(n => n)
           .ToList();

        MegoldasLista.ItemsSource = kulonbozoNemzetisegek;
    }

    private void Feladat32_4()
    {
        var f32 = lakossag.GroupBy(x => x.Tartomany)
            .Select(g => new
            {
                Tartomany = g.Key,
                LakosokSzama = g.Count(),
            })
            .OrderBy(t => t.LakosokSzama)
            .Select(t => t.Tartomany)
            .ToList();

        MegoldasLista.ItemsSource = f32;

    }

    private void Feladat33_4()
    {
        var f33 = lakossag.OrderBy(x => x.NettoJovedelem).Take(3);
        foreach (var item in f33)
        {
            MegoldasLista.Items.Add($"{item.Id} - {item.NettoJovedelem}");
        }
    }

    private void Feladat34_4()
    {
        var f34 = lakossag.Where(x => x.Nem == "férfi" && int.TryParse(x.KrumpliFogyasztasEventeSzoveges, out _) && int.Parse(x.KrumpliFogyasztasEventeSzoveges) > 55).Average(y => y.Suly);
        MegoldasMondatos.Content = $"Férfiak átlagos súlya aki blala {f34}";
    }

    private void Feladat35_4()
    {
        var csoportositasTartomanyokSzerint = lakossag
            .GroupBy(a => a.Tartomany)
            .Select(g => new
            {
                Tartomany = g.Key,
                MinKor = g.Min(a => DateTime.Now.Year - a.SzuletesiEv)
            })
         .ToList();

        foreach (var csoport in csoportositasTartomanyokSzerint)
        {
            MegoldasLista.Items.Add($"Tartomány: {csoport.Tartomany}, Minimális életkor: {csoport.MinKor}");
        }

    }

    private void Feladat36_4()
    {
        var nemzetisegTartomanyParok = lakossag
            .Select(a => new { a.Nemzetiseg, a.Tartomany })
            .Distinct()
            .ToList();

        foreach (var par in nemzetisegTartomanyParok)
        {
            MegoldasLista.Items.Add($"Nemzetiség: {par.Nemzetiseg}, Tartomány: {par.Tartomany}");
        }

    }

    private void Feladat37_4()
    {
        double atlagJovedelem = lakossag.Average(a => a.NettoJovedelem);

        var atlagFelettiAllampolgarok = lakossag
            .Where(a => a.NettoJovedelem > atlagJovedelem)
            .ToList();

        MegoldasMondatos.Content = ($"Átlag feletti állampolgárok száma: {atlagFelettiAllampolgarok.Count} Átlagjövedelem: {atlagJovedelem}");

        foreach (var allampolgar in atlagFelettiAllampolgarok)
        {
            MegoldasLista.Items.Add(allampolgar.ToString(false));
        }
    }

    private void Feladat38_4()
    {
        int nokSzama = lakossag.Count(a => a.Nem == "nő");
        int ferfiakSzama = lakossag.Count(a => a.Nem == "férfi");

        MegoldasLista.Items.Add($"Nők száma: {nokSzama}");
        MegoldasLista.Items.Add($"Férfiak száma: {ferfiakSzama}");
    }
    private void Feladat39_4()
    {
        var legmagasabbJovedelmek = lakossag
            .GroupBy(a => a.Tartomany)
            .Select(g => new
            {
                Tartomany = g.Key,
                LegmagasabbJovedelem = g.Max(a => a.NettoJovedelem)
            })
            .OrderByDescending(g => g.LegmagasabbJovedelem)
            .ToList();

        foreach (var tartomany in legmagasabbJovedelmek)
        {
            MegoldasLista.Items.Add($"Tartomány: {tartomany.Tartomany}, Legmagasabb jövedelem: {tartomany.LegmagasabbJovedelem}");
        }
    }

    private void Feladat40_4()
    {
        var nemetJovedelem = lakossag
             .Where(a => a.Nemzetiseg == "német")
             .Sum(a => a.HaviNettoJovedelem);

        var nemNemetJovedelem = lakossag
            .Where(a => a.Nemzetiseg != "német")
            .Sum(a => a.HaviNettoJovedelem);

        MegoldasLista.Items.Add($"Németek és nem németek havi jövedelmének aránya: {nemetJovedelem / (double)nemNemetJovedelem * 100:.00}%");
    }

    private void Feladat41_4()
    {
        var random = new Random();
        var torokAktivSzavazok = lakossag
            .Where(a => a.Nemzetiseg == "török" && a.AktivSzavazo)
            .OrderBy(a => random.Next())
            .Take(10)
            .ToList();

        MegoldasTeljes.ItemsSource = torokAktivSzavazok;
    }
    private void Feladat42_4()
    {
        var random = new Random();
        var atlagosSorFogyasztas = lakossag
            .Where(a => int.TryParse(a.SorFogyasztasEventeSzoveges, out _))
            .Average(a => int.Parse(a.SorFogyasztasEventeSzoveges));

        var atlagFelettiSorFogyasztok = lakossag
            .Where(a => int.TryParse(a.SorFogyasztasEventeSzoveges, out _) && int.Parse(a.SorFogyasztasEventeSzoveges) > atlagosSorFogyasztas)
            .OrderBy(a => random.Next())
            .Take(5)
            .ToList();

        MegoldasLista.Items.Add($"Átlagos sörfogyasztás: {atlagosSorFogyasztas:.00} liter/év");

        foreach (var allampolgar in atlagFelettiSorFogyasztok)
        {
            MegoldasLista.Items.Add(allampolgar.ToString(true));
        }

    }
    private void Feladat43_4()
    {
        var random = new Random();
        var atlagJovedelem = lakossag.Average(a => a.NettoJovedelem);

        var tartomanyok = lakossag
            .GroupBy(a => a.Tartomany)
            .Select(g => new
            {
                Tartomany = g.Key,
                MinJovedelem = g.Min(a => a.NettoJovedelem)
            })
            .Where(t => t.MinJovedelem > atlagJovedelem)
            .OrderBy(t => random.Next())
            .Take(2)
            .ToList();

        MegoldasLista.Items.Add($"Átlagos nettó jövedelem: {atlagJovedelem:F2}");

        foreach (var tartomany in tartomanyok)
        {
            MegoldasLista.Items.Add($"{tartomany.Tartomany}: {tartomany.MinJovedelem}");
        }

    }

    private void Feladat44_4()
    {
        var random = new Random();
        var iskolaiVegzettsegNelkuliek = lakossag
            .Where(a => string.IsNullOrEmpty(a.IskolaiVegzettseg))
            .OrderBy(a => random.Next())
            .Take(3)
            .ToList();

        foreach (var allampolgar in iskolaiVegzettsegNelkuliek)
        {
            MegoldasLista.Items.Add(allampolgar.ToString());
        }

    }

    private void Feladat45_4()
    {

        var egyetemiVegzettseguNok = lakossag
            .Where(a => a.Nem == "nő" && a.IskolaiVegzettseg == "Universität" && a.Nepcsoport != "bajor")
            .Take(5)
            .ToList();

        MegoldasTeljes.ItemsSource = egyetemiVegzettseguNok;

        if (egyetemiVegzettseguNok.Count > 0)
        {
            var elsoNoJovedelem = egyetemiVegzettseguNok[0].NettoJovedelem;

            var random = new Random();
            var magasabbJovedelemNemetNok = lakossag
                .Where(a => a.Nem == "nő" && a.Nemzetiseg == "német" && a.NettoJovedelem > elsoNoJovedelem)
                .OrderBy(a => random.Next())
                .Take(3)
                .ToList();

            foreach (var no in magasabbJovedelemNemetNok)
            {
                MegoldasLista.Items.Add($"{no.Id} {no.Nem} {no.SzuletesiEv} {no.Suly} {no.Magassag}");
            }
        }
    }

    #endregion

    #region Marcalek
    private void Feladat1_5()
    {
        var feladat = lakossag.OrderByDescending(l => l.NettoJovedelem).First().NettoJovedelem;
        MegoldasMondatos.Content = $"A legmagasabb éves nettó jövedelem: {feladat}";
    }

    private void Feladat2_5()
    {
        var feladat = lakossag.Average(l => l.NettoJovedelem);
        MegoldasMondatos.Content = $"A lakosok átlag nettó jövedelme: {feladat}";
    }

    private void Feladat3_5()
    {
        var feladat = lakossag.GroupBy(l => l.Tartomany).ToDictionary(l => l.Key, g => g.Count());
        foreach (var f in feladat)
        {
            string elem = $"{f.Key} - {f.Value}";
            MegoldasLista.Items.Add(elem);
        }
    }

    private void Feladat4_5()
    {
        var feladat = lakossag.Where(l => l.Nemzetiseg.Equals("angolai"));
        foreach (var f in feladat) MegoldasTeljes.Items.Add(f);
    }
    private void Feladat5_5()
    {
        var feladat = lakossag.Where(l => l.Kor.Equals(lakossag.Min(l => l.Kor)));
        foreach (var f in feladat) MegoldasTeljes.Items.Add(f);
    }
    private void Feladat6_5()
    {
        var feladat = lakossag.Where(l => !l.Dohanyzik);
        foreach (var f in feladat)
        {
            string elem = $"{f.Id} - {f.HaviNettoJovedelem}";
            MegoldasLista.Items.Add(elem);
        }
    }

    private void Feladat7_5()
    {
        var feladat = lakossag.Where(l => l.Tartomany.Equals("Bajorország") && l.NettoJovedelem > 30000).OrderBy(l => l.IskolaiVegzettseg);
        foreach (var f in feladat) MegoldasTeljes.Items.Add(f);
    }

    private void Feladat8_5()
    {
        var feladat = lakossag.Where(l => l.Nem.Equals("férfi"));
        foreach (var f in feladat) MegoldasLista.Items.Add(f.ToString(true));
    }

    private void Feladat9_5()
    {
        var feladat = lakossag.Where(l => l.Nem.Equals("nő") && l.Tartomany.Equals("Bajorország"));
        foreach (var f in feladat) MegoldasLista.Items.Add(f.ToString(false));
    }
    private void Feladat10_5()
    {
        var feladat = lakossag.Where(l => !l.Dohanyzik).OrderBy(l => l.NettoJovedelem).ToList();
        for (int i = 0; i < 10; i++) MegoldasLista.Items.Add(feladat[i].ToString(true));
    }
    private void Feladat11_5()
    {
        var feladat = lakossag.OrderByDescending(l => l.Kor).ToList();
        for (int i = 0; i < 5; i++) MegoldasTeljes.Items.Add(feladat[i]);
    }
    private void Feladat12_5()
    {
        var feladat = lakossag.Where(l => l.Nemzetiseg.Equals("német")).OrderBy(l => l.Tartomany).DistinctBy(l => l.Tartomany);
        foreach (var fel in feladat)
        {
            MegoldasLista.Items.Add(fel.Tartomany);
            var temp = feladat.Where(f => f.Tartomany.Equals(fel.Tartomany)).ToList();
            foreach (var f in temp) MegoldasLista.Items.Add(f.Id + " - " + (f.AktivSzavazo ? "aktív szavazó" : "nem aktív szavazó"));
        }

    }
    private void Feladat13_5()
    {
        var feladat = lakossag.Where(l => l.Nem.Equals("férfi")).Average(l => l.SorFogyasztasEvente);
        MegoldasMondatos.Content = $"A férfiak átlag sörfogyasztása évente: {feladat:0} liter";
    }
    private void Feladat14_5()
    {
        var feladat = lakossag.OrderBy(l => l.IskolaiVegzettseg).ToList();
        foreach (var f in feladat) MegoldasTeljes.Items.Add(f);
    }
    private void Feladat15_5()
    {
        var feladat = lakossag.OrderByDescending(l => l.NettoJovedelem).ToList();
        for (int i = 0; i < 3; i++) MegoldasLista.Items.Add(feladat[i].ToString(false));
        feladat.Reverse();
        for (int i = 0; i < 3; i++) MegoldasLista.Items.Add(feladat[i].ToString(false));
    }
    private void Feladat16_5()
    {
        float feladat = lakossag.Where(l => l.AktivSzavazo).Count() / lakossag.Count() * 100;
        MegoldasMondatos.Content = $"A lakosság {feladat:0}%-a aktív szaavazó";
    }
    private void Feladat17_5()
    {
        var feladat = lakossag.Where(l => l.AktivSzavazo).OrderBy(l => l.Tartomany);
        foreach (var f in feladat) MegoldasTeljes.Items.Add(f);
    }
    private void Feladat18_5()
    {
        var feladat = lakossag.Average(l => l.Kor);
        MegoldasMondatos.Content = $"A lakosok átlag életkora: {feladat:0} év";
    }
    private void Feladat19_5()
    {
        List<string> tartomanyok = new();
        var feladat = lakossag.DistinctBy(l => l.Tartomany);
        foreach (var f in feladat) tartomanyok.Add(f.Tartomany);
        string tartomany = string.Empty;
        double max = 0;
        for (int i = 0; i < tartomanyok.Count; i++)
        {
            if (max < lakossag.Where(l => l.Tartomany.Equals(tartomanyok[i])).Average(l => l.NettoJovedelem))
            {
                max = lakossag.Where(l => l.Tartomany.Equals(tartomanyok[i])).Average(l => l.NettoJovedelem);
                tartomany = tartomanyok[i];
            }
        }
        MegoldasMondatos.Content = $"{tartomany} - {max}";
    }
    private void Feladat20_5()
    {
        var atlagSuly = lakossag.Average(l => l.Suly);
        double median;
        var feladat = lakossag.OrderBy(l => l.Suly).ToList();
        if (feladat.Count() % 2 == 0) median = (feladat[feladat.Count() / 2].Suly + feladat[feladat.Count() / 2 + 1].Suly) / 2;
        else median = feladat[(feladat.Count() - 1) / 2 + 1].Suly;
        MegoldasMondatos.Content = $"A lakosok átlag súlya: {atlagSuly:0}, a medián: {median}";
    }
    private void Feladat21_5()
    {
        var aktivSzavazok = lakossag.Where(l => l.AktivSzavazo).Sum(l => l.SorFogyasztasEvente);
        var nemAktivSzavazok = lakossag.Where(l => !l.AktivSzavazo).Sum(l => l.SorFogyasztasEvente);
        MegoldasMondatos.Content = $"Az aktív szavazók évente {aktivSzavazok}l, a nem aktív szavazók pedig {nemAktivSzavazok}l sört isznak. {(aktivSzavazok > nemAktivSzavazok ? "Az aktív szavazók isznak többet" : "A nem aktív szavazók isznak többet")}";
    }
    private void Feladat22_5()
    {
        var ferfiAtlagMagassag = lakossag.Where(l => l.Nem.Equals("férfi")).Average(l => l.Magassag);
        var noiAtlagMagassag = lakossag.Where(l => l.Nem.Equals("nő")).Average(l => l.Magassag);
        MegoldasMondatos.Content = $"A férfiak átlag magassága {ferfiAtlagMagassag:0.0} cm, a nők átlag magassága {noiAtlagMagassag:0.0} cm";
    }
    private void Feladat23_5()
    {
        var feladat = lakossag.Where(l => l.Nepcsoport != null).GroupBy(l => l.Nepcsoport).ToDictionary(l => l.Key, l => l.Count());
        MegoldasMondatos.Content = $"A {feladat.First().Key} népcsoportba tartoznak a legtöbben: {feladat.First().Value} fő";
    }
    private void Feladat24_5()
    {
        var dohanyzoAtlagFizetes = lakossag.Where(l => l.Dohanyzik).Average(l => l.NettoJovedelem);
        var nemDohanyzoAtlagFizetes = lakossag.Where(l => !l.Dohanyzik).Average(l => l.NettoJovedelem);
        MegoldasMondatos.Content = $"{(dohanyzoAtlagFizetes > nemDohanyzoAtlagFizetes ? "A dohányzók átlag fizetése nagyobb" : "A nem dohányzók átlag fizetése nagyobb")}";
    }
    private void Feladat25_5()
    {
        var atlag = lakossag.Average(l => l.KrumpliFogyasztasEvente);
        var feladat = lakossag.OrderByDescending(l => l.KrumpliFogyasztasEvente).ToList();
        for (var i = 0; i < 15; i++) if (feladat[i].KrumpliFogyasztasEvente > atlag) MegoldasTeljes.Items.Add(feladat[i]);
    }

    private void Feladat26_5()
    {
        List<string> tartomanyok = new();
        var feladat = lakossag.DistinctBy(l => l.Tartomany);
        foreach (var f in feladat) tartomanyok.Add(f.Tartomany);
        string tartomany = string.Empty;
        double atlagEletkor = 0;
        for (var i = 0; i < tartomanyok.Count; i++)
        {
            atlagEletkor = lakossag.Where(l => l.Tartomany.Equals(tartomanyok[i])).Average(l => l.Kor);
            tartomany = tartomanyok[i];
            MegoldasLista.Items.Add($"{tartomany} - {atlagEletkor:0}");
        }
    }

    private void Feladat27_5()
    {
        var feladat = lakossag.Where(l => l.Kor > 50).ToList();
        foreach (var f in feladat) MegoldasLista.Items.Add(f.ToString(false));
        MegoldasLista.Items.Add(feladat.Count());
    }

    private void Feladat28_5()
    {
        var feladat = lakossag.Where(l => l.Nem.Equals("nő") && l.Dohanyzik).OrderByDescending(l => l.NettoJovedelem).ToList();
        if (feladat.Count() != 0) MegoldasMondatos.Content = $"A dohányzó nők legmagasabb nettó jövedelme: {feladat.First().NettoJovedelem}";
        else MegoldasMondatos.Content = "Nincs találat dohányzó nőkre";
    }

    private void Feladat29_5()
    {
        List<string> tartomanyok = new();
        var feladat = lakossag.DistinctBy(l => l.Tartomany);
        foreach (var f in feladat) tartomanyok.Add(f.Tartomany);
        string tartomany = string.Empty;
        double max = 0;
        int id = 0;
        for (int i = 0; i < tartomanyok.Count; i++)
        {
            tartomany = tartomanyok[i];
            max = lakossag.Where(l => l.Tartomany.Equals(tartomanyok[i])).MaxBy(l => l.SorFogyasztasEvente).SorFogyasztasEvente;
            id = lakossag.Where(l => l.Tartomany.Equals(tartomanyok[i])).MaxBy(l => l.SorFogyasztasEvente).Id;
            MegoldasLista.Items.Add($"{tartomany} - {id} - {max}");
        }
    }

    private void Feladat30_5()
    {
        var legidosebbFerfi = lakossag.Where(l => l.Nem.Equals("férfi")).OrderByDescending(l => l.Kor).First();
        var legidosebbNo = lakossag.Where(l => l.Nem.Equals("nő")).OrderByDescending(l => l.Kor).First();
        MegoldasLista.Items.Add(legidosebbFerfi.ToString(true));
        MegoldasLista.Items.Add(legidosebbNo.ToString(true));
    }

    private void Feladat31_5()
    {
        var feladat = lakossag.DistinctBy(l => l.Nemzetiseg).OrderByDescending(l => l.Nemzetiseg).ToList();
        foreach (var f in feladat) MegoldasLista.Items.Add($"{f.Nemzetiseg}");
    }

    private void Feladat32_5()
    {
        var feladat = lakossag.GroupBy(l => l.Tartomany).ToDictionary(l => l.Key, l => l.Count()).OrderBy(l => l.Value);
        foreach (var f in feladat) MegoldasLista.Items.Add(f.Key);
    }

    private void Feladat33_5()
    {
        var feladat = lakossag.OrderByDescending(l => l.NettoJovedelem).ToList();
        for (var i = 0; i < 3; i++) MegoldasLista.Items.Add($"{feladat[i].Id} - {feladat[i].NettoJovedelem}");
    }

    private void Feladat34_5()
    {
        var feladat = lakossag.Where(l => l.Nem.Equals("férfi") && l.KrumpliFogyasztasEvente > 55).Average(l => l.Suly);
        MegoldasMondatos.Content = $"Az évente 55 kilónál több krumpit fogyasztó férfiak átlag súlya: {feladat:0} kg ";
    }

    private void Feladat35_5()
    {
        var feladat = lakossag.GroupBy(l => l.Tartomany).ToDictionary(l => l.Key, l => l.MinBy(l => l.Kor).Kor);
        foreach (var f in feladat) MegoldasLista.Items.Add($"{f.Key} - {f.Value}");
    }

    private void Feladat36_5()
    {
        List<string> feladat = new();
        foreach (var l in lakossag) feladat.Add($"{l.Nemzetiseg} - {l.Tartomany}");
        feladat = feladat.Distinct().Order().ToList();
        foreach (var f in feladat) MegoldasLista.Items.Add(f);
    }

    private void Feladat37_5()
    {
        var feladat = lakossag.Where(l => l.NettoJovedelem > lakossag.Average(l => l.NettoJovedelem));
        MegoldasLista.Items.Add($"A {lakossag.Average(l => l.NettoJovedelem):0} átlag fizetést meghaladó lakosok száma: {feladat.Count()}");
        foreach (var f in feladat) MegoldasLista.Items.Add(f.ToString(false));
    }

    private void Feladat38_5()
    {
        var ferfiDB = lakossag.Where(l => l.Nem.Equals("férfi")).Count();
        var noDB = lakossag.Where(l => l.Nem.Equals("nő")).Count();
        MegoldasMondatos.Content = $"A férfiak száma {ferfiDB}, a nőké pedig {noDB}";
    }

    private void Feladat39_5()
    {
        var feladat = lakossag.GroupBy(l => l.Tartomany).ToDictionary(l => l.Key, l => l.MaxBy(l => l.NettoJovedelem).NettoJovedelem).OrderByDescending(l => l.Value);
        foreach (var f in feladat) MegoldasLista.Items.Add($"{f.Key} - {f.Value}");
    }

    private void Feladat40_5()
    {
        var nemet = lakossag.Where(l => l.Nemzetiseg.Equals("német")).Sum(l => l.HaviNettoJovedelem) / lakossag.Sum(l => l.HaviNettoJovedelem) * 100;
        var nemNemet = lakossag.Where(l => !l.Nemzetiseg.Equals("német")).Sum(l => l.HaviNettoJovedelem) / lakossag.Sum(l => l.HaviNettoJovedelem) * 100;
        MegoldasMondatos.Content = $"Németek havi jövedelme százalékosan: {nemet:0}%, nem németeké: {nemNemet:0}%";
    }

    private void Feladat41_5()
    {
        var feladat = lakossag.Where(l => l.Nemzetiseg.Equals("török")).ToList();
        for (int i = 0; i < 10; i++)
        {
            var item = feladat[Random.Shared.Next(0, feladat.Count())];
            MegoldasTeljes.Items.Add(item);
            feladat.Remove(item);
        }
    }

    private void Feladat42_5()
    {
        var feladat = lakossag.Where(l => l.SorFogyasztasEvente > lakossag.Average(l => l.SorFogyasztasEvente)).ToList();
        MegoldasLista.Items.Add($"Az átlagos sörfogyasztás évente {lakossag.Average(l => l.SorFogyasztasEvente):0}");
        for (int i = 0; i < 5; i++)
        {
            var item = feladat[Random.Shared.Next(0, feladat.Count())];
            MegoldasLista.Items.Add(item.ToString(true));
            feladat.Remove(item);
        }
    }

    private void Feladat43_5()
    {
        double atlag = lakossag.Average(l => l.NettoJovedelem);
        var feladat = lakossag.GroupBy(l => l.Tartomany).ToDictionary(l => l.Key, l => l.MinBy(l => l.NettoJovedelem).NettoJovedelem).Where(l => l.Value > atlag);
        MegoldasLista.Items.Add($"Az átlag: {atlag:0.00}");
        foreach (var f in feladat) MegoldasLista.Items.Add($"{f.Key} - {f.Value}");
    }

    private void Feladat44_5()
    {
        var feladat = lakossag.Where(l => l.IskolaiVegzettseg == null).ToList();
        for (int i = 0; i < 3; i++)
        {
            var item = feladat[Random.Shared.Next(0, feladat.Count())];
            MegoldasTeljes.Items.Add(item);
            feladat.Remove(item);
        }
    }

    private void Feladat45_5()
    {
        var feladat = lakossag.Where(l => l.Nem.Equals("nő") && l.IskolaiVegzettseg == "Universität" && !l.Nemzetiseg.Equals("bajor")).ToList();
        for (int i = 0; i < 5; i++) MegoldasTeljes.Items.Add(feladat[i]);
        var feladat2 = lakossag.Where(l => l.Nem.Equals("nő") && l.Nemzetiseg.Equals("német") && l.NettoJovedelem > feladat.First().NettoJovedelem).ToList();
        for (int i = 0; i < 3; i++)
        {
            var item = feladat[Random.Shared.Next(0, feladat.Count())];
            MegoldasLista.Items.Add(item.ToString(false));
            feladat.Remove(item);
        }
    }


    #endregion


}
