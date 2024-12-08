using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bevolkerung5Solutions
{
    internal class Allampolgar
    {
        public int Id { get; set; }
        public string Nem { get; set; }
        public int SzuletesiEv { get; set; }
        public int Kor => DateTime.Now.Year - SzuletesiEv; //redundáns, a GetAge is erre való
        public int Suly { get; set; }
        public int Magassag { get; set; }
        public bool Dohanyzik { get; set; }
        public string DohanyzikSzoveges => Dohanyzik ? "igen" : "nem";
        public string Nemzetiseg { get; set; }
        public string Nepcsoport { get; set; }
        public string Tartomany { get; set; }
        public int NettoJovedelem { get; set; }
        public double HaviNettoJovedelem => (double)NettoJovedelem / 12;
        public string IskolaiVegzettseg { get; set; }
        public string PolitikaiNezet { get; set; }
        public bool AktivSzavazo { get; set; }
        public string AktivSzavazoSzoveges { get; set; }
        public int SorFogyasztasEvente { get; set; }
        public string SorFogyasztasEventeSzoveges { get; set; }
        public int KrumpliFogyasztasEvente { get; set; }
        public string KrumpliFogyasztasEventeSzoveges { get; set; }

        public Allampolgar(string sor)
        {
            string[] adatok = sor.Split(';');

            Id = int.Parse(adatok[0]);
            Nem = adatok[1];
            SzuletesiEv = int.Parse(adatok[2]);
            Suly = int.Parse(adatok[3]);
            Magassag = int.Parse(adatok[4]);
            Dohanyzik = adatok[5].ToLower() == "igen";
            Nemzetiseg = adatok[6];

            if (Nemzetiseg == "német")
                Nepcsoport = adatok[7];
            else Nepcsoport = null;

            Tartomany = adatok[8];
            NettoJovedelem = int.Parse(adatok[9]);
            IskolaiVegzettseg = adatok[10];
            PolitikaiNezet = adatok[11];
            AktivSzavazo = adatok[12].ToLower() == "igen";
            AktivSzavazoSzoveges = adatok[12];

            SorFogyasztasEventeSzoveges = adatok[13];
            SorFogyasztasEvente = adatok[13] != "NA" ? SorFogyasztasEvente = int.Parse(adatok[13]) : -1;
            KrumpliFogyasztasEventeSzoveges = adatok[14];
            KrumpliFogyasztasEvente = adatok[14] != "NA" ? KrumpliFogyasztasEvente = int.Parse(adatok[14]) : -1;
        }
        public int GetAge() //redundáns, a Kor is erre való (A GetAge volt a feladatban)
        {
            return DateTime.Now.Year - SzuletesiEv;
        }

        public override string ToString()
        {
            return $"{Id} {Nem} {SzuletesiEv} {Suly}kg {Magassag}cm {(Dohanyzik ? "dohányzik" : "nem dohányzik")} {Nemzetiseg} {Nepcsoport} {Tartomany} {NettoJovedelem}EUR {IskolaiVegzettseg} {PolitikaiNezet} {(AktivSzavazo ? "aktív szavazó" : "nem aktív szavazó")} {SorFogyasztasEvente}l sör {KrumpliFogyasztasEvente}kg krumpli";
        }

        public string ToString(bool firstFive)
        {
            if (firstFive)
                return $"{Id}\t{Nem}\t{SzuletesiEv}\t{Suly} kg\t{Magassag} cm";
            else return $"{Id}\t{Nemzetiseg}\t{Nepcsoport}\t{Tartomany}\t{NettoJovedelem.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("de-DE"))}";
        }
    }
}
