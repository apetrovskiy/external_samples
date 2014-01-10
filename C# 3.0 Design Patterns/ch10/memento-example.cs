using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;

// Memento Pattern                Judith Bishop     September 2007
//  Simulates TicTacToe, where the game can roll back any
//  specified number of moves. Mementos are kept at each move.7
  
    class MementoPattern {

        // Client
		static void Main() {
            Console.WriteLine("Let's practice TicTacToe");
			Console.WriteLine("Commands are:\n1-9 for a position\n"+
									 "U-n where n is the number of moves to undo"+
									 "\nQ to end");
			Game game = new Game();
			
			//References to the mementos
			Caretaker [] c = new Caretaker [10];
			
			game.DisplayBoard();
			int move = 1;
			// Iterator for the moves
			Simulator simulator = new Simulator();
			
			foreach (string command in simulator) {
				Console.Write("Move "+move+" for "+game.Player+": "+command);
				if (command[0]=='Q') break;
				
				//Save at the start of each move
				c[move] = new Caretaker();
				c[move].Memento = game.Save();
				
				// Check for undo
				if (command[0]=='U') {
					int back = Int32.Parse(command.Substring(2,1));
					if (move-back >0) 
						game.Restore(c[move-back].Memento);
					else
						Console.WriteLine("Too many moves back");
					move = move - back - 1;
				}
				// otherwise play
				else	
					game.Play(Int32.Parse(command.Substring(0,1)));
				
				// Update board and move number
				game.DisplayBoard();
				move++;
			} 
			Console.WriteLine("Thanks for playing");
		}

    // Originator 
    [Serializable()]
    class Game {
		// nine spaces
		char [] board = {'X','1','2','3','4','5','6','7','8','9'};
		public char Player {get; set;}
		
		public Game ()  {
			Player = 'X';
		}
		
		public void Play (int pos) {
			board[pos] = Player;
			if (Player == 'X') Player = 'O'; else Player = 'X';
			// preserve player
			board[0] = Player;
		}
		
        // The reference to the memento is passed back to the client
		public Memento Save() { 
            Memento memento = new Memento();
            return memento.Save(board);
        }

        public void Restore(Memento memento) {
            board = (char []) memento.Restore();
			Player = board[0];
        }
		
		public void DisplayBoard() {
			Console.WriteLine();
			for (int i = 1; i<=9; i+=3) {
				Console.WriteLine(board[i]+" | "+board[i+1]+" | "+board[i+2]);
				if (i<6) Console.WriteLine("---------");
			}
		}
    }

	[Serializable()]
	// Serializes by deep copy to memory and back
	class Memento {
        MemoryStream stream = new MemoryStream();
        BinaryFormatter formatter = new BinaryFormatter();

        public Memento Save (object o) {
            formatter.Serialize(stream, o);
            return this;
        }

        public object Restore() {
            stream.Seek(0, SeekOrigin.Begin);
            object o = formatter.Deserialize(stream);
            stream.Close();
            return o;
        }
    }

    class Caretaker {
        public Memento Memento {get; set;}
    }
	
  class Simulator : IEnumerable {
	
   string [] moves = {"5","3","1","6","9","U-2","9","6","4","2","7","8","Q"};

  public IEnumerator GetEnumerator () {
    foreach( string element in moves )
        yield return element;
    }
  }

}
/* Output
Let's practice TicTacToe
Commands are:
1-9 for a position
U-n where n is the number of moves to undo
Q to end

1 | 2 | 3
---------
4 | 5 | 6
---------
7 | 8 | 9
Move 1 for X: 5
1 | 2 | 3
---------
4 | X | 6
---------
7 | 8 | 9
Move 2 for O: 3
1 | 2 | O
---------
4 | X | 6
---------
7 | 8 | 9
Move 3 for X: 1
X | 2 | O
---------
4 | X | 6
---------
7 | 8 | 9
Move 4 for O: 6
X | 2 | O
---------
4 | X | O
---------
7 | 8 | 9
Move 5 for X: 9
X | 2 | O
---------
4 | X | O
---------
7 | 8 | X
Move 6 for O: U-2
X | 2 | O
---------
4 | X | 6
---------
7 | 8 | 9
Move 4 for O: 9
X | 2 | O
---------
4 | X | 6
---------
7 | 8 | O
Move 5 for X: 6
X | 2 | O
---------
4 | X | X
---------
7 | 8 | O
Move 6 for O: 4
X | 2 | O
---------
O | X | X
---------
7 | 8 | O
Move 7 for X: 2
X | X | O
---------
O | X | X
---------
7 | 8 | O
Move 8 for O: 7
X | X | O
---------
O | X | X
---------
O | 8 | O
Move 9 for X: 8
X | X | O
---------
O | X | X
---------
O | X | O
Move 10 for O: QThanks for playing
*/
