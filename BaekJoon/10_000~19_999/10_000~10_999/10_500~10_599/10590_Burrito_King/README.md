# C#

## 난이도 : 골드 3

## 알고리즘 분류
  - 수학
  - 그리디 알고리즘

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 256 MB

## 문제
Two friends Albert and Barney came to the newly opened restaurant “Burrito King”. The restaurant had opened just yesterday, and Albert has got a special gift card, which allows the friends to get a free burrito. However, there is a constraint on the amount of ingredients — the burrito can contain at most gi grams of ingredient i for all i from 1 to n.<br/>
There are two satisfaction parameters a_i and b_i for each ingredient — the amount of Albert’s joy per gram of the corresponding ingredient, and the amount of Barney’s unhappiness per gram, correspondingly.<br/>
Therefore, the total Albert’s joy from the burrito is equal to:<br/>

	∑_{i=1}_{n}{s_i x a_i}

The total Barney’s unhappiness from the burrito is equal to:<br/>

	 ∑_{i=1}_{n}{s_i x b_i}

Here s_i is the number of grams of the i-th ingredient in the burrito. Note, that s_i is not necessarily an integer, and 0 ≤ s_i ≤ g_i.<br/>
Albert wants to make his total joy from the burrito to be at least A. Barney is his best friend, so Albert wants Barney’s total unhappiness to be no more than B. Among all possible burritos that satisfy the above constrains, Albert wants to choose one that maximises his total joy.<br/>
Your task is to help Albert to choose s_i to satisfy these conditions or to find out that there is no solution.<br/>


## 입력
The first line contains three integers n, A, and B (1 ≤ n ≤ 100 000, 0 ≤ A, B ≤ 10^9), the number of ingredients, the least amount of Albert’s joy and the maximal amount of Barney’s unhappiness. Each of the following n lines contains a description of an ingredient: three integers g_i, a_i, b_i (0 ≤ g_i, a_i, b_i ≤ 100) — the maximal number of grams allowed, the amount of Albert’s joy per gram and the amount of Barney’s unhappiness per gram.


## 출력
The first line of the output must contain two real numbers — the maximal amount of his joy and the amount of Barney’s unhappiness that Albert can obtain, satisfying the conditions in the problem statement, or “−1 −1”, if Albert cannot satisfy the conditions.<br/>
If the conditions are satisfiable the second line must contain n real numbers — the amount of each ingredient in grams.<br/>
Your output must have an absolute or relative error of at most 10^−8.<br/>
Any way to reach maximal Albert’s joy that satisfies the given conditions can be printed.<br/>


## 예제 입력
2 5 5<br/>
2 2 1<br/>
2 2 4<br/>


## 예제 출력
5.5 5<br/>
2 0.75<br/>


## 풀이
문제에서 요구하는건 불행도는 B이하 중 가장 높게 설정하고 A가 최대가 되게 해야한다.<br/>
그래서 그리디로 B / A의 값이 가장 작은거부터 채워넣는데 B를 초과하지 않게 채워넣으면 된다.<br/>
오차 10^-8까지 허용한다 했고 들어오는 수는 10^9이므로 decimal로 연산했다.<br/>
로직은 맞는거 같은데 5번 연달아 틀렸고, 케이스를 검색해 확인하니 오차를 잘못 설정함을 확인했다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/10590