using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 16
이름 : 배성훈
내용 : 소가 길을 건너간 이유
    문제번호 : 14464번

    그리디와 브루트 포스 문제다

    처음에는 소를 시작 시간을 최우선으로 정렬하고, 
    끝 시간을 차선으로 정렬하면 되지 않을까 했다
    그리고 닭을 오름차순으로 정렬해서 비교하면 될거 같았다

    그런데 해당 경우는 4%를 못넘겼다
    질문 게시판에 반례가 있었다
        3 3
        2
        5
        8
        1 7
        4 6
        3 9

    해당 방법으로 접근하면 소는 1 - 7, 3 - 9, 4 - 6으로 정렬되고
    2, 5 순으로 배정되다가 4 - 6에 닭이 배정안되어서 틀린다

    몇 번을 시행착오를 겪고 이후에는 간격을 적은 것부터 하면 되지 않을까 생각했다
    해당 경우는 22%쯤?에서 막혔다
        2 2
        4
        5
        3 5
        0 4
    실제로 반례도 금방 나온다

    그리고 힌트를 보게되었고, 우선순위 큐가 있기에 어떻게든 엮어보려고
    이리저리 시도하다가 여러 번 틀렸다

    그리고 다시 문제 분석을 했는데,
    닭을 오름차순으로 정렬해서 하려고 했다
    (소는 새로운 자료구조를 만들어야 하기에 새로운 정렬 기준을 만드는게 깔끔해보이기 때문이다)
    소를 닭의 형태에 맞는 정렬 방법을 찾는게 주된 아이디어라고 느꼈다

    그래서 문제를 다시 분석하게 되었고
    닭을 오름차순으로 했을 때, 소를 끝지점을 최우선 순위로 정렬 하고
    끝지점이 같다면 시작 지점을 차선으로 선택해야 최선임을 알게되었다
    이유는 닭이 오름차순이기에 먼저 끝나는 것들 부터 확인해야 최대한 배정할 수 있기 때문이다(그리디!)

    만약 닭을 내림차순으로 한다면 위 경우의 반대로 보기에
    시작지점을 최우선으로 끝지점을 차선으로 내림차순 정렬을 해야한다
    (내림 차순 한게 속도가 더 빠르다.;?)

    그리고 풀이 로직이 잡혔으니 각 소에 대해 닭을 모두 조사하는 방법으로 탐색을 했다
*/
namespace BaekJoon.etc
{
    internal class etc_0246
    {

        static void Main246(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int c = ReadInt(sr);
            int n = ReadInt(sr);

            int[] chicken = new int[c];
            bool[] useChick = new bool[c];
            for(int i = 0; i < c; i++)
            {

                chicken[i] = ReadInt(sr);
            }

            Cow[] cow = new Cow[n];
            for (int i = 0; i < n; i++)
            {

                int s = ReadInt(sr);
                int e = ReadInt(sr);

                cow[i].Set(s, e);
            }

            sr.Close();

            // Array.Sort(chicken);
            // Array.Sort(cow);

            Array.Sort(chicken, (x, y) => y.CompareTo(x));
            Array.Sort(cow);

            int ret = 0;

            for (int i = 0; i < n; i++)
            {

                for (int j = 0; j < c; j++)
                {

                    if (useChick[j] || cow[i].ChkInvalidTime(chicken[j])) continue;
                    useChick[j] = true;
                    ret++;
                    break;
                }
            }

            Console.WriteLine(ret);
        }

        public struct Cow : IComparable<Cow>
        {

            public int s;
            public int e;

            public bool ChkInvalidTime(int _t)
            {

                return !(s <= _t && _t <= e);
            }

            int IComparable<Cow>.CompareTo(Cow other)
            {

                // int ret = e.CompareTo(other.e);
                // if (ret == 0) s.CompareTo(other.s);

                int ret = other.s.CompareTo(s);
                if (ret == 0) other.e.CompareTo(e);
                return ret;
            }

            public void Set(int _s, int _e)
            {

                s = _s;
                e = _e;
            }
        }


        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;
            while ((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
