using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 19
이름 : 배성훈
내용 : 괄호 제거
    문제번호 : 2800번

    자료 구조, 문자열, 브루트포스, 비트마스킹, 스택 문제다
    
*/

namespace BaekJoon.etc
{
    internal class etc_1043
    {

        static void Main1043(string[] args)
        {

            StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            string str = Console.ReadLine();

            StringBuilder sb = new(str.Length);

            int[] left = new int[10];
            int[] right = new int[10];
            bool[] use = new bool[10];
            string[] ret;
            int lrLen = 0;
            int retLen = 0;

            Find();

            DFS();

            Array.Sort(ret, 0, retLen);

            for (int i = 0; i < retLen; i++)
            {

                if (ret[i] == ret[i + 1]) continue;
                sw.Write(ret[i]);
            }

            sw.Close();

            void Find()
            {

                // 옳은 () 괄호 위치 찾기
                // left[i] : i번째 괄호의 왼쪽 인덱스
                // right[i] : i번째 괄호의 오른쪽 인덱스
                int[] stack = new int[10];
                int stkLen = 0;
                for (int i = 0; i < str.Length; i++)
                {

                    if (str[i] == '(')
                    {

                        stack[stkLen++] = lrLen;
                        left[lrLen++] = i;
                    }
                    else if (str[i] == ')') right[stack[--stkLen]] = i;
                }

                ret = new string[(1 << lrLen) + 1];
                use = new bool[lrLen];
            }

            void DFS(int _depth = 0, int _select = 0)
            {

                if (_depth == lrLen)
                {

                    if (_select == 0) return;
                    sb.Clear();
                    for (int i = 0; i < str.Length; i++)
                    {

                        bool flag = false;
                        for (int j = 0; j < lrLen; j++)
                        {

                            if (use[j]) continue;
                            if (i != left[j] && i != right[j]) continue;
                            flag = true;
                            break;
                        }

                        if (flag) continue;
                        sb.Append(str[i]);
                    }

                    sb.Append('\n');
                    ret[retLen++] = sb.ToString();
                    return;
                }

                DFS(_depth + 1, _select + 1);
                use[_depth] = true;
                DFS(_depth + 1, _select);
                use[_depth] = false;
            }
        }
    }

#if other
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// #nullable disable

public static class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var s = sr.ReadLine();
        var n = s.Length;

        var parens = new List<(int st, int ed)>();
        var st = new Stack<int>();
        for (var idx = 0; idx < n; idx++)
        {
            var ch = s[idx];

            if (ch == '(')
                st.Push(idx);
            else if (ch == ')')
                parens.Add((st.Pop(), idx));
        }

        var set = new HashSet<string>();
        for (var mask = 1; mask < (1 << parens.Count); mask++)
        {
            var manip = s.ToCharArray();
            for (var idx = 0; idx < parens.Count; idx++)
            {
                if ((mask & (1 << idx)) == 0)
                    continue;

                var (x, y) = parens[idx];
                manip[x] = default;
                manip[y] = default;
            }

            set.Add(new string(manip.Where(ch => ch != default).ToArray()));
        }

        foreach (var str in set.OrderBy(v => v))
            sw.WriteLine(str);
    }
}

#endif
}
