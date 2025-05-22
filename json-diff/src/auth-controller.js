import {
  getUsername,
  setUsername,
  clearUsername,
  isAuthenticated,
} from "./auth.js";
import { showSection } from "./navigation.js";

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

export const getLoginInputValue = () => loginInput.value.trim();

export const clearLoginInput = () => {
  loginInput.value = "";
};

export const showLoginError = () => {
  const errorSpan = loginInput.nextElementSibling;
  if (errorSpan) {
    errorSpan.removeAttribute("hidden");
  }
};

export const initAuth = () => {
  updateAuthView(getUsername());

  greetingLoginButton.addEventListener("click", () => {
    if (isAuthenticated()) {
      clearUsername();
      updateAuthView(null);
      showSection("promo");
    } else {
      showSection("auth");
    }
  });

  authButton.addEventListener("click", (event) => {
    event.preventDefault();

    if (isAuthenticated()) {
      clearUsername();
      updateAuthView(null);
      showSection("promo");
      return;
    }

    const inputValue = getLoginInputValue();
    if (!inputValue) {
      showLoginError();
      return;
    }

    setUsername(inputValue);
    updateAuthView(inputValue);
    clearLoginInput();
    showSection("promo");
  });

  loginInput.addEventListener("input", () => {
    const errorSpan = loginInput.nextElementSibling;
    if (loginInput.value.trim() !== "" && errorSpan) {
      errorSpan.setAttribute("hidden", true);
    }
  });
};
