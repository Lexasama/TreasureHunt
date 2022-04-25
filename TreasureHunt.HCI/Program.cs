using TreasureHunt.Game;


Console.WriteLine("Welcome");

Console.WriteLine("Enter the path to the starting file");
string path = Console.ReadLine();


Game game = new Game(path);

string output = Console.ReadLine();
await game.Save(output);






/*-------------------*/
/* Coded By Hamsters */
/* -- Lexasama --    */
/*-------------------*/