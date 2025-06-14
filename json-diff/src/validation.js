export const validateForm = (formElement) => {
  let isValid = true;

  const fields = formElement.querySelectorAll(
    "input[validation-rules], textarea[validation-rules]"
  );

  fields.forEach((field) => {
    const rules = field.getAttribute("validation-rules").split(".");
    const value = field.value.trim();
    const errorSpan = field.nextElementSibling;

    let fieldValid = true;
    let errorMessage = "";

    if (rules.includes("required") && value === "") {
      fieldValid = false;
      errorMessage = "Обязательное поле";
    }

    if (fieldValid && rules.includes("json")) {
      try {
        JSON.parse(value);
      } catch (e) {
        fieldValid = false;
        errorMessage = "Некорректный JSON";
      }
    }

    if (!fieldValid) {
      errorSpan.textContent = errorMessage;
      errorSpan.hidden = false;
      isValid = false;
    } else {
      errorSpan.textContent = "";
      errorSpan.hidden = true;
    }
  });

  return isValid;
};
