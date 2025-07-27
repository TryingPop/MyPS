# C#

## 난이도 : 골드 5

## 알고리즘 분류
  - 다이나믹 프로그래밍
  - 문자열
  - 파싱
  - 최장 공통 부분 수열 문제
  - utf-8 입력 처리

## 제한조건
  - 시간 제한 : 2초
  - 메모리 제한 : 512 MB

## 문제
LCS(Longest Common Subsequence, 최장 공통 부분 수열)문제는 두 수열이 주어졌을 때, 모두의 부분 수열이 되는 수열 중 가장 긴 것을 찾는 문제이다.<br/>
예를 들어, "최백준"과 "백준온라인"의 LCS는 "백준"이고, "스타트링크"와 "스트라토캐스터"의 LCS는 "스트"이다.<br/>


## 입력
첫째 줄과 둘째 줄에 두 문자열이 주어진다. 문자열은 최대 1000글자이고, 유니코드 U+AC00(가)부터 U+D7A3(힣)까지로만 이루어져 있으며, UTF-8로 인코딩 되어 있다.<br/>


## 출력
첫째 줄에 입력으로 주어진 두 문자열의 LCS의 길이를 출력한다.<br/>


## 예제 입력
가나다라가나다라<br/>
가다나가다라<br/>

## 예제 출력
5<br/>


## 요약
  - 목표: 두 문자열의 최장 공통 부분 수열(LCS) 길이 구하기

  - 정의:
    - dp[i][j] = str1[i:] 문자까지 확인, str2[j:]번 문자까지 확인했을 때 가장 긴 LCS 길이

  - 초기값:
    - dp[i][j] = 0

  - 점화식:
    - str1[i] = str2[j]인 경우, dp[i][j] = dp[i + 1][j + 1] + 1
    - str1[i] ≠ str2[j]인 경우, dp[i][j] = Max(dp[i + 1][j], dp[i][j + 1])

  - 정답: dp[0][0]

  - 시간 복잡도: O(N x M) (N = str1.Length, M = str2.Length)

  - 주의사항
    - StreamReader를 이용해 입력 데이터를 받아올 시 encoding 방법을 Console.InputEncoding으로 설정


## 풀이
두 문자열 str1, str2간의 LCS를 찾는 문제다.<br/>
LCS 문제는 dp를 쓰는 문제로 잘 알려져 있다.<br/>


dp[i][j] = val를 다음과 같이 정의한다.<br/>

  - i : str1의 문자 위치를 가리키는 인덱스
  - j : str2의 문자 위치를 가리키는 인덱스
  - val : str1[i:]와 str2[j:]의 최장 공통 부분 수열(LCS)의 길이

그러면 다음과 같은 점화식을 얻을 수 있다.<br/>
최댓값 함수를 Max라 하자.<br/>

  - str1[i] = str2[j]인 경우, dp[i][j] = dp[i + 1][j + 1] + 1
  - str1[i] ≠ str2[j]인 경우, dp[i][j] = Max(dp[i + 1][j], dp[i][j + 1])


i = 0, j = 0에서 시작해 재귀적으로 dp 값을 채워나가는 Top-down 방식이다. 이를 위해 메모이제이션을 활용해 이미 계산된 값은 저장하고 다시 사용한다.<br/>


모든 dp[i][j] 값은 한 번만 계산되므로 LCS를 찾는 경우 시간 복잡도는 dp 배열의 크기와 같다.<br/>
그래서 N = str1.Length, M = str2.Length라 하면 시간 복잡도는 O(N x M)이 된다.<br/>




추가로 이 문제에서는 입력 인코딩 문제도 있었다.

C#의 Console에서 입력받을 때 기본 인코딩은 System.Text.OSEncoding이며, 이는 Console.InputEncoding을 통해 확인할 수 있다. 이 인코딩은 기본적으로 한글을 잘 지원한다.<br/>
하지만 StreamReader로 입력을 받으면 기본 인코딩이 UTF-8로 설정되어 있어, CP949 기반의 한글 입력이 깨지는 현상이 발생한다.<br/>
따라서 입력 성능을 향상 시키기 위해 StreamReader를 사용할 경우, 입력 인코딩을 Console.InputEncoding 혹은 CP949로 명시적으로 설정해주면 된다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/15482