# C#

## 난이도 : 실버 1

## 알고리즘 분류
  - 그리디 알고리즘
  - 정렬

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 128 MB

## 문제
Once upon a time, in the Kingdom of Loowater, a minor nuisance turned into a major problem.<br/>
The shores of Rellau Creek in central Loowater had always been a prime breeding ground for geese. Due to the lack of predators, the geese population was out of control. The people of Loowater mostly kept clear of the geese. Occasionally, a goose would attack one of the people, and perhaps bite off a finger or two, but in general, the people tolerated the geese as a minor nuisance.<br/>
One day, a freak mutation occurred, and one of the geese spawned a multi-headed fire-breathing dragon. When the dragon grew up, he threatened to burn the Kingdom of Loowater to a crisp. Loowater had a major problem. The king was alarmed, and called on his knights to slay the dragon and save the kingdom.<br/>
The knights explained: "To slay the dragon, we must chop off all its heads. Each knight can chop off one of the dragon's heads. The heads of the dragon are of different sizes. In order to chop off a head, a knight must be at least as tall as the diameter of the head. The knights' union demands that for chopping off a head, a knight must be paid a wage equal to one gold coin for each centimetre of the knight's height."<br/>
Would there be enough knights to defeat the dragon? The king called on his advisors to help him decide how many and which knights to hire. After having lost a lot of money building Mir Park, the king wanted to minimize the expense of slaying the dragon. As one of the advisors, your job was to help the king. You took it very seriously: if you failed, you and the whole kingdom would be burnt to a crisp!<br/>


## 입력
The input contains several test cases. The first line of each test case contains two integers between 1 and 20000 inclusive, indicating the number n of heads that the dragon has, and the number m of knights in the kingdom. The next n lines each contain an integer, and give the diameters of the dragon's heads, in centimetres. The following m lines each contain an integer, and specify the heights of the knights of Loowater, also in centimetres.<br/>
The last test case is followed by a line containing:<br/>

	0 0


## 출력
For each test case, output a line containing the minimum number of gold coins that the king needs to pay to slay the dragon. If it is not possible for the knights of Loowater to slay the dragon, output the line:<br/>

	Loowater is doomed!


## 예제 입력
2 3<br/>
5<br/>
4<br/>
7<br/>
8<br/>
4<br/>
2 1<br/>
5<br/>
5<br/>
10<br/>
0 0<br/>

## 예제 출력
11<br/>
Loowater is doomed!<br/>

## 풀이
가장 약한 기사부터 가장 약한 용을 잡는게 용을 최대한 잡을 수 있다.<br/>
그래서 기사와 용을 정렬하고 약한 기사가 약한 용을 잡아가면서 시뮬레이션 돌렸다.<br/>

## 문제 링크
https://www.acmicpc.net/problem/4228