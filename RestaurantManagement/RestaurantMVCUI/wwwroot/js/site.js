// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $('#sidebarCollapse').on('click', function () {
        $('#sidebar').toggleClass('active');
    });
});


function init() {
    let res_elm = document.createElement("div");
    res_elm.innerHTML = "Hello Myself Aco, How can I help you?";
    res_elm.setAttribute("class", "left");

    document.getElementById('msg').appendChild(res_elm);
}


document.getElementById('reply').addEventListener("click", async (e) => {
    e.preventDefault();

    var req = document.getElementById('msg_send').value;

    if (req == undefined || req == "") {

    }
    else {

        var res = "";
        await axios.get(`https://api.monkedev.com/fun/chat?msg=${req}`).then(data => {
            res = JSON.stringify(data.data.response)
        })

        let data_req = document.createElement('div');
        let data_res = document.createElement('div');

        let container1 = document.createElement('div');
        let container2 = document.createElement('div');

        container1.setAttribute("class", "msgCon1");
        container2.setAttribute("class", "msgCon2");

        data_req.innerHTML = req;
        data_res.innerHTML = res;


        data_req.setAttribute("class", "right");
        data_res.setAttribute("class", "left");

        let message = document.getElementById('msg');


        message.appendChild(container1);
        message.appendChild(container2);

        container1.appendChild(data_req);
        container2.appendChild(data_res);

        document.getElementById('msg_send').value = "";

        function scroll() {
            var scrollMsg = document.getElementById('msg')
            scrollMsg.scrollTop = scrollMsg.scrollHeight;
        }
        scroll();

    }


});

let nav = document.querySelector("nav");
let scrollBtn = document.querySelector(".scroll-button a");
console.log(scrollBtn);
let val;
window.onscroll = function () {
    if (document.documentElement.scrollTop > 20) {
        nav.classList.add("sticky");
        scrollBtn.style.display = "block";
    } else {
        nav.classList.remove("sticky");
        scrollBtn.style.display = "none";
    }



}

function deleteFunction() {
    let val = confirm("Are you sure.You want to delete???"); if (val == false) { return event.preventDefault(); }
}
function confirmFunction() {
    let val = confirm("Are you sure.You want to confirm your order?"); if (val == false) { return event.preventDefault(); }
}
function clearcartFunction() {
    let val = confirm("Are you sure.You want to clear your cart?"); if (val == false) { return event.preventDefault(); }
}
function cancelallorderFunction() {
    let val = confirm("Are you sure.You want to cancel all order?"); if (val == false) { return event.preventDefault(); }
}
$(document).ready(function () {
    //Menu Toggle Script
    $("#menu-toggle").click(function (e) {
        e.preventDefault();
        $("#wrapper").toggleClass("toggled");
    });

    // For highlighting activated tabs
    $("#tab1").click(function () {
        $(".tabs").removeClass("active1");
        $(".tabs").addClass("bg-light");
        $("#tab1").addClass("active1");
        $("#tab1").removeClass("bg-light");
    });
    $("#tab2").click(function () {
        $(".tabs").removeClass("active1");
        $(".tabs").addClass("bg-light");
        $("#tab2").addClass("active1");
        $("#tab2").removeClass("bg-light");
    });
    $("#tab3").click(function () {
        $(".tabs").removeClass("active1");
        $(".tabs").addClass("bg-light");
        $("#tab3").addClass("active1");
        $("#tab3").removeClass("bg-light");
    });
})
$(document).ready(function () {
    $('#sidebarCollapse').on('click', function () {
        $('#sidebar').toggleClass('active');
    });
});
let nav = document.querySelector("nav");
let scrollBtn = document.querySelector(".scroll-button a");
console.log(scrollBtn);
let val;
window.onscroll = function () {
    if (document.documentElement.scrollTop > 20) {
        nav.classList.add("sticky");
        scrollBtn.style.display = "block";
    } else {
        nav.classList.remove("sticky");
        scrollBtn.style.display = "none";
    }





}



function deleteFunction() {
    let val = confirm("Are you sure.You want to delete???"); if (val == false) { return event.preventDefault(); }
}
function confirmFunction() {
    let val = confirm("Are you sure.You want to confirm your order?"); if (val == false) { return event.preventDefault(); }
}
function clearcartFunction() {
    let val = confirm("Are you sure.You want to clear your cart?"); if (val == false) { return event.preventDefault(); }
}
function cancelallorderFunction() {
    let val = confirm("Are you sure.You want to cancel all order?"); if (val == false) { return event.preventDefault(); }
}
$(document).ready(function () {
    //Menu Toggle Script
    $("#menu-toggle").click(function (e) {
        e.preventDefault();
        $("#wrapper").toggleClass("toggled");
    });



    // For highlighting activated tabs
    $("#tab1").click(function () {
        $(".tabs").removeClass("active1");
        $(".tabs").addClass("bg-light");
        $("#tab1").addClass("active1");
        $("#tab1").removeClass("bg-light");
    });
    $("#tab2").click(function () {
        $(".tabs").removeClass("active1");
        $(".tabs").addClass("bg-light");
        $("#tab2").addClass("active1");
        $("#tab2").removeClass("bg-light");
    });
    $("#tab3").click(function () {
        $(".tabs").removeClass("active1");
        $(".tabs").addClass("bg-light");
        $("#tab3").addClass("active1");
        $("#tab3").removeClass("bg-light");
    });
})
