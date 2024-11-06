using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 27
이름 : 배성훈
내용 : 최종 순위
    문제번호 : 3665번

    ... 위상 정렬이 아닌 다른 방법으로 먼저 풀었다;
    (가장 빠르게 푼 사람(other1 부분)이랑 아이디어가 같다)

    아이디어는 간단하다
    순위가 변경된 모든 경우를 다 줬다

    모두 다 주었으므로 이동 정도를 확인했다
    예를들어 
        작년 순위 : 1, 2, 3, 4
        올해 순위 : 2, 3, 4, 1

    라면, 순위가 변동된건
        1, 2
        1, 3
        1, 4
    모든 경우를 준다
    만약 여기서 순위가 변동된게 하나라도 빠졌다면 잘못된 정보를 줬으므로 IMPOSSIBLE로 해석했다
    (만약 최소한도의 변동된 순위를 주었다면, 한번 더 틀리고 다른 방법을 알아봤을꺼 같다)

    그래서 모든 경우를 주기에, 랭킹 이동 정도를 dp로 기록했다
    1, 2번이 바꼈으니, 작년 순위를 확인하고 
    작년 1번의 순위가 높았으므로 순위가 낮아져야하므로 + 1
    2번의 순위가 낮았으므로 순위가 높아져야 하므로 -1

    그리고 1, 3번이 바꼈으니,
    작년 순위를 확인해서 똑같이 한다
    1번 + 1, 3번 -1
    1, 4 번이 바뀐 것에서
    1번 + 1, 4번 -1

    그러면 1번은 + 3, 2번은 -1, 3번도 -1, 4번도 -1
    이제 변동된 순위를 작성한다
    1번은 1위에서 +3을해서 4위로,
    2번은 2위에서 -1을 해서 1위로,
    3번은 3위에서 +1을 해서 2위로,
    4번은 4위에서 -1을 해서 3위로,

    그러면 2, 3, 4, 1위가 된다

    그럼 모든 경우를 안주고 최소한도로 주면
        1, 4
    만 줫을 경우
    1번은 + 1, 4번은 -1
    그래서 2, 3위로 간다
    그런데, 기존에 2, 3이 해당 위치에 존재하기에
    잘못된 정보 IMPOSSIBLE이라 내렸다

    그리고, 올해 팀이 작년에 똑같이 다 참여 했는데, 
    순위표를 int[]로 줬기에 모든 팀간에 서열이 같은 등수는 없다는 말로 봤다(만약 int[][]라면, "?"가능성을 고려했을 것이다!)
    그래서 작년 순위표 크기가 올해 팀보다 적은 경우 역시 잘못된 정보를 줬으므로 IMPOSSIBLE 판정을 했다
    만약 같은 등수가 있다면 제출 할 때 우리는 int[]로 표현하기에 여러 가지로 표현할 수 있게 된다

    아래는 해당 논리를 코드로 작성한 것이다
*/

namespace BaekJoon._43
{
    internal class _43_02
    {

        static void Main2(string[] args)
        {

            int MAX_TEAM = 500;
            int[] result = new int[MAX_TEAM + 1];
            int[] move = new int[MAX_TEAM + 1];
            int[] rankIdx = new int[MAX_TEAM + 1];
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int test = int.Parse(sr.ReadLine());
            for (int t = 0; t < test; t++)
            {

                int teams = int.Parse(sr.ReadLine());

                for (int i = 1; i <= teams; i++)
                {

                    result[i] = 0;
                    move[i] = 0;
                }

                int[] bRank = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

                int len = int.Parse(sr.ReadLine());
                bool impossible = false;

                if (bRank.Length < teams) impossible = true;
                else
                {

                    for (int i = 0; i < bRank.Length; i++)
                    {

                        rankIdx[bRank[i]] = i + 1;
                    }

                    for (int i = 0; i < len; i++)
                    {

                        string[] temp = sr.ReadLine().Split(' ');
                        if (impossible) continue;

                        int f = int.Parse(temp[0]);
                        int b = int.Parse(temp[1]);

                        int rankF = rankIdx[f];
                        int rankB = rankIdx[b];

                        bool minF = rankIdx[f] < rankIdx[b];
                        
                        if(minF)
                        {

                            move[rankF] -= 1;
                            move[rankB] += 1;
                            
                        }
                        else
                        {

                            move[rankF] += 1;
                            move[rankB] -= 1;
                        }
                    }
                }

                if (!impossible) 
                {
                    
                    // 이동 및 적합성 검사
                    for (int i = 1; i <= teams; i++)
                    {

                        int idx = i - move[i];
                        if (result[idx] != 0) 
                        { 

                            impossible = true;
                            break;
                        }

                        result[idx] = bRank[i - 1];
                    }
                }

                if (impossible)
                {

                    sw.Write("IMPOSSIBLE\n");
                    sw.Flush();
                    continue;
                }
                

                for (int i = 1; i <= teams; i++)
                {

                    sw.Write(result[i]);
                    sw.Write(' ');
                }

                sw.Write('\n');
                sw.Flush();
            }

            sr.Close();
            sw.Close();
        }
    }

#if other

    using var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
    using var sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
    var t = ScanInt();
    var rankOwner = new int[500];
    var rankOf = new int[501];

    var degreeTo = new int[501];
    var degreeOwner = new int[500];
    var has = new bool[500];
    // 케이스 연산
    while (t-- > 0)
    {

        var n = ScanInt();
        for (int i = 0; i < n; i++)
        {
            var owner = ScanInt();
            rankOwner[i] = owner;
            rankOf[owner] = degreeTo[owner] = i;
        }

        var m = ScanInt();
        for (int i = 0; i < m; i++)
        {
            int a = ScanInt(), b = ScanInt();
            if (rankOf[a] > rankOf[b])
                (a, b) = (b, a);
            degreeTo[a]++;
            degreeTo[b]--;
        }

        var impossible = false;
        for (int i = 1; i <= n; i++)
        {
            ref var hasDegree = ref has[degreeTo[i]];
            if (hasDegree)
            {
                impossible = true;
                break;
            }
            degreeOwner[degreeTo[i]] = i;
            hasDegree = true;
        }
        Array.Fill(has, false, 0, n);
        if (impossible)
        {
            sw.WriteLine("IMPOSSIBLE");
            continue;
        }

        for (int i = 0; i < n; i++)
        {
            sw.Write(degreeOwner[i]);
            sw.Write(' ');
        }
        sw.WriteLine();
    }

    // 수 받아오기
    int ScanInt()
    {
        int c, n = 0;
        while (!((c = sr.Read()) is ' ' or '\n' or -1))
        {
            if (c == '\r')
            {
                sr.Read();
                break;
            }
            n = 10 * n + c - '0';
        }
        return n;
    }
#endif
}
