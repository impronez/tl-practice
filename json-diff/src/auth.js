import { showSection } from './navigation.js';

const loginInfo = document.getElementById('greeting-login');
const greeting = document.getElementById('greeting');
const greetingLoginButton = document.getElementById('greeting-login-button');
const authButton = document.getElementById('auth-button');
const startButton = document.getElementById('start-button');
const loginInput = document.getElementById('login');

export function updateAuthState(username) {
  if (username) {
    loginInfo.textContent = username;
    greetingLoginButton.innerHTML = 'Log out';
    greeting.style.display = 'inline';
    startButton.style.display = 'inline';
  } else {
    greetingLoginButton.textContent = 'Log in';
    greeting.style.display = 'none';
    startButton.style.display = 'none';
  }
}

export function initAuth() {
  authButton.addEventListener('click', (event) => {
    event.preventDefault();
    const username = localStorage.getItem('username');
    
    if (username) {
      localStorage.removeItem('username');
      updateAuthState(null);
      showSection('promo');
    } else {
      if (loginInput.value) {
        localStorage.setItem('username', loginInput.value);
        updateAuthState(loginInput.value);
        showSection('promo');
      } else {
        loginInput.nextElementSibling.removeAttribute('hidden');
      }
    }
  });

  greetingLoginButton.addEventListener('click', () => {
    const username = localStorage.getItem('username');
    if (username) {
      localStorage.removeItem('username');
      updateAuthState(null);
      showSection('promo');
    } else {
      showSection('auth');
    }
  });
}