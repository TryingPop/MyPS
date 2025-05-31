using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 30
이름 : 배성훈
내용 : Automatic Editing
    문제번호 : 4668번

    구현, 문자열 문제다.
    replace가 하나만이 아닌 뒤의 모든 경우를 바꿔서 계속해서 틀렸다;
    예를들어 bananana가 있을 때 ana를 cba로 바꾼다고 하자. 그러면
    우리는 b"ana"nana가 b"cba"nana -> bcb"cba"na -> bcbcb"cba"로 바뀌길 기대한다.
    그런데 b"cba"n"cba" -> b"cba"n"cba" 로 바껴서 원하는 결과를 못얻는다.
    그래서 하나씩 바꾸는 것으로 수정하니 이상없이 통과한다.
*/

namespace BaekJoon.etc
{
    internal class etc_1659
    {

        static void Main1659(string[] args)
        {

#if !first

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n;
            string[] b = new string[10], a = new string[10];
            while ((n = int.Parse(sr.ReadLine())) != 0)
            {

                for (int i = 0; i < n; i++)
                {

                    b[i] = sr.ReadLine();
                    a[i] = sr.ReadLine();
                }

                string input = sr.ReadLine();

                for (int i = 0; i < n; i++)
                {

                    int pos;
                    while ((pos = input.IndexOf(b[i])) >= 0) 
                    { 
                        
                        input = input.Substring(0, pos) + a[i] + input.Substring(pos + b[i].Length);
                        // 이걸로 풀면 모두 바꾸므로 틀린다;
                        // input = input.Replace(b[i], a[i]);
                    }

                }

                sw.Write($"{input}\n");
            }

#elif GPT

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            List<string> lines = new();
            string line;

            while ((line = sr.ReadLine()) != null)
            {

                if (line == "0") break;
                lines.Add(line);
            }

            int index = 0;
            while (index < lines.Count)
            {

                int ruleCount = int.Parse(lines[index++]);
                var rules = new List<(string find, string replace)>();

                for (int i = 0; i < ruleCount; i++)
                {

                    string find = lines[index++];
                    string replace = lines[index++];
                    rules.Add((find, replace));
                }

                string text = lines[index++];

                foreach (var rule in rules)
                {

                    string find = rule.find;
                    string replace = rule.replace;

                    int pos;
                    while ((pos = text.IndexOf(find)) != -1)
                    {

                        text = text.Substring(0, pos) + replace + text.Substring(pos + find.Length);
                    }
                }

                sw.WriteLine(text);
            }
#endif
        }
    }
}
