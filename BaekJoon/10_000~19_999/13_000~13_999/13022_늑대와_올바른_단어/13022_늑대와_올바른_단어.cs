using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 9
이름 : 배성훈
내용 : 늑대와 올바른 단어
    문제번호 : 13022번

    ... 구현 문제인데 70%벽에 막혀서 7번이나 틀렸다;
    문제는 wwww로 끝나는 경우 일어났다;

    푸는건 상황을 하나씩 나눠서 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0007
    {

        static void Main7(string[] args)
        {

            string str = Console.ReadLine();

            // 4배수가 아니면 for문 자체를 안돌린다
            bool success = str.Length % 4 == 0;
            
            if (success)
            {

                int wCnt = 1;
                int curCnt = 1;
                char pre = 'f';

                for (int i = 0; i < str.Length; i++)
                {

                    if (curCnt < wCnt && str[i] == pre)
                    {

                        curCnt++;
                        continue;
                    }
                    else if (curCnt == wCnt)
                    {

                        if (pre == 'o' && str[i] == 'l')
                        {

                            pre = 'l';
                            curCnt = 1;
                            continue;
                        }
                        else if (pre == 'l' && str[i] == 'f')
                        {

                            pre = 'f';
                            curCnt = 1;
                            continue;
                        }
                        else if (pre == 'f' && str[i] == 'w')
                        {

                            pre = 'w';
                            wCnt = 1;
                            curCnt = 1;
                            continue;
                        }
                        else if (pre == 'w')
                        {

                            if (str[i] == 'o')
                            {

                                pre = 'o';
                                curCnt = 1;
                                continue;
                            }
                            else if (str[i] == 'w')
                            {

                                curCnt++;
                                wCnt++;
                                continue;
                            }
                        }
                    }

                    success = false;
                    break;
                }

                // 마지막에 wwwwoooo으로 끝나거나 문자열이 없으면 for문에서 못찾기에 여기서 한 번 확인한다
                if (pre != 'f' || str.Length == 0) success = false;
            }

            if (success) Console.WriteLine(1);
            else Console.WriteLine(0);
        }


    }
}
