using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

/*
날짜 : 2024. 6. 7
이름 : 배성훈
내용 : 수학숙제
    문제번호 : 2870번

    문자열, 정렬, 파싱 문제다
    다른 사람들 풀이를 보니 string을 써서 해결했다
    수의 범위가 long을 넘어갈 수 있다고 인지 못해서 2번 틀렸다;

    수의 범위에 제한이 없는 BigInteger 구조체를 써서 해결했다
    다른 사람들 풀이를 보니 string으로 해결했다

    앞에 0들이 오는 경우만 처리해주는 코드만 추가하면 string으로 변경된다
*/

namespace BaekJoon.etc
{
    internal class etc_0754
    {

        static void Main754(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            Solve();
            void Solve()
            {

                Init();

                int test = int.Parse(sr.ReadLine());
                List<BigInteger> ret = new(test);
                BigInteger cur;
                while(test-- > 0)
                {

                    string temp = sr.ReadLine();

                    cur = 0;
                    bool isNum = false;
                    for (int i = 0; i < temp.Length; i++)
                    {

                        if (temp[i] < '0' || '9' < temp[i])
                        {

                            if (isNum) 
                            { 
                                
                                isNum = false;
                                ret.Add(cur); 
                                cur = 0;
                            }

                            continue;
                        }

                        isNum = true;
                        cur = cur * 10 + temp[i] - '0';
                    }

                    if (isNum) ret.Add(cur);
                }

                ret.Sort();
                for (int i = 0; i < ret.Count; i++)
                {

                    sw.Write($"{ret[i]}\n");
                }

                sr.Close();
                sw.Close();
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
            }
        }
    }

#if other
int n = int.Parse(Console.ReadLine());
var l = new List<string>();
var s = "";
for (int i = 0; i < n; i++)
{
    var input = Console.ReadLine();
    foreach (var c in input)
    {
        if (char.IsDigit(c))
        {
            bool zero = true;  
            foreach (var z in s)
                if (z != '0')
                {            
                    zero = false;
                    break;
                }
            s = zero ? "" + c : s + c;            
        }
        else if (!string.IsNullOrEmpty(s))
        {
            l.Add(s);
            s = "";
        }
    }    
    if (!string.IsNullOrEmpty(s))
    {
        l.Add(s);
        s = "";
    }
}
l.Sort((i, j) => i.Length == j.Length ? string.Compare(i, j) : i.Length - j.Length);
Console.Write(string.Join("\n", l));
#elif other2
int n = int.Parse(Console.ReadLine());

List<string> numList = new List<string>();

for (int i = 0; i < n; i++)
{
    string str = Console.ReadLine();
    string parseNum = "";
    
    for (int j = 0; j < str.Length; j++)
    {
        if (char.IsDigit(str[j]))
        {
            parseNum += str[j];
        }
        else
        {
            if (parseNum != "")
            {
                if (parseNum.StartsWith("0"))
                {
                    parseNum = StartZero(parseNum);
                }

                numList.Add(parseNum);
                parseNum = "";
            }
        }
    }

    if (!string.IsNullOrEmpty(parseNum))
    {
        if (parseNum.StartsWith("0"))
        {
            parseNum = StartZero(parseNum);
        }
        
        numList.Add(parseNum);
    }
}

numList.Sort((a, b) => 
{
    if (a.Length == b.Length)
        return string.Compare(a, b);
    else
        return a.Length - b.Length;
});

for (int i = 0; i < numList.Count; i++)
{
    Console.WriteLine(numList[i]);
}

string StartZero(string str)
{
    if (str.Length == 1)
    {
        return "0";
    }

    int subIdx = -1;
        
    for (int k = 0; k < str.Length; k++)
    {
        if (str[k] != '0')
        {
            subIdx = k;
            break;
        }
    }

    if (subIdx == -1)
    {
        return "0";
    }
                        
    str =str.Substring(subIdx, str.Length-subIdx);
    
    if (string.IsNullOrEmpty(str))
    {
        str = "0";
    }

    return str;
}
#endif
}
