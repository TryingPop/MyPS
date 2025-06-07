using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 10
이름 : 배성훈
내용 : 비행기
    문제번호 : 3256번

    구현 문제이다
    좌석 배열을 두고, 사람들이 이동하는 형식의 코드를 구현했다
    문제 상황만 잘 인식하면 쉽게 구현된다    

    예를들어 문제 상황을 보자
        1   <- 전체 사람
        3   <- 1번째 사람의 목적지
        
        1번째 사람은 시작과 동시에 1번에 위치해있다
        일단 사람은 다 입장했다
            해당 사람이 아직 좌석에 못 앉았으므로 다음 시간으로 간다

        1초 시점에 2번 자리에 사람이 없어 1번 사람은 2로 간다
            아직 모든 사람이 못 앉아서 다음 시간으로 간다

        2초 시점에 3번에 사람이 없어서 1번 사람이 3으로 간다
            이제 막 도착했고 바로 5초간 시간을 세기 시작한다
            아직 모든 사람이 못 앉아서 다음 시간으로 간다

        3초 1번 사람이 목적지에 도착했고 앉을 준비를한지 1초 지났다(4초 남았다)
            아직 모든 사람이 못 앉아서 다음 시간으로 간다

        4.. 5.. 6... 초가 흐르고

        7초 1번 사람이 목적지에 도착했고 앉을 준비를 끝냈다 그리고 앉는다
            이제 모든 사람이 다 앉았으므로 종료한다


    또 다른 예를 보자
        2   <- 전체 사람 수
        3   <- 1번째 사람이 앉을 목적지
        3   <- 2번째 사람이 앉을 목적지

        1초 1번째 사람이 2번에 위치해있고, 2번째 사람이 1번에 입장한다
            1,2 번 둘 다 현재 위치가 목적지가 아니다

        2초 1번째 사람이 3번에 위치해있고, 2번 사람이 2번에 있다
            1번 사람은 목적지에 도착해있고 5초간 대기한다

        3초 1번 사람이 3번에 위치해있다, 그리고 2번 사람이 2번에 있다
            1번 사람은 목적지에 4초간 있어야한다

        ...
        7초 1번 사람이 이제 앉았다 그래서 해당 좌석은 비었고,
            2번 사람이 3번에 도착한다 그리고 5초간 대기한다

        ...
        11초 2번 사람이 3번 자리에 있고 1초간 더 기다려야한다(4초째 대기 중)

        12초 2번 사람이 다 앉았다
            모든 사람이 다 앉았으므로 종료한다

    해당 상황대로 코드를 구현했다
    주의할건 다음과 같다
    기차놀이 하듯이 사람들이 지나간다! <<< 그래서 역순으로 for문을 짰다

    다음으로 만약 사람이 자기 좌석에 갔다면, 5초동안 있다고 치고 못가게 막아야한다
    그리고 5초가 지나는 시점에 바로 지날 수 있는 공간이 된다! <<< 역순이 이 문제를 해결해 준다!

    //////
    처음 제출할 때에는, 앉는데 적어도 1초가 걸리고, 사람이 1명이 보장되어 있어서
    n초가 되면 바로 행동을 시작하고 이후에 검증하는 형식으로 while문을 돌렸다

    타이머 증가와 행동 부분 순서는 따로 중요하지 않지만 검증과 (행동 + 시간 증가) 부분 순서는 중요하다고 생각한다
    해당 문제에서는 일어날 수 없는 상황이지만 0명이 입장한 경우에도 쓸러면 검증 -> 행동 + 시간 증가로 하면 판별 가능해지기 때문이다
*/

namespace BaekJoon.etc
{
    internal class etc_0013
    {

        static void Main13(string[] args)
        {

            int MAX = 1_000;
            int WAIT = 5;

            // 현재 좌석 MAX + 1 사이즈를 할당했다
            int[] seat = new int[MAX + 2];
            Array.Fill(seat, -1);
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            // StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());
            int timer = 0;

            int len = ReadInt(sr);
            // 목적지
            int[] goal = new int[len];
            // 5초 대기 완료했는지 결과용
            int[] chk = new int[len];
            Array.Fill(chk, WAIT);
            for (int i = 0; i < len; i++)
            {
                
                goal[i] = ReadInt(sr);
            }

            seat[1] = 0;
            
            sr.Close();

            int enter = 1;
            while (true)
            {

                // 모든 사람이 제대로 입장하면 착석했는지 확인시작
                if (enter == len)
                {

                    bool end = true;

                    // 타이머로 착석 여부 확인
                    for (int i = 0; i < len; i++)
                    {

                        if (chk[i] != 0)
                        {

                            end = false;
                            break;
                        }
                    }

                    if (end) break;
                }

                // 앉지 못했다 그래서 행동시작!
                timer++;

                for (int i = MAX; i >= 1; i--)
                {

                    // 자리에 도착했는지 확인
                    if (seat[i] != -1 && goal[seat[i]] == i)
                    {

                        if (chk[seat[i]] > 0)
                        {

                            chk[seat[i]]--;

                            // 도착하고 대기시간 지났으므로 바로 
                            if (chk[seat[i]] == 0)
                            {

                                goal[seat[i]] = 0;
                                seat[i] = -1;
                            }
                        }
                    }
                    // 앞으로 전진
                    else if (seat[i + 1] == -1 && seat[i] != -1)
                    {

                        int cur = seat[i];
                        if (goal[cur] > i)
                        {

                            seat[i + 1] = cur;
                            seat[i] = -1;
                        }
                    }
                }

                if (seat[1] == -1 && enter < len)
                {

                    seat[1] = enter++;
                }
            }

            // sw.WriteLine(timer);
            // sw.Close();
            Console.WriteLine(timer);
        }

        static int ReadInt(StreamReader _sr)
        {

            int ret = 0;
            int c;

            while ((c = _sr.Read()) != -1 && c != '\n' && c != ' ')
            {

                if (c == '\r') continue;

                ret *= 10;
                ret += c - '0';
            }

            return ret;
        }
    }
}
