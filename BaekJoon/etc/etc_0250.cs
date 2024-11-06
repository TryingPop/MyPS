using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 16
이름 : 배성훈
내용 : 시리얼 번호
    문제번호 : 1431번

    정렬 문제다
    새로운 구조체를 만들고 조건대로 정렬을 정의했다
*/

namespace BaekJoon.etc
{
    internal class etc_0250
    {

        static void Main250(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = int.Parse(sr.ReadLine());

            MyString[] myData = new MyString[n];

            for (int i = 0; i < n; i++)
            {

                myData[i].Set(sr.ReadLine());
            }

            sr.Close();

            Array.Sort(myData);

            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                for (int i = 0; i < n; i++)
                {

                    sw.WriteLine(myData[i]);
                }
            }
        }

        struct MyString : IComparable<MyString>
        {

            public string str;
            public int myStringNum;

            public override string ToString()
            {

                return str;
            }

            public void Set(string _str)
            {

                str = _str;
                myStringNum = 0;

                for (int i = 0; i < _str.Length; i++)
                {

                    if (_str[i] >= '0' && _str[i] <= '9')
                    {

                        myStringNum += _str[i] - '0';
                    }
                }
            }

            public int CompareTo(MyString other)
            {

                int ret = str.Length.CompareTo(other.str.Length);
                if (ret != 0) return ret;

                ret = myStringNum.CompareTo(other.myStringNum);
                if (ret != 0) return ret;

                ret = str.CompareTo(other.str);
                return ret;
            }
        }

    }
}
