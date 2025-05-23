import { Auth } from "./auth.js";
import { showSection } from "./navigation.js";
import { isNullOrEmpty } from "./validation.js";

const loginInfo = document.getElementById("greeting-login");
const greeting = document.getElementById("greeting");
const greetingLoginButton = document.getElementById("greeting-login-button");
const authButton = document.getElementById("auth-button");
const startButton = document.getElementById("start-button");
const loginInput = document.getElementById("login");

export const updateAuthView = (username) => {
  if (username) {
    loginInfo.textContent = username;
    greetingLoginButton.textContent = "Log out";
    greeting.style.display = "inline";
    startButton.style.display = "inline";
  } else {
    greetingLoginButton.textContent = "Log in";
    greeting.style.display = "none";
    startButton.style.display = "none";
  }
};

const getLoginInputValue = () => loginInput.value.trim();

const clearLoginInput = () => {
  loginInput.value = "";
};

const showLoginError = () => {
  const errorSpan = loginInput.nextElementSibling;
  if (errorSpan) {
    errorSpan.removeAttribute("hidden");
  }
};

export const initAuth = () => {
  updateAuthView(Auth.getUsername());

  greetingLoginButton.addEventListener("click", () => {
    if (Auth.isAuthenticated()) {
      Auth.clearUsername();
      updateAuthView(null);
      showSection("promo");
    } else {
      showSection("auth");
    }
  });

  authButton.addEventListener("click", (event) => {
    event.preventDefault();

    if (Auth.isAuthenticated()) {
      Auth.clearUsername();
      updateAuthView(null);
      showSection("promo");
      return;
    }

    const inputValue = getLoginInputValue();
    if (!inputValue) {
      showLoginError();
      return;
    }

    Auth.setUsername(inputValue);
    updateAuthView(inputValue);
    clearLoginInput();
    showSection("promo");
  });

  loginInput.addEventListener("input", () => {
    const errorSpan = loginInput.nextElementSibling;
    if (!isNullOrEmpty(loginInput.value) && errorSpan) {
      errorSpan.setAttribute("hidden", true);
    }
  });
};
