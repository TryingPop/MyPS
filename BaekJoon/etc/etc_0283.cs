using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 18
이름 : 배성훈
내용 : 카드 캡터 상근이
    문제번호 : 5587

    구현, 시뮬레이션 문제다
    게임하는 상황을 구현해 정답을 제출했다

    현재 낼 수 있는 가장 작은 카드는
    처음에 정렬하고 앞에서부터 for문으로 확인해서 했다
    세그먼트 트리를 쓴다면 logN으로 찾을 수 있을거 같다

    그러나 전체 경우가 100이므로 매번 for문 돌려 찾았다
*/

namespace BaekJoon.etc
{
    internal class etc_0283
    {

        static void Main283(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);
            int[] sk = new int[n];
            int[] ks = new int[n];

            int ret1 = n;
            int ret2 = n;

            {

                bool[] isMine = new bool[2 * n + 1];
                for (int i = 0; i < n; i++)
                {

                    int get = ReadInt(sr);
                    sk[i] = get;
                    isMine[get] = true;
                }

                int ksIdx = 0;
                for (int i = 1; i < isMine.Length; i++)
                {

                    if (isMine[i]) continue;
                    ks[ksIdx++] = i;
                }
            }

            sr.Close();

            Array.Sort(sk);

            bool skTurn = true;
            int curMin = 0;
            while(ret2 != 0 && ret1 != 0)
            {

                bool notFind = true;
                int[] chk = skTurn ? sk : ks;
                
                for (int i = 0; i < n; i++)
                {

                    if (curMin >= chk[i]) continue;

                    notFind = false;
                    curMin = chk[i];
                    chk[i] = 0;

                    if (skTurn) ret2--;
                    else ret1--;

                    break;
                }

                skTurn = !skTurn;

                if (notFind) curMin = 0;
            }

            Console.WriteLine(ret1);
            Console.WriteLine(ret2);
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;
            while ((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
