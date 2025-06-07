# C#

## 난이도 : 실버 2

## 알고리즘 분류
  - 자료 구조
  - 문자열
  - 해시를 사용한 집합과 맵
  - 트리를 사용한 집합과 맵

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 128 MB

## 문제
Peter studies the theory of relational databases. Table in the relational database consists of values that are arranged in rows and columns.<br/>
There are different normal forms that database may adhere to. Normal forms are designed to minimize the redundancy of data in the database. For example, a database table for a library might have a row for each book and columns for book name, book author, and author’s email.<br/>
If the same author wrote several books, then this representation is clearly redundant. To formally define this kind of redundancy Peter has introduced his own normal form. A table is in Peter’s Normal Form (PNF) if and only if there is no pair of rows and a pair of columns such that the values in the corresponding columns are the same for both rows.<br/>

|책 이름|저자|이메일|
|:---:|:---:|:---:|
|How to compete in ACM ICPC|Peter|peter@neerc.ifmo.ru|
|How to win ACM ICPC|Michael|michael@neerc.ifmo.ru|
|Notes from ACM ICPC champion|Michael|michael@neerc.ifmo.ru|

The above table is clearly not in PNF, since values for 2rd and 3rd columns repeat in 2nd and 3rd rows. However, if we introduce unique author identifier and split this table into two tables — one containing book name and author id, and the other containing book id, author name, and author email, then both resulting tables will be in PNF.<br/>

|책 이름|ID|
|:---:|:---:|
|How to compete in ACM ICPC|1|
|How to win ACM ICPC|2|
|Notes from ACM ICPC champion|2|


|ID|저자|이메일
|:---:|:---:|:---:|
|1|Peter|peter@neerc.ifmo.ru|
|2|Michael|michael@neerc.ifmo.ru|


Given a table your task is to figure out whether it is in PNF or not.<br/>


## 입력
The first line of the input file contains two integer numbers n and m (1 ≤ n ≤ 10 000, 1 ≤ m ≤ 10), the number of rows and columns in the table. The following n lines contain table rows. Each row has m column values separated by commas. Column values consist of ASCII characters from space (ASCII code 32) to tilde (ASCII code 126) with the exception of comma (ASCII code 44). Values are not empty and have no leading and trailing spaces. Each row has at most 80 characters (including separating commas).<br/>


## 출력
If the table is in PNF write to the output file a single word “YES” (without quotes). If the table is not in PNF, then write three lines. On the first line write a single word “NO” (without quotes). On the second line write two integer row numbers r1 and r2 (1 ≤ r1, r2 ≤ n, r1 ≠ r2), on the third line write two integer column numbers c1 and c2 (1 ≤ c1, c2 ≤ m, c1 ≠ c2), so that values in columns c1 and c2 are the same in rows r1 and r2.<br/>


## 예제 입력
3 3<br/>
How to compete in ACM ICPC,Peter,peter@neerc.ifmo.ru<br/>
How to win ACM ICPC,Michael,michael@neerc.ifmo.ru<br/>
Notes from ACM ICPC champion,Michael,michael@neerc.ifmo.ru<br/>


## 예제 출력
NO<br/>
2 3<br/>
2 3<br/>


## 풀이
문제를 보면 서로 다른 두 행에 대해 서로 다른 두 열의 값이 같은게 존재하는지 확인하면 된다.<br/>
그래서 2열을 고정하고 각 행을 조사해 같은게 있는지 판별했다.<br/>
같은건 Dictionary 자료구조에 문자열 2개를 포함하는 ValueTuple을 키로 넣고 해당하는 행 r을 값으로 넣었다.<br/>
그래서 해당 값이 포함되면 이전 행과 현재 행 그리고 선택된 열 2개를 반환했다.<br/>
해당 방법으로 접근하면 Dictionary에 저장과 확인하는걸 O(1)이라하면 m^2 x n = 100만의 시간이 걸린다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/3542