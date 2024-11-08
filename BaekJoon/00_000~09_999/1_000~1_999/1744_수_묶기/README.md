# C#

## 난이도 : 골드 4

## 알고리즘 분류
  - 그리디 알고리즘
  - 정렬
  - 많은 조건 분기

## 제한조건
  - 시간 제한 : 2초
  - 메모리 제한 : 128 MB

## 문제
길이가 N인 수열이 주어졌을 때, 그 수열의 합을 구하려고 한다. 하지만, 그냥 그 수열의 합을 모두 더해서 구하는 것이 아니라, 수열의 두 수를 묶으려고 한다. 어떤 수를 묶으려고 할 때, 위치에 상관없이 묶을 수 있다. 하지만, 같은 위치에 있는 수(자기 자신)를 묶는 것은 불가능하다. 그리고 어떤 수를 묶게 되면, 수열의 합을 구할 때 묶은 수는 서로 곱한 후에 더한다.<br/>
예를 들면, 어떤 수열이 {0, 1, 2, 4, 3, 5}일 때, 그냥 이 수열의 합을 구하면 0+1+2+4+3+5 = 15이다. 하지만, 2와 3을 묶고, 4와 5를 묶게 되면, 0+1+(2*3)+(4*5) = 27이 되어 최대가 된다.<br/>
수열의 모든 수는 단 한번만 묶거나, 아니면 묶지 않아야한다.<br/>
수열이 주어졌을 때, 수열의 각 수를 적절히 묶었을 때, 그 합이 최대가 되게 하는 프로그램을 작성하시오.<br/>

## 입력
첫째 줄에 수열의 크기 N이 주어진다. N은 50보다 작은 자연수이다. 둘째 줄부터 N개의 줄에 수열의 각 수가 주어진다. 수열의 수는 -1,000보다 크거나 같고, 1,000보다 작거나 같은 정수이다.<br/>

## 출력
수를 합이 최대가 나오게 묶었을 때 합을 출력한다. 정답은 항상 2^31보다 작다.<br/>

## 예제 입력
4<br/>
-1<br/>
2<br/>
1<br/>
3<br/>

## 예제 출력
6<br/>

## 풀이
수들 몇 개를 예시를 들어 규칙성을 찾아갔다.<br/>
가장 작은 음수와 다음으로 작은 음수를 곱한게 음수들 중 가장 큰 경우의 수가 된다.<br/>
이후에는 양수 부분은 1보다 큰 수들에 대해 곱한게 더한 것보다 항상 크거나 같다.<br/>
양수는 가장 큰 것과 그 다음으로 큰 것을 곱해주는게 가장 큰 경우의 수가 된다.<br/>
그리고 앞에 음수가 1개 있으면 0과 곱해서 음수를 제거할 수 있다.<br/>
이후에 남은 수들은 덧셈으로 처리하면 됨을 알고 해당 방법으로 풀었다.<br/>

## 문제 링크
https://www.acmicpc.net/problem/1744