using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 13
이름 : 배성훈
내용 : 김식당
    문제번호 : 14612번

    구현, 정렬, 시뮬레이션 문제다
    입력값이 이상한거 같다

    중간에 입력받고 수시로 정렬하기에 
    List 자료구조를 이용했다

    배열로 해볼려고 했으나, 여러번 틀렸다
    배열 부분은 나중에 따로 해봐야겠다
*/

namespace BaekJoon.etc
{
    internal class etc_0220
    {

        static void Main220(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int order = ReadInt(sr);

            int tableLen = ReadInt(sr);
            List<MyTable> table = new(tableLen);

            for (int i = 0; i < order; i++)
            {

                string[] temp = sr.ReadLine().Trim().Split(' ');

                if (temp[0][0] == 'o')
                {

                    int id = int.Parse(temp[1]);
                    int time = int.Parse(temp[2]);

                    var tempTable = new MyTable();
                    tempTable.Set(id, time);
                    table.Add(tempTable);
                }
                else if (temp[0][0] == 's')
                {

                    table.Sort();
                }
                else
                {

                    int del = int.Parse(temp[1]);
                    for (int j = 0; j < table.Count; j++)
                    {

                        if (table[j].id == del)
                        {

                            table.RemoveAt(j);
                            break;
                        }
                    }
                }

                if (table.Count > 0)
                {

                    for (int j = 0; j < table.Count; j++)
                    {

                        sw.Write(table[j].id);
                        sw.Write(' ');
                    }
                }
                else sw.Write("sleep");

                sw.Write('\n');
            }

            sr.Close();
            sw.Close();
        }

        struct MyTable : IComparable<MyTable>
        {

            public int id;
            public int time;

            public void Set(int _id, int _time)
            {

                id = _id;
                time = _time;
            }

            public int CompareTo(MyTable other)
            {

                int ret = time.CompareTo(other.time);
                if (ret == 0) ret = id.CompareTo(other.id);
                return ret;
            }
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;

            while((c = _sr.Read()) != -1)
            {

                if (c == '\r') continue;
                if (c == '\n' || c == ' ') break;

                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
