// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {

    $('.carousel').carousel();
    $('.carousel').carousel('prev');
    $('.carousel').carousel('next');
    //$("#submit").on("click", function () {

    //    $("#aid").attr("disabled", "disabled");
    //});
    $('#sub').submit(function () {
        alert("error");
    });


});