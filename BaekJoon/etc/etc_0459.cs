using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 5
이름 : 배성훈
내용 : 개미의 이동
    문제번호 : 3221번

    정렬, 에드혹 문제다
    k의 범위가 20만이고, 개미 수가 7만이라 k로 카운트해서 정렬했다

    아이디어는 다음과 같다
    세 번째 예제를 직접 돌려본 결과
    개미들 이동은 개미 번호만 무시하면 서로 뚫고 지나가는 경우와 놓인 위치가 일치한다

    그래서 서로 투명개미로 취급하고 t초 후 상황을 봤다
    여기서 t는 2k마다 원위치와 원방향으로 돌아오므로 나머지만 계산하고
    해당 위치에 개미를 놓았다

    그리고 좌표 순서대로 개미를 확인했다
    이렇게 제출하니 정렬과정이 생략되어 88ms에 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0459
    {

        static void Main459(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            int k = ReadInt();
            int t = ReadInt();
            int n = ReadInt();

            // 2 * k 번돌면 원위치다
            t %= 2 * k;

            int[] cnt = new int[k + 1];

            for (int i = 0; i < n; i++)
            {

                // 개미 이동
                int cur = ReadInt();
                bool isLeft = ReadDir();

                cur += t * (isLeft ? -1 : 1);
                if (cur < 0) cur = -cur;
                cur %= 2 * k;
                if (cur > k) cur = 2 * k - cur;

                cnt[cur]++;
            }
            sr.Close();

            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
            // 정렬된 순서로 좌표 꺼내기
            for (int i = 0; i < cnt.Length; i++)
            {

                while (cnt[i] > 0)
                {

                    sw.Write($"{i} ");
                    cnt[i]--;
                }
            }
            sw.Close();

            bool ReadDir()
            {

                int dir = sr.Read();
                int c;
                while((c = sr.Read()) != ' ' && c != '\n') { }
                return dir == 'L';
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader(Console.OpenStandardInput());
            StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());
            sw.AutoFlush = true;

            int iii = 8 % 10;

            int[] LT = Array.ConvertAll(sr.ReadLine().Split(), f => Convert.ToInt32(f));
            int L = LT[0];
            int T = LT[1];

            int N = Convert.ToInt32(sr.ReadLine());

            int[] pos = new int[N];
            for (int i = 0; i < N; i++)
            {
                string[] ant = sr.ReadLine().Split();

                int antPos = Convert.ToInt32(ant[0]);

                if (ant[1] == "D")
                {
                    int move = (T + antPos) / L;
                    pos[i] = move % 2 == 0 ? (T + antPos) % L : L - ((T + antPos) % L);
                }
                else
                {
                    // 개미위치 0으로 보정 (오른쪽으로만 진행하는거 계산)
                    antPos = L - antPos;

                    int move = (T + antPos) / L;
                    pos[i] = move % 2 == 1 ? (T + antPos) % L : L - ((T + antPos) % L);
                }
            }

            Array.Sort(pos);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < pos.Length; i++)
            {
                sb.Append(string.Format("{0} ", pos[i]));
            }
            sw.Write(sb.ToString());
        }
    }
}

#endif
}
