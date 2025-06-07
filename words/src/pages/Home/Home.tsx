import { Button } from "@mui/material";
import { useNavigate } from "react-router";

export const Home = () => {
  const navigate = useNavigate();

  return (
    <>
      <Button variant="contained" onClick={() => navigate("/dictionary")}>
        Заполнить словарь
      </Button>
      <Button variant="outlined" onClick={() => navigate("/check")}>
        Проверить знания
      </Button>
    </>
  );
};
