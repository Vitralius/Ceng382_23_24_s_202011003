function myFunction1(event) {
  event.preventDefault();
  var password = document.getElementById("password-item");
  var btnhide = document.getElementById("btn-hide-1");

  if (password.style.getPropertyValue("display") === "block") {
      password.style.setProperty("display", "none");
      btnhide.innerHTML = "Show the Password TextBox";
  } else {
      password.style.setProperty("display", "block");
      btnhide.innerHTML = "Hide the Password TextBox";
  }
}

function myFunction2(event) {
  event.preventDefault();
  var email = document.getElementById("email-item");
  var btnhide = document.getElementById("btn-hide-2");

  if (email.style.getPropertyValue("display") === "block") {
      email.style.setProperty("display", "none");
      btnhide.innerHTML = "Show the E-mail TextBox";
  } else {
      email.style.setProperty("display", "block");
      btnhide.innerHTML = "Hide the E-mail TextBox";
  }
}