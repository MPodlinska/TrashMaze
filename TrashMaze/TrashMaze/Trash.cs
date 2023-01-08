using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TrashMaze
{
    public class Trash
    {
        public Trash() { }

        string[] plasticTab = new string[8] {"Karton po mleku", "Karton po soku","Butelka po wodzie", "Puszka po konserwie", "Puszka po Coli","Aluminiowa folia", "Opakowanie po jogurcie", "Butelka po soku"};
        string[] paperTab = new string[8] {"Karton","Tektura","Zeszyt", "Książka","Zadrukowane kartki","Gazeta","Kartka", "Komiks"};
        string[] glassTab = new string[5] {"Butelka po piwie", "Butelka po winie", "Uszczerbiona szklanka", "Słoik po ogórkach", "Słoik po burakach" };
        
        public string plastic (int i)
        {
            return plasticTab[i];
        }
        public string papers (int i)
        {
            return paperTab[i];
        }
        public string glass (int i)
        {
            return glassTab[i];
        }
    }
}
