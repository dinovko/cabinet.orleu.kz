import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import { AppRouter } from "@/app/router";
import { Provider } from "react-redux";
import { store } from "./app/store";
import { ThemeProvider } from "@mui/system";
import { theme } from "./styles/theme";
import "@/i18n/index";
import "./styles/variables.scss";

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <ThemeProvider theme={theme}>
      <Provider store={store}>
        <AppRouter />
      </Provider>
    </ThemeProvider>
  </StrictMode>
);
