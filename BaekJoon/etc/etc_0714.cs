using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 21
이름 : 배성훈
내용 : N과 M (10)
    문제번호 : 15664번

    백트래킹 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_0714
    {

        static void Main714(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int n, m;

            int[] arr;
            int[] calc;
            int len;
            Dictionary<int, int> dic;

            StringBuilder sb;

            Solve();

            void Solve()
            {

                Input();
                sb = new(m * 10);
                calc = new int[m];

                DFS(0, 0);
                sw.Close();
            }

            void DFS(int _depth, int _s)
            {

                if (_depth == m)
                {

                    for (int i = 0; i < m; i++)
                    {

                        sb.Append(calc[i]);
                        sb.Append(' ');
                    }

                    sw.Write($"{sb}\n");
                    sb.Clear();
                    return;
                }

                for (int i = _s; i < len; i++)
                {

                    if (dic[arr[i]] == 0) continue;
                    dic[arr[i]]--;
                    calc[_depth] = arr[i];
                    DFS(_depth + 1, i);
                    dic[arr[i]]++;
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput());
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                n = ReadInt();
                m = ReadInt();

                dic = new(n);
                arr = new int[n];
                len = 0;
                for (int i = 0; i < n; i++)
                {

                    int k = ReadInt();
                    if (dic.ContainsKey(k)) dic[k]++;
                    else 
                    { 
                        
                        dic[k] = 1; 
                        arr[len++] = k;
                    }
                }

                Array.Sort(arr, 0, len);
                sr.Close();
            }

            int ReadInt()
            {

                int c, ret = 0;

                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
#if other

StreamReader reader = new(Console.OpenStandardInput());
StreamWriter writer = new(Console.OpenStandardOutput());

var input  = Array.ConvertAll(reader.ReadLine().Split(), int.Parse);
var list = Array.ConvertAll(reader.ReadLine().Split(), int.Parse);
Array.Sort(list);

int[] output = new int[input[1]];

void loop(int index=0,int start = 0) {
    if (index >= input[1])
    {
        writer.WriteLine(string.Join(' ',output));
        return;
    }

    int last = 0;
    for (int i = start; i < list.Length; i++)
    {
        output[index] = list[i];
        if (last == list[i]) continue;
        last = list[i];
        loop(index+1,i+1);
    }
}

loop();
writer.Flush();
#endif
}
