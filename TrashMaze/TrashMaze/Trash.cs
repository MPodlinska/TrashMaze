using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TrashMaze
{
    public class Trash
    {
        string[] plasticTab = new string[6] {"Karton po mleku", "Karton do soku","Butelka po wodzie", "Puszka po konserwie", "Puszka po Coli","Aluminiowa folia"};
        string[] paperTab = new string[6] {"Karton","Tektura","Zeszyt", "Książka","Zadrukowane kartki","Gazeta" };
        string[] glassTab = new string[3] {"Butelka po piwie","Słoik po ogórkach","Uszczerbiona szklanka"};
        int i;
        Random rand = new Random();
        public Trash()
        {
        }
        public string trashPla()
        {
            i = rand.Next(7);
            return plasticTab[i];
        }
        public string trashPap()
        {
            i = rand.Next(7);
            return paperTab[i];
        }
        public string trashGla()
        {
            i = rand.Next(4);
            return glassTab[i];
        }

    }
}
