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

            string valid_tokens =
                @"{|}|\(|\)|;|int|return|[a-zA-Z]\w*|[0-9]+"; // index of all valid
                                                              // tokens

            string check_id = @"[a-zA-Z]\w*";
            string check_int = @"[0-9]+";

            Regex pattern = new Regex(
                valid_tokens); // regular expression class for easy pattern matching

            var results = pattern.Matches(contents); // stores all tokens found

            int counter = 0;

            Parsing parse = new Parsing();

            Component parse_check = parse.ParseProgram(counter)
        }
    }

    class Component
    {
        public int counter;
        public bool status;
        public string info;

        public Component(int counter_value, bool status_code, string info_message)
        {
            counter = counter_value;
            status = status_code;
            info = info_message;
        }
    }

    class Parsing
    {
        public Component ParseProgram(List<Match> tokens, int counter,
                                      string check_int, string check_id)
        {
            Component function = ParseFunc(tokens, counter, check_int, check_id);

            return function;
        }

        public Component ParseFunc(List<Match> tokens, int counter, string check_int,
                                   string check_id)
        {
            Component func = new Component(counter, true, "All good!");

            Regex integer = new Regex(check_int);

            if (!integer.IsMatch(tokens[counter].Value))
            {
                func.status = false;
                func.info = "Function type is invalid!";
                return func;
            }

            counter++;

            Regex id = new Regex(check_id);

            if (!id.IsMatch(tokens[counter].Value))
            {
                func.status = false;
                func.info = "Invalid ID!";
                return func;
            }

            counter++;

            if (tokens[counter].Value != "(")
            {
                func.status = false;
                func.info = "Invalid syntax!";
                return func;
            }

            counter++;

            if (tokens[counter].Value != ")")
            {
                func.status = false;
                func.info = "Invalid syntax!";
                return func;
            }

            counter++;

            if (tokens[counter].Value != "{")
            {
                func.status = false;
                func.info = "Invalid syntax!";
                return func;
            }

            counter++;

            Component statement = ParseStatement(tokens, counter, check_int);

            if (!statement.status)
            {
                func.status = false;
                func.info = "Invalid statement, " + statement.info;
                return func;
            }

            counter = statement.counter;

            if (tokens[counter].Value != "}")
            {
                func.status = false;
                func.info = "Invalid syntax!";
                return func;
            }

            counter++;

            func.counter = counter;

            return func;
        }

        public Component ParseStatement(List<Match> tokens, int counter,
                                        string check_int)
        {
            Component statement = new Component(counter, true, "All good!");

            if (tokens[counter].Value != "return")
            {
                statement.status = false;
                statement.info = "Invalid command!";
            }

            counter++;

            Component expression = ParseExpression(tokens, counter, check_int);

            if (!expression.status)
            {
                statement.status = false;
                statement.info = "Invalid expression, " + expression.info;
            }

            counter = expression.counter;

            if (tokens[counter].Value != ";")
            {
                statement.status = false;
                statement.info = "Invalid syntax!";
            }

            counter++;

            statement.counter = counter;

            return statement;
        }

        public Component ParseExpression(List<Match> tokens, int counter,
                                         string check_int)
        {

            Component expression = new Component(counter, true, "All good!");

            Regex integer = new Regex(check_int);

            if (!integer.IsMatch(tokens[counter].Value))
            {
                expression.status = false;
                expression.info = "Invalid type!";
            }

            counter++;

            expression.counter = counter;

            return expression;
        }
    }
}
