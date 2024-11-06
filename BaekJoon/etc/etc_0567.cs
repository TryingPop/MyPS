using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 18
이름 : 배성훈
내용 : 학생 인기도 측정
    문제번호 : 25325번

    문자열, 해시, 정렬 문제다
    Linq의 문법을 이용해 풀었다

    만약 딕셔너리를 정렬하는게 아닌 다른 방법으로 한다면
    딕셔너리로 인덱스를 저장하고, 따로 튜플 배열을 만들어
    값과 인덱스를 저장한 뒤 정렬해서 풀었을 것이다

    다른 사람 풀이 보니 같은 방법으로 먼저 푼 사람이 있었다
*/

namespace BaekJoon.etc
{
    internal class etc_0567
    {

        static void Main567(string[] args)
        {

            StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 1024 * 16);
            StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 1024 * 8);

            int n;
            Dictionary<string, int> dic;

            Solve();

            sr.Close();
            sw.Close();

            void Solve()
            {

                Input();

                // 값 내림차순을 최우선으로 이후에 이름순을 차선으로
                foreach(var item in dic.OrderByDescending(val => val.Value).ThenBy(val => val.Key))
                {

                    sw.Write($"{item.Key} {item.Value}\n");
                }
            }
            
            void Input()
            {

                n = int.Parse(sr.ReadLine());
                dic = new(n);

                // 이름 입력
                string[] temp = sr.ReadLine().Split();
                for (int i = 0; i < n; i++)
                {

                    dic[temp[i]] = 0;
                }

                // 선호도 입력
                for (int i = 0; i < n; i++)
                {

                    temp = sr.ReadLine().Split();
                    for (int j = 0; j < temp.Length; j++)
                    {

                        dic[temp[j]]++;
                    }
                }
            }
        }
    }

#if other
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace test
{
    internal class Program
    {
        static void Main()
        {
            StringBuilder 정답 = new StringBuilder();
            int t = int.Parse(Console.ReadLine());
            string[] 학생 = Console.ReadLine().Split(' ');
            Dictionary<string, int> 인기도 = new Dictionary<string, int>();
            
            foreach (var Key in 학생) { 인기도.Add(Key, 0); }

            for (int i = 0; i < t; i++)
            {
                string[] 정보 = Console.ReadLine().Split(' ');
                foreach (var Key in 정보) { 인기도[Key]++; }
            }

            foreach (var item in 인기도.OrderByDescending(x => x.Value).ThenBy(x => x.Key))
            {
                정답.AppendLine($"{item.Key} {item.Value}");
            }
            Console.Write(정답);
        }
    }
}
#elif other2
//var sb = new StringBuilder();
//using var rd = new StreamReader(Console.OpenStandardInput());
//using var wr = new StreamWriter(Console.OpenStandardOutput());
//wr.AutoFlush = false;
//wr.Write(sb);
//wr.Flush();
//for (int repeat = IP(); repeat-- > 0;)

internal sealed class Program
{
    static void Main(string[] args)
    {
        var n = IP();
        var s = SCR();
        var student = new Dictionary<string, int>();
        for (int j = 0; j < n; ++j)
        {
            student[s[j]] = 0;
        }
        for (int i = 0; i < n; ++i)
        {
            var names = SCR();
            for (int j = 0; j < names.Length; ++j)
            {
                student[names[j]]++;
            }
        }
        var sortedStudent = student.OrderByDescending(x => x.Value).ThenBy(x => x.Key);
        foreach(var e in sortedStudent)
        {
            Console.WriteLine($"{e.Key} {e.Value}");
        }
    }

    private static T[] STP<T>() where T : struct => Array.ConvertAll(Console.ReadLine()!.Split(), value => (T)Convert.ChangeType(value, typeof(T)));
    private static T[] STP<T>(char c) where T : struct => Array.ConvertAll(Console.ReadLine()!.Split(c), value => (T)Convert.ChangeType(value, typeof(T)));
    private static int[] SIP() => Array.ConvertAll(Console.ReadLine()!.Split(), int.Parse);
    private static int IP() => int.Parse(Console.ReadLine()!);
    private static T TP<T>() where T : struct => (T)Convert.ChangeType(Console.ReadLine()!, typeof(T));
    private static string CR() => Console.ReadLine()!;
    private static string[] SCR() => Console.ReadLine()!.Split();
}
#endif
}
