using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 16
이름 : 배성훈
내용 : 단어 수학
    문제번호 : 1339번

    그리디 알고리즘 문제다
    아이디어는 다음과 같다
    알파벳에 수를 주는데 우선순위를 부여해 풀었다

    가장 큰 자리수에 오는 경우를 최우선으로하고,
    다음으로 가장 큰 자리수에 많이 나온 순서,

    이래도 같다면 다음 자리수 중에서 
    가장 큰 자리수에 오는 것을 최우선으로 부여했다

    마지막까지 같다면 그냥 임의로 부여하면 된다
    우선순위를 부여하는 방법을 숫자로 보니 
    앞자리를 1로 하는 자리수를 더해주면 된다

    예를들어 ABBB인 경우 A는 1000, B는 111

    이걸 누적하면 숫자를 부여하기 전이고,
    이를 내림차순 정렬하면 위의 우선순위와 완벽히 일치한다
    그리고 큰 수부터 9, 8, ... 0까지 부여해 계산하니 64ms에 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0540
    {

        static void Main540(string[] args)
        {

            StreamReader sr = new(new BufferedStream(Console.OpenStandardInput()));

            Solve();
            sr.Close();

            void Solve()
            {

                int n = int.Parse(sr.ReadLine());
                int[] alphabet = new int[26];
                int[] nums = new int[10];

                for (int i = 0; i < n; i++)
                {

                    string str = sr.ReadLine();
                    int cur = 1;
                    for (int j = str.Length - 1; j >= 0; j--)
                    {

                        int c = str[j] - 'A';
                        alphabet[c] += cur;
                        cur *= 10;
                    }
                }

                int idx = 0;
                for (int i = 0; i < 26; i++)
                {

                    if (alphabet[i] == 0) continue;
                    nums[idx++] = alphabet[i];
                }

                Array.Sort(nums, (x, y) => y.CompareTo(x));

                int ret = 0;
                for (int i = 0; i < 10; i++)
                {

                    ret += nums[i] * (9 - i);
                }

                Console.WriteLine(ret);
            }
        }



    }
#if other
using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
class Baekjoon{
    static void Main(String[] args){ 
        int num = int.Parse(Console.ReadLine());
        char[,] arr = new char[10,10];
        int[] len = new int[10];
        int[] alpha = new int[26];

        for(int i = 0 ; i<num ; i++){
            string n = Console.ReadLine();
            for(int j = 0 ; j<n.Length ; j++){
                arr[i,j] = n[j];
            }
            len[i]= n.Length;
        }
        int cal;
        for(int i = 0 ; i<num ; i++){
            cal =1;
            for(int j = len[i]-1 ; j>=0 ; j--){
                alpha[arr[i,j]-'A']+=cal;
                cal*=10;
            } 
        }

        Array.Sort(alpha);
        Array.Reverse(alpha);
        int result = 0;

        for(int i = 0 ; i<10 ; i++){
            result += alpha[i]*(9-i);
        }

        Console.WriteLine(result);
        //자릿수 나오면 100*A+10*B+1*C 생각
    }   
}
#elif other2
using System;
using System.Collections.Generic;
using System.Linq;

// CapitalLetters for class names and methods, camelCase for variable names.
// Write your code clearly enough so that it doesn't need to be commented, or at least, so that it rarely needs to be commented.

namespace Testpad
{
    public class BaekJoon1339
    {
        static Dictionary<char, long> charValue = new Dictionary<char, long>(); // 해당 문자의 기대값

        // 그리디
        static void Main()
        {
            int n = int.Parse(Console.ReadLine()); // 단어의 수

            for (int i = 0; i < n; i++)
            {
                char[] input = Console.ReadLine().ToCharArray();

                // 주어진 단어를 나눠서 각 문자의 기대값을 갱신
                for (int j = input.Length - 1, multipler = 1; j >= 0; j--, multipler *= 10)
                {
                    if (charValue.ContainsKey(input[j]) == false)
                    {
                        charValue.Add(input[j], multipler);
                    }
                    else
                    {
                        charValue[input[j]] += multipler;
                    }
                }
            }

            // 기대값이 큰 순으로 정렬하고, 기대값이 높은 쪽에 큰 숫자를 할당해 결과를 구함
            long result = 0;
            var temp = charValue.OrderByDescending(s1 => s1.Value);
            int currentNumber = 9;

            foreach (var item in temp)
            {
                result += item.Value * currentNumber;
                currentNumber--;
            }

            Console.Write(result);
        }
    }
}
#elif other3
var n = int.Parse(Console.ReadLine()!);
var lines = new string[n];
for (int i = 0; i < n; i++)
{
    lines[i] = Console.ReadLine()!;
}

var dict = new Dictionary<char, int>();
foreach (var line in lines)
{
    for (int i = 0; i < line.Length; i++)
    {
        var c = line[i];
        if (!dict.ContainsKey(c))
            dict.Add(c, 0);
        dict[c] += Pow10(line.Length - 1 - i);
    }
}
var values = dict.Values.ToArray();
Array.Sort(values);
Array.Reverse(values);
var sum = 0;
for (int i = 0; i < values.Length; i++)
{
    sum += values[i] * (9 - i);
}
Console.Write(sum);

static int Pow10(int y)
{
    var ret = 1;
    for (int i = 0; i < y; i++)
    {
        ret *= 10;
    }
    return ret;
}
#endif
}
