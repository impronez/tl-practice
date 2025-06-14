import { IconButton, Menu, MenuItem } from "@mui/material";
import DehazeIcon from "@mui/icons-material/Dehaze";
import DeleteIcon from "@mui/icons-material/Delete";
import EditIcon from "@mui/icons-material/Edit";
import React from "react";
import { useNavigate } from "react-router";
import { useDictionaryStore } from "../../../stores/useDictionaryStore";
import type { DictionaryWord } from "../../../types/DictionaryWord";

interface DictionaryActionProps {
  word: DictionaryWord;
}

export const DictionaryAction = ({ word }: DictionaryActionProps) => {
  const navigate = useNavigate();
  const removeWord = useDictionaryStore((state) => state.remove);
  const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);
  const open = Boolean(anchorEl);

  const handleClick = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

  const handleEditButtonClick = () => {
    handleClose();
    navigate("/edit-word", { state: word });
  };

  const handleDeleteButtonClick = () => {
    handleClose();
    removeWord(word);
  };

  return (
    <div>
      <IconButton
        aria-label="more"
        id="long-button"
        aria-controls={open ? "long-menu" : undefined}
        aria-expanded={open ? "true" : undefined}
        aria-haspopup="true"
        onClick={handleClick}
      >
        <DehazeIcon />
      </IconButton>
      <Menu
        id="dictionary-actions-menu"
        anchorEl={anchorEl}
        open={open}
        onClose={handleClose}
        variant="selectedMenu"
      >
        <MenuItem
          onClick={handleEditButtonClick}
          sx={{ display: "flex", alignItems: "center", gap: 1 }}
        >
          <EditIcon color="primary" />
          <span>Редактировать</span>
        </MenuItem>
        <MenuItem
          onClick={handleDeleteButtonClick}
          sx={{ display: "flex", alignItems: "center", gap: 1 }}
        >
          <DeleteIcon color="primary" />
          <span>Удалить</span>
        </MenuItem>
      </Menu>
    </div>
  );
};
