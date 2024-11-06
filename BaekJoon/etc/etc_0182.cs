using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 11
이름 : 배성훈
내용 : 배틀로얄
    문제번호 : 19639번

    성립하는 경우 아무거나 하나만 출력하면 되기에
    동형인 풀이로 해결했다

    택한 방법은 정렬하고 그리디로 풀었다

    적을 잡아도 체력이 남아있다면 적을 먼저 잡는다
    그리고 못잡는다면 물약을 먹는다

    그리고 다시 적을 잡을 수 있는지 확인한다
    적의 전체 체력이 반 이하이고, 최대 회복이 전체 체력의 반 이하이므로
    풀피를 초과하여 물약을 먹지 않는다

    이를 물약이나 몬스터를 다 잡거나 둘 중 하나가 완료될 때까지 계속한다
    그리고 남은 몬스터나 물약을 먹기 시작한다(여기서는 초과 여부가 중요치 않기에 그냥 계속해서 더한다)

    다 잡고, 다 먹고 나서 체력이 0 이하면 못이기는 경우고,
    1 이상이면 승리 가능한 경우다
*/

namespace BaekJoon.etc
{
    internal class etc_0182
    {

        static void Main182(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int x = ReadInt(sr);
            int y = ReadInt(sr);
            int m = ReadInt(sr);

            MyData[] enemy = new MyData[x];
            for (int i = 0; i < x; i++)
            {

                enemy[i].Set(ReadInt(sr), i + 1);
            }

            MyData[] potion = new MyData[y];
            for (int i = 0; i < y; i++)
            {

                potion[i].Set(ReadInt(sr), i + 1);
            }

            sr.Close();

            Array.Sort(enemy);
            Array.Sort(potion);

            int ePtr = 0;
            int pPtr = 0;

            StringBuilder sb = new StringBuilder(enemy.Length * 2 + potion.Length);

            int curHp = m;

            while (ePtr < x && pPtr < y)
            {

                if (curHp - enemy[ePtr].val > 0)
                {

                    // 몬스터를 잡을 수 있으면 최우선으로 몬스터 사냥
                    sb.Append($"{-enemy[ePtr].idx}\n");
                    curHp -= enemy[ePtr].val;
                    ePtr++;
                }
                else
                {

                    // 체력 부족으로 몬스터를 못잡는 것이므로 체력을 채운다
                    sb.Append($"{potion[pPtr].idx}\n");
                    curHp += potion[pPtr].val;
                    pPtr++;
                }
            }

            while (ePtr < x)
            {

                // 물약을 다 먹은 경우 몬스터만 사냥한다
                sb.Append($"{-enemy[ePtr].idx}\n");
                curHp -= enemy[ePtr].val;
                ePtr++;
            }

            while(pPtr < y)
            {

                // 몬스터 다잡았으니 물약 먹는다
                sb.Append($"{potion[pPtr].idx}\n");
                curHp += potion[pPtr].val;
                pPtr++;
            }

            if (curHp <= 0) 
            { 
                
                // hp <= 0이면,
                // 못 이기는 경우다
                sb.Clear();
                sb.Append(0);
            }

            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            sw.Write(sb);
            sw.Close();
        }

        struct MyData : IComparable<MyData>
        {

            public int val;
            public int idx;

            public void Set(int _val, int _idx)
            {

                val = _val;
                idx = _idx;
            }

            public int CompareTo(MyData other)
            {

                return val.CompareTo(other.val);
            }
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
