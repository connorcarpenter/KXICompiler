﻿using System;
using System.Collections.Generic;

namespace KXIParse
{
    class Program
    {
        static void Main()
        {
            try
            {
                var lexer = new Lexer("../../program.kxi");
                var tokens = lexer.GenerateTokens();
                //PrintTokenList(tokens);
                var syntaxer = new Syntaxer(tokens);
                var symbolTable = syntaxer.ParseTokens();
                PrintSymbolTable(symbolTable);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }

        static void PrintTokenList(IEnumerable<Token> tokenList)
        {
            foreach (var t in tokenList)
                Console.WriteLine("" + t.LineNumber + ": " + t.Type + ": " + t.Value);
        }


        static void PrintSymbolTable(Dictionary<string,Symbol> symbolTable)
        {
            Console.WriteLine("\nSymbol Table\n---\n");

            foreach (var s in symbolTable)
            {
                Console.WriteLine("" + s.Key + "\n" + "---");
                Console.WriteLine("Scope:   "+s.Value.Scope);
                Console.WriteLine("Symid:   " + s.Value.SymId);
                Console.WriteLine("Value:   " + s.Value.Value);
                Console.WriteLine("Kind:   " + s.Value.Kind);
                if (s.Value.Data != null)
                {
                    if (!string.IsNullOrEmpty(s.Value.Data.Type))
                        Console.WriteLine("Type:   " + s.Value.Data.Type + (s.Value.Data.IsArray ? "[]" : ""));
                    if (s.Value.Data.Params != null)
                        foreach (var p in s.Value.Data.Params)
                            Console.Write("Param:   " + p);
                    if (!string.IsNullOrEmpty(s.Value.Data.AccessMod))
                        Console.WriteLine("AccessMod:   " + s.Value.Data.AccessMod);
                }
                Console.WriteLine();
            }
        }
    }
}