using System;
using System.Collections.Generic;
using System.IO;

namespace Ekz_ver2
{
    public class Data
    {
        public static string[] MassiveLinkes = Directory.GetFiles("files");

        public static List<string> FirstFile;

        public static List<string> SecondFile;

        public static List<string> Result = new List<string>();
    }
    class Program
    {
        static void Main(string[] args)
        {
            
            Data.FirstFile = Search.Files(0);
            Data.SecondFile = Search.Files(1);

            CompareFiles.Compare();

            OutputResult.Output();
        }
    }
    public class Search
    {
        public static List<string> Files(int i)
        {
            List<string> FileInfo = new List<string>(File.ReadAllText(Data.MassiveLinkes[i]).Split(new char []{ '\n' }));

            return FileInfo;
        }
    }
    public class CompareFiles
    {
        public static void Compare()
        {
            for(int i = 0; i < Data.FirstFile.Count; i++)
            {
                
                    if(Data.FirstFile[i] != Data.SecondFile[i])
                    {
                        CompareFiles.Transfer(i, CompareFiles.Change(i));
                    }
               
            }
        }
        private static int Change(int i)
        {
            int NumberOfCorrect = 0;

            for(int k = 0; k < Data.FirstFile[i].Length; k++)
            {
                if (Data.FirstFile[i][k] == Data.SecondFile[i][k])
                    NumberOfCorrect++;
                else
                    break;
            }

            return NumberOfCorrect;
        }
        private static void Transfer(int i, int NumberOfCorrect)
        {
            if (NumberOfCorrect > 0)
                CompareFiles.StringIsChanged(i, NumberOfCorrect);
            if (NumberOfCorrect == 0)
                CompareFiles.AdditionOrDeletion(i);
        }
        private static void StringIsChanged(int i, int NumberOfCorrect)
        {
            Data.Result.Add($"{i + 1}: {Data.FirstFile[i]}");
            Data.Result.Add($"{i + 1}: ~{Data.SecondFile[i]}");
            Data.Result.Add($"{new string (' ', NumberOfCorrect + 4)}^");
        }
        private static void AdditionOrDeletion(int i)
        {
            Data.Result.Add($"{i + 1}: -{Data.FirstFile[i]}");
            Data.Result.Add($"{i + 1}: +{Data.SecondFile[i]}");
        }

    }
    public class OutputResult
    {
        public static void Output()
        {
            foreach (var i in Data.Result)
            {
                OutputResult.Painting(i);

                Console.WriteLine(i);

                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        private static void Painting(string i)
        {
            if(i.Contains('~'))
                Console.ForegroundColor = ConsoleColor.Blue;
            else if (i.Contains('-'))
                Console.ForegroundColor = ConsoleColor.Red;
            else if (i.Contains('+'))
                Console.ForegroundColor = ConsoleColor.Green;

        }
    }
}
