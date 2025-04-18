using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 10
이름 : 배성훈
내용 : 교차점개수
    문제번호 : 2650번

    CCW 를 써서 풀었다;
    그런데 = 조건에서 잘못해서 여러 번 틀렸다;
*/

namespace BaekJoon.etc
{
    internal class etc_0180
    {

        static void Main180(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);

            (int x, int y)[] pos = new (int x, int y)[n];

            int len = n / 2;
            for (int i = 0; i < len; i++)
            {

                pos[i] = GetPos(ReadInt(sr), ReadInt(sr));
                pos[i + len] = GetPos(ReadInt(sr), ReadInt(sr));
            }

            sr.Close();


            int ret1 = 0;
            int ret2 = -1;

            for (int i = 0; i < len; i++)
            {

                int chk = 0;
                for (int j = 0; j < len; j++)
                {

                    if (i == j) continue;

                    // CCW 로 선분교차 판정
                    int calc1 = CCW(pos[i], pos[i + len], pos[j]);
                    int calc2 = CCW(pos[i], pos[i + len], pos[j + len]);

                    int calc3 = CCW(pos[j], pos[j + len], pos[i]);
                    int calc4 = CCW(pos[j], pos[j + len], pos[i + len]);

                    if (calc1 * calc2 < 0 || calc3 * calc4 < 0) chk++;
                    else if (calc1 * calc2 == 0 && calc3 * calc4 == 0)
                    {

                        if (pos[i].x == pos[i + len].x)
                        {

                            // y가 다른 경우다!
                            int min1 = pos[i].y < pos[i + len].y ? pos[i].y : pos[i + len].y;
                            int max1 = pos[i].y < pos[i + len].y ? pos[i + len].y : pos[i].y;

                            int min2 = pos[j].y < pos[j + len].y ? pos[j].y : pos[j + len].y;
                            int max2 = pos[j].y < pos[j + len].y ? pos[j + len].y : pos[j].y;

                            if (min1 < min2 && min2 < max1 && max1 < max2) chk++;
                            else if (min2 < min1 && min1 < max2 && max2 < max1) chk++;
                        }
                        else
                        {

                            // x가 다른 경우!
                            int min1 = pos[i].x < pos[i + len].x ? pos[i].x : pos[i + len].x;
                            int max1 = pos[i].x < pos[i + len].x ? pos[i + len].x : pos[i].x;

                            int min2 = pos[j].x < pos[j + len].x ? pos[j].x : pos[j + len].x;
                            int max2 = pos[j].x < pos[j + len].x ? pos[j + len].x : pos[j].x;

                            if (min1 < min2 && min2 < max1 && max1 < max2) chk++;
                            else if (min2 < min1 && min1 < max2 && max2 < max1) chk++;
                        }
                    }
                }

                ret1 += chk;
                if (ret2 < chk) ret2 = chk;
            }
            ret1 /= 2;

            Console.WriteLine(ret1);
            Console.WriteLine(ret2);
        }

        static (int x, int y) GetPos(int _type, int _dis)
        {

            switch (_type)
            {

                case 1:
                    return (_dis, 51);

                case 2:
                    return (_dis, 0);

                case 3:
                    return (0, 51 - _dis);

                default:
                    return (51, 51 - _dis);
            }
        }

        static int CCW((int x, int y) pos1, (int x, int y) pos2, (int x, int y) pos3)
        {

            int ret = pos1.x * pos2.y + pos2.x * pos3.y + pos3.x * pos1.y;
            ret -= pos2.x * pos1.y + pos3.x * pos2.y + pos1.x * pos3.y;

            if (ret > 0) ret = 1;
            else if (ret < 0) ret = -1;
            return ret;
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;

            while((c = _sr.Read()) != -1 && c != '\n' && c != ' ')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
