const USERNAME_KEY = "username";

export const getUsername = () => {
  return localStorage.getItem(USERNAME_KEY);
};

export const setUsername = (username) => {
  localStorage.setItem(USERNAME_KEY, username);
};

export const clearUsername = () => {
  localStorage.removeItem(USERNAME_KEY);
};

export const isAuthenticated = () => {
  return !!getUsername();
};
