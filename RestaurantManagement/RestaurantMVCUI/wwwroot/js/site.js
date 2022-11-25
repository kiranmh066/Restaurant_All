// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
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