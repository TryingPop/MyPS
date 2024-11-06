using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 8
이름 : 배성훈
내용 : 암호 만들기
    문제번호 : 1759번

    수학, 브루트포스, 조합론, 백트래킹 문제다
    DFS 탐색으로 백트래킹과, 조합처럼 되게 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0477
    {

        static void Main477(string[] args)
        {

            StreamReader sr = new(Console.OpenStandardInput());
            int n = ReadInt();
            int r = ReadInt();
            bool[] vo = new bool[128];

            // 모음 true
            vo['a'] = true;
            vo['e'] = true;
            vo['i'] = true;
            vo['o'] = true;
            vo['u'] = true;

            char[] alphabet = new char[r];
            for (int i = 0; i < r; i++)
            {

                alphabet[i] = (char)(ReadInt() + '0');
            }
            sr.Close();

            Array.Sort(alphabet);
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()), bufferSize: 65536 * 8);
            char[] ret = new char[n];

            DFS(0);
            sw.Close();

            void DFS(int _depth, int _s = 0)
            {

                if (_depth == n)
                {

                    int chk1 = 0;               // 모음 수
                    int chk2 = 0;               // 자음 수
                    for (int i = 0; i < n; i++)
                    {

                        // 자음 모음 체크
                        if (vo[ret[i]]) chk1++;
                        else chk2++;
                    }

                    // 조건 불만족?
                    if (chk1 < 1 || chk2 < 2) return;

                    // 조건 만족해야 출력
                    for (int i = 0; i < n; i++)
                    {

                        sw.Write(ret[i]);
                    }
                    sw.Write('\n');
                    return;
                }

                for (int i = _s; i < r; i++)
                {

                    // 알파벳 조합처럼 선택
                    ret[_depth] = alphabet[i];
                    DFS(_depth + 1, i + 1);
                }
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
public static class PS
{
    private static StreamWriter sw = new(new BufferedStream(Console.OpenStandardOutput()));

    private static int l, c;
    private static char[] chars;
    private static char[] buffer;

    static PS()
    {
        string[] lc = Console.ReadLine().Split();
        l = int.Parse(lc[0]);
        c = int.Parse(lc[1]);
        buffer = new char[l];

        chars = Array.ConvertAll(Console.ReadLine().Split(), char.Parse);
        Array.Sort(chars);
    }

    public static void Main()
    {
        DFS(0, 0, 0, 0);
        sw.Close();
    }

    private static void DFS(int depth, int start, int vCnt, int cCnt)
    {
        if (depth == l)
        {
            if (vCnt >= 1 && cCnt >= 2)
                sw.WriteLine(buffer);

            return;
        }

        for (int i = start; i < chars.Length; i++)
        {
            buffer[depth] = chars[i];

            if (buffer[depth] == 'a' || buffer[depth] == 'e' ||
                buffer[depth] == 'i' || buffer[depth] == 'o' || buffer[depth] == 'u')
                DFS(depth + 1, i + 1, vCnt + 1, cCnt);
            else
                DFS(depth + 1, i + 1, vCnt, cCnt + 1);
        }
    }
}
#endif
}
