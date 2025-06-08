# C#

## 난이도 : 골드 5

## 알고리즘 분류
  - 다이나믹 프로그래밍
  - 배낭 문제

## 제한조건
  - 시간 제한 : 3초
  - 메모리 제한 : 256 MB

## 문제
At the start of the Hunger Games, the participant from each district can select a set of weapons from the toolboxes. However, each participant will be given a sack. Each sack can hold a certain weight. Thus, the total weight of the weapons selected must not exceed the limit of the sack. However, each participant prefers different type of weapons. Therefore, the selection must maximize the participant’s preference as well.<br/>


## 입력
The first line of standard input contains an integer T, the number of test cases (1 <= T <= 100). T data sets follow. Each test case consists of four lines. The first line contains an integer N, the number of weapons where 1 <= N <= 50. The second line contains an integer W, the capacity of the sack where 1 <= W <= 5000. The third line consists of N integers, each of which represents the weight of each weapon. The last line consists of N integers, each of which represents the participant’s preference value of each weapon. The weight value and the preference value of each weapon are ranged between 1 and 100.<br/>


## 출력
For each case, you are to output a line giving the maximum total preference value possible of that test case.<br/>


## 예제 입력
2<br/>
8<br/>
20<br/>
2 3 4 5 5 5 5 9<br/>
3 4 5 8 8 8 8 10<br/>
5<br/>
20<br/>
2 3 4 5 9<br/>
3 4 5 8 10<br/>


## 예제 출력
32<br/>
26<br/>


## 풀이
전체 무게의 합은 5,000을 넘기지 못하고 dp[idx] = val를 idx 무게에 따른 최대 가치 val로 잡으면 배낭문제로 풀 수 있다.<br/>
다만 입력 데이터의 끝에 띄어쓰기가 있어 여러 번 틀렸다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/11261