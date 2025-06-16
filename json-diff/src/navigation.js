const logo = document.querySelector(".logo");
const promoSection = document.getElementById("promo");
const authSection = document.getElementById("auth");
const mainSection = document.getElementById("main");

const startButton = document.getElementById("start-button");

const init = () => {
  logo.addEventListener("click", () => {
    navigateTo("promo");
  });

  startButton.addEventListener("click", () => {
    const username = localStorage.getItem("username");

    if (username) {
      navigateTo("main");
    }
  });
};

const navigateTo = (sectionId) => {
  promoSection.classList.remove("visible");
  authSection.classList.remove("visible");
  mainSection.classList.remove("visible");

  document.getElementById(sectionId).classList.add("visible");
};

export const Navigation = {
  init,
  navigateTo,
};
