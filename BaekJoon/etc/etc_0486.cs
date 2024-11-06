using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 8
이름 : 배성훈
내용 : 가희와 자원 놀이
    문제번호 : 21775번

    시뮬레이션, 구현 문제다
    카드 id가 len 만큼만 주어지는줄 알고 그냥 제출했다가
    인덱스 에러로 한 번 틀렸다 -> 벗어나는 경우가 있다!
    그래서 id의 최대값으로 배열을 할당했다

    조건대로 구현하니 248ms로 통과했다
    다른 사람 시간을 보니 데이터 양이 많은거 같다
*/

namespace BaekJoon.etc
{
    internal class etc_0486
    {

        static void Main486(string[] args)
        {

            int NEXT = 68_088;
            int ACQUIRE = 54_825_413;
            int RELEASE = 71_958_623;
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()), bufferSize: 65536 * 8);
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()), bufferSize: 65536 * 2);

            int p = ReadInt();
            int len = ReadInt();

            int[] turn = new int[len];
            for (int i = 0; i < len; i++)
            {

                turn[i] = ReadInt();
            }

            int[] player = new int[p + 1];
            int[] acN = new int[500_000 + 1];
            HashSet<int> resource = new(len);
            int cur = 0;

            for (int i = 0; i < len; i++)
            {

                int curPlayer = turn[i];
                if (player[curPlayer]== 0)
                {

                    int n = ReadInt();
                    int t = ReadInt();

                    sw.WriteLine(n);
                    if (t == NEXT) continue;
                    else if (t == RELEASE)
                    {

                        int k = ReadInt();
                        resource.Remove(k);
                    }
                    else
                    {

                        int k = ReadInt();
                        if (resource.Contains(k))
                        {

                            player[curPlayer] = n;
                            acN[n] = k;
                        }
                        else resource.Add(k);
                    }

                    continue;
                }

                int get = acN[player[curPlayer]];
                sw.WriteLine(player[curPlayer]);
                if (resource.Contains(get)) continue;

                resource.Add(get);
                player[curPlayer] = 0;
            }

            sw.Close();
            sr.Close();
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
System.Text.StringBuilder sb = new System.Text.StringBuilder();

string[] line = Console.ReadLine().Split(' ').ToArray();
int N = int.Parse(line[0]);
Player[] P = new Player[N + 1]; for (int i = 0; i <= N; i++) P[i] = new Player();

int T = int.Parse(line[1]);
int[] Tern = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

HashSet<int> minusDeck = new HashSet<int>();
foreach(var player in Tern)
{
    if (!P[player].card) // 카드를 새로 가져온다
    {
        line = Console.ReadLine().Split(' ').ToArray();
        switch (line[1])
        {
            case "next": // next
                sb.AppendLine(line[0]);
                break;
            case "acquire": // 카드 가져오기 // 대기줄에 있으면 기다린다
                if (minusDeck.Contains(int.Parse(line[2]))) // 대기중
                {
                    P[player].card = true;
                    P[player].id = int.Parse(line[0]);
                    P[player].num = int.Parse(line[2]);
                    sb.AppendLine(line[0]);
                }
                else // 숫자 가져온다.
                {
                    minusDeck.Add(int.Parse(line[2]));
                    sb.AppendLine(line[0]);
                }
                break;
            case "release": // 덱에서 삭제
                minusDeck.Remove(int.Parse(line[2]));
                sb.AppendLine(line[0]);
                break;
        }
    }
    else // 현재 플레이어는 acquire 가지고있음
    {
        if (minusDeck.Contains(P[player].num)) // 대기줄에 있으면 넘어간다
        {
            sb.AppendLine(P[player].id.ToString());
            continue;
        }
        // 대기줄에 없으면 가져오고 대기줄에 추가
        minusDeck.Add(P[player].num);
        P[player].card = false;
        sb.AppendLine(P[player].id.ToString());
    }
}

Console.Write(sb);


return;

class Player
{
    public bool card;
    public int id;
    public int num;

    public Player()
    {
        card = false;
        num = -1;
    }
}
#endif
}
