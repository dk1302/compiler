// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;

namespace compiler
{
    class Program
    {
        static void Main(string[] args)
        {
            string file = "multi_digit.c";

            string contents = "";

            if (File.Exists(file))
            {
                // Read all the content in one string
                // and display the string
                contents = File.ReadAllText(file);
            }

            string valid_tokens = @"{|}|\(|\)|;|int|return|[a-zA-Z]\w*|[0-9]+";
            Regex pattern = new Regex(valid_tokens);

            var results = pattern.Matches(contents);

            Console.WriteLine(results);
            // foreach (Match match in results)
            // {
            //     no_of_tokens++;
            // }
            //
            // string[] token_list = new string[no_of_tokens];
            //
            //
            // foreach (Match match in results)
            // {
            //
            // }
        }
    }

    class Token
    {
        public string id = @"[a-zA-Z]\w*";
        public string integer = @"[0-9]+";
    }

    class func_decl
    {
        public void Func(string name, statement stat) { }
    }

    class prog
    {
        public void Prog(func_decl func) { }
    }

    class statement
    {
        public void Return(expression exp) { }
    }

    class expression
    {
        public void Const(int integer) { }
    }
}
