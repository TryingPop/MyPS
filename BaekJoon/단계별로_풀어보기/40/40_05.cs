using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 23
이름 : 배성훈
내용 : 선분 교차 3
    문제번호 : 20149번

    40_04를 참고하면 쉽게 풀리는 문제이다.
    조건문 잘못 설정해서 2% 에서 한참 틀렸다

    4점 모두 한 직선에 있는 경우 크라머를 사용하면 det = 0이므로 zero division을 한다!
    그래서 Nan을 출력해 틀릴 것이다!    
*/

namespace BaekJoon._40
{
    internal class _40_05
    {

        static void Main5(string[] args)
        {

            long[] line1 = Array.ConvertAll(Console.ReadLine().Split(' '), long.Parse);
            long[] line2 = Array.ConvertAll(Console.ReadLine().Split(' '), long.Parse);

            int r1 = ChkCCW(line1, line2[0], line2[1]);
            int r2 = ChkCCW(line1, line2[2], line2[3]);

            if (r1 == r2 && r1 == 0)
            {

                // 4점이 모두 일직선인 경우다!
                if (line1[0] == line2[2])
                {

                    // 먼저 |형태인지 고려 !한다
                    // 이 경우 y값의 범위 체크가 필요하다
                    long min1 = line1[1] < line1[3] ? line1[1] : line1[3];
                    long max1 = line1[1] < line1[3] ? line1[3] : line1[1];

                    long min2 = line2[1] < line2[3] ? line2[1] : line2[3];
                    long max2 = line2[1] < line2[3] ? line2[3] : line2[1];

                    // 겹치는 범위가 없는 경우
                    if (min1 > max2 || min2 > max1) Console.WriteLine(0);
                    // 여기에 else안넣어서 생긴 문제!
                    else
                    {

                        // 범위가 겹치는 경우
                        Console.WriteLine(1);

                        // 끝점이 겹치면 유일한 점이된다!
                        if (min1 == max2)
                        {

                            if (min1 == line1[1]) Console.WriteLine($"{line1[0]} {line1[1]}");
                            else Console.WriteLine($"{line1[2]} {line1[3]}");
                        }
                        else if (min2 == max1)
                        {

                            if (max1 == line1[1]) Console.WriteLine($"{line1[0]} {line1[1]}");
                            else Console.WriteLine($"{line1[2]} {line1[3]}");
                        }
                    }
                }
                else
                {

                    // |인 경우는 없으므로
                    // y -> x로 바꾸면 위와 마찬가지이다
                    long min1 = line1[0] < line1[2] ? line1[0] : line1[2];
                    long max1 = line1[0] < line1[2] ? line1[2] : line1[0];

                    long min2 = line2[0] < line2[2] ? line2[0] : line2[2];
                    long max2 = line2[0] < line2[2] ? line2[2] : line2[0];

                    if (min1 > max2 || min2 > max1) Console.WriteLine(0);
                    else
                    {
                        Console.WriteLine(1);

                        if (min1 == max2)
                        {

                            if (min1 == line1[0]) Console.WriteLine($"{line1[0]} {line1[1]}");
                            else Console.WriteLine($"{line1[2]} {line1[3]}");
                        }
                        else if (min2 == max1)
                        {

                            if (max1 == line1[0]) Console.WriteLine($"{line1[0]} {line1[1]}");
                            else Console.WriteLine($"{line1[2]} {line1[3]}");
                        }
                    }
                }

            }
            else if (r1 * r2 > 0)
            {

                // 없는 경우다!
                Console.WriteLine(0);
            }
            else
            {

                r1 = ChkCCW(line2, line1[0], line1[1]);
                r2 = ChkCCW(line2, line1[2], line1[3]);

                if (r1 * r2 > 0) Console.WriteLine(0);
                else 
                { 
                    
                    // 이외에 있는 경우
                    Console.WriteLine(1);

                    Crammer(line1, line2);
                }
            }
        }

        static int ChkCCW(long[] _line, long _x, long _y)
        {

            long det = _line[0] * _line[3];
            det -= _line[0] * _y;
            det += _line[2] * _y;
            det -= _line[2] * _line[1];
            det += _x * _line[1];
            det -= _x * _line[3];

            if (det == 0) return 0;
            else if (det > 0) return 1;
            else return -1;
        }

        static void Crammer(long[] _line1, long[] _line2)
        {

#if !chk1
            ///double

            long dX21 = _line1[2] - _line1[0];
            long dX43 = _line2[2] - _line2[0];

            long dY21 = _line1[3] - _line1[1];
            long dY43 = _line2[3] - _line2[1];

            long chk = 0;
            double calc = 0;

            long det = dX21 * dY43;
            det -= dY21 * dX43;

            double inverseDet = 1 / (double)det;

            double x = 0;
            if (_line1[0] == _line1[2])
            {

                x = _line1[0];
            }
            else if (_line2[0] == _line2[2])
            {

                x = _line2[0];
            }
            else
            {

                chk = dX21 * _line1[1] - dY21 * _line1[0];
                calc = (double)dX43 * inverseDet;
                x = (double)chk * calc;

                chk = dY43 * _line2[0] - dX43 * _line2[1];
                calc = (double)dX21 * inverseDet;
                x += (double)chk * calc;
            }

            double y = 0;
            if (_line1[1] == _line1[3])
            {

                y = _line1[1];
            }
            else if (_line2[1] == _line2[3])
            {

                y = _line2[1];
            }
            else
            {

                chk = dX21 * _line1[1] - dY21 * _line1[0];
                calc = (double)dY43 * inverseDet;
                y = (double)chk * calc;

                chk = dY43 * _line2[0] - dX43 * _line2[1];
                calc = (double)dY21 * inverseDet;
                y += (double)chk * calc;
            }
#elif chk2
            // decimal
            long dX21 = _line1[2] - _line1[0];
            long dX43 = _line2[2] - _line2[0];

            long dY21 = _line1[3] - _line1[1];
            long dY43 = _line2[3] - _line2[1];

            long det = dX21 * dY43;
            det -= dY21 * dX43;

            decimal inverseDet = 1 / (decimal)det;

            long chk = 0;
            decimal calc = 0;
            
            
            decimal x = 0;
            if (_line1[0] == _line1[2])
            {

                x = _line1[0];
            }
            else if (_line2[0] == _line2[2])
            {

                x = _line2[0];
            }
            else
            {

                chk = dX21 * _line1[1] - dY21 * _line1[0];
                calc = (decimal)dX43 * inverseDet;
                x = (decimal)chk * calc;

                chk = dY43 * _line2[0] - dX43 * _line2[1];
                calc = (decimal)dX21 * inverseDet;
                x += (decimal)chk * calc;
            }


            decimal y = 0;
            if (_line1[1] == _line1[3])
            {

                y = _line1[1];
            }
            else if (_line2[1] == _line2[3])
            {

                y = _line2[1];
            }
            else
            {

                chk = dX21 * _line1[1] - dY21 * _line1[0];
                calc = (decimal)dY43 * inverseDet;
                y = (decimal)chk * calc;

                chk = dY43 * _line2[0] - dX43 * _line2[1];
                calc = (decimal)dY21 * inverseDet;
                y += (decimal)chk * calc;
            }
#endif

            decimal X = (decimal)x;
            decimal Y = (decimal)y;
            Console.WriteLine($"{X:0.#########} {Y:0.#########}");
        }
    }
}
