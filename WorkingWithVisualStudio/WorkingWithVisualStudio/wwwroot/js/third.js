document.addEventListener("DOMContentLoaded", function () {
    var element = document.createElement("p");
    element.textContent = "This is the element from the third.js file (updated)";
    document.querySelector("body").appendChild(element);
});