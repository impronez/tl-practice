const USERNAME_KEY = "username";

const getUsername = () => {
  return localStorage.getItem(USERNAME_KEY);
};

const setUsername = (username) => {
  localStorage.setItem(USERNAME_KEY, username);
};

const clearUsername = () => {
  localStorage.removeItem(USERNAME_KEY);
};

const isAuthenticated = () => {
  return !!getUsername();
};

export const Auth = {
  getUsername,
  setUsername,
  clearUsername,
  isAuthenticated,
};
