using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 18
이름 : 배성훈
내용 : 암호
    문제번호 : 1394번

    수학, 조합론, 문자열 문제다
    n자리가 시작되는 지점만 확인하면 n진법으로 풀 수 있다
*/

namespace BaekJoon.etc
{
    internal class etc_0268
    {

        static void Main268(string[] args)
        {

            int MOD = 900_528;
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()), bufferSize: 65536 * 8);

            string key = sr.ReadLine();
            string password = sr.ReadLine();

            sr.Close();

            int pLen = password.Length;
            int[] digit = new int[pLen];
            digit[0] = 1;
            int ret = 1;
            for (int i = 1; i < digit.Length; i++)
            {

                digit[i] = (digit[i - 1] * key.Length) % MOD;
                ret = (ret + digit[i]) % MOD;
            }

            Dictionary<char, int> cToi = new(key.Length);
            for (int i = 0; i < key.Length; i++)
            {

                cToi[key[i]] = i;
            }
            

            for (int i = 0; i < pLen; i++)
            {

                int cur = cToi[password[i]];
                int add = cur * digit[pLen - i - 1];
                add %= MOD;
                ret = (ret + add) % MOD;
            }

            Console.WriteLine(ret);
        }
    }

#if other
var keys = Console.ReadLine();
var pass = Console.ReadLine();

var keyMap = new Dictionary<char, int>();
for (int i = 1; i <= keys.Length; i++)
    keyMap.Add(keys[i - 1], i);

int tries = 0;
int pow = 1;
for (int i = pass.Length - 1, d = 0; i >= 0; i--, d++)
{
    tries = (tries + (pow * keyMap[pass[i]])) % 900528;
    pow = (pow * keys.Length) % 900528;
}

Console.Write(tries);
#elif other2

internal class Program
{
    static void Main(string[] args)
    {
        string chars = Console.ReadLine().TrimEnd();
        int len = chars.Length;

        Dictionary<char, int> cdic = new Dictionary<char, int>();

        for (int i = 0; i < len; i++)
        {
            cdic.Add(chars[i], i + 1);
        }

        string password = Console.ReadLine().TrimEnd();

        int answer = cdic[password[0]];
        int idx = 1;

        while (idx < password.Length)
        {
            answer = (answer * len + cdic[password[idx]]) % 900528;
            idx++;
        }

        Console.WriteLine(answer);
    }
}
#endif
}
