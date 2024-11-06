using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 26
이름 : 배성훈
내용 : Surface Area of Cubes
    문제번호 : 16811번

    기하학, 해시 문제다
    아이디어는 다음과 같다
    자르는 도형의 면으로 접근하면 된다

    만약 해당면이 가장 바깥이면 이미 포함된 것으로 간주하고 빼고
    해당 면이 가장 바깥이 아니면 
    set에 이미 포함되었는지 확인하고
    포함되었다면 빼주고, 포함 안되었으면 더해준다

    포함되었다면 면이 합쳐져서 가려졌다고 보면 된다
    이렇게 하니 이상없이 통과한다
*/

namespace BaekJoon.etc
{
    internal class etc_1002
    {

        static void Main1002(string[] args)
        {

            StreamReader sr;

            // Front Back : fb
            // Left Right : lr
            // Up Down : ud
            HashSet<(int x, int y, int z)> fb, lr, ud;
            int A, B, C;
            int n;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                long ret = 2L * A * B + 2L * B * C + 2L * C * A;

                for (int i = 0; i < n; i++)
                {

                    int x = ReadInt();
                    int y = ReadInt();
                    int z = ReadInt();

                    ret += ChkArea(fb, x, y, z, 1);
                    ret += ChkArea(fb, x + 1, y, z, 1);

                    ret += ChkArea(lr, x, y, z, 2);
                    ret += ChkArea(lr, x, y + 1, z, 2);

                    ret += ChkArea(ud, x, y, z, 3);
                    ret += ChkArea(ud, x, y, z + 1, 3);
                }

                sr.Close();

                Console.Write(ret);

                int ChkArea(HashSet<(int x, int y, int z)> _area, int _x, int _y, int _z, int _t)
                {

                    if (_area.Contains((_x, _y, _z)))
                    {

                        _area.Remove((_x, _y, _z));
                        return -1;
                    }
                    else if (ChkOut(_x, _y, _z, _t)) return -1;

                    _area.Add((_x, _y, _z));
                    return 1;
                }

                bool ChkOut(int _x, int _y, int _z, int _t)
                {

                    switch (_t)
                    {

                        case 1:
                            return _x == 0 || _x >= A;

                        case 2:
                            return _y == 0 || _y >= B;

                        case 3:
                            return _z == 0 || _z >= C;
                    }

                    return false;
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                fb = new(2_000);
                lr = new(2_000);
                ud = new(2_000);

                A = ReadInt();
                B = ReadInt();
                C = ReadInt();

                n = ReadInt();
            }

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
}
