using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 15
이름 : 배성훈
내용 : RPG 마스터 오명진
    문제번호 : 22941번

    무한 회복되면, 완전 탐색의 경우 언제 죽는지 확인하기 위해 스킬 타이밍을 많으면 용사 공격력 만큼 돌여야한다
    난이도 올라가는 소리가 들려서, 문제를 다시 읽으니 1회에 한해서만 회복된다고 한다

    이상 없이 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0037
    {

        static void Main37(string[] args)
        {

            long[] info = Array.ConvertAll(Console.ReadLine().Split(' '), long.Parse);

            long[] skill = Array.ConvertAll(Console.ReadLine().Split(' '), long.Parse);


            // 이기는 여부 확인하자!
            // 용사 사망턴
            long turn1 = info[0] / info[3];
            if (info[0] % info[3] != 0) turn1++;

            // 마왕 스킬 쓰기 전 의 턴
            long turn2 = (info[2] - skill[0] - 1) / info[1];

            turn2++;
            // 스킬 쓰는 턴의 hp
            long hillHp = (info[2] - info[1] * turn2);

            // 살아야 스킬을 쓴다!
            if (hillHp > 0)
            {

                // 1번에 한해 회복한다;
                hillHp += skill[1];

                turn2 += hillHp / info[1];
                if (hillHp % info[1] != 0) turn2++;
            }

            bool win = turn1 >= turn2;
            if (win) Console.WriteLine("Victory!");
            else Console.WriteLine("gg");
        }
    }
}
