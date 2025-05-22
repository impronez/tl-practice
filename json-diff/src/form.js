const isValidJSON = (jsonString) => {
  try {
    JSON.parse(jsonString);
    return true;
  } catch {
    return false;
  }
};

export const validateJSONField = (value) => {
  if (value.trim() === "") {
    return { valid: false, error: "Обязательное поле" };
  }
  if (!isValidJSON(value)) {
    return { valid: false, error: "Некорректный JSON" };
  }
  return { valid: true };
};
