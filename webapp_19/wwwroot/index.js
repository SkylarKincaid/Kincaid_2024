JS CODE

//Define application
function webapp_19() {

    //Get elements
    var button01 = document.getElementById("button-01");

    //Add event listeners
    button01.addEventListener("click", handleButton01Click);

    //Functions
    function handleButton01Click() {
        alert("Hello world!");
    }
}

//Run application
webapp_19();
