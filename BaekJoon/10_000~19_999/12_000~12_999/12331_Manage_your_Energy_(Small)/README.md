# C#

## 난이도 : 실버 5

## 알고리즘 분류
  - 다이나믹 프로그래밍
  - 브루트포스 알고리즘

## 제한조건
  - 시간 제한 : 5초
  - 메모리 제한 : 512 MB

## 문제
You've got a very busy calendar today, full of important stuff to do. You worked hard to prepare and make sure all the activities don't overlap. Now it's morning, and you're worried that despite all of your enthusiasm, you won't have the energy to do all of this with full engagement.<br/>
You will have to manage your energy carefully. You start the day full of energy - E joules of energy, to be precise. You know you can't go below zero joules, or you will drop from exhaustion. You can spend any non-negative, integer number of joules on each activity (you can spend zero, if you feel lazy), and after each activity you will regain R joules of energy. No matter how lazy you are, however, you cannot have more than E joules of energy at any time; any extra energy you would regain past that point is wasted.<br/>
Now, some things (like solving Code Jam problems) are more important than others. For the ith activity, you have a value vi that expresses how important this activity is to you. The gain you get from each activity is the value of the activity, multiplied by the amount of energy you spent on the activity (in joules). You want to manage your energy so that your total gain will be as large as possible.<br/>
Note that you cannot reorder the activities in your calendar. You just have to manage your energy as well as you can with the calendar you have.<br/>


## 입력
The first line of the input gives the number of test cases, T. T test cases follow. Each test case is described by two lines. The first contains three integers: E, the maximum (and initial) amount of energy, R, the amount you regain after each activity, and N, the number of activities planned for the day. The second line contains N integers vi, describing the values of the activities you have planned for today.<br/>
Limits<br/>

  - 1 ≤ T ≤ 100.
  - 1 ≤ E ≤ 5.
  - 1 ≤ R ≤ 5.
  - 1 ≤ N ≤ 10.
  - 1 ≤ vi ≤ 10.


## 출력
For each test case, output one line containing "Case #x: y", where x is the case number (starting from 1) and y is the maximum gain you can achieve by managing your energy that day.<br/>


## 예제 입력
3<br/>
5 2 2<br/>
2 1<br/>
5 2 2<br/>
1 2<br/>
3 3 4<br/>
4 1 3 5<br/>


## 예제 출력
Case #1: 12<br/>
Case #2: 12<br/>
Case #3: 39<br/>


## 힌트
In the first case, we can spend all 5 joules of our energy on the first activity (for a gain of 10), regain 2 and spend them on the second activity. In the second case, we spend 2 joules on the first activity, regain them, and spend 5 on the second. In the third case, our regain rate is equal to the maximum energy, meaning we always recover all energy after each activity - so we can spend full 3 joules on each activity.<br/>


## 풀이
초기에는 가장 큰 값에 모든 에너지를 쓰고 나머지는 회복에너지에 쓰면 되겠지하고 가볍게 접근했다.<br/>
그러니 5 4 5 1 이고, 최대 에너지가 5, 충전량이 2라하면 해당 방법으로 진행할 시 45가 나온다.<br/>
반면 해당 경우의 최댓값은 47로 반례가 존재한다.<br/>

이에 입력 범위가 작아 모든 경우를 확인하는 배낭 형식으로 진행해 풀었다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/12331