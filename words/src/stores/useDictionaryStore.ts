import { create } from "zustand";
import type { DictionaryWord } from "../types/DictionaryWord";
import { persist } from "zustand/middleware";

interface DictionaryStore {
  words: DictionaryWord[];
  add: (dWord: DictionaryWord) => void;
  remove: (dWord: DictionaryWord) => void;
  edit: (oldValue: DictionaryWord, newValue: DictionaryWord) => void;
}

export const useDictionaryStore = create<DictionaryStore>()(
  persist(
    (set) => ({
      words: [],

      add: (dWord: DictionaryWord) => {
        set((state) => {
          const isDuplicate = state.words.some(
            (w) =>
              w.original.trim().toLowerCase() ===
                dWord.original.trim().toLowerCase() &&
              w.translation.trim().toLowerCase() ===
                dWord.translation.trim().toLowerCase()
          );

          return isDuplicate ? state : { words: [...state.words, dWord] };
        });
      },

      remove: (dWord: DictionaryWord) => {
        set((state) => ({
          words: state.words.filter(
            (w) =>
              !(
                w.original === dWord.original &&
                w.translation === dWord.translation
              )
          ),
        }));
      },

      edit: (oldValue: DictionaryWord, newValue: DictionaryWord) => {
        set((state) => ({
          words: state.words.map((w) =>
            w.original === oldValue.original &&
            w.translation === oldValue.translation
              ? newValue
              : w
          ),
        }));
      },
    }),
    {
      name: "dictionary-storage",
    }
  )
);
