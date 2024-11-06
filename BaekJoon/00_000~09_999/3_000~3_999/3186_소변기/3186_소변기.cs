using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 22
이름 : 배성훈
내용 : 소변기
    문제번호 : 3186번

    조건에 맞는 상황 구현 문제다
    상황은 일정 시간동안 사람이 있어야한다
    사람이 있은 직후 일정시간동안 사람이 없어야한다

    그러면 1회 플러시가 일어난다
    만약 5초동안 사람이 있고 10초동안 사람이 없어야 하는 경우면
        0 ` 6초동안 사람이 있고, 10 ~ 20초동안 사람이 있고,
        이후로 사람이 없으면, 카운트는 1회 30초만 된다
        0 ~ 6초 에 on이 됐으나 10초동안 연속으로 사람이 없어야하는데,
        20초에 온다 중앙에 사람이 와도 그 사람의 카운트는 안된다!
    
    그리고 만약 카운트가 0이면 NIKAD를 외쳐야하는데, 이걸 안해서 1번 틀렸다
*/

namespace BaekJoon.etc
{
    internal class etc_0080
    {

        static void Main80(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int[] info = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
            
            int chk = 0;
            int curTime = 0;
            bool used = false;
            bool isOn = false;
            for (int i = 0; i < info[2]; i++)
            {

                curTime++;
                int cur = sr.Read() - '0';
                // 주어진 시간동안 사람이 소변기 앞에 있는지 확인
                if (cur == 1 && !isOn)
                {

                    chk++;
                    if (chk == info[0])
                    {

                        used = true;
                        isOn = true;
                        chk = 0;
                    }
                }
                // 사람이 가고 일정 시간 지났는지 확인
                else if (cur == 0 && isOn)
                {

                    chk++;
                    if (chk == info[1])
                    {

                        isOn = false;
                        sw.WriteLine(curTime);
                        chk = 0;
                    }
                }
                // 카운트 전에 탈출한 경우
                else chk = 0;
            }

            if (isOn)
            {

                sw.WriteLine(curTime + info[1] - chk);
            }

            else if (!used)
            {

                sw.WriteLine("NIKAD");
            }
            sr.Close();
            sw.Close();
        }
    }
}
