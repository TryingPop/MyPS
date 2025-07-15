using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 15
이름 : 배성훈
내용 : A+B - 10 (제2편)
    문제번호 : 30924번

    무작위화
*/

namespace BaekJoon.etc
{
    internal class etc_1769
    {

        static void Main1769(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            Random rand = new(Seed: 7777);
            // StringBuilder sb = new(200);

            int a = GetVal('A'), b = GetVal('B');

            sw.Write($"! {a + b}");

            int GetVal(char _str)
            {

                int pop = rand.Next(1, 9_999);
                int ret = 10_000;

                for (int i = 1; i < 10_000; i++)
                {

                    if (i == pop) continue;
                    // sb.Append($"? {_str} {i}\n");
                    // sw.Write(sb);
                    sw.Write($"? {_str} {i}\n");

                    sw.Flush();
                    // sb.Clear();

                    int ans = ReadInt();
                    if (ans == 0) continue;
                    ret = i;
                    break;
                }

                return ret;

                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) ;
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == ' ' || c == '\n') return true;
                        ret = c - '0';

                        while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        return false;
                    }
                }
            }
        }
    }

#if other
public static class Program
{
    public static void Main()
    {
        var reader = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
        var writer = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

        int A = 0;
        int B = 0;

        List<int> values = new();
        for (int i = 1; i <= 10000; i++)
        {
            values.Add(i);
        }
        
        for(int i = 1; i <= 10000; i++)
        {
            var random = new Random();
            int idx = random.Next(0, values.Count);
            
            writer.WriteLine($"? A {values[idx]}");
            writer.Flush();
            
            string result = reader.ReadLine()!;
            if (result == "1")
            {
                A = values[idx];
                break;
            }
            
            values.RemoveAt(idx);
        }

        values.Clear();
        for (int i = 1; i <= 10000; i++)
        {
            values.Add(i);
        }
        
        for(int i = 1; i <= 10000; i++)
        {
            var random = new Random();
            int idx = random.Next(0, values.Count);
            
            writer.WriteLine($"? B {values[idx]}");
            writer.Flush();
            
            string result = reader.ReadLine()!;
            if (result == "1")
            {
                B = values[idx];
                break;
            }
            
            values.RemoveAt(idx);
        }
        
        writer.WriteLine($"! {A + B}");
        writer.Flush();

        reader.Close();
        writer.Close();
    }
}
#endif
}
