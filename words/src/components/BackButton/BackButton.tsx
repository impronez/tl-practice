import styles from "./BackButton.module.scss";
import ArrowBackIosNewIcon from "@mui/icons-material/ArrowBackIosNew";
import { Button } from "@mui/material";
import { useLocation, useNavigate } from "react-router";

export const BackButton = () => {
  const navigate = useNavigate();
  const location = useLocation();

  if (location.pathname === "/" || location.pathname === "/result") return null;

  return (
    <Button
      startIcon={<ArrowBackIosNewIcon />}
      variant="outlined"
      onClick={() => navigate(-1)}
      className={styles.backButton}
      sx={{
        "& .MuiButton-startIcon": {
          marginRight: 0,
        },
      }}
    ></Button>
  );
};
