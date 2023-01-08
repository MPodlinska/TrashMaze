using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TrashMaze
{
    public class Trash
    {
        /// <summary>
        /// konstruktor odpadu
        /// </summary>
        public Trash() { }
        /// <summary>
        /// tablica z odpadami plastikowymi
        /// </summary>
        string[] plasticTab = new string[8] {"Karton po mleku", "Karton po soku","Butelka po wodzie", "Puszka po konserwie", "Puszka po Coli","Aluminiowa folia", "Opakowanie po jogurcie", "Butelka po soku"};
        /// <summary>
        /// tablica z odpadami papierowymi
        /// </summary>
        string[] paperTab = new string[8] {"Karton","Tektura","Zeszyt", "Książka","Zadrukowane kartki","Gazeta","Kartka", "Komiks"};
        /// <summary>
        /// tablica z odpadami szklanymi
        /// </summary>
        string[] glassTab = new string[5] {"Butelka po piwie", "Butelka po winie", "Uszczerbiona szklanka", "Słoik po ogórkach", "Słoik po burakach" };
        /// <summary>
        /// metoda zwracająca rodzaj odpadu plastikowego z tablicy
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public string plastic (int i)
        {
            return plasticTab[i];
        }
        /// <summary>
        /// metoda zwracająca rodzaj odpadu papierowego z tablicy
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public string papers (int i)
        {
            return paperTab[i];
        }
        /// <summary>
        /// metoda zwracająca rodzaj odpadu szklanego z tablicy
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public string glass (int i)
        {
            return glassTab[i];
        }
    }
}
