using System.Diagnostics;
using AdventofCode._2021.Day15;

/*

var solution1 = new Solution1("Data2.txt");
var answer1 = solution1.Solve();

Console.WriteLine($"Lowest Risk is : {answer1}");
// Answer for data2 is 447
*/

Stopwatch watch1 = new();
watch1.Start();

var solution2 = new Solution2("Data2.txt");
var answer2 = solution2.Solve();

watch1.Stop();

Console.WriteLine($"Lowest Risk is : {answer2}; Answer took {watch1.Elapsed.TotalSeconds} seconds");
// Answer for data2 is 2967, 2835 - took 2116 secs


/*Stopwatch watch2 = new();
watch2.Start();

var puzzle = new Puzz15();
var answer = puzzle.GiveMeTheAnswerPart20();

watch2.Stop();
Console.WriteLine($"Lowest Risk is : {answer}; Answer took {watch2.Elapsed.TotalSeconds} seconds");*/


