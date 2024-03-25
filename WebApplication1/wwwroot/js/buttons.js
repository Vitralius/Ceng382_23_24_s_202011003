function myFunction1(event) {
  event.preventDefault(); // Prevent form submission and page refresh
  var password = document.getElementById("password-item");
  var btnhide = document.getElementById("btn-hide-1");

  if (password.getAttribute("hidden") === null) {
      password.setAttribute("hidden", "");
      btnhide.innerHTML = "Show the Password TextBox";
  } else {
      password.removeAttribute("hidden");
      btnhide.innerHTML = "Hide the Password TextBox";
  }
}

function myFunction2(event) {
  event.preventDefault(); // Prevent form submission and page refresh
  var email = document.getElementById("email-item");
  var btnhide = document.getElementById("btn-hide-2");

  if (email.getAttribute("hidden") === null) {
      email.setAttribute("hidden", "");
      btnhide.innerHTML = "Show the E-mail TextBox";
  } else {
      email.removeAttribute("hidden");
      btnhide.innerHTML = "Hide the E-mail TextBox";
  }
}