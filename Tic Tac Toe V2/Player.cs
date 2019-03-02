using System.Collections.Generic;

namespace Tic_Tac_Toe_V2
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public string Soldier { get; set; }
        public List<string> Selections { get; set; }
    }
}
