import { validateForm } from "./validation.js";
import { Auth } from "./auth.js";
import { Navigation } from "./navigation.js";

const authForm = document.getElementById("login-form");
const loginInput = document.getElementById("login");
const loginInfo = document.getElementById("greeting-login");
const greeting = document.getElementById("greeting");
const greetingLoginButton = document.getElementById("greeting-login-button");
const startButton = document.getElementById("start-button");

const updateGreetingInfo = () => {
  if (Auth.isAuthenticated()) {
    loginInfo.textContent = Auth.getUsername();
    greetingLoginButton.textContent = "Log out";
    greeting.style.display = "inline";
    startButton.style.display = "inline";
  } else {
    greetingLoginButton.textContent = "Log in";
    greeting.style.display = "none";
    startButton.style.display = "none";
  }
};

export const initAuth = () => {
  updateGreetingInfo();

  greetingLoginButton.addEventListener("click", () => {
    if (Auth.isAuthenticated()) {
      Auth.clearUsername();
      updateGreetingInfo();
      Navigation.navigateTo("promo");
    } else {
      Navigation.navigateTo("auth");
    }
  });

  authForm.addEventListener("submit", (event) => {
    event.preventDefault();

    const isFormValid = validateForm(authForm);
    if (!isFormValid) return;

    Auth.setUsername(loginInput.value);
    updateGreetingInfo();
    loginInput.value = "";
    Navigation.navigateTo("promo");
  });
};
