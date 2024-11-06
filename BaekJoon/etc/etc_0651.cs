using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 20
이름 : 배성훈
내용 : 돌 옮기기
    문제번호 : 28436번

    애드 혹, 게임 이론 문제다
    먼저 옮겨도 괜찮은 돌과 안되는 돌을 구분해야한다
    옮겨도 괜찮은건 뒤에 다른 사람의 돌이 없는 경우다

    왜냐하면 자신이 움직이는 만큼 상대의 이동거리가 늘어나기에
    자신이 움직이는 양보다 더 움직일 수 있다

    ..W..BBB
    라 보면
    W는 2칸 이동하면 끝이지만 B는 뒤에 2개의 B가 2칸씩 더 이동할 수 있어
    전체 4칸을 움직일 수 있다

    그래서 뒤에 다른색 돌이 있다면 움직이지 않는다
    이후 구현 부분에서 막혀 풀이 방법을 보고 따라 구현했다;

    풀이도 뒤에부터 이동하는데, 
    바로 뒤에 다른 색 돌이 있다면 움직이지 않는 선택을 한다
*/

namespace BaekJoon.etc
{
    internal class etc_0651
    {

        static void Main651(string[] args)
        {

            string BLACK = "BLACK\n";
            string WHITE = "WHITE\n";

            StreamReader sr;
            StreamWriter sw;

            int[] dol;
            int len;

            Solve();
            void Solve()
            {

                Init();

                int test = int.Parse(sr.ReadLine());
                while (test-- > 0)
                {

                    Input();

                    sw.Write(GetRet() ? WHITE : BLACK);
                }

                sw.Close();
                sr.Close();
            }

            bool GetRet()
            {

                long ret = 0, cnt = 0;
                int cur = 0;

                for (int i = 0; i < len; i++)
                {

                    // 현재 돌이 비어있고 현재 지점이 돌인지 확인
                    if (cur == 0) cur = dol[i];
                    if (cur == 0) continue;

                    // 비어있는 곳이면 이동가능한 곳으로 이동 카운트
                    if (dol[i] == 0) ret += (cur == 1 ? 1 : -1) * cnt;
                    // 같은 색 돌이므로 개수 추가
                    else if (cur == dol[i]) cnt++;
                    else 
                    {

                        // 다른색 돌이면 이동 끝
                        cur = 0;
                        cnt = 0;
                    }
                }

                return ret > 0;
            }

            void Input()
            {

                string temp = sr.ReadLine();
                len = temp.Length;
                
                for (int i = 0; i < len; i++)
                {

                    int cur;
                    switch(temp[len - i - 1])
                    {

                        case 'W':
                            cur = 1;
                            break;

                        case 'B':
                            cur = 2;
                            break;

                        default:
                            cur = 0;
                            break;
                    }

                    dol[i] = cur;
                }
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                dol = new int[500_000];
            }
        }
    }
}