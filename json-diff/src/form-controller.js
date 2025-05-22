import { validateJSONField } from "./form.js";
import { JsonDiff } from "./json-diff.js";

const checkJsonValidateResults = (
  oldJsonValidateResult,
  newJsonValidateResult
) => {
  const oldJsonErrorSpan = document.querySelector(`#oldJson + span`);
  const newJsonErrorSpan = document.querySelector(`#newJson + span`);

  if (!oldJsonValidateResult.valid) {
    oldJsonErrorSpan.innerHTML = oldJsonValidateResult.error;
    oldJsonErrorSpan.removeAttribute("hidden");
  } else {
    oldJsonErrorSpan.setAttribute("hidden", true);
  }

  if (!newJsonValidateResult.valid) {
    newJsonErrorSpan.innerHTML = newJsonValidateResult.error;
    newJsonErrorSpan.removeAttribute("hidden");
  } else {
    newJsonErrorSpan.setAttribute("hidden", true);
  }

  return oldJsonValidateResult.valid && newJsonValidateResult.valid;
};

export const initJsonValidator = () => {
  const form = document.getElementById("main-form");
  const textareaOld = document.querySelector(`#oldJson`);
  const textareaNew = document.querySelector(`#newJson`);
  const resultBlock = document.querySelector(`.result`);
  const compareButton = document.getElementById("diff-button");

  form.addEventListener(`submit`, async (event) => {
    event.preventDefault();

    const oldJsonValidateResult = validateJSONField(textareaOld.value);
    const newJsonValidateResult = validateJSONField(textareaNew.value);

    if (
      !checkJsonValidateResults(oldJsonValidateResult, newJsonValidateResult)
    ) {
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
};
