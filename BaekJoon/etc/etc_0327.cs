using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 22
이름 : 배성훈
내용 : 올림픽
    문제번호 : 8979번

    구현, 정렬 문제다
    조건에 맞게 따로 데이터를 만들고 구현했다
    그리고 랭킹은, 정렬한 뒤 조건에 맞게 전체 세팅 해줬다
    정렬을 메달로만 정렬 했기에 id는 일일히 조사하면서 찾아야한다
*/

namespace BaekJoon.etc
{
    internal class etc_0327
    {

        static void Main327(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);

            int find = ReadInt(sr);

            MyData[] data = new MyData[n];
            for (int i = 0; i < n; i++)
            {

                data[i].Set(ReadInt(sr), ReadInt(sr), ReadInt(sr), ReadInt(sr));
            }

            sr.Close();

            Array.Sort(data);

            int add = 0;
            data[0].rank = 1;
            int ret = 1;
            // 랭킹 세팅
            for (int i = 1; i < n; i++)
            {

                data[i].GetRank(data[i - 1]);
                if (data[i].rank == data[i - 1].rank) add++;
                else
                {

                    data[i].rank += add;
                    add = 0;
                }

                if (data[i].id == find) ret = data[i].rank;
            }

            Console.WriteLine(ret);
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

        struct MyData : IComparable<MyData> 
        {

            public int id;
            public int g;
            public int s;
            public int b;

            public int rank;

            public int CompareTo(MyData _other)
            {

                int ret = _other.g.CompareTo(g);
                if (ret != 0) return ret;

                ret = _other.s.CompareTo(s);
                if (ret != 0) return ret;

                ret = _other.b.CompareTo(b);
                return ret;
            }

            public void GetRank(MyData _other)
            {

                rank = _other.rank;
                rank += this.CompareTo(_other);
            }

            public void Set(int _id, int _g, int _s, int _b)
            {

                id = _id;
                g = _g;
                s = _s;
                b = _b;
            }
        }
    }
}
