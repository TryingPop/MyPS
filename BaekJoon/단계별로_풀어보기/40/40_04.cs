using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 22
이름 : 배성훈
내용 : 선분 교차 1, 선분 교차 2
    문제번호 : 17386번, 17387번

    Wrong은 선형대수학의 크라머의 공식으로 구하려고 했다;
    그런데, 좌표의 입력값이 100만이고, det를 구하면 10의 12승까지 가고,
    그리고 여기서 det를 마지막에 나누는 경우 오른쪽은 10^18승을 넘길 수 있다(diffX, x or y, diffY 를 곱하기 때문!)
    이는 long으로도 오버플로우가 발생할 수 있다 (실제로 틀렸다)
    그래서 연산 순서를 바꿔가며 했으나 여러 번 틀렸다.

    이후에 이를 약간 수정해서 x, y의 공통 범위로 선분들을 축소시켰다
    즉 p1의 좌표를 x1, y1, p2의 좌표를 x2, y2라 하고 선분 1이 p1,p2를 끝점으로 한다고 하자
    그리고 p3의 좌표를 x3, y3, p4의 좌표를 x4, y4라하고 선분 2가 p3, p4를 끝점으로 한다고 하자

    그러면 선분 2와 선분 1에 대해 | 형태의 직선이 없을 때, 공통 x범위를 찾았다
    만약 교차한다면 이 구간에서 교차하기 때문이다

    여기서 한 점에서만 만나는 경우 그 점만 비교했다

    이제 한 점에서 만나지 않는 경우를 보면, 공통 x범위는 점이 아닌 구간이 된다!
    (서로 다른 두 점 사이에는 유리수가 항상존재한다, 완비성공리로 봐도 된다)

    공통 x범위로 선분1과 선분2를 축소시켰다
    축소시키는 과정에서 양 끝값을 비교해서 소수점의 올림과 내렸다
    만약 line1의 x최소값인 점을 p1으로 다시 정의하고 나머지 점을 p2라 하자 
    마찬가지로 line2의 x최소값인 점을 p3, 나머지를 p4 (Swap해주는거다!)

    이후에 p1의 x1값이 p3의 x3값보다 작다고 하면,
    p1의 x1값을 p3의 x3값으로 선분으로 이동시켜야한다

    이때, 직선에 따라 이동했을 때, p1의 y값이 만약 p3의 y3의 값보다 큰 경우
    소수점 부분은 올림 연산을 했다(Ceiling)
    반면 p1의 y값이 p3의 y3의 값보다 작은 경우면 y1의 소수점 값을 버렸다

    이러한 연산은 결과에 대소 비교하는데 지장이 없다!
    그리고 x의 최대값도 교정했다

    그리고 p1, p2의 각각의 y값 y1, y2에 대해 구간 y1, y2와 p3, p4의 각각의 y값 y3, y4가 X자 형태로 만나는지만 판단했다
    X자 형태가 되면 1, 아니면 0

    그리고 y값의 범위를 비교해서 X자 형태가 나타나는 경우면 교차한다고하고, 안나타나는 경우면 교창 안한다고 했다

    만약 둘 중 하나가 |인 경우면 둘 다 ㅡ형태의 직선이 아닌 경우로 가정해서, 공통 y범위를 찾았다
    위와 같은 논리이다!

    이외에 남는 경우는 ㅡ와 |가 섞인 형태인데 이는 x,y의 공통 범위가 존재하면 자동으로 해가 존재한다고 볼 수 있다!
    이 방법이 Wrong73인데 선분 교차 1은 통과되고, 선분교차 2는 73%에서 틀린다...;
    ... double연산 중에 오차로 생긴 문제 같다; -> 수정하니 시간은 오래 걸리지만 통과했다

    Wrong73에서 73%에서 몇 시간 동안 못벗어나서 결국에 CCW 알고리즘으로 풀었다
    논리는 간단하다

    만약 p1, p2를 양 끝접으로 하는 직선1과 p3, p4를 양 끝점으로 하는 직선 2에 대해
    직선 1과 직선 2가 만난다고 하면, 직선 1과 p3의 CCW 값을 r1, 직선 1과 p4의 CCW 값을 r2
    마찬가지로 직선 2와 p1의 CCW 값을 r3, 직선 2와 p2의 CCW 값을 r4라하면

    그리고 r1 == r2 == 0이면 같은 직선에 있다는 말이고 이 때는 x나 y의 범위가 겹치는지 확인해야한다!
    겹치면 만난다고 볼 수 있고 안겹치면 안만난다!
    그리고 r1 * r2 < 0 이고 r3 * r4 < 0 이면 교차한다!
    r1 * r2 > 0 또는 r3 * r4 > 0이면 해당 조건에서는 서로 교차하지 않는다고 할 수 있다!

    나머지 경우는 r1, r2 중에서 하나가 0, r3와 r4 중에서 하나가 0인 경우 뿐인데
    이 경우는 끝점을 공유하는 경우이므로 교차한다!
*/

namespace BaekJoon._40
{
    internal class _40_04
    {

        static void Main4(string[] args)
        {

#if Wrong
            long[] p1 = { NextInt(), NextInt() };
            long[] p2 = { NextInt(), NextInt() };

            long[] p3 = { NextInt(), NextInt() };
            long[] p4 = { NextInt(), NextInt() };


            long diffX21 = p2[0] - p1[0];
            long diffX43 = p4[0] - p3[0];

            long diffY21 = p2[1] - p1[1];
            long diffY43 = p4[1] - p3[1];

            long det = diffY43 * diffX21;
            det -= diffX43 * diffY21;

            if (det == 0)
            {

                Console.WriteLine(0);
                return;
            }


            long chk = diffX43 * diffX21 * p1[1];
            chk -= diffX43 * diffY21 * p1[0];

            chk += diffX21 * diffY43 * p3[0];
            chk -= diffX21 * diffX43 * p3[1];
            
            if (Chk(p1[0], p2[0], chk, det) && Chk(p3[0], p4[0], chk, det)) Console.WriteLine(1);
            else Console.WriteLine(0);
#elif Wrong73

            // 일단 범위 줄이기!
            long[] line1 = Array.ConvertAll(Console.ReadLine().Split(' '), long.Parse);
            long[] line2 = Array.ConvertAll(Console.ReadLine().Split(' '), long.Parse);

            if (ChkCommonXY(line1, line2))
            {

                if (line1[0] != line1[2] && line2[0] != line2[2])
                {

                    // 여기서 먼저 체크! 한점 만나는 상황나오는지;
                    SetPos(line1, line2, false);

                    // 여기서 한차례 솎아내자;
                    if (line1[0] == line2[2])
                    {

                        if (line1[1] == line2[3]) Console.WriteLine(1);
                        else Console.WriteLine(0);
                    }
                    else if (line1[2] == line2[0])
                    {

                        if (line1[3] == line2[1]) Console.WriteLine(1);
                        else Console.WriteLine(0);
                    }
                    else
                    {

                        ScaleXInterval(line1, line2);

                        // 이제는 y값이 겹치는지만 확인하자!
                        if (ChkInterval(line1, line2, true)) Console.WriteLine(1);
                        else Console.WriteLine(0);
                    }
                }
                else if (line1[1] != line1[3] && line2[1] != line2[3])
                {

                    SetPos(line1, line2, true);

                    // 한차례 솎아내기
                    if (line1[1] == line2[3])
                    {

                        if (line1[0] == line2[2]) Console.WriteLine(1);
                        else Console.WriteLine(0);
                    }
                    else if (line1[3] == line2[1])
                    {

                        if (line1[2] == line2[0]) Console.WriteLine(1);
                        else Console.WriteLine(0);
                    }
                    else
                    {

                        ScaleYInterval(line1, line2);

                        if (ChkInterval(line1, line2, false)) Console.WriteLine(1);
                        else Console.WriteLine(0);
                    }
                }
                else
                {

                    // 섞인 경우만 여기로 온다!
                    // 여기는 조건에 의해 참!
                    Console.WriteLine(1);
                }
            }
            else Console.WriteLine(0);
#else

            // CCW 풀이!
            long[] line1 = Array.ConvertAll(Console.ReadLine().Split(' '), long.Parse);
            long[] line2 = Array.ConvertAll(Console.ReadLine().Split(' '), long.Parse);

            int r1 = ChkCCW(line1, line2[0], line2[1]);
            int r2 = ChkCCW(line1, line2[2], line2[3]);
            
            if (r1 == r2 && r1 == 0)
            {

                if (line1[0] == line1[2])
                {

                    // '|' 인 경우!
                    // 사이에 잇는지 확인
                    long min1 = line1[1] < line1[3] ? line1[1] : line1[3];
                    long max1 = line1[1] < line1[3] ? line1[3] : line1[1];

                    long min2 = line2[1] < line2[3] ? line2[1] : line2[3];
                    long max2 = line2[1] < line2[3] ? line2[3] : line2[1];

                    if (min1 > max2 || min2 > max1) Console.WriteLine(0);
                    else Console.WriteLine(1);
                }
                else
                {

                    long min1 = line1[0] < line1[2] ? line1[0] : line1[2];
                    long max1 = line1[0] < line1[2] ? line1[2] : line1[0];

                    long min2 = line2[0] < line2[2] ? line2[0] : line2[2];
                    long max2 = line2[0] < line2[2] ? line2[2] : line2[0];

                    if (min1 > max2 || min2 > max1) Console.WriteLine(0);
                    else Console.WriteLine(1);
                }
            }
            else if (r1 * r2 <= 0)
            {

                r1 = ChkCCW(line2, line1[0], line1[1]);
                r2 = ChkCCW(line2, line1[2], line1[3]);

                if (r1 * r2 <= 0)
                {

                    Console.WriteLine(1);
                }
                else
                {

                    Console.WriteLine(0);
                }
            }
            else
            {

                Console.WriteLine(0);
            }
#endif
        }


#if Wrong
        static bool Chk(long _a, long _b, long _chk, long _det)
        {

            _a *= _det;
            _b *= _det;

            long min = _a < _b ? _a : _b;
            long max = _a < _b ? _b : _a;
            
            if (_chk < min) return false;
            if (_chk > max) return false;

            return true;
        }

        static long NextInt()
        {

            long result = 0;
            bool minus = false;
            int c;
            while((c = Console.Read()) != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                if (result == 0 && c == '-') 
                { 
                    minus = true;
                    continue;
                }
                c -= '0';

                result *= 10;
                result += c;
            }

            if (minus) result = -result;
            return result;
        }
#elif Wrong73

        static bool ChkCommonXY(long[] _line1, long[] _line2)
        {

            // 겹치는 구간이 있는지 확인
            long min1 = _line1[0] < _line1[2] ? _line1[0] : _line1[2];
            long min2 = _line2[0] < _line2[2] ? _line2[0] : _line2[2];

            long max1 = _line1[0] < _line1[2] ? _line1[2] : _line1[0];
            long max2 = _line2[0] < _line2[2] ? _line2[2] : _line2[0];

            if (min1 > max2 || min2 > max1) return false;

            min1 = _line1[1] < _line1[3] ? _line1[1] : _line1[3];
            min2 = _line2[1] < _line2[3] ? _line2[1] : _line2[3];

            max1 = _line1[1] < _line1[3] ? _line1[3] : _line1[1];
            max2 = _line2[1] < _line2[3] ? _line2[3] : _line2[1];

            if (min1 > max2 || min2 > max1) return false;

            return true;
        }

        static void SetPos(long[] _line1, long[] _line2, bool _isY)
        {

            int first = _isY ? 1 : 0;
            int next = _isY ? 0 : 1;

            if (_line1[first] > _line1[first + 2]
                || (_line1[first] == _line1[first + 2] && _line1[next] > _line1[next + 2]))
            {

                Swap(_line1, 0, 2);
                Swap(_line1, 1, 3);
            }
            if (_line2[first] > _line2[first + 2]
                || (_line2[first] == _line2[first + 2] && _line2[next] > _line2[next + 2]))
            {

                Swap(_line2, 0, 2);
                Swap(_line2, 1, 3);
            }
        }

        private static void ScaleYInterval(long[] _line1, long[] _line2)
        {

            if (_line1[1] < _line2[1])
            {

                _line1[0] = CalcY(_line1, _line2, true);
                _line1[1] = _line2[1];
            }
            else if (_line1[1] > _line2[1])
            {

                _line2[0] = CalcY(_line2, _line1, true);
                _line2[1] = _line1[1];
            }

            if (_line1[3] > _line2[3])
            {

                _line1[2] = CalcY(_line1, _line2, false);
                _line1[3] = _line2[3];
            }
            else if (_line1[3] < _line2[3])
            {

                _line2[2] = CalcY(_line2, _line1, false);
                _line2[3] = _line1[3];
            }
        }

        // 일단은 x 값의 범위를 바꾼다!
        static void ScaleXInterval(long[] _line1, long[] _line2)
        {

            // min값 수정
            if (_line1[0] < _line2[0])
            {

                _line1[1] = CalcX(_line1, _line2, true);
                _line1[0] = _line2[0];
            }
            else if (_line1[0] > _line2[0])
            {

                _line2[1] = CalcX(_line2, _line1, true);
                _line2[0] = _line1[0];
            }

            // max값 수정
            if (_line1[2] > _line2[2])
            {

                _line1[3] = CalcX(_line1, _line2, false);
                _line1[2] = _line2[2];
            }
            else if (_line1[2] < _line2[2])
            {

                _line2[3] = CalcX(_line2, _line1, false);
                _line2[2] = _line1[2];
            }
        }

        static long CalcY(long[] _calcLine, long[] _chkLine, bool _calcMin)
        {

            long diffX = _calcLine[2] - _calcLine[0];
            long diffY = _calcLine[3] - _calcLine[1];

            long result = _calcMin ? diffX * _chkLine[1] : diffX * _chkLine[3];
            result -= diffX * _calcLine[1];
            result += diffY * _calcLine[0];

            long chk = _calcMin ? _chkLine[0] * diffY : _chkLine[2] * diffY;

            if (chk == result) result = _calcMin ? _chkLine[0] : _chkLine[2];
            else if (chk < result) result = _calcMin ? _chkLine[0] + 1 : _chkLine[2] + 1;
            else result = _calcMin ? _chkLine[0] - 1 : _chkLine[2] - 1;
            /*
            else if (chk < result)
            {

                double c = (double)result / (double)diffY;
                c = Math.Ceiling(c);
                result = (long)c;
            }
            else result /= diffY;
            */
            return result;
        }


        static long CalcX(long[] _calcLine, long[] _chkLine, bool _calcMin)
        {

            long diffX = _calcLine[2] - _calcLine[0];
            long diffY = _calcLine[3] - _calcLine[1];

            long result = _calcMin ? diffY * _chkLine[0] : diffY * _chkLine[2];
            result -= diffY * _calcLine[0];
            result += diffX * _calcLine[1];

            long chk = _calcMin ? _chkLine[1] * diffX : _chkLine[3] * diffX;

            if (chk == result) result = _calcMin ? _chkLine[1] : _chkLine[3];
            else if (chk < result) result = _calcMin ? _chkLine[1] + 1 : _chkLine[3] + 1;
            else result  = _calcMin ? _chkLine[1] - 1: _chkLine[3] - 1;
            /*
            else if (chk < result)
            {

                // 올림 계산
                double c = (double)result / (double)diffX;
                c = Math.Ceiling(c);
                result = (long)c;
            }
            // 내림 계산
            else result /= diffX;
            */
            return result;
        }

        static bool ChkInterval(long[] _line1, long[] _line2, bool _isY)
        {

            // y값 비교인지? 묻는다 y값 비교면 홀수!
            int first = _isY ? 1 : 0;

            if (_line1[first] < _line2[first] && _line1[first + 2] < _line2[first + 2]) return false;
            if (_line1[first] > _line2[first] && _line1[first + 2] > _line2[first + 2]) return false;

            return true;
        }

        static void Swap(long[] _line, int _idx1, int _idx2)
        {

            long temp = _line[_idx1];
            _line[_idx1] = _line[_idx2];
            _line[_idx2] = temp;
        }
#else

        static int ChkCCW(long[] _line, long _x3, long _y3)
        {

            long det = _line[0] * _line[3];
            det -= _line[0] * _y3;
            det += _line[2] * _y3;
            det -= _line[2] * _line[1];
            det += _x3 * _line[1];
            det -= _x3 * _line[3];

            if (det == 0) return 0;
            else if (det > 0) return 1;
            else return -1;
        }
#endif
    }
}
