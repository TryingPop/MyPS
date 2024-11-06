using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 8
이름 : 배성훈
내용 : Spiral
    문제번호 : 9714번

    나선형 좌표를 찾는 문제이다
    포폴에서 그룹 유닛 배치할 때 비슷한 로직을 써서 규칙을 빠르게 찾았다
*/

namespace BaekJoon.etc
{
    internal class etc_0004
    {

        static void Main4(string[] args)
        {

            StreamReader sr = new StreamReader(Console.OpenStandardInput());
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int test = int.Parse(sr.ReadLine());

            for (int t = 0; t < test; t++)
            {

                int num = int.Parse(sr.ReadLine());

                int len = (int)Math.Ceiling(Math.Sqrt(num));

                // 홀수
                int cur = len / 2;
                int end = len * len;
                int x, y;
                // 홀수
                if ((len & 1) != 0)
                {

                    // 끝의 x, y좌표
                    y = cur;
                    x = -cur;

                    // 위쪽에 위치한 경우
                    if (num > end - len)
                    {

                        // y축 이동만 하면 된다
                        y += num - end;
                    }
                    else
                    {

                        // x 값을 찾아야한다
                        y = -y;
                        x += (end - len + 1) - num;
                    }
                }
                else
                {

                    // 끝값
                    y = 1 - cur;
                    x = cur;
                    
                    if (num > end - len)
                    {

                        // y계산
                        y += end - num;
                    }
                    else
                    {

                        // x계산
                        y += len - 1;
                        x += num - (end - len + 1);
                    }
                }

                // 결과 출력 
                sw.Write('(');
                sw.Write(x);
                sw.Write(',');
                sw.Write(y);
                sw.Write(')');
                sw.Write('\n');
            }

            sr.Close();
            sw.Close();
        }
    }
}
