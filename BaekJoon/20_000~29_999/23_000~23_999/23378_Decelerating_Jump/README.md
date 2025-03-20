# C#

## 난이도 : 골드 3

## 알고리즘 분류
  - 다이나믹 프로그래밍

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 1024 MB

## 문제
An athlete is participating in a new sport that is the perfect mix of hopscotch and triple jumping. For this jury sport, n squares are laid out on the ground in a line, with equal distances between them. The first phase is the approach, where an athlete sprints towards the first square, in which they start their first jump. Then, they may jump on any number of other squares, and must finally land in the last square.<br/>
The jury has given a predetermined number of points to jumping in each square, and the score of the athlete will be the sum of the scores of all the squares they jump in, including the very first and last squares.<br/>
Due to the nature of this sport, once the athlete starts jumping, they can not accelerate anymore, and the length of consecutive jumps can never increase. Naturally, it is also impossible to reverse direction.<br/>
Given the points the jury has allocated to each square, find the maximal possible score an athlete can get on this event.<br/>


## 입력
The input consists of:

  - One line with an integer n (2 ≤ n ≤ 3000), the number of squares.
  - One line with n integers p_1, p_2, ..., p_n (-10^9 ≤ p_i ≤ 10^9), the number of points the jury awards for jumping on each of the squares.<br/>


## 출력
Output the maximum score that an athlete can get.<br/>


## 예제 입력
4<br/>
1 1 -1 1<br/>


## 예제 출력
2<br/>


## 풀이
처음에는 2차원 배열을 이용해 아래와 같은 방법으로 풀었다.<br/>
dp[i][j] = val를 i번째까지 택하고 j거리만큼 뛰었을 때 최댓값을 val로 삼으면 된다.<br/>
그러면 점화식은 dp[i][j] = arr[i] + max(dp[i-k][k])를 얻을 수 있다.<br/>
그리고 DFS를 이용해 dp를 채워갔다.<br/>


이후 해당 과정을 분석하니 정방향으로 도 정답을 구할 수 있었고,<br/>
1차원 배열로도 해결가능함을 확인했다.<br/>
현재 코드는 1차원 배열 풀이이고, 주석 부분이 2차원 배열 코드이다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/23378