# Bevolkerung5Solutions
# Bevölkerung (Lakosság)

Németország anonimizált lakossági nyilvántartásából kiemelt adatokat kell feldolgoznia.

## 1. rész

Az adatforrás `bevölkerung.txt` UTF-8 kódolású szöveges állomány. Az állomány első sora tartalmazza a mezőneveket (ezeket felhasználhatja a mezők definiálásához). Az adatok pontosvesszővel vannak elválasztva. A mezőket a megadott típussal hozza létre.

Az osztály neve legyen `Allampolgar`. A lista neve legyen `lakossag`.

A feladatsor megoldásához segítségül hívhatja a mellékelt `bevölkerung.xlsx` állományt.

### Az egyes mezőnevek jellemzői (pontosan ezeket a mezőneveket használja)

- `Id`: az állampolgár azonosítója (egész szám)
- `Nem`: az állampolgár neme (szöveg: férfi, nő)
- `SzuletesiEv`: az állampolgár születési éve (egész szám)
- `Suly`: az állampolgár súlya kg-ban (egész szám)
- `Magassag`: az állampolgár magassága cm-ben (egész szám)
- `Dohanyzik`: dohányzik-e az állampolgár (logikai, értékei jelenleg: igen, nem)
- `Nemzetiseg`: az állampolgár nemzetisége (szöveg, pl. német, török, lengyel, angolai, etióp, stb.)
- `Nepcsoport`: az állampolgár népcsoportja; ha nem német, null az értéke (szöveg, pl. szász, bajor, sváb, stb.)
- `Tartomany`: a tartomány neve, ahol az állampolgár lakik (szöveg, pl. Bajorország, Hessen, stb.)
- `NettoJovedelem`: az állampolgár nettó éves jövedelme euróban (EUR) (egész szám)
- `IskolaiVegzettseg`: az állampolgár legmagasabb iskolai végzettsége (szöveg: Grundschule, Hauptschule, Realschule, Gymnasium, Gesamtschule, Berufsschule, Fachhochschule, Universität, illetve null érték is lehet)
- `PolitikaiNezet`: az állampolgár politikai beállítódása (szöveg, pl. szociáldemokrata, konzervatív, liberális, populista, szocialista, egyéb)
- `AktivSzavazo`: az állampolgár aktív szavazó-e? (logikai, értékei jelenleg: igen, nem)
- `SorFogyasztasEvente`: az állampolgár évente hány liter sört fogyaszt? (egész szám, vagy NA, ha nincs adat)
- `KrumpliFogyasztasEvente`: az állampolgár évente hány kg krumplit fogyaszt? (egész szám, vagy NA, ha nincs adat)

## Feladatleírás

Készítsen grafikus alkalmazást a következő feladatok megoldására, melynek projektjét `BevolkerungGUI` néven mentse el.

1. Olvassa be a forrásállományban talált adatokat, és tárolja el egy osztálypéldányokat tartalmazó listában. A forrásállományt az `src` mappában helyezze el a tanult módon.
2. Implementáljon az osztályban egy megoldást, ami visszaadja az adott állampolgár havi nettó jövedelmét.
3. Implementáljon az osztályban egy megoldást, ami visszaadja az adott állampolgár életkorát. A mindenkori aktuális évet vegye figyelembe az életkor kiszámításánál.
4. Implementáljon az osztályban egy `ToString()` függvényt, ami visszaad tabulátorokkal elválasztva a paraméterétől függően adatokat szövegesen. Ha `true` a paraméter értéke, akkor az első 5 mező értékét, ha `false`, akkor az `Id`-t, továbbá a következő 4 mezőt (`Nemzetiseg`, `Nepcsoport`, `Tartomany`, `NettoJovedelem`).
5. A grafikus ablak kialakításánál a leírás mellett vegye figyelembe az első mintaablakot. Az ablak mérete: 750 * 1300. A logikai értéket tartalmazó mezők igen/nem formában jelenjenek meg. A `Nepcsoport` és az `IskolaiVegzettseg` esetén ahol `null` érték van, ott ne jelenjen meg semmi, illetve a sör-, és krumplifogyasztás adatai az eredeti formában jelenjenek meg.
6. A képernyőn jelenítsen meg egy `ComboBox`-ot, aminek az értékeit töltse föl 1-től 40-ig, a számok mögött pont jelenjen meg. Mellé egy `Megoldas` nevű `Label`-ben jelenítse meg a következő szöveget: „feladat egy mondatos megoldása: ”. Mellette helyezzen el egy `MegoldasMondatos` nevű `Label`-t, amelyben a feladatok egy mondatos megoldása megjelenik.
7. Ezek alatt a program indulásakor jelenítse meg a `MegoldasTeljes` nevű `DataGrid`-ben a teljes lista tartalmát, az összes, magyarul megjelenő mezővel. Használja a `DataBinding` technikát. Javasolt a következő tulajdonságokat beállítani:
   - `Height="310"`
   - `Width="auto"`
   - `AutoGenerateColumns="False"`

   Az oszlopok beállításai:
   ```xml
   <DataGrid.Columns>
       <DataGridTextColumn Header="Id" Binding="{Binding Id}" />
       <!-- és így tovább -->
   </DataGrid.Columns>
8.	Helyezzen el a DataGrid alatt egy üres ListBoxot MegoldasLista néven. Itt jelennek meg majd azok a megoldások, amit listaszerűen lehet megjeleníteni.
A továbbiakban a feladatok háromféle megjelenítést kérnek, általában egyszerre csak az egyiket, de esetenként kettőt is:
•	egész mondatban megjelenítve –> ekkor a MegoldasMondatos labelben válaszoljon
•	a teljes rekordok megjelenítése –> ekkor a MegoldasTeljes nevű DataGridben válaszoljon
•	más formájú, általában felsorolásos megoldás –> ekkor a MegoldasLista nevű ListBoxban válaszoljon.
9.	Amikor a felhasználó új feladatot választ, töröljük az előző megoldást az összes vezérlőben, majd jelenítsük meg az új megoldást/megoldásokat. Javasolt a metódusoknak azonos szerkezetű elnevezést adni, hogy automatizálni tudjuk a metódusok működtetését. Ha a felhasználó kiválasztja a feladatot, a megfelelő megoldást kell megjeleníteni a 3 vezérlőben.
2. rész - további feladatok
Az átlagértékeket mindig 2 tizedesjegyre kerekítve adja meg.
1.	Írja ki a legmagasabb nettó éves jövedelmet.
2.	Számítsa ki az állampolgárok átlagos nettó éves jövedelmét.
3.	Csoportosítsa az állampolgárokat tartományok szerint, és írja ki a tartományok nevét, illetve az oda tartozó állampolgárok számát.
4.	Szűrje le az összes angolai állampolgárt, és írja ki az összes adatukat.
5.	Írja ki a legfiatalabb állampolgár összes adatát. Ha több is, van, akkor mindegyiket.
6.	Sorolja fel az összes nem dohányzó állampolgár azonosítóját, és havi jövedelmét.
7.	Szűrje le azokat az állampolgárokat, akik Bajorországban élnek, éves nettó jövedelmük meghaladja a 30000 EUR-t, és rendezze őket iskolai végzettség szerint. Írja ki az összes rekordot.
8.	Listázza az összes férfi állampolgár adatait a ToString(true) függvény használatával.
9.	Szűrje le azon nőket, akik Bajorországban élnek, majd jelenítse meg az adataikat a ToString(false) függvénnyel.
10.	Keresse meg az összes nem dohányzó állampolgárt, majd adja vissza közülük a legmagasabb jövedelműek 10-es listáját.
11.	Adja vissza az 5 legidősebb állampolgár összes adatát.
12.	Csoportosítsa a német nemzetiségű állampolgárokat népcsoport szerint. Jelenítse meg eredményként a népcsoport nevét, alatta pedig állampolgáronként azt, hogy az illető aktív szavazó e (aktív szavazó /nem aktív szavazó formában), és a politikai nézetét. További mezőket nem kell kiírni.
13.	Számítsa ki az éves átlagos sörfogyasztást a férfiak körében.
14.	Sorolja fel az állampolgárokat iskolai végzettségük szerint csoportosítva (sorba rendezve).
15.	Jelenítse meg a 3 legmagasabb és 3 legalacsonyabb nettó éves jövedelmű állampolgárok adatait a ToString(false) függvény segítségével. Ha több is van, akkor is csak az első 3-3 találatra van szükség. (További magyarázat megjelenítésére nincs szükség.)
16.	Számítsa ki két tizedesjegy pontossággal, hogy az állampolgárok hány százaléka aktív szavazó.
17.	Csoportosítsa az aktív szavazókat tartományok szerint, és adja vissza egymás alatt a csoportosított sorokat. Jelenjen meg mindenki összes adata. (Help: ez rendezési feladat.)
18.	Számítsa ki az állampolgárok átlagos életkorát. 
19.	Határozza meg, melyik tartományban a legmagasabb az átlagos éves nettó jövedelem. Ha van két ilyen tartomány is, akkor azt jelenítse meg, ahol több a lakosok száma. Jelenjen meg a tartomány neve, az átlagos éves nettó jövedelem, és a lakosok száma.
20.	Számítsa ki az állampolgárok átlagos súlyát, és írja ki a medián súlyt is. ( A medián egy eloszlás közepét jelöli. Ha a súlyokat sorba rendezzük növekvő sorrendben, akkor a medián az páratlan számú adat esetén a középső érték lesz, páros számú adat esetén a két középső érték átlaga.)
21.	Számítsa ki, hogy az aktív szavazók vagy a nem szavazók fogyasztanak-e több sört évente átlagosan. Jelenítse meg a két értéket, és a döntést.
22.	Számítsa ki, hogy átlagosan milyen magasak a férfiak, és külön a nők az adatbázisban. Jelenítse meg a két értéket.
23.	Határozza meg, hogy melyik népcsoportba tartoznak a legtöbben (első találat). Írja ki a népcsoportot és az értéket. Ha van két ilyen népcsoport is, akkor azt a népcsoportot jelenítse meg, ahol az átlagos életkor magasabb.
24.	Kinek magasabb az átlagos nettó éves jövedelme: a dohányzóké, vagy a nem dohányzóké? Jelenítse meg a mondatban mindkét értéket.
25.	Számítsa ki, és írja ki az átlagos krumplifogyasztást. Jelenítse meg az első 15 lakos összes adatát, aki az átlag fölött fogyaszt.
26.	Számítsa ki az állampolgárok átlagos életkorát tartományonként. Jelenjen meg a tartomány neve és az érték.
27.	Listázza az összes állampolgár első 5 adatát, akik 50 évnél idősebbek. Jelenítse meg ezek darabszámát is.
28.	Szűrje le a dohányzó nőket, és jelenítse meg az adataikat a ToString(false) függvény használatával.  Írja ki a  maximális nettó éves jövedelmüket.
29.	Határozza meg, hogy az egyes tartományokban ki a legnagyobb sörfogyasztó. Írja ki soronként a tartomány nevét, a lakos Id-jét, és a sörfogyasztását.
30.	Határozza meg a legidősebb női és férfi állampolgárt. Írja ki az adataikat a ToString(true) függvény használatával, ha több is van, akkor csak az elsőket.
31.	Írja ki az összes különböző nemzetiséget csökkenő abc-ben. 
32.	Írja ki az összes különböző tartományt a lakosok száma szerinti növekvő sorrendben. Csak a tartomány neve jelenjen meg.
33.	Írja ki az első 3 legmagasabb jövedelmű állampolgár azonosítóját és jövedelmét.
34.	Számítsa ki azoknak a férfiaknak az átlagos súlyát, akiknek 55 kg feletti a krumplifogyasztása.
35.	Csoportosítsa az állampolgárokat tartományok szerint, és írja ki csoportonként a minimális  életkort. Ha több érték is van, akkor csak az elsőt.
36.	Listázza az összes különböző nemzetiség és tartomány párost.
37.	Listázza azokat az állampolgárokat a ToString(false) függvény használatával, akiknek a jövedelme meghaladja az átlagot. Írja ki az átlagot és a leszűrt állampolgárok darabszámát is.
38.	Számítsa ki a nők és a férfiak számát.
39.	Határozza meg az egyes tartományokban a legmagasabb nettó éves jövedelmeket. Jelenjen meg a tartomány neve és a számított érték. A listát rendezze az érték szerint csökkenőbe.
40.	Számítsa ki a németek havi jövedelmének és a nem németek havi jövedelmének százalékos arányát.
41.	Jelenítse meg véletlenszerűen maximum 10 török, aktív szavazó állampolgár összes adatát.
42.	Jelenítse meg véletlenszerűen 5 olyan állampolgár adatait ToString(true) függvény használatával, akik átlag fölött fogyasztanak sört. Írja ki az átlagos értéket is.
43.	Válasszon ki véletlenszerűen 2 olyan tartományt, ahol a legkisebb nettó jövedelem is nagyobb a teljes lakosságból számított átlagnál. Írja ki az átlagos értéket is. A felsorolásban jelenjen meg a tartomány neve és a legkisebb nettó jövedelem értéke.
44.	Jelenítsen meg véletlenszerűen 3 olyan állampolgárt (az összes adatával), akiknek nem ismerjük az iskolai végzettségét.
45.	Jelenítse meg az első 5 (eredeti felviteli sorrend szerinti), egyetemi végzettségű (Universität) nő összes adatát, kivéve a bajor népcsoportba tartozó nőket. Jelenítse meg véletlenszerűen 3 olyan német nemzetiségű nő első 5 adatát, akiknek több a jövedelmük, mint az előző találati listában található első nőé.
