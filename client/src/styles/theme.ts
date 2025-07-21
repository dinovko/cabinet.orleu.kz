import { createTheme } from "@mui/material/styles";

export const theme = createTheme({
  typography: {
    fontSize: 12, // базовый размер шрифта (по умолчанию 14)
    h1: {
      fontSize: "2rem", // ~32px
    },
    h2: {
      fontSize: "1.75rem", // ~28px
    },
    h3: {
      fontSize: "1.5rem", // ~24px
    },
    body1: {
      fontSize: "0.875rem", // ~14px
    },
    body2: {
      fontSize: "0.75rem", // ~12px
    },
  },
  palette: {
    background: {
      paper:
        getComputedStyle(document.documentElement)
          .getPropertyValue("--color-bg-paper")
          ?.trim() || "#fff",
    },
    text: {
      primary:
        getComputedStyle(document.documentElement)
          .getPropertyValue("--color-text-primary")
          ?.trim() || "#000",
    },
    divider:
      getComputedStyle(document.documentElement)
        .getPropertyValue("--color-divider")
        ?.trim() || "#e0e0e0",
  },
  shape: {
    borderRadius: 8, // скругление по умолчанию для всех компонентов, включая Box
  },
});
