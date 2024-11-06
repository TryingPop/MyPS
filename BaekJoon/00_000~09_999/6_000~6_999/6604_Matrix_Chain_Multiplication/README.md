# C#

## 난이도 : 실버 3

## 알고리즘 분류
  - 자료 구조
  - 문자열
  - 파싱
  - 스택

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 128 MB

## 문제
#### Suppose you have to evaluate an expression like A*B*C*D*E where A,B,C,D and E are matrices.
#### Since matrix multiplication is associative, the order in which multiplications are performed is arbitrary. However, the number of elementary multiplications needed strongly depends on the evaluation order you choose.
#### For example, let A be a 50*10 matrix, B a 10*20 matrix and C a 20*5 matrix.
#### There are two different strategies to compute A*B*C, namely (A*B)*C and A*(B*C).
#### The first one takes 15000 elementary multiplications, but the second one only 3500.
#### Your job is to write a program that determines the number of elementary multiplications needed for a given evaluation strategy.

## 입력
#### Input consists of two parts: a list of matrices and a list of expressions.
#### The first line of the input file contains one integer n (1 n n lines each contain one capital letter, specifying the name of the matrix, and two integers, specifying the number of rows and columns of the matrix.
#### The second part of the input file strictly adheres to the following syntax (given in EBNF):
	SecondPart = Line { Line } <EOF>
	Line       = Expression <CR>
	Expression = Matrix | "(" Expression Expression ")"
	Matrix     = "A" | "B" | "C" | ... | "X" | "Y" | "Z"

## 출력
#### For each expression found in the second part of the input file, print one line containing the word "error" if evaluation of the expression leads to an error due to non-matching matrices. Otherwise print one line containing the number of elementary multiplications needed to evaluate the expression in the way specified by the parentheses.

## 예제 입력
9<br/>
A 50 10<br/>
B 10 20<br/>
C 20 5<br/>
D 30 35<br/>
E 35 15<br/>
F 15 5<br/>
G 5 10<br/>
H 10 20<br/>
I 20 25<br/>
A<br/>
B<br/>
C<br/>
(AA)<br/>
(AB)<br/>
(AC)<br/>
(A(BC))<br/>
((AB)C)<br/>
(((((DE)F)G)H)I)<br/>
(D(E(F(G(HI)))))<br/>
((D(EF))((GH)I))<br/>

## 예제 출력
0<br/>
0<br/>
0<br/>
error<br/>
10000<br/>
error<br/>
3500<br/>
15000<br/>
40500<br/>
47500<br/>
15125<br/>

## 문제 링크
https://www.acmicpc.net/problem/6604