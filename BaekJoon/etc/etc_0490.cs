using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 9
이름 : 배성훈
내용 : 국영수
    문제번호 : 10825번

    정렬 문제다
    문자열 사전식 정렬을 잘못 해석해서 한 번 틀렸다;
    아스키코드 설명글 있기에 주의하라는 의미로 해석하고 a 와 A를 같은 순서로 뒀다;
    이후에는 조건대로 하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0490
    {

        static void Main490(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()), bufferSize: 65536 * 16);

            int n = int.Parse(sr.ReadLine());
            Student[] s = new Student[n];
            for (int i = 0; i < n; i++)
            {

                string[] temp = sr.ReadLine().Split();
                s[i].Set(temp[0], int.Parse(temp[1]), int.Parse(temp[2]), int.Parse(temp[3]));
            }

            sr.Close();

            Array.Sort(s);

            using (StreamWriter sw = new(new BufferedStream(Console.OpenStandardOutput()), bufferSize: 65536))
            {

                for (int i = 0; i < n; i++)
                {

                    sw.WriteLine(s[i]);
                }
            }
        }

        /*
        static int Comp(string _str1, string _str2)
        {

            int len = Math.Min(_str1.Length, _str2.Length);
            for (int i = 0; i < len; i++)
            {

                int chk1 = GetOrder(_str1[i]);
                int chk2 = GetOrder(_str2[i]);
                if (chk1 == chk2) continue;

                return chk1 < chk2 ? -1 : 1;
            }

            if (_str1.Length == _str2.Length) return 0;
            else return _str1.Length < _str2.Length ? -1 : 1;
        }

        static int GetOrder(char _c)
        {

            if (_c > 'Z') return _c - 'a';
            else return _c - 'A';
        }
        */

        struct Student : IComparable<Student>
        {

            string name;

            // 국영수
            int score1;
            int score2;
            int score3;

            public void Set(string _name, int _score1, int _score2, int _score3)
            {

                name = _name;
                score1 = _score1;
                score2 = _score2;
                score3 = _score3;
            }

            public int CompareTo(Student other)
            {

                int ret = other.score1.CompareTo(score1);
                if (ret != 0) return ret;
                ret = score2.CompareTo(other.score2);
                if (ret != 0) return ret;
                ret = other.score3.CompareTo(score3);
                if (ret != 0) return ret;
                // return Comp(name, other.name);
                return name.CompareTo(other.name);
            }

            public override string ToString()
            {

                return name;
            }
        }
    }

#if other
using System;
using System.Collections.Generic;
using System.IO;

using var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
int n = ScanInt();
var scores = new Data[n];
var buffer = new char[10];
for (int i = 0; i < n; i++)
    scores[i] = new(ScanString(), ScanInt(), ScanInt(), ScanInt());
Array.Sort(scores, new Comparer());

using var sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
foreach (var o in scores)
    sw.WriteLine(o.Name);

string ScanString()
{
    int c, count = 0;
    while ((c = sr.Read()) != ' ')
        buffer[count++] = (char)c;
    return new string(buffer, 0, count);
}

int ScanInt()
{
    int c, ret = 0;
    while ((c = sr.Read()) != '\n' && c != ' ' && c != -1)
    {
        if (c == '\r')
        {
            sr.Read();
            break;
        }
        ret = 10 * ret + (c - '0');
    }
    return ret;
}

struct Data
{
    public string Name;
    public int Language;
    public int English;
    public int Math;

    public Data(string name, int language, int english, int math)
    {
        Name = name;
        Language = language;
        English = english;
        Math = math;
    }
}

class Comparer : IComparer<Data>
{
    public int Compare(Data x, Data y)
    {
        var pastRet =
        (y.Language - x.Language) * 101 * 101 +
        (x.English - y.English) * 101 +
        (y.Math - x.Math);
        if (pastRet != 0)
            return pastRet;
        return x.Name.CompareTo(y.Name);
    }
}
#elif other2

using System;
using System.IO;
using System.Linq;

#nullable disable

public static class Program
{
    private static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var n = Int32.Parse(sr.ReadLine());
        var people = new (string name, int gook, int yeong, int math)[n];

        for (var idx = 0; idx < n; idx++)
        {
            var l = sr.ReadLine().Split(' ');
            people[idx] = (l[0], Int32.Parse(l[1]), Int32.Parse(l[2]), Int32.Parse(l[3]));
        }

        foreach (var v in people
            .OrderByDescending(v => v.gook)
            .ThenBy(v => v.yeong)
            .ThenByDescending(v => v.math)
            .ThenBy(v => v.name))
        {
            sw.WriteLine(v.name);
        }
    }
}

#endif
}
