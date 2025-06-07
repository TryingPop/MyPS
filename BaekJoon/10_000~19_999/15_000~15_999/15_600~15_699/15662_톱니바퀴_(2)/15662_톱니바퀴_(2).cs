using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 14
이름 : 배성훈
내용 : 톱니바퀴 2
    문제번호 : 15662번

    구현, 시뮬레이션 문제다

    매번 배열을 회전 시킬 수 없기에
    방법을 출력하지 않는 숫자 맞추기 문제처럼
    회전 수를 저장해 연산으로 왼쪽과 오른쪽을 찾았다

    이외는 조건대로 구현하고 시뮬레이션 돌렸다
*/

namespace BaekJoon.etc
{
    internal class etc_0221
    {

        static void Main221(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = ReadInt(sr);

            int[][] sectors = new int[len][];

            for (int i = 0; i < len; i++)
            {

                sectors[i] = GetSector(sr);
            }

            int test = ReadInt(sr);

            while(test-- > 0)
            {

                int target = ReadInt(sr) - 1;
                int rot = ReadInt(sr);

                int left = GetMeet(sectors[target], true);
                int right = GetMeet(sectors[target], false);

                Rotate(sectors[target], rot);

                int curRot = -rot;

                // 오른쪽 돌리기여부 확인
                for (int i = target + 1; i < len; i++)
                {

                    int nextLeft = GetMeet(sectors[i], true);

                    if (right == nextLeft) break;

                    right = GetMeet(sectors[i], false);
                    Rotate(sectors[i], curRot);
                    curRot = -curRot;
                }

                // 왼쪽 돌리기 확인
                curRot = -rot;
                for (int i = target - 1; i >= 0; i--)
                {
                    
                    int nextRight = GetMeet(sectors[i], false);

                    if (left == nextRight) break;

                    left = GetMeet(sectors[i], true);
                    Rotate(sectors[i], curRot);
                    curRot = -curRot;
                }
            }

            sr.Close();

            int ret = 0;
            for (int i = 0; i < len; i++)
            {

                int idx = sectors[i][0];
                if (sectors[i][idx] == 1) ret++;
            }

            Console.WriteLine(ret);
        }

        static void Rotate(int[] _sector, int _rot)
        {

            _sector[0] -= _rot;
            if (_sector[0] > 8) _sector[0] -= 8;
            else if (_sector[0] < 1) _sector[0] += 8;
        }

        static int GetMeet(int[] _sector, bool _isLeft)
        {

            int add = _isLeft ? 6 : 2;

            int ret = _sector[0] + add;
            if (8 < ret) ret -= 8;
            else if (ret < 1) ret += 8;

            ret = _sector[ret];
            return ret;
        }

        static int[] GetSector(StreamReader _sr)
        {

            // 0 : 회전 정도
            // 1 ~ 8 : 톱니바퀴 초기 상태
            int[] ret = new int[9];
            ret[0] = 1;
            for (int i = 1; i < 9; i++)
            {

                ret[i] = _sr.Read() - '0';
            }

            // \n 출력!
            ReadInt(_sr);

            return ret;
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;
            bool plus = true;
            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                else if (c == '-')
                {

                    plus = false;
                    continue;
                }

                ret = ret * 10 + c - '0';
            }

            return plus ? ret : -ret;
        }
    }
}
