using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryGame_UI
{
    public class Program
    {
        public static void Main()
        {
            Application.EnableVisualStyles();
            UI gameUI = new UI();
            gameUI.Run();
        }
    }
}
