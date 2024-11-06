using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 20
이름 : 배성훈
내용 : 타일 채우기 2
    문제번호 : 13976번

    점화식 f(n + 2) - 4 f(n) + f(n - 2) = 0
    까지는 나왔었는데, 행렬식은 못찾았다;

    그래서 다른 사람 풀이를 보니, 당황했다;
    그냥 밑에 이어 붙여 2 * 2 행렬식으로 맞춘 것이었다..
    
    | f(n + 2) |   =  | 4   -1 |  | f(n)      |
    | f(n)     |      | 1   0  |  | f(n - 2)  |

    해당 행렬식을 구하니 이상없이 구해졌다.
    다만 큰수의 제곱 계산식이 이게 맞나 의문을 가져 잠깐 고민타임을 가졌다

    해당 코드를 분석하니, 
        n = 2^p_i * a_i + 2^p_i-1 * a_i-1 + ... + 1 * a_0
    의 형태로 변환해서 뒤에께 a_0가 1이면 결과에 곱해주고
    곱셈 값이 2라면
    2로 나눠 (n/2) = 2^(p_i - 1) * a_i + .... + 1 * a_1
    형태로 만들고 연산자는 2^2으로 만드는 걸로 
    홀수 부분을 끝 부분을 결과에 미리 곱한 것임을 상기했다

    처음에는 2, 4, 6은 따로 처리하려고 했다 6 이상만 하려고 했다
    점화식 나오게 된게 6이상의 수열에 대해서 구했기 때문이다;
    
    그런데
        a_2 = 3, a_0 = 1로 놓고 하니 이상없이 되어
    처음부터 되게 수정했다
*/

namespace BaekJoon._48
{
    internal class _48_05
    {

        static long MOD = 1_000_000_007;
        static void Main5(string[] args)
        {

            long n = long.Parse(Console.ReadLine());

            if ((n & 1) == 1) 
            { 
                
                Console.WriteLine(0);
                return;
            }

            /*
            if (n == 2)
            {

                Console.WriteLine(3);
                return;
            }
            else if (n == 4)
            {

                Console.WriteLine(11);
                return;
            }
            else if ( n == 6)
            {

                Console.WriteLine(41);
                return
            }
            */
            // long exp = (n / 2) - 2;
            long exp = (n / 2);

            (long p11, long p12, long p21, long p22) calc = (4, -1, 1, 0);
            (long p11, long p12, long p21, long p22) e = (1, 0, 0, 1);

            while (exp > 0)
            {

                if ((exp & 1) == 1)
                {

                    MulMatrix(ref e, calc);
                }

                exp /= 2;
                MulMatrix(ref calc, calc);
            }


            long ret = (3 * e.p21 + e.p22) % MOD;
            // long ret = (41 * e.p21 + 11 * e.p22) % MOD;
            ret = ret < 0 ? ret + MOD : ret;

            Console.WriteLine(ret);
        }
        
        static void MulMatrix(ref (long p11, long p12, long p21, long p22) _l, (long p11, long p12, long p21, long p22) _r)
        {

            (long p11, long p12, long p21, long p22) ret;

            ret.p11 = (_l.p11 * _r.p11) % MOD;
            ret.p11 += (_l.p12 * _r.p21) % MOD;
            ret.p11 %= MOD;

            ret.p12 = (_l.p11 * _r.p12) % MOD;
            ret.p12 += (_l.p12 * _r.p22) % MOD;
            ret.p12 %= MOD;

            ret.p21 = (_l.p21 * _r.p11) % MOD;
            ret.p21 += (_l.p22 * _r.p21) % MOD;
            ret.p21 %= MOD;

            ret.p22 = (_l.p21 * _r.p12) % MOD;
            ret.p22 += (_l.p22 * _r.p22) % MOD;
            ret.p22 %= MOD;

            _l = ret;
            return;
        }
    }

#if other
using System.IO;
using System.Text;
using System;

class Programs
{
    static StreamReader sr = new StreamReader(Console.OpenStandardInput(), Encoding.Default);
    static StreamWriter sw = new StreamWriter(Console.OpenStandardOutput(), Encoding.Default);
    static long n;
    static long mod = 1000000007;
    static info Get(info a, long b)
    {
        if (b == 1)
        {
            return a;
        }
        info temp = Get(a, b / 2);
        info mul = temp * temp;
        if (b % 2 == 0)
        {
            return mul;
        }

        return mul * a;
    }

    class info
    {
        public long a, b, c, d;
        public info(long a = 0, long b = 0, long c = 0,long d = 0)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
        }
        public static info operator *(info A, info B)
        {
            //long aa = (A.a * B.a+ A.b * B.c) % mod;
            //long bb = (A.a * B.b+ A.b * B.d) % mod;
            //long cc = (A.c * B.a+ A.d * B.c) % mod;
            //long dd = (A.c * B.b+ A.d * B.d) % mod;
            long aa = ((A.a * B.a) % mod + (A.b * B.c) % mod) % mod;
            long bb = ((A.a * B.b) % mod + (A.b * B.d) % mod) % mod;
            long cc = ((A.c * B.a) % mod + (A.d * B.c) % mod) % mod;
            long dd = ((A.c * B.b) % mod + (A.d * B.d) % mod) % mod;
            aa=(aa+mod)% mod;
            bb = (bb + mod) % mod;
            cc = (cc + mod) % mod;
            dd = (dd + mod) % mod;
            info c = new info(aa,bb,cc,dd);
            return c;
        }
    }
    static void Main(String[] args)
    {
        n = long.Parse(sr.ReadLine());
        //f(n)=4*f(n-2)-f(n-4);
        //f(n-2)=f(n-2);
        //f(n-2)를 그냥f(n-2)로 
        //f(n-2)는 x고 f(n-4)는 y라면
        //f(n)=4*x-y;
        //f(n-2)=x-0*y;
        /*
        f(n)= {4,-1}{x} {4,-1}{f(n-2)}     
        f(n-2)={1, 0}{y} {1, 0}{f(n-4)}
        가 나오고
        2가 3인지 확인해보자
        { 4,-1      11 3
          */
        // 흠 전부 이해되진 않는다. ㅠㅠ
        if (n % 2 == 1)
        {
            //홀수면 0
            sw.Write(0);
        }
        else
        {
            info ab = new info(4,-1,1,0);
            ab = Get(ab, n / 2);
           long answer = (ab.c * 3) + ab.d;
            //일단 해당 함수 두개의 행렬변환으로 관계식 이 나왔고, f(n-2),f(n-4) 를 구했어  n이 4인경우 3,1
            //n값이 크기 때문에 행렬의 거듭제곱으로 구해야함 구한뒤 행렬[3,1]과 곱해주면 값이 나옴..
            //거듭제곱은 &연산자를 활용 특정 변수 a&1이라면 a의 마지막 비트와 1을 비교한다.
            //a=/2를 한뒤 a의 마지막 비트는 사라진다. 직접해보면 앎 1000 8임, 8/2는 2진수로 100임 마지막이 사라짐.
            sw.Write(answer % mod);
        }
        sw.Dispose();
    }
}

#elif other2
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    public class Program
    {
        public static int Main(string[] args)
        {
            using var io = new IoInstance();
                var input = IO.GetLong();

                var result = Solve(input);
                result.Dump();
            return 0;
        }

        public static long Solve(long N)
        {
            if (N % 2 == 1)
            {
                return 0;
            }
            else if (N == 2)
            {
                return 3;
            }
            else if (N == 4)
            {
                return 11;
            }

            int mod = 1_000_000_007;
            long n = ((N - 6) / 2 + 1);

            var seedMatrix = new Matrix(2, 2, 4, -1, 1, 0);
            var left = seedMatrix.Pow(n, mod);

            var right = new Matrix(2, 1, 11, 3);

            Matrix result = left * right;
            return (result[0][0] + mod + mod) % mod;
        }
    }

    public static partial class Ex
    {
    }

    public class IoInstance : IDisposable
    {
        public void Dispose()
        {
#if !DEBUG
            IO.Dispose();
#endif
        }
    }

    public static class IO
    {

#if !DEBUG
        static StreamReader _inputReader;
        static StringBuilder _outputBuffer = new();

        static IO()
        {
            _inputReader = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
        }
#endif

        public static string GetLine()
        {
#if DEBUG
            return _input[_readInputCount++];
#else
            return _inputReader.ReadLine();
#endif
        }

        public static string GetString()
            => GetLine();

        public static string[] GetStringList()
            => GetLine().Split(' ');

        public static (string, string) GetStringTuple2()
        {
            var arr = GetStringList();
            return (arr[0], arr[1]);
        }

        public static (string, string, string) GetStringTuple3()
        {
            var arr = GetStringList();
            return (arr[0], arr[1], arr[2]);
        }

        public static (string, string, string, string) GetStringTuple4()
        {
            var arr = GetStringList();
            return (arr[0], arr[1], arr[2], arr[3]);
        }

        public static int[] GetIntList()
            => GetLine().Split(' ').Where(x => x.Length > 0).Select(x => x.ToInt()).ToArray();

        public static (int, int) GetIntTuple2()
        {
            var arr = GetLine().Split(' ');
            return (arr[0].ToInt(), arr[1].ToInt());
        }

        public static (int, int, int) GetIntTuple3()
        {
            var arr = GetLine().Split(' ');
            return (arr[0].ToInt(), arr[1].ToInt(), arr[2].ToInt());
        }

        public static (int, int, int, int) GetIntTuple4()
        {
            var arr = GetLine().Split(' ');
            return (arr[0].ToInt(), arr[1].ToInt(), arr[2].ToInt(), arr[3].ToInt());
        }

        public static int GetInt()
            => GetLine().ToInt();

        public static long[] GetLongList()
            => GetLine().Split(' ').Where(x => x.Length > 0).Select(x => x.ToLong()).ToArray();

        public static (long, long) GetLongTuple2()
        {
            var arr = GetLine().Split(' ');
            return (arr[0].ToLong(), arr[1].ToLong());
        }

        public static (long, long, long) GetLongTuple3()
        {
            var arr = GetLine().Split(' ');
            return (arr[0].ToLong(), arr[1].ToLong(), arr[2].ToLong());
        }

        public static (long, long, long, long) GetLongTuple4()
        {
            var arr = GetLine().Split(' ');
            return (arr[0].ToLong(), arr[1].ToLong(), arr[2].ToLong(), arr[3].ToLong());
        }

        public static long GetLong()
            => GetLine().ToLong();

        public static T Dump<T>(this T obj, string format = "")
        {
            var text = string.IsNullOrEmpty(format) ? $"{obj}" : string.Format(format, obj);
#if !DEBUG
            _outputBuffer.AppendLine(text);
#endif
            return obj;
        }

        public static List<T> Dump<T>(this List<T> list)
        {
#if !DEBUG
            _outputBuffer.AppendLine(list.StringJoin(" "));
#endif
            return list;
        }

#if !DEBUG
        public static void Dispose()
        {
            _inputReader.Close();
            using var streamWriter = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
            streamWriter.Write(_outputBuffer.ToString());
        }
#endif
    }

    public enum LoopResult
    {
        Void,
        Break,
        Continue,
    }

    public static class Extensions
    {
        public static IEnumerable<long> GetPrimeList(int maximum)
        {
            if (maximum < 2)
                yield break;

            var isPrime = Enumerable.Range(0, maximum + 1).Select(x => false).ToList();

            yield return 2;
            for (var prime = 3; prime <= maximum; prime += 2)
            {
                if (isPrime[prime] == true)
                    continue;
                yield return prime;
                for (var i = prime; i <= maximum; i += prime)
                    isPrime[i] = true;
            }
        }

        public static string With(this string format, params object[] obj)
        {
            return string.Format(format, obj);
        }

        public static string StringJoin<T>(this IEnumerable<T> list, string separator = " ")
        {
            return string.Join(separator, list);
        }

        public static string Left(this string value, int length = 1)
        {
            if (value.Length < length)
                return value;
            return value.Substring(0, length);
        }

        public static string Right(this string value, int length = 1)
        {
            if (value.Length < length)
                return value;
            return value.Substring(value.Length - length, length);
        }


        public static int ToInt(this string str)
        {
            return int.Parse(str);
        }

        public static long ToLong(this string str)
        {
            return long.Parse(str);
        }


        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
                action(item);
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T, int> action)
        {
            var index = 0;
            foreach (var item in source)
                action(item, index++);
        }

        public static void ForEach1<T>(this IEnumerable<T> source, Action<T, int> action)
        {
            var index = 1;
            foreach (var item in source)
                action(item, index++);
        }

        public static void ForEach<T>(this IEnumerable<T> source, Func<T, LoopResult> action)
        {
            foreach (var item in source)
            {
                var result = action(item);
                switch (result)
                {
                    case LoopResult.Break:
                        break;
                    case LoopResult.Continue:
                        continue;
                }
            }
        }

        public static void ForEach<T>(this IEnumerable<T> source, Func<T, int, LoopResult> action)
        {
            var index = 0;
            foreach (var item in source)
            {
                var result = action(item, index++);
                switch (result)
                {
                    case LoopResult.Break:
                        break;
                    case LoopResult.Continue:
                        continue;
                }
            }
        }

        public static void ForEach1<T>(this IEnumerable<T> source, Func<T, int, LoopResult> action)
        {
            var index = 1;
            foreach (var item in source)
            {
                var result = action(item, index++);
                switch (result)
                {
                    case LoopResult.Break:
                        break;
                    case LoopResult.Continue:
                        continue;
                }
            }
        }

        public static bool ForEachBool<T>(this IEnumerable<T> source, Func<T, bool> func)
        {
            var result = true;
            foreach (var item in source)
            {
                if (!func(item))
                    result = false;
            }
            return result;
        }

        public static bool ForEachBool<T>(this IEnumerable<T> source, Func<T, int, bool> func)
        {
            var result = true;
            var index = 0;
            foreach (var item in source)
            {
                if (!func(item, index++))
                    result = false;
            }
            return result;
        }

        public static void For(this int count, Action<int> action)
        {
            for (var i = 0; i < count; i++)
            {
                action(i);
            }
        }

        public static void For1(this int count, Action<int> action)
        {
            for (var i = 1; i <= count; i++)
            {
                action(i);
            }
        }

        public static void For(this int count, Func<int, LoopResult> action)
        {
            for (var i = 0; i < count; i++)
            {
                var result = action(i);
                switch (result)
                {
                    case LoopResult.Break:
                        break;
                    case LoopResult.Continue:
                        continue;
                }
            }
        }

        public static void For1(this int count, Func<int, LoopResult> action)
        {
            for (var i = 1; i <= count; i++)
            {
                var result = action(i);
                switch (result)
                {
                    case LoopResult.Break:
                        break;
                    case LoopResult.Continue:
                        continue;
                }
            }
        }

        public static List<T> MakeList<T>(this int count, Func<int, T> func)
        {
            var result = new List<T>();
            for (var i = 0; i < count; i++)
            {
                result.Add(func(i));
            }
            return result;
        }
        public static TResult Reduce<TSource, TResult>(this IEnumerable<TSource> source, TResult initValue, Func<TResult, TSource, TResult> fn)
        {
            return Reduce(source, initValue, (value, item, index, list) => fn(value, item));
        }

        public static TResult Reduce<TSource, TResult>(this IEnumerable<TSource> source, TResult initValue, Func<TResult, TSource, int, TResult> fn)
        {
            return Reduce(source, initValue, (value, item, index, list) => fn(value, item, index));
        }

        public static TResult Reduce<TSource, TResult>(this IEnumerable<TSource> source, TResult initValue, Func<TResult, TSource, int, IEnumerable<TSource>, TResult> fn)
        {
            var value = initValue;

            var index = 0;
            foreach (var item in source)
            {
                value = fn(value, item, index++, source);
            }

            return value;
        }

        public static IEnumerable<(TItem Item1, TItem Item2)> AllPairs<TItem>(this List<TItem> source, bool includeDuplicate = false)
        {
            for (var i = 0; i < source.Count(); i++)
            {
                var item1 = source[i];
                for (var j = i + (includeDuplicate ? 0 : 1); j < source.Count(); j++)
                {
                    var item2 = source[j];
                    yield return (item1, item2);
                }
            }
        }

        public static IEnumerable<(TItem Item1, TItem Item2)> AllPairs<TItem>(this TItem[] source, bool includeDuplicate = false)
        {
            for (var i = 0; i < source.Count(); i++)
            {
                var item1 = source[i];
                for (var j = i + (includeDuplicate ? 0 : 1); j < source.Count(); j++)
                {
                    var item2 = source[j];
                    yield return (item1, item2);
                }
            }
        }

        public static bool Empty<TSource>(this IEnumerable<TSource> source)
        {
            return !source.Any();
        }

        public static bool Empty<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            return !source.Any(predicate);
        }
    }

    public static partial class Ex
    {
        public static int Ccw(Point a, Point b, Point c)
        {
            // 출처: https://jason9319.tistory.com/358 [ACM-ICPC 상 탈 사람]
            var op = a.X * b.Y + b.X * c.Y + c.X * a.Y;
            op -= (a.Y * b.X + b.Y * c.X + c.Y * a.X);
            if (op > 0) return 1;
            else if (op == 0) return 0;
            else return -1;
        }

        public static bool IsIntersect(Line line1, Line line2)
        {
            // 출처: https://jason9319.tistory.com/358 [ACM-ICPC 상 탈 사람]
            var a = line1.P1;
            var b = line1.P2;
            var c = line2.P1;
            var d = line2.P2;
            int ab = Ccw(a, b, c) * Ccw(a, b, d);
            int cd = Ccw(c, d, a) * Ccw(c, d, b);
            if (ab == 0 && cd == 0)
            {
                if (a > b) (a, b) = Swap(a, b);
                if (c > d) (c, d) = Swap(c, d);
                return c <= b && a <= d;
            }
            return ab <= 0 && cd <= 0;
        }

        public static (T b, T a) Swap<T>(T a, T b)
        {
            return (b, a);
        }

        public static (bool found, long x, long y) FindDiophantusEquation(long a, long b, long c)
        {
            // https://m.blog.naver.com/PostView.naver?isHttpsRedirect=true&blogId=beneys&logNo=221122957338

            var initA = a;
            var initB = b;

            var list = new List<(long A, long B, long M, long R)>();
            do
            {
                var m = a / b;
                var r = a % b;
                list.Add((a, b, m, r));
                a = b;
                b = r;
            } while (a % b != 0);

            var gcd = list.Last().R;
            if (c % gcd != 0)
            {
                return (false, 0, 0);
            }

            //list.Dump();
            list.Reverse();

            var first = list.First();
            var list2 = new List<(long A, long X, long B, long Y)>
            {
                (first.A, 1, first.B, -first.M)
            };
            foreach (var (A, B, M, R) in list.Skip(1))
            {
                var prev = list2.Last();
                if (R == prev.A)
                {
                    var nextA = A;
                    var nextX = prev.X;
                    var nextB = B;
                    var nextY = prev.Y + (-M) * prev.X;
                    list2.Add((nextA, nextX, nextB, nextY));
                }
                else // if (R == prev.B)
                {
                    var nextA = B;
                    var nextX = prev.X + (-M) * prev.Y;
                    var nextB = A;
                    var nextY = prev.Y;
                    list2.Add((nextA, nextX, nextB, nextY));
                }
            }
            //list2.Dump();

            var mm = c / gcd;
            var last = list2.Last();
            var x = (initA == last.A ? last.X : last.Y) * mm;
            var y = (initA == last.A ? last.Y : last.X) * mm;

            //((initA * x + initB * y)).Dump("C: " + c);

            return (true, x, y);
        }

        public static long ChineseRemainderTheorem(List<(long A, long M)> arr)
        {
            // https://j1w2k3.tistory.com/1340
            var M = arr.Select(x => x.M).Aggregate((a, b) => a * b);
            var nList = arr.Select(x => M / x.M).ToList();

            var xxxList = arr
                .Zip(nList, (condition, N) => new
                {
                    condition.A,
                    N,
                    Dio = FindDiophantusEquation(N, condition.M, 1), // 특수해
                })
                .ToList();

            long x = 0;
            foreach (var xxx in xxxList)
            {
                x += (xxx.A * xxx.N * xxx.Dio.x) % M;
                x %= M;
            }

            return x;
        }
    }

    public class Matrix
    {
        private List<List<long>> Value;

        public int Row => Value.Count;
        public int Column => Value.First().Count;

        public Matrix(int row, int column)
        {
            Value = row.MakeList(_ => column.MakeList(_ => 0L));
        }

        public Matrix(int row, int column, params int[] values)
            : this(row, column)
        {
            var index = 0;
            row.For(r => column.For(c => Value[r][c] = values[index++]));
        }

        public Matrix(List<List<long>> value)
        {
            Value = value;
        }

        public List<long> this[int row] => Value[row];

        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            var result = m1.Row.MakeList(r =>
            {
                return m2.Column.MakeList(c =>
                {
                    long sum = 0;
                    m1.Column.For(k =>
                    {
                        sum += m1[r][k] * m2[k][c];
                    });
                    return sum;
                });
            });

            return new Matrix(result);
        }

        private Matrix Multiply(Matrix m2, long mod)
        {
            var m1 = this;
            var result = m1.Row.MakeList(r =>
            {
                return m2.Column.MakeList(c =>
                {
                    long sum = 0;
                    m1.Column.For(k =>
                    {
                        sum += m1[r][k] * m2[k][c];
                    });
                    return sum % mod;
                });
            });

            return new Matrix(result);
        }

        public Matrix Pow(int N)
        {
            var 항등원 = new Matrix(Row.MakeList(r => Column.MakeList(c => r == c ? 1L : 0L)));

            var result = MathEx.Pow(this, N, 항등원, (m1, m2) => m1 * m2);

            return result;
        }

        public Matrix Pow(long N, int mod)
        {
            var 항등원 = new Matrix(Row.MakeList(r => Column.MakeList(c => r == c ? 1L : 0L)));

            Row.For(row => Column.For(column =>
            {
                Value[row][column] = (Value[row][column] + mod) % mod;
            }));

            var result = MathEx.Pow(this, N, 항등원, (m1, m2) => m1.Multiply(m2, mod));

            return result;
        }

        public Matrix Pow(int N, Func<Matrix, Matrix, Matrix> fnMultifly)
        {
            var 항등원 = new Matrix(Row.MakeList(r => Column.MakeList(c => r == c ? 1L : 0L)));

            var result = MathEx.Pow(this, N, 항등원, fnMultifly);

            return result;
        }

    }

    public static class MathEx
    {

        /// <summary>
        /// pow1의 exp제곱을 구한다. \n
        /// 2^10 = 2.Pow(10, 1, (a, b) => a * b);
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="base">밑</param>
        /// <param name="exp">지수</param>
        /// <param name="pow0">밑의 0제곱</param>
        /// <param name="fnMultifly">곱셈연산</param>
        /// <returns></returns>
        public static T Pow<T>(this T @base, int exp, T pow0, Func<T, T, T> fnMultifly)
        {
            // Addition-Chain exponentiation

            var basee = @base;
            var res = pow0;

            while (exp > 0)
            {
                if ((exp & 1) != 0)
                    res = fnMultifly(res, basee);
                exp >>= 1;
                basee = fnMultifly(basee, basee);
            }

            return res;
        }
        public static T Pow<T>(this T @base, long exp, T pow0, Func<T, T, T> fnMultifly)
        {
            // Addition-Chain exponentiation

            var basee = @base;
            var res = pow0;

            while (exp > 0)
            {
                if ((exp & 1) != 0)
                    res = fnMultifly(res, basee);
                exp >>= 1;
                basee = fnMultifly(basee, basee);
            }

            return res;
        }

        public static int Pow(this int @base, int exp)
        {
            return @base.Pow(exp, 1, (a, b) => a * b);
        }

        public static long Pow(this long @base, int exp)
        {
            return @base.Pow(exp, 1, (a, b) => a * b);
        }
        public static long Gcd(long a, long b)
        {
            if (a == b) { return a; }
            else if (a > b && a % b == 0) { return b; }
            else if (b > a && b % a == 0) { return a; }

            long _gcd = 0;
            while (b != 0)
            {
                _gcd = b;
                b = a % b;
                a = _gcd;
            }
            return _gcd;
        }

        public static long Lcm(long a, long b)
        {
            var gcd = Gcd(a, b);
            var lcm = (a / gcd) * b;
            return lcm;
        }

        /// <summary> [a, b) 인지 판단한다.  </summary>
        public static bool Between(this int value, int a, int b)
        {
            return value >= a && value < b;
        }

        public static long Sqrt(long value)
        {
            long a = 0;
            long c = 3037000499;

            while (a <= c)
            {
                long b = (a + c) / 2;
                long square = b * b;

                if (value == square)
                {
                    return b;
                }
                else if (value < square)
                {
                    c = b - 1;
                }
                else
                {
                    a = b + 1;
                }
            }

            return c;
        }
    }

    public class Line
    {
        public Point P1;
        public Point P2;

        public bool IsVertical => P1.X == P2.X;
        public bool IsHorizontal => P1.Y == P2.Y;
    }

    public record Point
    {
        public int X;
        public int Y;
        public Point() { }
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static bool operator <(Point a, Point b)
        {
            if (a.X < b.X)
                return true;
            else if (a.X == b.X && a.Y < b.Y)
                return true;
            return false;
        }

        public static bool operator <=(Point a, Point b)
        {
            if (a.X < b.X)
                return true;
            else if (a.X == b.X && a.Y <= b.Y)
                return true;
            return false;
        }

        public static bool operator >(Point a, Point b)
        {
            return !(a <= b);
        }

        public static bool operator >=(Point a, Point b)
        {
            return !(a < b);
        }

        public static Point operator +(Point a, Point b)
        {
            return new Point(a.X + b.X, a.Y + b.Y);
        }
    }

}

#endif
}
