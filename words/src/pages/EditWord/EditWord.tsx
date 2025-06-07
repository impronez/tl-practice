import { useLocation, useNavigate } from "react-router";
import { WordEditor } from "../../components/WordEditor/WordEditor";
import type { DictionaryWord } from "../../types/DictionaryWord";
import { useDictionaryStore } from "../../stores/useDictionaryStore";
import { useEffect } from "react";

export const EditWord = () => {
  const navigate = useNavigate();
  const location = useLocation();
  const word = location.state as DictionaryWord;

  useEffect(() => {
    if (!word) {
      navigate("/dictionary");
    }
  }, [word, navigate]);

  if (!word) return null;

  const editWord = useDictionaryStore((state) => state.edit);

  const handleSaveButtonClick = (original: string, translation: string) => {
    if (original == "" || translation == "") return;

    editWord(word, {
      original: original.trim(),
      translation: translation.trim(),
    });
    navigate(-1);
  };

  return (
    <WordEditor
      inputOriginal={word.original}
      inputTranslation={word.translation}
      onCancel={() => navigate(-1)}
      onSubmit={handleSaveButtonClick}
    />
  );
};
