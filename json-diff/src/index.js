import { updateAuthState, initAuth } from "./auth.js";
import { initNavigation } from "./navigation.js";
import { initJsonValidator } from "./json-validator.js";

document.addEventListener('DOMContentLoaded', () => {
  const username = localStorage.getItem('username');
  updateAuthState(username);

  initAuth();

  initNavigation();

  initJsonValidator();
});