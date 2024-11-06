using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 18
이름 : 배성훈
내용 : 애너그램
    문제번호 : 6443번

    문자열, 백트래킹 문제다
    조건대로 구현했다

    많은 서로다른 문자열을 엄청나게 출력하는 것이기에 그냥 string이 아닌 stringbuilder를 이용하고
    그리고 이전에 간간히 buffer를 비워주는게 빨라서 거의 !에 근접한 경우의 수를 출력하기에 매번 비워줬다
    그리고 사전식 순서는 따로 정렬하는게 아닌 개수를 세고 조합식으로 출력해서 해결했다

    이렇게 제출하니 104ms에 무리없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0569
    {

        static void Main569(string[] args)
        {

            StreamReader sr = new(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new(new BufferedStream(Console.OpenStandardOutput()), bufferSize: 65536 * 8);

            StringBuilder sb = new(200_000);

            int len = 0;
            int[] cnt = new int[26];
            int[] calc = new int[20];

            Solve();
            sr.Close();
            sw.Close();

            void Solve()
            {

                int n = int.Parse(sr.ReadLine());

                for (int i = 0; i < n; i++)
                {

                    string str = sr.ReadLine();
                    len = str.Length;

                    for (int j = 0; j < len; j++)
                    {

                        int idx = str[j] - 'a';
                        cnt[idx]++;
                    }

                    DFS();

                    sw.Write(sb);
                    sb.Clear();
                    sw.Flush();

                    for (int j = 0; j < 26; j++)
                    {

                        cnt[j] = 0;
                    }
                }
            }

            void DFS(int _depth = 0)
            {

                if (_depth == len)
                {

                    for (int i = 0; i < len; i++)
                    {

                        char s = (char)(calc[i] + 'a');
                        sb.Append(s);
                    }

                    sb.Append('\n');
                    return;
                }

                for (int i = 0; i < 26; i++)
                {

                    if (cnt[i] == 0) continue;
                    cnt[i]--;
                    calc[_depth] = i;
                    DFS(_depth + 1);
                    cnt[i]++;
                }
            }
        }
    }

#if other
using System.Text;

namespace boj_6443
{
    internal class Program
    {
        static int[] alpha = new int[26];
        static HashSet<string> anaset = new HashSet<string>();
        static string _s;
        
        static void Main(string[] args)
        {
            int N = int.Parse(Console.ReadLine());

            for (int i = 0; i < N; i++)
            {
                _s = Console.ReadLine();

                Array.Fill(alpha, 0);
                anaset.Clear();


                foreach (char c in _s)
                    alpha[c - 'a']++;

                bt(0, "");

                List<string> analist = anaset.ToList();
                analist.Sort();

                StringBuilder sb = new StringBuilder();
                foreach (string ana in analist)
                {
                    sb.Append(ana + "\n");
                }

                sb.Remove(sb.Length - 1, 1);

                Console.WriteLine(sb);
            }
        }

        static void bt(int idx, string s)
        {
            if (idx == _s.Length)
            {
                anaset.Add(s);
                return;
            }

            for (int i = 0; i < 26; i++)
            {
                if (alpha[i] > 0)
                {
                    alpha[i]--;
                    bt(idx + 1, s + (char)(i + 'a'));
                    alpha[i]++;
                }
            }
        }
    }
}
#endif
}
