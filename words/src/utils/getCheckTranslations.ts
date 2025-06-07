import type { DictionaryWord } from "../types/DictionaryWord";

const MAX_TRANSLATIONS_IN_OPTIONS = 5;

export const getTranslationsForTestCase = (
  current: DictionaryWord,
  allWords: DictionaryWord[]
): string[] => {
  const correct = current.translation.trim().toLowerCase();
  const currentOriginal = current.original.trim().toLowerCase();

  const uniqueTranslations = Array.from(
    new Set(
      allWords
        .filter((w) => {
          const original = w.original.trim().toLowerCase();
          const translation = w.translation.trim().toLowerCase();
          return original !== currentOriginal && translation !== correct;
        })
        .map((w) => w.translation.trim())
    )
  );

  const randomTranslations = uniqueTranslations
    .sort(() => Math.random() - 0.5)
    .slice(0, MAX_TRANSLATIONS_IN_OPTIONS - 1);

  const options = [...randomTranslations, current.translation.trim()].sort(
    () => Math.random() - 0.5
  );

  return options;
};
