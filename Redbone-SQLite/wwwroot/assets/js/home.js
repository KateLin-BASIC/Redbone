//타자기 효과 설정
let app = document.getElementById('app');

let typewriter = new Typewriter(app, {
    loop: true,
    delay: 75,
});

typewriter
    .typeString("논리보단 유머를 더 사랑하는 이상한 개발자")
    .pauseFor(5000)
    .deleteAll()
    .typeString("미래에 뭐 먹고 살지 모르겠는 개발자")
    .pauseFor(5000)
    .deleteAll()
    .typeString("윈도우 서버를 포기 못 하고 있었지만 이제 포기한 개발자")
    .pauseFor(5000)
    .deleteAll()
    .typeString("수십 년 전 운영체제를 너무 좋아하는 개발자")
    .pauseFor(5000)
    .deleteAll()
    .start();