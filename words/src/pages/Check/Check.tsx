import { useState } from "react";
import styles from "./Check.module.scss";
import {
  Button,
  MenuItem,
  Select,
  Table,
  TableBody,
  TableCell,
  TableRow,
  TextField,
  Typography,
} from "@mui/material";
import { useDictionaryStore } from "../../stores/useDictionaryStore";
import { getTranslationsForTestCase } from "../../utils/getCheckTranslations";
import { useNavigate } from "react-router";

export const Check = () => {
  const navigate = useNavigate();

  const words = useDictionaryStore((state) => state.words);
  if (words.length == 0)
    return (
      <Typography align="left" fontSize={24}>
        Словарь пуст
      </Typography>
    );

  const [answer, setAnswer] = useState("");
  const isDisabled = answer == "";

  const [currentIndex, setCurrentIndex] = useState(0);
  const [correctCount, setCorrectCount] = useState(0);

  const currentWord = words[currentIndex];

  const options = getTranslationsForTestCase(currentWord, words);

  const handleCheck = () => {
    const isCorrect = answer === currentWord.translation;
    const newCorrectCount = isCorrect ? correctCount + 1 : correctCount;

    if (currentIndex + 1 < words.length) {
      if (isCorrect) {
        setCorrectCount((prev) => prev + 1);
      }
      setCurrentIndex((prev) => prev + 1);
      setAnswer("");
    } else {
      navigate("/result", {
        state: { correctCount: newCorrectCount, allCount: words.length },
      });
    }
  };

  return (
    <div className={styles.checkWrapper}>
      <Typography align="left" color="black">
        Слово: {currentIndex + 1} из {words.length}
      </Typography>
      <Table className={styles.checkTableWrapper}>
        <TableBody>
          <TableRow>
            <TableCell
              className={`${styles.checkTableCell} ${styles.checkTableCellLabel}`}
            >
              <label>Слово на русском языке</label>
            </TableCell>
            <TableCell className={styles.checkTableCell}>
              <div className={styles.checkInputWrapper}>
                <TextField
                  size="small"
                  value={currentWord.original}
                  fullWidth
                  slotProps={{
                    input: {
                      sx: {
                        pointerEvents: "none",
                      },
                    },
                  }}
                />
              </div>
            </TableCell>
          </TableRow>
          <TableRow>
            <TableCell className={styles.checkTableCellLabel}>
              <label>Перевод на английский язык</label>
            </TableCell>
            <TableCell>
              <div className={styles.checkInputWrapper}>
                <Select
                  size="small"
                  value={answer}
                  displayEmpty
                  fullWidth
                  onChange={(e) => setAnswer(e.target.value)}
                >
                  <MenuItem value="">Не выбрано</MenuItem>
                  {options.map((option, key) => (
                    <MenuItem value={option} key={key}>
                      {option}
                    </MenuItem>
                  ))}
                </Select>
              </div>
            </TableCell>
          </TableRow>
        </TableBody>
      </Table>
      <div className={styles.checkButtonMenu}>
        <Button variant="contained" disabled={isDisabled} onClick={handleCheck}>
          Проверить
        </Button>
      </div>
    </div>
  );
};
