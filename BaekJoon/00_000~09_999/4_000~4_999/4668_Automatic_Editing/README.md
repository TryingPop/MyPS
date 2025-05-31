# C#

## 난이도 : 실버 4

## 알고리즘 분류
  - 구현
  - 문자열

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 128 MB

## 문제
Text-processing tools like awk and sed allow you to automatically perform a sequence of editing operations based on a script. For this problem we consider the specific case in which we want to perform a series of string replacements, within a single line of text, based on a fixed set of rules. Each rule specifies the string to find, and the string to replace it with, as shown below.<br/>

|Rule|Find|Replace-by|
|:---:|:---:|:---:|
|1.|ban|bab|
|2.|baba|be|
|3.|ana|any|
|4.|ba b|hind the g|

To perform the edits for a given line of text, start with the first rule. Replace the first occurrence of the find string within the text by the replace-by string, then try to perform the same replacement again on the new text. Continue until the find string no longer occurs within the text, and then move on to the next rule. Continue until all the rules have been considered. Note that (1) when searching for a find string, you always start searching at the beginning of the text, (2) once you have finished using a rule (because the find string no longer occurs) you never use that rule again, and (3) case is significant.<br/>
For example, suppose we start with the line<br/>
banana boat<br/>
and apply these rules. The sequence of transformations is shown below, where occurrences of a find string are underlined and replacements are boldfaced. Note that rule 1 was used twice, then rule 2 was used once, then rule 3 was used zero times, and then rule 4 was used once.<br/>

|Before|After|
|:---:|:---:|
|~ban~ana boat|*bab*ana boat|
|ba~ban~a boat|ba*bab*a boat|
|~baba~ba boat|*be*ba boat|
|be~ba bo~at|be*hind the g*oat|


## 입력
The input contains one or more test cases, followed by a line containing only 0 (zero) that signals the end of the file. Each test case begins with a line containing the number of rules, which will be between 1 and 10. Each rule is specified by a pair of lines, where the first line is the find string and the second line is the replace-by string. Following all the rules is a line containing the text to edit.<br/>


## 출력
For each test case, output a line containing the final edited text.<br/>


## 예제 입력
4<br/>
ban<br/>
bab<br/>
baba<br/>
be<br/>
ana<br/>
any<br/>
ba b<br/>
hind the g<br/>
banana boat<br/>
1<br/>
t<br/>
sh<br/>
toe or top<br/>
0<br/>


## 예제 출력
behind the goat<br/>
shoe or shop<br/>


## 힌트
Both find and replace-by strings will be at most 80 characters long. Find strings will contain at least one character, but replace-by strings may be empty (indicated in the input file by an empty line). During the edit process the text may grow as large as 255 characters, but the final output text will be less than 80 characters long.<br/>
The first test case in the sample input below corresponds to the example shown above.<br/>


## 풀이
1번부터 n번까지 바꿀 수 있는 만큼 바꾸면 된다.<br/>
그리고 마지막 변환된 문장을 출력하면 된다.<br/>


다만 string.Replace로 바꿀 수 있는 만큼 변환을 시도하니 계속해서 틀렸다.<br/>
내부 코드를 보니 찾기 -> 변환 -> 찾기 변환이 아닌 한 번에 일치하는 문자를 모두 찾아 시작 위치를 저장하고 찾는 과정이 끝나면 찾은 인덱스로 변환을 진행한다.<br/>


그래서 다음과 같은 반례가 있다.<br/>
bananana에서 ana를 cba로 변환한다고 하자.<br/>
그러면 원하는건 bananana -> bcbanana -> bcbcbana -> bcbcbcba이다.<br/>
만약 string.Replace를 쓴다면 bananana -> bcbancba로 끝난다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/4668