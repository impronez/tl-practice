import { validateForm } from "./validation.js";
import { JsonDiff } from "./json-diff.js";

export const initJsonDiff = () => {
  const form = document.getElementById("main-form");
  const textareaOld = document.querySelector(`#oldJson`);
  const textareaNew = document.querySelector(`#newJson`);
  const resultBlock = document.querySelector(`.result`);
  const compareButton = document.getElementById("diff-button");

  form.addEventListener(`submit`, async (event) => {
    event.preventDefault();

    const isFormValid = validateForm(form);
    if (!isFormValid) return;

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
};
