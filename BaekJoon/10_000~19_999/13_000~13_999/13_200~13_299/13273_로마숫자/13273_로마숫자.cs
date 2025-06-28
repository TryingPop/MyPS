using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 9
이름 : 배성훈
내용 : 로마숫자
    문제번호 : 13273번

    아라비아 숫자를 로마 숫자로,
    로마 숫자를 아라비아 숫자로 바꾸는 문제다

    하드코딩으로 해결했다;
*/

namespace BaekJoon.etc
{
    internal class etc_0170
    {

        static void Main170(string[] args)
        {

            char[] c = new char[7] { 'I', 'V', 'X', 'L', 'C', 'D', 'M' };
            int[] n = new int[7] { 1, 5, 10, 50, 100, 500, 1000 };

            int[] cTon = new int[26];
            for (int i = 0; i < 7; i++)
            {

                cTon[c[i] - 'A'] = n[i];
            }

            char[] ret = new char[16];

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
            int test = int.Parse(sr.ReadLine());
            int[] nums = new int[4];

            while(test-- > 0)
            {

                int r = 0;
                string str = sr.ReadLine();
                if (str[0] >= '0' && str[0] <= '9')
                {

                    int num = int.Parse(str);

                    nums[0] = num / 1000;
                    num %= 1000;
                    nums[1] = num / 100;
                    num %= 100;
                    nums[2] = num / 10;

                    nums[3] = num % 10;

                    for (int i = 0; i < 4; i++)
                    {

                        if (nums[i] == 0)
                        {

                            for (int j = 0; j < 4; j++)
                            {

                                ret[4 * i + j] = '#';
                            }

                            continue;
                        }

                        if (nums[i] < 4)
                        {

                            for (int j = 0; j < nums[i]; j++)
                            {

                                ret[4 * i + j] = c[6 - 2 * i];
                            }

                            for (int j = nums[i]; j < 4; j++)
                            {

                                ret[4 * i + j] = '#';
                            }
                        }
                        else if (nums[i] == 4)
                        {

                            ret[4 * i] = c[6 - 2 * i];
                            ret[4 * i + 1] = c[7 - 2 * i];
                            for (int j = 2; j < 4; j++)
                            {

                                ret[4 * i + j] = '#';
                            }
                        }
                        else if (nums[i] < 9)
                        {

                            ret[4 * i] = c[7 - 2 * i];
                         
                            for (int j = 1; j < nums[i] - 4; j++)
                            {

                                ret[4 * i + j] = c[6 - 2 * i];
                            }

                            for (int j = nums[i] - 4; j < 4; j++)
                            {

                                ret[4 * i + j] = '#';
                            }
                        }
                        else
                        {

                            ret[4 * i] = c[6 - 2 * i];
                            ret[4 * i + 1] = c[8 - 2 * i];
                            for (int j = 2; j < 4; j++)
                            {

                                ret[4 * i + j] = '#';
                            }
                        }
                    }

                    for (int i = 0; i < ret.Length; i++)
                    {

                        if (ret[i] == '#') continue;
                        sw.Write(ret[i]);
                    }
                    sw.Write('\n');
                }
                else
                {

                    
                    for (int i = 0; i < str.Length - 1; i++)
                    {

                        int cur = cTon[str[i] - 'A'];
                        int next = cTon[str[i + 1] - 'A'];

                        if (cur < next)
                        {

                            r -= cur;
                        }
                        else r += cur;
                    }

                    r += cTon[str[^1] - 'A'];

                    sw.WriteLine(r);
                }
            }

            sr.Close();
            sw.Close();
        }
    }

#if other
var reader = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
var t = int.Parse(reader.ReadLine());

using (var writer = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
while (t-- > 0)
{
    var input = reader.ReadLine();
    if (int.TryParse(input, out int arabian))
        writer.WriteLine(ArabianToRoman(arabian));
    else
        writer.WriteLine(RomanToArabian(input));
}

int RomanToArabian(string value)
{
    int arabian = 0;
    var map = (char c) => c switch {
        'M' => 1000,
        'D' => 500, 'C' => 100,
        'L' => 50, 'X' => 10,
        'V' => 5, 'I' => 1
    };

    for (int i = 0; i < value.Length; i++)
    {
        var cur = map(value[i]);
        var next = i + 1 == value.Length ? 0 : map(value[i + 1]);

        if (cur >= next)
            arabian += cur;
        else
        {
            arabian += next - cur;
            i++;
        }
    }

    return arabian;
}

string ArabianToRoman(int value)
{
    var map = (int d, int n) => d switch {
        0 => new string[] { "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX" }[n],
        1 => new string[] { "", "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC" }[n],
        2 => new string[] { "", "C", "CC", "CCC", "CD", "D", "DC", "DCC", "DCCC", "CM" }[n],
        3 => new string[] { "", "M", "MM", "MMM" }[n],
    };

    string roman = "";
    var vs = value.ToString();
    for (int i = 0; i < vs.Length; i++)
        roman += map(vs.Length - i - 1, vs[i] - '0');

    return roman;
}

class Reader{StreamReader R;public Reader()=>R=new(new BufferedStream(Console.OpenStandardInput()));
public int NextInt(){var(v,n,r)=(0,false,false);while(true){int c=R.Read();if((r,c)==(false,'-')){(n,r)=(true,true);continue;}if('0'<=c&&c<='9'){(v,r)=(v*10+(c-'0'),true);continue;}if(r==true)break;}return n?-v:v;}
}

#elif other2

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#nullable disable

public class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var d = new Dictionary<string, int>
        {
            { "MMMM", 4000 },
            { "MMM", 3000 },
            { "MM", 2000 },
            { "M", 1000 },
            { "CM", 900 },
            { "DCCC", 800 },
            { "DCC", 700 },
            { "DC", 600 },
            { "D", 500 },
            { "CD", 400 },
            { "CCC", 300 },
            { "CC", 200 },
            { "C", 100 },
            { "XC", 90 },
            { "LXXX", 80 },
            { "LXX", 70 },
            { "LX", 60 },
            { "L", 50 },
            { "XL", 40 },
            { "XXX", 30 },
            { "XX", 20 },
            { "X", 10 },
            { "IX", 9 },
            { "VIII", 8 },
            { "VII", 7 },
            { "VI", 6 },
            { "V", 5 },
            { "IV", 4 },
            { "III", 3 },
            { "II", 2 },
            { "I", 1 },
            { "", 0 },
        };

        var revd = d.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);

        var n = Int32.Parse(sr.ReadLine());
        while (n-- > 0)
        {
            var s = sr.ReadLine();
            if (s.Any(ch => Char.IsDigit(ch)))
            {
                var v = Int32.Parse(s);

                sw.WriteLine(revd[v / 1000 * 1000] + revd[((v / 100) % 10) * 100] + revd[((v / 10) % 10) * 10] + revd[v % 10]);
            }
            else
            {
                var sub = String.Empty;
                var sum = 0;

                for (var idx = 0; idx < s.Length; idx++)
                {
                    if (!d.ContainsKey(sub + s[idx]))
                    {
                        sum += d[sub];
                        sub = String.Empty;
                    }

                    sub = sub + s[idx];
                }

                sum += d[sub];

                sw.WriteLine(sum);
            }
        }
    }
}
#endif
}
