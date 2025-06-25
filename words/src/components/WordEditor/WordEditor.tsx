import styles from "./WordEditor.module.scss";
import {
  Button,
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableRow,
  TextField,
  Typography,
} from "@mui/material";
import { useState } from "react";

interface WordEditorProps {
  inputOriginal?: string;
  inputTranslation?: string;
  onSubmit: (original: string, translation: string) => void;
  onCancel: () => void;
}

export const WordEditor = ({
  inputOriginal,
  inputTranslation,
  onSubmit,
  onCancel,
}: WordEditorProps) => {
  const [original, setOriginal] = useState(inputOriginal ?? "");
  const [translation, setTranslation] = useState(inputTranslation ?? "");

  const canSubmit =
    original != "" &&
    translation != "" &&
    (original != inputOriginal || translation != inputTranslation);

  const array = [
    {
      id: "word-original",
      value: original,
      label: "Слово на русском языке",
      set: setOriginal,
    },
    {
      id: "word-translation",
      value: translation,
      label: "Перевод на английский язык",
      set: setTranslation,
    },
  ];

  const handleSubmit = () => {
    if (original == undefined || translation == undefined || !canSubmit) return;

    onSubmit(original, translation);
  };

  return (
    <div className={styles.wordEditorWrapper}>
      <Table className={styles.wordEditorTableWrapper}>
        <TableHead>
          <TableRow>
            <TableCell>
              <Typography variant="h5" component="h1">
                Словарное слово
              </Typography>
            </TableCell>
            <TableCell />
          </TableRow>
        </TableHead>
        <TableBody>
          {array.map((word, key) => (
            <TableRow key={key}>
              <TableCell
                align="left"
                className={`${styles.wordEditorTableCell} ${styles.wordEditorInputLabel}`}
              >
                <label>{word.label}</label>
              </TableCell>
              <TableCell align="left" className={styles.wordEditorTableCell}>
                <TextField
                  error={word.value === ""}
                  id={word.id}
                  size="small"
                  value={word.value ?? ""}
                  onChange={(e) => word.set(e.target.value)}
                />
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
      <div className={styles.wordEditorMenu}>
        <Button
          variant="contained"
          onClick={handleSubmit}
          disabled={!canSubmit}
        >
          Сохранить
        </Button>
        <Button variant="outlined" onClick={onCancel}>
          Отменить
        </Button>
      </div>
    </div>
  );
};
