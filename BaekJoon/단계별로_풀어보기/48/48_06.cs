using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. -
이름 : 배성훈
내용 : 격자판 채우기
    문제번호 : 1648번

    dp, 비트마스킹, 비트필드를 이용한 dp 문제다
    해당 사이트 보고 참조해서 풀었다
    https://glanceyes.com/entry/BOJ-%EB%B0%B1%EC%A4%80-1648%EB%B2%88-%EA%B2%A9%EC%9E%90%ED%8C%90-%EC%B1%84%EC%9A%B0%EA%B8%B0

    아이디어는 다음과 같다
    줄 단위에 격자를 채워놓은 상태를 인덱스로 해서 dp를 만들면 된다
    그리고 값으로는 해당 경우의 인덱스이다

    격자가 채워져 있으면 벽돌을 넣지 않고 다음 타일로 간다
    격자가 채워지지 않았으면 오른쪽에 벽돌을 놓을 수 있는지 혹은
    아래에 벽돌을 놓을 수 있는지 점검한다
*/

namespace BaekJoon._48
{
    internal class _48_06
    {

        static void Main6(string[] args)
        {

            int[] input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            int len1 = input[0] * input[1];
            int len2 = 1 << input[1];
            int[][] dp = new int[len1][];
            

            for (int r = 0; r < len1; r++)
            {

                dp[r] = new int[len2];
                for (int c = 0; c < len2; c++)
                {

                    dp[r][c] = -1;
                }
            }

            int ret = DFS(0, 0);
            Console.WriteLine(ret);

            int DFS(int _idx, int _state)
            {

                if (_idx >= len1)
                {

                    if (_idx == len1 && _state == 0) return 1;
                    else return 0;
                }

                int ret = dp[_idx][_state];
                if (ret != -1) return ret;
                ret = 0;

                if ((_state & 1) != 0)
                {

                    // 현재 벽돌이 있다면
                    ret += DFS(_idx + 1, _state >> 1);
                }
                else
                {

                    // 먼저 오른쪽에 돌 심기
                    if (((_idx % input[1]) != input[1] - 1) && (_state & 2) == 0) ret += DFS(_idx + 2, _state >> 2);
                    ret += DFS(_idx + 1, (_state >> 1) | (1 << (input[1] - 1)));
                }

                ret %= 9_901;
                dp[_idx][_state] = ret;

                return ret;
            }
        }
    }

#if other
using System.IO;
using System.Text;
using System;

class Programs
{
    static StreamReader sr = new StreamReader(Console.OpenStandardInput(), Encoding.Default);
    static StreamWriter sw = new StreamWriter(Console.OpenStandardOutput(), Encoding.Default);
    static int n, m, mod = 9901;
    static long[,] dp = new long[14 * 14, 1 << 14];//핵심은 메모이제이션 1차배열
    static long DFS(int num, int visit)
    {
        if (n * m-1 == num)
        {
            if(visit==1)
            {
                return 1;
            }
           return 0;
        }
        if (dp[num, visit] != -1)
        {
            return dp[num, visit];
        }
        dp[num, visit] = 0;
        //가로2*1과 세로 1*2를 만들어서 비교해야됨
        //가로
        int next = 0;
        if((1&visit)==1)//현재가 채워져서 블록을 채울 수 없음.
        {
          dp[num,visit]+=  DFS(num + 1, visit >> 1);
        }
        else
        {
            if ((num % m) + 1 < m&&(visit&2)==0)
            {
                //가로로 갈 수 있는 경우 
               
                next = (visit | 2);//0과1
                dp[num, visit] += DFS(num + 1, next >> 1);//0의 자리 없앰 1이 0의자리가됨
            }
            if ((num / m) + 1 < n && (visit & ((1 << m) + 1)) == 0)
            {
                //세로 가능// 블록이 없는 경우
                next = (visit | ((1 << m) + 1));
                dp[num, visit] += DFS(num + 1, next >> 1);
            }
        }
        return dp[num, visit]%mod;
    }

    static void Main(String[] args)
    {
        //비트마스크로 경우의 수를 다 찾는 것이 아닐까?
        string[] str = sr.ReadLine().Split();
        n = int.Parse(str[0]);
        m = int.Parse(str[1]);
        for (int i = 0; i < n * m; i++)
        {
            for (int j = 0; j < 1<<m; j++)
            {
                dp[i,j] = -1;
            }
        }
      sw.Write(DFS(0,0));
        //풀이를 참조함
        //순서대로 0~n*m까지 번호로 진행하면서
        //해당 번호에서 +m번까지의 칸이 채워진 상태를 2진수로 나타내고
        //다음 번호로 진행될때 >>쉬프트 연산으로 1의 자리를 삭제한뒤 다시 <<1

        sw.Dispose();
    }
}

#endif
}
