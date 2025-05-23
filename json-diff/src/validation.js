const isValidJSON = (jsonString) => {
  try {
    JSON.parse(jsonString);
    return true;
  } catch {
    return false;
  }
};

export const isNullOrEmpty = (value) => {
  return value === null || value === undefined || value.trim() === "";
};

export const validateField = (value, rules = []) => {
  if (rules.includes(`required`) && isNullOrEmpty(value)) {
    return { valid: false, error: "Обязательное поле" };
  }
  if (rules.includes(`json`) && !isValidJSON(value)) {
    return { valid: false, error: "Некорректный JSON" };
  }
  return { valid: true };
};
