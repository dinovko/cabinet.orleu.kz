import { useTranslation } from "react-i18next";
import { MenuItem, Select, type SelectChangeEvent } from "@mui/material";

const LanguageSwitcher = () => {
  const { i18n } = useTranslation();

  const handleChange = (event: SelectChangeEvent) => {
    i18n.changeLanguage(event.target.value);
  };

  return (
    <Select
      value={i18n.language}
      onChange={handleChange}
      size="small"
      sx={{ color: "white", borderColor: "white", minWidth: 100 }}
    >
      <MenuItem value="ru">Русский</MenuItem>
      <MenuItem value="kk">Қазақша</MenuItem>
    </Select>
  );
};

export default LanguageSwitcher;
