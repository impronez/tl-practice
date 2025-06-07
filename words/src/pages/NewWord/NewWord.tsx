import { useNavigate } from "react-router";
import { WordEditor } from "../../components/WordEditor/WordEditor";
import { useDictionaryStore } from "../../stores/useDictionaryStore";

export const NewWord = () => {
  const navigate = useNavigate();
  const saveWord = useDictionaryStore((state) => state.add);

  const handleSaveButtonClick = (original: string, translation: string) => {
    if (original == "" || translation == "") return;

    saveWord({ original: original.trim(), translation: translation.trim() });
    navigate(-1);
  };

  return (
    <WordEditor
      onSubmit={handleSaveButtonClick}
      onCancel={() => navigate(-1)}
    />
  );
};
