using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 27
이름 : 배성훈
내용 : 기차가 어둠을 헤치고 은하수를
    문제번호 : 15787번

    문제 해석을 잘못해 2번 틀렸다
    명령으로 해당되는 기차의 승객을 조절한다
    그리고 명령이 끝난 은하수를 지나가는데
    만약 이미 기록된 앉는 방법이면 해당 기차는 못지나가고,
    아직 기록 안되었다면 은하수를 건너게하고 해당 앉는 방법을 기록한다
*/

namespace BaekJoon.etc
{
    internal class etc_0110
    {

        static void Main110(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int maxSize = 0;
            for (int i = 0; i < 20; i++)
            {

                maxSize |= 1 << i;
            }


            int num = ReadInt(sr);
            int[] train = new int[num + 1];

            int len = ReadInt(sr);

            for (int i = 0; i < len; i++)
            {

                int order = ReadInt(sr);
                int trainNum;
                int calc;
                int chk;
                switch (order)
                {

                    case 1:
                        // calc 좌석에 사람을 앉힌다
                        // 이미 있다면 넘긴다
                        trainNum = ReadInt(sr);
                        calc = ReadInt(sr) - 1;

                        chk = train[trainNum];
                        chk |= 1 << calc;

                        train[trainNum] = chk;
                        break;

                    case 2:
                        // calc 좌석에 사람을 빼낸다
                        // 없으면 그냥 넘긴다
                        trainNum = ReadInt(sr);
                        calc = ReadInt(sr) - 1;

                        chk = train[trainNum];
                        chk &= ~(1 << calc);

                        train[trainNum] = chk;
                        break;

                    case 3:
                        // +1좌석으로 이동
                        // 이동 전 20번에 앉은 승객은 내보낸다
                        trainNum = ReadInt(sr);

                        chk = train[trainNum];
                        chk = (chk << 1);
                        chk &= maxSize;

                        train[trainNum] = chk;
                        break;

                    case 4:
                        // -1좌석으로 이동
                        // 이동 전 1번 좌석에 앉은 승객은 내보낸다
                        trainNum = ReadInt(sr);

                        chk = train[trainNum];
                        chk = (chk >> 1);
                        chk &= maxSize;

                        train[trainNum] = chk;
                        break;
                }
            }

            sr.Close();
            // 중복되는 경우 확인하기
            bool[] record = new bool[maxSize + 1];
            int ret = 0;
            for (int i = 1; i <= num; i++)
            {

                if (record[train[i]]) continue;
                record[train[i]] = true;
                ret++;

            }

            Console.WriteLine(ret);
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;

            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;

                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
