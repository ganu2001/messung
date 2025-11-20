

// See https://aka.ms/new-console-template for more information
using ConsoleApp1;
//Repeat:
//string[] lines = new string[] { };
//string[] FunctionBlock = new string[] { };

//Array.Clear(FunctionBlock, 0, FunctionBlock.Length);
//Array.Clear(FunctionExpression, 0, FunctionExpression.Length);
//Array.Clear(FunctionExpression1, 0, FunctionExpression1.Length);


//Console.WriteLine();

//Console.WriteLine("Hello, World!");
// The files used in this example are created in the topic
// How to: Write to a Text File. You can change the path and
// file name to substitute text files of your own.
//string expression = "";
// Example #1
// Read the file as one string.
//string text = System.IO.File.ReadAllText(@"// See https://aka.ms/new-console-template for more information");
//Console.WriteLine("Hello, World!");
// The files used in this example are created in the topic
// How to: Write to a Text File. You can change the path and
// file name to substitute text files of your own.
//string expression = "";
// Example #1
// Read the file as one string.
System.Console.WriteLine("Enter the file ");
//string fun2 = "";
//string fun11 = "";
//string fun22 = "";
//string fun1 = "";
string userInput = Console.ReadLine();
string text = System.IO.File.ReadAllText(@"C:\" + userInput + ".txt");

// Display the file contents to the console. Variable text is a string.
System.Console.WriteLine("Contents of WriteText.txt = {0}", text);

// Example #2
// Read each line of the file into a string array. Each element
// of the array is one line of the file.

string[] lines = System.IO.File.ReadAllLines(@"C:\" + userInput + ".txt");

// Display the file contents by using a foreach loop.

//System.Console.WriteLine("line contains " + lines.Length); //(22)
System.Console.WriteLine();
//Console.WriteLine("length of abc = " + abc.Length);
//string abc = "( (**) (**) (**) , (**) (*T1*) (**)";




//// Display the file contents to the console. Variable text is a string.
//System.Console.WriteLine("Contents of WriteText.txt = {0}", text);

//// Example #2
//// Read each line of the file into a string array. Each element
//// of the array is one line of the file.
//string[] lines = System.IO.File.ReadAllLines(@"C:\Example 1");

//// Display the file contents by using a foreach loop.
//System.Console.WriteLine("Contents of WriteLines2.txt = ");
///*foreach (string line in lines)
//{
//    if (line.Contains("OTE"))
//    {
//       // var temp = from s in line where
//        expression = line.Substring(12, 2) + " = ";
//    }
//    // Use a tab to indent each line of the file.
//    Console.WriteLine("\t" + line);

//}*/



//Console.Write(expression.ToString());
bool NSBfound = false;
bool first = true;
bool bracket = false;
string expression1 = "";
string eor = "EOR";


List<List<string>> rungs = new List<List<string>>();

// getting rungs as list from the given text file
for (int i = 0; i < lines.Length; i++)
{
    var line = lines[i];
    Console.WriteLine("-> " + line);
    if (line.Contains("SOR"))
    {
        Console.WriteLine("Start of rung");
        List<string> arr = new List<string>();
        for (int j = i + 1; j < lines.Length; j++)
        {
            if (lines[j].Contains("EOR"))
            {
                i = j - 1;
                rungs.Add(arr);
                break;
            }
            else
            {
                arr.Add(lines[j]);
                //Console.WriteLine(arr[i]);

            }

        }
    }

}

for (int i = 0; i < rungs.Count; i++)
{


    RungOperations.formula(rungs[i]);
}
Console.WriteLine();


Console.WriteLine("Press 1 to continue and 2 to exit.");

int var1 = Convert.ToInt32(Console.ReadLine());
if (var1 == 1)
{
    goto Repeat;
    //  Array.Clear(lines, 0, lines.Length);
}

else
{
    Console.WriteLine("press any key to exit");
    System.Console.ReadKey();
}
