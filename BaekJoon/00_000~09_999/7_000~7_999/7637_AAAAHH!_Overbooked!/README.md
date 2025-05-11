# C#

## 난이도 : 브론즈 1

## 알고리즘 분류
  - 구현
  - 문자열
  - 파싱

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 128 MB

## 문제
Elaine is excited to begin the school year—so excited, in fact, that she signed herself up to attend several events today (This programming contest, sadly, is not one of them). She may have overdone it, though; she didn’t bother to check whether the events she signed up for have conflicting times. While you’re sitting here in this contest, why not check for her?<br/>


## 입력
The input consists of multiple test cases. Each test case begins with an integer N, 1 ≤ N ≤ 100, on a line by itself denoting the number of events. After that follow N lines giving the start and end times of each event, in hh:mm-hh:mm 24-hour format. The end time is guaranteed to be strictly after the start time. Input is followed by a single line with N = 0, which should not be processed.<br/>


## 출력
For each test case, print out a single line that says “conflict” (no quotes) if Elaine’s events have conflicting times, and “no conflict” (no quotes) otherwise. Assume that Elaine can travel around campus instantaneously, so if an event starts at the same time another event ends, the two events do not conflict.<br/>


## 예제 입력
3<br/>
09:00-09:50<br/>
13:00-17:00<br/>
09:50-10:30<br/>
2<br/>
10:00-11:00<br/>
09:00-12:00<br/>
0<br/>


## 예제 출력
no conflict<br/>
conflict<br/>


## 풀이
k번째 시작시간을 s_k, 끝 시간을 e_k라 하자.<br/>
시간이 겹치는 경우를 보면, e_i < e_j이면서 s_j < e_i인 i, j가 존재하는 것이다.<br/>
혹은 s_i < s_j이면서 s_j < e_i인 i, j가 존재하는 것으로 봐도 된다.<br/>
여기서는 e_i < e_j이면서 s_j < e_i인 i, j가 존재하는 것으로 봤다.<br/>


그래서 겹치는 것을 찾기 위해 시작 시간으로 정렬한다.<br/>
그러면 i < j이면 s_i < s_j가 자명하게 성립한다.<br/>
이후 0, 1, ..., i - 1까지의 끝지점의 최댓값 e_M을 확인하며 s_i < e_M인지 확인해 풀었다.<br/>


그리고 시간의 대소관계를 비교해야하기에 hh:mm을 분 단위로 변형했다.<br/>
또한 -로 시작 시간과 끝 시간이 구분되기에 string 클래스의 Split 메소드를 이용해 구분했다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/7637