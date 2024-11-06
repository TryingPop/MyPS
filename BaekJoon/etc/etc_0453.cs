using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 5
이름 : 배성훈
내용 : DNA
    문제번호 : 1969번

    구현, 그리디, 브루트포스 문제다
    그리디하게 접근해서 각 자리수에서 같은것의 개수가 많은 걸로 해당 자리의 문자를 설정했다
    그리고 출력조건에 맞춰 같은게 있다면 사전으로 앞서는걸 앞에오게했다
*/

namespace BaekJoon.etc
{
    internal class etc_0453
    {

        static void Main453(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            int n = ReadInt();
            int len = ReadInt();

            string[] dna = new string[n];
            for (int i = 0; i < n; i++)
            {

                dna[i] = sr.ReadLine();
            }

            sr.Close();

            char[] s = new char[4];
            s[0] = 'A';
            s[1] = 'C';
            s[2] = 'G';
            s[3] = 'T';

            int[] cnt = new int[4];
            char[] ret1 = new char[len];
            int ret2 = 0;
            for (int i = 0; i < len; i++)
            {

                for (int j = 0; j < n; j++)
                {

                    switch (dna[j][i])
                    {

                        case 'A':
                            cnt[0]++;
                            break;

                        case 'C':
                            cnt[1]++;
                            break;

                        case 'G':
                            cnt[2]++;
                            break;

                        case 'T':
                            cnt[3]++;
                            break;

                        default:
                            break;
                    }
                }

                int max = 0;
                for (int j = 0; j < 4; j++)
                {

                    if (max < cnt[j]) 
                    { 
                        
                        max = cnt[j];
                        ret1[i] = s[j];

                    }

                    cnt[j] = 0;
                }

                ret2 += n - max;
            }

            using (StreamWriter sw = new(Console.OpenStandardOutput()))
            {

                for (int i = 0; i < len; i++)
                {

                    sw.Write(ret1[i]);
                }
                sw.Write($"\n{ret2}");
            }

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
    private static int n, m;
    private static string[] dna;

    static PS()
    {
        string[] nm = Console.ReadLine().Split();
        n = int.Parse(nm[0]);
        m = int.Parse(nm[1]);
        dna = new string[n];

        for (int i = 0; i < n; i++)
        {
            dna[i] = Console.ReadLine();
        }

        Array.Sort(dna);
    }

    public static void Main()
    {
        StreamWriter sw = new(new BufferedStream(Console.OpenStandardOutput()));
        int[] cnt;
        int sum = 0;
        (int i, int cnt) max;

        for (int col = 0; col < m; col++)
        {
            cnt = new int[26];
            max = (' ', 0);

            for (int row = 0; row < n; row++)
            {
                cnt[dna[row][col] - 'A']++;
            }

            for (int i = 0; i < 26; i++)
            {
                sum += cnt[i];

                if (cnt[i] > max.cnt)
                    max = (i, cnt[i]);
            }

            sum -= max.cnt;
            sw.Write((char)(max.i + 'A'));
        }

        sw.Write('\n');
        sw.Write(sum);
        sw.Close();
    }
}
#endif
}
