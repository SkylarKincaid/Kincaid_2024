

//Define application
function webapp_19() {

    // //Get elements
    // var button01 = document.getElementById("button-01");

    // //Add event listeners
    // button01.addEventListener("click", handleButton01Click);

    // //Functions
    // function handleButton01Click() {
    //     alert("Hello world!");
    // }


    //Get

    var navPage01 = document.getElementById("nav-page-01");
    var navPage02 = document.getElementById("nav-page-02");
    var navPage03 = document.getElementById("nav-page-03");
    var navPage04 = document.getElementById("nav-page-04");

    var page01 = document.getElementById("page-01");
    var page02 = document.getElementById("page-02");
    var page03 = document.getElementById("page-03");
    var page04 = document.getElementById("page-04");

    //Add event listeners

    window.addEventListener('popstate', handlePopState);

    navPage01.addEventListener("click", handleButtonNavPage01Click);
    navPage02.addEventListener("click", handleButtonNavPage02Click);
    navPage03.addEventListener("click", handleButtonNavPage03Click);
    navPage04.addEventListener("click", handleButtonNavPage04Click);

    //Functions

    function handleButtonNavPage01Click(event) {
        event.preventDefault();
        window.history.pushState({}, "", "/" + "Home");
        showPage("Home");
    }

    function handleButtonNavPage02Click(event) {
        event.preventDefault();
        window.history.pushState({}, "", "/" + "About");
        showPage("About");
    }

    function handleButtonNavPage03Click(event) {
        event.preventDefault();
        window.history.pushState({}, "", "/" + "Blog");
        showPage("Blog");
    }

    function handleButtonNavPage04Click(event) {
        event.preventDefault();
        window.history.pushState({}, "", "/" + "Contact");
        showPage("Contact");
    }

    function showPage(page) {
        if (page.toLowerCase() === "home" || page === "") {  //lowercase comparison
            showPage01();
            hidePage02();
            hidePage03();
            hidePage04();
        } else if (page.toLowerCase() === "about") {  //lowercase comparison
            hidePage01();
            showPage02();
            hidePage03();
            hidePage04();
        } else if (page.toLowerCase() === "blog") {  //lowercase comparison
            hidePage01();
            hidePage02();
            showPage03();
            hidePage04();
        } else if (page.toLowerCase() === "contact") {  //lowercase comparison
            hidePage01();
            hidePage02();
            hidePage03();
            showPage04();
        }
    }

    function showPage01() {
        navPage01.classList.add("my-nav-bold");
        page01.classList.remove("visually-hidden");
    }

    function hidePage01() {
        page01.classList.add("visually-hidden");
        navPage01.classList.remove("my-nav-bold");
    }

    function showPage02() {
        navPage02.classList.add("my-nav-bold");
        page02.classList.remove("visually-hidden");
    }

    function hidePage02() {
        page02.classList.add("visually-hidden");
        navPage02.classList.remove("my-nav-bold");
    }

    function showPage03() {
        navPage03.classList.add("my-nav-bold");
        page03.classList.remove("visually-hidden");
    }

    function hidePage03() {
        page03.classList.add("visually-hidden");
        navPage03.classList.remove("my-nav-bold");
    }

    function showPage04() {
        navPage04.classList.add("my-nav-bold");
        page04.classList.remove("visually-hidden");
    }

    function hidePage04() {
        page04.classList.add("visually-hidden");
        navPage04.classList.remove("my-nav-bold");
    }
    function handleNewUrl() {
        var page = window.location.pathname.split('/')[1];

        if (page === "") {
            window.history.replaceState({}, "", "/" + "Home");
        } else {
            window.history.replaceState({}, "", "/" + page);
        }

        showPage(page);
    }

    function handlePopState() {
        var page = window.location.pathname.split('/')[1];
        showPage(page);
    }

    //Execute any functions that need to run on page load
    handleNewUrl();

}

//Run application
webapp_19();
