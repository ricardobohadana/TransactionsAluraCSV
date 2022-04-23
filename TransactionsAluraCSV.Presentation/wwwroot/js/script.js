window.onload = function () {
    const burger = document.getElementsByClassName("navbar-burger")[0];

    burger.addEventListener('click', () => {
        burger.classList.contains("is-active") ? burger.classList.remove("is-active") : burger.classList.add("is-active")
    })
}