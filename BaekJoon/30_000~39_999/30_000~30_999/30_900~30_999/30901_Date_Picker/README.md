# C#

## 난이도 : 골드 5

## 알고리즘 분류
  - 수학
  - 그리디 알고리즘
  - 브루트포스 알고리즘
  - 정렬
  - 확률론

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 1024 MB

## 문제
The NWERC is coming up and your agenda is filling up with meetings. One of your teammates wants to plan a meeting, and asks for your input. However, instead of asking you for your exact agenda, you have to fill out two separate polls: one for indicating which days you are available, and one for the hours!<br/>
As a computer scientist, you plan your meetings only on whole hours and each meeting takes an integer number of hours. Therefore, your agenda can be modelled as a matrix of 7 rows (days), and 24 columns (hours). Each cell in this matrix is either '.' or 'x', meaning that hour of that day you are either free or have a meeting, respectively.<br/>
You have to pick at least d days in the first poll and h hours in the second poll, and we assume the meeting will take place on any of your picked hour/day combinations with equal probability. What is the probability that you can attend the meeting if you fill in the polls optimally?<br/>

## 입력
The input consists of:
  - 7 lines with 24 characters, each character being either '.' or 'x', with '.' indicating the time slots you are available.
  - One line with two integers d and h (1 ≤ d ≤ 7, 1 ≤ h ≤ 24), the minimum number of days and hours you have to fill in.

## 출력
Output the probability that you are available at the chosen meeting time.<br/>
Your answer should have an absolute or relative error of at most 10^-6.

## 예제 입력
xxxxxxxxx.....x...xxxxxx<br/>
xxxxxxxx..x...x...xxxxxx<br/>
xxxxxxxx......x...x.xxxx<br/>
xxxxxxxx...xxxxxxxxxxxxx<br/>
xxxxxxxx...xxxxxxxxxxxxx<br/>
xxxxxxxx...xxxxxxxx.xxxx<br/>
......xxxxxxxxxxxxxxxxxx<br/>
3 8<br/>

## 예제 출력
0.958333333333333<br/>

## 풀이
문제 해석에 많은 시간이 걸렸다.<br/>
'.'은 회의 가능한 시간을 나타내고, 'x'는 회의 불가능한 시간을 나타낸다.<br/>
입력 마지막에 주어지는 숫자 d는 날짜, 숫자 h는 시간을 의미한다.<br/>
찾아야 하는건 7일 중 d일을 택하고 24시간 중 h를 택해서 가능한 회의 확률을 찾아야 한다.<br/>
위 예제로보면 0, 1, 2일을 택하고 0, 1, 2, 3, ..., 7시간을 택하면 모두 'x'다.<br/>
회의 가능한 시간이 0이므로 확률은 0%다.<br/>
0, 1, 2일을 택하고 시간을 바꿔 9, 10, 11, 12, 13, 15, 16, 17시간을 택하면 가능한 시간은 23시간이다.<br/>
회의 가능한 확률은 0.958333333...이 나온다.<br/>
그리고 이외에 0, 1, 2 말고도 다른 날짜를 골라야하고 시간역시 모두 탐색해야 한다.<br/>


해당 방법은 최악의 경우 7C4 x 24C12 x 12 x 4인 경우 20억을 가뿐히 넘어 시간초과 날 수 있다.<br/>
그래서 날짜는 조합을 이용하되 시간은 24시간을 조사해 내림차순으로 정렬한다.<br/>
가장 큰 시간들 h개에 대해 회의 확률을 구해도 최대값을 찾을 수 있다.<br/>
이 경우 7Cd x d x h x h x log h로 시간이 많이 줄어든다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/30901