const logo = document.querySelector('.logo');
const promoSection = document.getElementById('promo');
const authSection = document.getElementById('auth');
const mainSection = document.getElementById('main');

const startButton = document.getElementById('start-button');

export function initNavigation() {
  logo.addEventListener('click', () => {
    showSection('promo');
  });

  startButton.addEventListener('click', () => {
    const username = localStorage.getItem('username');
    
    if (username) {
      showSection('main');
    }
  });
}

export function showSection(sectionId) {
  promoSection.classList.remove('visible');
  authSection.classList.remove('visible');
  mainSection.classList.remove('visible');
  
  document.getElementById(sectionId).classList.add('visible');
}