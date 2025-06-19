# C#

## 난이도 : 실버 3

## 알고리즘 분류
  - 구현
  - 브루트포스 알고리즘
  - 백트래킹

## 제한조건
  - 시간 제한 : 5초
  - 메모리 제한 : 512 MB

## 문제
In a kingdom there are prison cells (numbered 1 to P) built to form a straight line segment. Cells number i and i+1 are adjacent, and prisoners in adjacent cells are called "neighbours." A wall with a window separates adjacent cells, and neighbours can communicate through that window.<br/>
All prisoners live in peace until a prisoner is released. When that happens, the released prisoner's neighbours find out, and each communicates this to his other neighbour. That prisoner passes it on to his other neighbour, and so on until they reach a prisoner with no other neighbour (because he is in cell 1, or in cell P, or the other adjacent cell is empty). A prisoner who discovers that another prisoner has been released will angrily break everything in his cell, unless he is bribed with a gold coin. So, after releasing a prisoner in cell A, all prisoners housed on either side of cell A - until cell 1, cell P or an empty cell - need to be bribed.<br/>
Assume that each prison cell is initially occupied by exactly one prisoner, and that only one prisoner can be released per day. Given the list of Q prisoners to be released in Qdays, find the minimum total number of gold coins needed as bribes if the prisoners may be released in any order.<br/>
Note that each bribe only has an effect for one day. If a prisoner who was bribed yesterday hears about another released prisoner today, then he needs to be bribed again.<br/>


## 입력
The first line of input gives the number of cases, N. N test cases follow. Each case consists of 2 lines. The first line is formatted as<br/>

	P Q

where P is the number of prison cells and Q is the number of prisoners to be released.<br/>
This will be followed by a line with Q distinct cell numbers (of the prisoners to be released), space separated, sorted in ascending order.<br/>


## 출력
For each test case, output one line in the format<br/>

	Case #X: C

where X is the case number, starting from 1, and C is the minimum number of gold coins needed as bribes.<br/>


## 제한
  - 1 ≤ N ≤ 100
  - Q ≤ P
  - Each cell number is between 1 and P, inclusive.
  - 1 ≤ P ≤ 100
  - 1 ≤ Q ≤ 5


## 예제 입력
2<br/>
8 1<br/>
3<br/>
20 3<br/>
3 6 14<br/>

## 예제 출력
Case #1: 7<br/>
Case #2: 35<br/>


## 힌트
In the second sample case, you first release the person in cell 14, then cell 6, then cell 3. The number of gold coins needed is 19 + 12 + 4 = 35. If you instead release the person in cell 6 first, the cost will be 19 + 4 + 13 = 36.<br/>


## 풀이
탈출 시키는 죄수의 수가 5명이므로 탈출 순서의 경우의 수는 5! = 120이다.<br/>
그리고 한명씩 확인하면서 인원을 계산해도 500씩 확인하므로 하나의 케이스당 60_000번에 해결된다.<br/>
그래서 브루트포스 방법으로 접근했고 구현은 DFS를 이용해 구현했다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/12641