# C#

## 난이도 : 실버 1

## 알고리즘 분류
  - 수학
  - 트리

## 제한조건
  - 시간 제한 : 2초
  - 메모리 제한 : 512 MB

## 문제
We inductively label the nodes of a rooted binary tree with an infinite number of nodes as follows:<br/>

  - The root is labeled by 1/1,
  - If the label of a node is p/q, then
    - The label of its left child is p/(p+q), and
    - The label of its right child is (p+q)/q. 

By having this tree in our hand, we define a rational sequence a1, a2, a3, … by a breadth first traversal of the tree in such a way that nodes in the same level are visited from left to right. Therefore, we have a1 = 1/1, a2 = 1/2, a3 = 2/1, a4 = 1/3, a5 = 3/2, …<br/>
You are to write a program that gets values p and q and computes an integer n for which an = p/q.<br/>


## 입력
The first line of the input includes the number of test cases, 1 ≤ t ≤ 1000. Each test case consists of one line. This line contains p, followed by / and then q without any space between them.<br/>


## 출력
For each test case, output in one line an integer n for which an = p/q. It is guaranteed that in all test cases n fits in a 32-bit integer.<br/>


## 예제 입력
4<br/>
1/1<br/>
1/3<br/>
5/2<br/>
2178309/1346269<br/>


## 예제 출력
1<br/>
4<br/>
11<br/>
1431655765<br/>


## 풀이
2/2와 같은 분수처럼 만들어질 수 없는 경우도 존재한다.<br/>
수학적 귀납법으로 언제나 p, q ≥ 1임을 알 수 있고 이는 p + q > p, q이다.<br/>
그래서 p = q인 p/q는 1/1로 유일함을 알 수 있다.<br/>


문제에서 따로 존재하지 않는 경우 처리하는 방법이 없다.<br/>
만들어질 수 없는 경우가 입력되면 질문게시판을 이용한다는 마음으로 무시하고 풀었다.<br/>
(고려하지 않아도 이상없이 통과된다.)<br/>


이제 몇 번째 인지 찾아야 한다.<br/>
해당 수열을 역과정으로 하면서 1=1로 가는데 몇 번의 연산으로 가는지로 확인한다.<br/>
그래프의 형태가 이진 트리이므로 연산횟수를 y라하면 2^y ~ 2^(y+1) - 1임을 알 수 있다.<br/>


범위에서 몇 번째인지 확인해야 하는게 주된 쟁점이다.<br/>
이는 분자가 분모보다 큰가로 확인할 수 있다.<br/>


1/1에서 오른쪽 자식은 (p+q)/q이므로 언제나 분자가 분모보다 크다.<br/>
마찬가지로 왼쪽 자식은 p/(p+q)이므로 언제나 분모가 분자보다 크다.<br/>


i번째 역연산에서 분자가 분모보다 큰 경우 왼쪽에서 떨어진 정도는 2^(i-1)임을 알 수 있다.<br/>
이렇게 누적하면서 왼쪽에서 떨어진 정도 x를 찾았다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/13077