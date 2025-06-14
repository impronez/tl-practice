import styles from "./Dictionary.module.scss";
import {
  Button,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
} from "@mui/material";
import { useNavigate } from "react-router";
import { useDictionaryStore } from "../../stores/useDictionaryStore";
import { DictionaryAction } from "./components/DictionaryAction";

export const Dictionary = () => {
  const navigate = useNavigate();
  const words = useDictionaryStore((state) => state.words);

  return (
    <div className={styles.dictionaryWrapper}>
      <Button
        variant="contained"
        size="large"
        onClick={() => navigate("/new-word")}
      >
        + Добавить слово
      </Button>
      <TableContainer sx={{ backgroundColor: "#fff" }}>
        <Table>
          <TableHead sx={{ backgroundColor: "#e0e4e9" }}>
            <TableRow>
              <TableCell>Слово на русском языке</TableCell>
              <TableCell>Перевод на английский язык</TableCell>
              <TableCell>Действие</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {words.map((word, key) => (
              <TableRow key={key}>
                <TableCell>{word.original}</TableCell>
                <TableCell>{word.translation}</TableCell>
                <TableCell>
                  <DictionaryAction word={word} />
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    </div>
  );
};
