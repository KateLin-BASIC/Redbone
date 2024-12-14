# [Redbone](https://katelin.xyz)

> [C#](https://learn.microsoft.com/ko-kr/dotnet/csharp/)으로 작성된 [ASP.Net](https://asp.net) 포트폴리오 + 블로그 웹 사이트

## 요구 사항

 1. MySQL 또는 MariaDB 서버 (SQL 서버를 사용할 경우에만)
 2. .NET 8.0 이상

## 설정하기
### SQL 서버 버전
 1. [appsettings.json](https://github.com/KateLin-BASIC/Redbone/blob/master/Redbone/appsettings.json)을 알맞게 설정합니다. (관리자 계정 이름, Github 계정 등...)
 2. 환경 변수 `REDBONE_CONNECTION_STRING`, `REDBONE_ADMIN_HASH` 를 추가하시고 각각 MySQL 연결 문자열과 비밀번호 해시를 입력합니다.
 3. [Database.sql](https://github.com/KateLin-BASIC/Redbone/blob/master/Redbone/Database.sql)을 SQL 서버에서 실행합니다.
 4. 이제 끝입니다! 프로그램을 시작하면 웹 서버가 자동으로 시작됩니다.

### SQLite 버전
 1. [appsettings.json](https://github.com/KateLin-BASIC/Redbone/blob/master/Redbone-SQLite/appsettings.json)을 알맞게 설정합니다. (관리자 계정 이름, Github 계정 등...)
 2. 환경 변수 `REDBONE_ADMIN_HASH` 를 추가하시고 비밀번호 해시를 입력합니다.
 3. 이제 끝입니다! 프로그램을 시작하면 자동적으로 데이터베이스가 생성됩니다.