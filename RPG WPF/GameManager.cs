using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_WPF
{
    class GameManager
    {
        public List<Classa> Classy = new List<Classa>();
        public GameManager(List<Classa> classy) { 
        Classy = classy;
        }
        
    }
}
