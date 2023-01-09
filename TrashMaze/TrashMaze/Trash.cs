using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TrashMaze
{
    /// <summary>
    /// Klasa opisująca elementy zbierane przez gracza
    /// </summary>
    public class Trash
    {
        /// <summary>
        /// Konstruktor klasy Trash
        /// </summary>
        public Trash() { }
        /// <summary>
        /// Tablica z odpadami plastikowymi
        /// </summary>
        string[] plasticTab = new string[8] {"Karton po mleku", "Karton po soku","Butelka po wodzie", "Puszka po konserwie", "Puszka po Coli","Aluminiowa folia", "Opakowanie po jogurcie", "Butelka po soku"};
        /// <summary>
        /// Tablica z odpadami papierowymi
        /// </summary>
        string[] paperTab = new string[8] {"Karton","Tektura","Zeszyt", "Książka","Zadrukowane kartki","Gazeta","Kartka", "Komiks"};
        /// <summary>
        /// Tablica z odpadami szklanymi
        /// </summary>
        string[] glassTab = new string[5] {"Butelka po piwie", "Butelka po winie", "Uszczerbiona szklanka", "Słoik po ogórkach", "Słoik po burakach" };
        /// <summary>
        /// Metoda zwracająca rodzaj odpadu plastikowego z tablicy
        /// </summary>
        /// <param name="i">Numer pola tablicy z odpadami</param>
        /// <returns>Zwraca konkrety odpad platikowy z tablicy</returns>
        public string plastic (int i)
        {
            return plasticTab[i];
        }
        /// <summary>
        /// Metoda zwracająca rodzaj odpadu papierowego z tablicy
        /// </summary>
        /// <param name="i">Numer pola tablicy z odpadami</param>
        /// <returns>Zwraca konkrety odpad papierowy z tablicy</returns>
        public string papers (int i)
        {
            return paperTab[i];
        }
        /// <summary>
        /// Metoda zwracająca rodzaj odpadu szklanego z tablicy
        /// </summary>
        /// <param name="i">Numer pola tablicy z odpadami</param>
        /// <returns>Zwraca konkrety odpad szklany z tablicy</returns>
        public string glass (int i)
        {
            return glassTab[i];
        }
    }
}
