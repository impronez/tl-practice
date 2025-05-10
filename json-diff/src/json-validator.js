import { JsonDiff } from "./json-diff.js";

const form = document.querySelector(`.main-form`);
const textareaOld = document.querySelector(`#oldJson`);
const textareaNew = document.querySelector(`#newJson`);
const resultBlock = document.querySelector(`.result`);
const compareButton = document.querySelector(`.main-form button`);

function isValidJSON(jsonString) {
  try {
    JSON.parse(jsonString);
    return true;
  } catch (error) {
    return false;
  }
}

function validateTextarea(textarea) {
  const errorSpan = textarea.nextElementSibling;
  errorSpan.setAttribute("hidden", true);
  if (textarea.value === "") {
    errorSpan.innerHTML = "Обязательное поле";
    errorSpan.removeAttribute("hidden");
    return false;
  } else if (!isValidJSON(textarea.value)) {
    errorSpan.innerHTML = "Некорректный JSON";
    errorSpan.removeAttribute("hidden");
    return false;
  }

  return true;
}

function checkInputs() {
  const isOldValid = validateTextarea(textareaOld);
  const isNewValid = validateTextarea(textareaNew);

  return isOldValid && isNewValid;
}

export function initJsonValidator() {
  form.addEventListener(`submit`, async (event) => {
    event.preventDefault();
    console.log(`submit`);

    if (!checkInputs()) {
      return;
    }

    const defaultButtonHtml = compareButton.innerHTML;
    compareButton.innerHTML = `Loading...`;
    compareButton.disabled = true;

    const oldValue = JSON.parse(textareaOld.value);
    const newValue = JSON.parse(textareaNew.value);
    const result = await JsonDiff.create(oldValue, newValue);

    compareButton.innerHTML = defaultButtonHtml;
    compareButton.disabled = false;

    const jsonResult = JSON.stringify(result, undefined, 2);
    resultBlock.innerHTML = `<pre>${jsonResult}</pre>`;
    resultBlock.classList.add(`result__visible`);
  });
}