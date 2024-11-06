using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 28
이름 : 배성훈
내용 : Returning Home
    문제번호 : 6873

    구현, 문자열 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_0918
    {

        static void Main918(string[] args)
        {

            string L = "L";
            string R = "R";
            string LEFT = "LEFT";
            string RIGHT = "RIGHT";
            string SCHOOL = "SCHOOL";
            StreamReader sr;
            StreamWriter sw;

            Stack<string> stack;

            Solve();
            void Solve()
            {

                Init();

                string input;
                while ((input = sr.ReadLine().Trim()) != SCHOOL)
                {

                    stack.Push(input);
                }

                while (stack.Count > 2)
                {

                    string DIR = stack.Pop();
                    DIR = DIR == L ? RIGHT : LEFT;
                    string SPOT = stack.Pop();

                    sw.Write($"Turn {DIR} onto {SPOT} street.\n");
                }

                {

                    string DIR = stack.Pop();
                    DIR = DIR == L ? RIGHT : LEFT;

                    sw.Write($"Turn {DIR} into your HOME.");
                }

                sw.Close();
                sr.Close();
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                stack = new();
            }
        }
    }

#if other
// #include <iostream>
// #include <vector>
// #include <string>
using namespace std;

vector<string> vec;

int main() {
	ios::sync_with_stdio(0);
	cin.tie(0);

	vec.emplace_back("HOME");
	while (vec.back() != "SCHOOL") cin >> vec.emplace_back();
	vec.pop_back();

	while (vec.size() > 2) {
		cout << "Turn ";
		if (vec.back() == "R") cout << "LEFT";
		else cout << "RIGHT";
		vec.pop_back();
		cout << " onto " << vec.back() << " street.\n";
		vec.pop_back();
	}
	cout << "Turn ";
	if (vec.back() == "R") cout << "LEFT";
	else cout << "RIGHT";
	vec.pop_back();
	cout << " into your HOME.\n";
}
#endif
}
