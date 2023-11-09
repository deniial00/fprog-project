namespace FPROG.Tokenizer;

class Program {
    static void Main(string[] args)
    {
        var reader = new FileReader(
            @"war-and-peace.txt",
            new(){
                @"peace-terms.txt",
                @"war-terms.txt"
            }
        );
        Console.WriteLine("Hello, World!");
    }
}
